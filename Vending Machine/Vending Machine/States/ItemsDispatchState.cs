using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vending_Machine.Entities;

namespace Vending_Machine.States
{
    internal class ItemsDispatchState : IState
    {
        VendingMachine VendingMachine;
        int collectedMoney;
        Dictionary<string, int> items;
        int totalCostOfItems;
        public ItemsDispatchState(VendingMachine vendingMachine,int collectedMoney,Dictionary<string,int> dispatcItems, int totalCostOfItems) 
        {
            this.VendingMachine = vendingMachine;
            this.collectedMoney = collectedMoney;
            this.items = dispatcItems;
            this.totalCostOfItems = totalCostOfItems;
            foreach (var item in items) 
            {
                Console.WriteLine($"Dispatching {item.Value} of {item.Key} quantity");
            }
            if(collectedMoney>totalCostOfItems)
                Console.WriteLine($"Refunding the remaining money{collectedMoney-totalCostOfItems}");
        }
        public void Cancle()
        {
            Console.WriteLine("Cannot cancle now");
        }

        public void DispatchItems()
        {
            this.VendingMachine.UpdateState(new IdleState(VendingMachine));
        }

        public void InsertCoin(Coin coin, int quantity)
        {
            throw new InvalidOperationException("");
        }

        public void InsertNote(Note note, int quantity)
        {
            throw new InvalidOperationException("");
        }

        public void SelectItem(string item, int quantity)
        {
            throw new InvalidOperationException("");
        }

        public void StartInsertingMoney()
        {
            throw new InvalidOperationException("");
        }

        public void StartSelectingItem()
        {
            throw new InvalidOperationException("");
        }
    }
}
