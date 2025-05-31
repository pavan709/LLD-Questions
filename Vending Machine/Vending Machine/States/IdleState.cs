using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vending_Machine.Entities;

namespace Vending_Machine.States
{
    public class IdleState : IState
    {
        private VendingMachine _vendingMachine;
        public IdleState(VendingMachine vendingMachine)
        {
            this._vendingMachine = vendingMachine;
        }
        public void Cancle()
        {
            throw new InvalidOperationException("Nothing to cancle");
        }

        public void DispatchItems()
        {
            throw new InvalidOperationException("Nothing to dispatch");
        }

        public void InsertCoin(Coin coin, int quantity)
        {
            throw new InvalidOperationException("Items are not selected");
        }

        public void InsertNote(Note note, int quantity)
        {
            throw new InvalidOperationException("Items are not selected");
        }

        public void SelectItem(string item, int quantity)
        {
            throw new InvalidOperationException("Cannot select");
        }

        public void StartInsertingMoney()
        {
            throw new InvalidOperationException("Items are not selected");
        }

        public void StartSelectingItem()
        {
            _vendingMachine.UpdateState(new ItemSelectionState(_vendingMachine));
        }
    }
}
