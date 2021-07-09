using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Venda
    {
        private int _id;
        private int _saleId;
        private int _itemId;
        private int _itemQuantity;
        private float _itemPrice;
        private string _salesmanName;
        private float _saleTotal;

        public Venda()
        {
            
        }
        public Venda(int id, int saleId, int itemId, int itemQuantity, float itemPrice, string salesmanName, float saleTotal)
        {
            _id = id;
            _saleId = saleId;
            _itemId = itemId;
            _itemQuantity = itemQuantity;
            _itemPrice = itemPrice;
            _salesmanName = salesmanName;
            _saleTotal = saleTotal;
        }

        public int Id { get => _id; set => _id = value; }
        public int SaleId { get => _saleId; set => _saleId = value; }
        public int ItemId { get => _itemId; set => _itemId = value; }
        public int ItemQuantity { get => _itemQuantity; set => _itemQuantity = value; }
        public float ItemPrice { get => _itemPrice; set => _itemPrice = value; }
        public string SalesmanName { get => _salesmanName; set => _salesmanName = value; }
        public float SaleTotal { get => _saleTotal; set => _saleTotal = value; }
    }
}
