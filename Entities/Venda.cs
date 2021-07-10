using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Venda
    {
        private int _id;
        private int _saleId;
        private string _salesmanName;
        private float _saleTotal;
        private IList<ItemVenda> _itensVenda = new List<ItemVenda>();

        public Venda()
        {
            
        }

        public Venda(int id, int saleId, string salesmanName)
        {
            _id = id;
            _saleId = saleId;
            _salesmanName = salesmanName;
            _saleTotal = 0;
        }

        public void AddValorVenda(float valor)
        {
            _saleTotal += valor;
        }

        public void AddItemVenda(ItemVenda itemVenda)
        {
            _itensVenda.Add(itemVenda);
            AddValorVenda(itemVenda.ItemPrice * itemVenda.ItemQtd);
        }

        public int Id { get => _id; set => _id = value; }
        public int SaleId { get => _saleId; set => _saleId = value; }
        public string SalesmanName { get => _salesmanName; set => _salesmanName = value; }
        public float SaleTotal { get => _saleTotal; set => _saleTotal = value; }
        public IList<ItemVenda> ItensVenda
        {
            get => _itensVenda;
        }
    }
}
