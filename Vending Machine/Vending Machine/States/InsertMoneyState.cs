using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vending_Machine.Entities;

namespace Vending_Machine.States
{
    internal class InsertMoneyState : IState
    {
        VendingMachine VendingMachine;
        Dictionary<string, int> dispatchItems;
        Dictionary<Coin, int> coins;
        Dictionary<Note, int> notes;
        int collectedMoney;
        public InsertMoneyState(VendingMachine vendingMachine,Dictionary<string,int> items) 
        {
            this.notes = new Dictionary<Note, int>();
            this.dispatchItems = items;
            this.VendingMachine = vendingMachine;
            this.coins = new Dictionary<Coin, int>();
            collectedMoney = 0;
        }
        public void Cancle()
        {
            this.VendingMachine.UpdateState(this);
            Console.WriteLine("Refunding money and going to idle state");
        }

        public void DispatchItems()
        {
            Dictionary<string,Item> stockItems = VendingMachine.GetItems();
            int dispatchItemsCost = 0;
            foreach((string item,int quantity) in dispatchItems)
            {
                dispatchItemsCost += stockItems[item].GetPrice() * quantity;
            }
            if(dispatchItemsCost > collectedMoney)
            {
                Console.WriteLine($"Funds are insufficient insert more {dispatchItemsCost-collectedMoney} money");
                return;
            }
            this.VendingMachine.UpdateState(new ItemsDispatchState(VendingMachine,collectedMoney,dispatchItems,dispatchItemsCost));
            this.VendingMachine.DispatchItems();
        }

        public void InsertCoin(Coin coin, int quantity)
        {
            if (!coins.ContainsKey(coin)) { coins.Add(coin, 0); }
            coins[coin] += quantity;
            collectedMoney += (int)coin * quantity;
        }

        public void InsertNote(Note note, int quantity)
        {
            if (!notes.ContainsKey(note)) { notes.Add(note, 0); }
            notes[note] += quantity;
            collectedMoney += (int)note * quantity;
        }

        public void SelectItem(string item, int quantity)
        {
            throw new InvalidOperationException("Cannot select items");
        }

        public void StartInsertingMoney()
        {
            throw new InvalidOperationException("Already inserting money");
        }

        public void StartSelectingItem()
        {
            throw new InvalidOperationException("Cannot select items");
        }
    }
}
