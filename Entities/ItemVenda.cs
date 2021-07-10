using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ItemVenda
    {
        private int _itemId;
        private int _itemQtd;
        private float _itemPrice;

        public ItemVenda()
        {
            
        }

        public ItemVenda(int itemId, int itemQtd, float itemPrice)
        {
            _itemId = itemId;
            _itemQtd = itemQtd;
            _itemPrice = itemPrice;
        }

        public int ItemId { get => _itemId; set => _itemId = value; }
        public int ItemQtd { get => _itemQtd; set => _itemQtd = value; }
        public float ItemPrice { get => _itemPrice; set => _itemPrice = value; }
    }
}
