
using System.Linq;
using Entidades;
using NUnit.Framework;

namespace NUnitTest
{
    class VendaTest
    {

        private Venda _venda;
        [SetUp]
        public void Setup()
        {
            _venda = new Venda();
        }

        [Test]
        [TestCase(32f)]
        [TestCase(32.25f)]
        [TestCase(40f)]
        [TestCase(1000.5f)]
        public void AddValorVenda_adicionadoValor_resultadoMesmoValor(float valor)
        {
            _venda.AddValorVenda(valor);
            Assert.AreEqual(valor, _venda.SaleTotal);
        }

        [Test]
        public void AddItemVenda_adicionandoItemVenda_IgualPrimeiro()
        {
            var itemVenda = new ItemVenda('1', 2, 32.2f);
            _venda.AddItemVenda(itemVenda);
            Assert.AreEqual(itemVenda, _venda.ItensVenda.First());
        }

        [Test]
        public void AddItemVendaAddValorVenda_itemValor32qtd10_ValorTotal320()
        {
            var itemVenda = new ItemVenda('1', 10, 32f);
            _venda.AddItemVenda(itemVenda);
            Assert.AreEqual((itemVenda.ItemQtd * itemVenda.ItemPrice), _venda.SaleTotal);
        }
    }
}
