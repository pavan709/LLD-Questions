using System;
using System.Collections.Generic;
using Vending_Machine.Entities;
using Vending_Machine.States;

namespace Vending_Machine.Test
{
    public class VendingMachineTest1
    {
        public VendingMachineTest1()
        {
            Simulate();
        }

        public void Simulate()
        {
            VendingMachine vendingMachine = VendingMachine.GetInstance();
            vendingMachine.UpdateState(new IdleState(vendingMachine));
            // Admin: Add coins and notes (20 of each denomination)
            foreach (Coin coin in Enum.GetValues(typeof(Coin)))
            {
                vendingMachine.AddCoinsToSystem(coin, 20);
            }
            foreach (Note note in Enum.GetValues(typeof(Note)))
            {
                vendingMachine.AddNotesToSystem(note, 20);
            }

            // Admin: Add 20 different products
            vendingMachine.AddItem("Coke", 30, 15);
            vendingMachine.AddItem("Pepsi", 25, 12);
            vendingMachine.AddItem("Water", 40, 10);
            vendingMachine.AddItem("Juice", 20, 18);
            vendingMachine.AddItem("Sprite", 15, 14);
            vendingMachine.AddItem("Fanta", 18, 13);
            vendingMachine.AddItem("Iced Tea", 12, 16);
            vendingMachine.AddItem("Lemonade", 10, 11);
            vendingMachine.AddItem("Energy Drink", 8, 22);
            vendingMachine.AddItem("Chocolate Bar", 25, 9);
            vendingMachine.AddItem("Chips", 30, 8);
            vendingMachine.AddItem("Cookies", 20, 10);
            vendingMachine.AddItem("Sandwich", 10, 25);
            vendingMachine.AddItem("Gum", 50, 3);
            vendingMachine.AddItem("Candy", 40, 5);
            vendingMachine.AddItem("Crackers", 15, 7);
            vendingMachine.AddItem("Nuts", 12, 15);
            vendingMachine.AddItem("Granola Bar", 18, 12);
            vendingMachine.AddItem("Protein Bar", 10, 20);
            vendingMachine.AddItem("Milk", 10, 17);

            // Order 1: Sufficient coins
            var order1 = new Dictionary<string, int>
            {
                {"Coke", 2},           // 2 x 15 = 30
                {"Chips", 3},          // 3 x 8 = 24
                {"Candy", 2},          // 2 x 5 = 10
                {"Water", 1},          // 1 x 10 = 10
                {"Chocolate Bar", 2},  // 2 x 9 = 18
                {"Gum", 5}             // 5 x 3 = 15
            };
            // Total: 30+24+10+10+18+15 = 107
            Console.WriteLine("Order 1: Buy multiple items with coins");
            SimulateOrder(vendingMachine, order1, coins: new Dictionary<Coin, int>
            {
                {Coin.Ten, 50}, // 100
                {Coin.Five, 1}, // 5
                {Coin.Two, 1},  // 2
            });

            // Order 2: Sufficient notes
            var order2 = new Dictionary<string, int>
            {
                {"Pepsi", 2},      // 2 x 12 = 24
                {"Juice", 1},      // 1 x 18 = 18
                {"Sprite", 2},     // 2 x 14 = 28
                {"Cookies", 2},    // 2 x 10 = 20
                {"Sandwich", 1},   // 1 x 25 = 25
                {"Milk", 1}        // 1 x 17 = 17
            };
            // Total: 24+18+28+20+25+17 = 132
            Console.WriteLine("\nOrder 2: Buy multiple items with notes");
            SimulateOrder(vendingMachine, order2, notes: new Dictionary<Note, int>
            {
                {Note.Hundred, 7}, // 100
                {Note.Twenty, 1},  // 20
                {Note.Ten, 1},     // 10
            });

            // Order 3: Sufficient coins and notes
            var order3 = new Dictionary<string, int>
            {
                {"Fanta", 2},         // 2 x 13 = 26
                {"Iced Tea", 1},      // 1 x 16 = 16
                {"Lemonade", 2},      // 2 x 11 = 22
                {"Energy Drink", 1},  // 1 x 22 = 22
                {"Granola Bar", 2},   // 2 x 12 = 24
                {"Protein Bar", 1}    // 1 x 20 = 20
            };
            // Total: 26+16+22+22+24+20 = 130
            Console.WriteLine("\nOrder 3: Buy multiple items with coins and notes");
            SimulateOrder(vendingMachine, order3,
                coins: new Dictionary<Coin, int>
                {
                    {Coin.Ten, 20}, // 80
                    {Coin.Five, 2} // 10
                },
                notes: new Dictionary<Note, int>
                {
                    {Note.Fifty, 10} // 50
                });

            // Order 4: Insufficient funds
            var order4 = new Dictionary<string, int>
            {
                {"Nuts", 2},      // 2 x 15 = 30
                {"Crackers", 2},  // 2 x 7 = 14
                {"Candy", 3},     // 3 x 5 = 15
                {"Gum", 2},       // 2 x 3 = 6
                {"Chips", 1}      // 1 x 8 = 8
            };
            // Total: 30+14+15+6+8 = 73
            Console.WriteLine("\nOrder 4: Try to buy multiple items with insufficient funds");
            SimulateOrder(vendingMachine, order4, coins: new Dictionary<Coin, int>
            {
                {Coin.Ten, 2} // 20 (insufficient)
            });

            // Order 5: More than available stock, but sufficient money
            var order5 = new Dictionary<string, int>
            {
                {"Sandwich", 11}, // Only 10 in stock, 11 x 25 = 275
                {"Milk", 5}       // 5 x 17 = 85
            };
            // Total: 275+85 = 360
            Console.WriteLine("\nOrder 5: Try to buy more than available stock");
            SimulateOrder(vendingMachine, order5, notes: new Dictionary<Note, int>
            {
                {Note.Hundred, 3}, // 300
                {Note.Fifty, 2},   // 100
                {Note.Ten, 1}      // 10 (total 410, more than enough)
            });
        }

        static void SimulateOrder(
            VendingMachine vendingMachine,
            Dictionary<string, int> items,
            Dictionary<Coin, int> coins = null,
            Dictionary<Note, int> notes = null)
        {
            try
            {
                vendingMachine.StartSelectingItem();
                foreach (var kvp in items)
                {
                    vendingMachine.SelectItem(kvp.Key, kvp.Value);
                }
                vendingMachine.StartInsertingMoney();

                if (coins != null)
                {
                    foreach (var coin in coins)
                        vendingMachine.InsertCoin(coin.Key, coin.Value);
                }
                if (notes != null)
                {
                    foreach (var note in notes)
                        vendingMachine.InsertNote(note.Key, note.Value);
                }

                vendingMachine.DispatchItems();
                Console.WriteLine("Successfully purchased\n");
                //foreach (var kvp in items)
                //{
                //    Console.WriteLine($"  {kvp.Value} x {kvp.Key}");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Order failed: {ex.Message}");
            }
        }
    }
}
