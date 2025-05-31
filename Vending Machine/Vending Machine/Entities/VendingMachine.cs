using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vending_Machine.States;

namespace Vending_Machine.Entities
{
    public class VendingMachine
    {
        private static VendingMachine instance;
        private readonly static object _lock = new object();
        private static Dictionary<Coin, int> _coins;
        private static Dictionary<Note, int> _notes;
        private static Dictionary<string, Item> _items;
        private IState state;
        private VendingMachine() 
        {
            
        }
        public static VendingMachine GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null) 
                    { 
                        instance = new VendingMachine();
                        _items = new Dictionary<string, Item>();
                        //state = new IdleState(this);
                        _coins = new Dictionary<Coin, int>();
                        _notes = new Dictionary<Note, int>();
                    }
                }
            }
            return instance;
        }

        #region Admin Work
        public void AddCoinsToSystem(Coin coin, int quantity)
        {
            if (!_coins.ContainsKey(coin)) { _coins.Add(coin, 0); }
            _coins[coin] += quantity;
        }
        public void AddNotesToSystem(Note note,int quantity)
        {
            if (!_notes.ContainsKey(note)) { _notes.Add(note, 0); }
            _notes[note] += quantity;
        }
        public void AddItem(string name,int quantity,int price) 
        {
            if (_items.ContainsKey(name)) throw new Exception("Item already exist");
            _items.Add(name,new Item(name, quantity, price));
        }
        public void UpdateItemQuantity(string name,int quantity)
        {
            if (!_items.ContainsKey(name)) throw new Exception("Item does not exist");
            _items[name].UpdateQuantity(quantity);
        }
        public void UpdateItemPrice(string name,int price) 
        {
            if (!_items.ContainsKey(name)) throw new Exception("Item does not exist");
            _items[name].UpdateQuantity(price);

        }
        #endregion
        public Dictionary<string,Item> GetItems()
        {
            return _items;
        }
        public Dictionary<Coin,int> GetCoins() { return _coins; }
        public Dictionary<Note,int> GetNotes() { return _notes; }

        #region State Actions
        public void UpdateState(IState state)
        {
            this.state = state;
        }
        public void StartSelectingItem()
        {
            this.state.StartSelectingItem();
        }
        public void Cancle()
        {
            this.state.Cancle();
        }

        public void DispatchItems()
        {
            this.state.DispatchItems();
        }

        public void InsertCoin(Coin coin, int quantity)
        {
            this.state.InsertCoin(coin, quantity);
        }

        public void InsertNote(Note note, int quantity)
        {
            this.state.InsertNote(note, quantity);
        }

        public void SelectItem(string item,int quantity)
        {
            this.state.SelectItem(item, quantity);
        }

        public void StartInsertingMoney()
        {
            this.state.StartInsertingMoney();
        }
        #endregion

    }
}
