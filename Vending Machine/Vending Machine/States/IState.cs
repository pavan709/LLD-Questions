using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine.States
{
    public interface IState
    {
        void StartSelectingItem();
        void SelectItem(string item,int quantity);
        void Cancle();
        void StartInsertingMoney();
        void InsertCoin(Coin coin, int quantity);
        void InsertNote(Note note,int quantity);
        void DispatchItems();

    }
}
