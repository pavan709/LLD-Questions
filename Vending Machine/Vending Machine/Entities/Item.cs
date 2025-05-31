using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine.Entities
{
    public class Item
    {
        private string _name;
        private int _price;
        private int _quantity;
        public Item(string name, int price, int quantity)
        {
            _name = name;
            _price = price;
            _quantity = quantity;
        }
        public void UpdateQuantity(int quantity) 
        { 
            _quantity += quantity;
        }
        public void UpdatePrice(int price)
        {
            _price = price;
        }
        public int GetPrice() { return _price; }
        public int GetQuantity() { return _quantity; }
    }
}
