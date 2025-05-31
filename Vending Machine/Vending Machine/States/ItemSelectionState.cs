using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vending_Machine.Entities;

namespace Vending_Machine.States
{
    internal class ItemSelectionState : IState
    {
        private VendingMachine _vendingMachine;
        Dictionary<string, int> _items;
        public ItemSelectionState(VendingMachine vendingMachine)
        {
            this._vendingMachine = vendingMachine;
            _items = new Dictionary<string, int>();
        }
        public void Cancle()
        {
            _vendingMachine.UpdateState(new IdleState(_vendingMachine));
            Console.WriteLine("Cancleing selection state and going to idle state");
        }

        public void DispatchItems()
        {
            throw new InvalidOperationException("Cannot dispatch items money not yet isnerted");
        }

        public void InsertCoin(Coin coin, int quantity)
        {
            throw new InvalidOperationException("Cannot money coins items not yet selected");
        }

        public void InsertNote(Note note, int quantity)
        {
            throw new InvalidOperationException("Cannot money coins items not yet selected");
        }

        public void SelectItem(string item, int quantity)
        {
            Dictionary<string, Item> items = _vendingMachine.GetItems();
            if (!items.ContainsKey(item)) throw new Exception("Item does not exist");
            if (items[item].GetQuantity() < quantity)
            {
                Console.WriteLine($"{item} quantity is not sufficient choose less than {items[item].GetQuantity() + 1}");
                return;
            }
            if (_items.ContainsKey(item))
            {
                if (items[item].GetQuantity() < _items[item]+quantity)
                {
                    Console.WriteLine($"{item} quantity is not sufficient choose less than {items[item].GetQuantity() + 1}");
                    return;
                }
            }
            else
            {
                _items.Add(item, quantity);
            }
        }

        public void StartInsertingMoney()
        {
            _vendingMachine.UpdateState(new InsertMoneyState(_vendingMachine,_items));
        }

        public void StartSelectingItem()
        {
            throw new InvalidOperationException("Already in seletion state");
        }
    }
}
