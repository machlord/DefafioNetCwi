using System.Collections.Generic;
using NUnit.Framework;
using Desafio;
using Entidades;
using Serilog;
using Serilog.Core;

namespace NUnitTest
{
    class EtlTest
    {
        private Etl _etl;
        private IList<Venda> _vendas;
        private List<Vendedor> _vendedores;
        private List<Cliente> _clientes;
        private List<Venda> _vendasVazia;
        private IList<string> _linha;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _etl = new Etl();
            _vendas = new List<Venda>
            {
                new Venda
                {
                    Id = 1,
                    SaleId = 1,
                    SalesmanName = "Antonio",
                    SaleTotal = 30,
                    ItensVenda =
                    {
                        new ItemVenda {ItemId = 1, ItemPrice = 10, ItemQtd = 1},
                        new ItemVenda {ItemId = 2, ItemPrice = 20, ItemQtd = 1},
                    }
                },
                new Venda
                {
                    Id = 2,
                    SaleId = 2,
                    SalesmanName = "Cristiano",
                    SaleTotal = 2,
                    ItensVenda =
                    {
                        new ItemVenda {ItemId = 1, ItemPrice = 1, ItemQtd = 1},
                        new ItemVenda {ItemId = 2, ItemPrice = 1, ItemQtd = 1},
                    }
                }
            };

            _vendedores = new List<Vendedor>();
            _clientes = new List<Cliente>();
            _vendasVazia = new List<Venda>();

            string texto =   $"001ç1234567891234çPedroç5000\n" +
                             $"001ç3245678865434çPauloç4000.99\n" +
                             $"002ç2345675434544345çJose da SilvaçRural\n" +
                             $"002ç2345675433444345çEduardo PereiraçRural\n" +
                             $"003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro\n" +
                             $"003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çPauloRicardo\n";

            _linha = _etl.DividirStream(texto);
        }

        [Test]
        [TestCase("001", 1)]
        [TestCase("001ç1234567891234", 2)]
        [TestCase("001ç1234567891234çPedro", 3)]
        [TestCase("001ç1234567891234çPedroç5000", 4)]
        public void DividirStream(string linha, int total)
        {
            IList<string> lista = _etl.DividirStream(linha);
            Assert.AreEqual(total,lista.Count);
        }

        [Test]
        public void VendaMaisCara()
        {
            var maisCaraEsperada = new Venda
            {
                Id = 1,
                SaleId = 1,
                SalesmanName = "Antonio",
                SaleTotal = 30,
                ItensVenda =
                {
                    new ItemVenda {ItemId = 1, ItemPrice = 10, ItemQtd = 1},
                    new ItemVenda {ItemId = 2, ItemPrice = 20, ItemQtd = 1},
                }
            };
            var vendaMairCara = _etl.VendaMaisCara(_vendas);
            Assert.AreEqual(maisCaraEsperada.Id, vendaMairCara.Id);
        }

        [Test]
        public void PiorVendedor()
        {
            var piorVendedorEsperado = new Vendedor("Cristiano");
            var piorVendedor = _etl.PiorVendedor(_vendas);
            Assert.AreEqual(piorVendedorEsperado.Name, piorVendedor.Name);
        }

        [Test]
        public void ProcessarLinhas_numeroDeClientes()
        {
            _etl.ProcessarLinhas(_linha, _vendedores, _clientes, _vendasVazia);
            var numeroEsperado = 2;
            Assert.AreEqual(numeroEsperado, _clientes.Count);
        }

        [Test]
        public void ProcessarLinhas_numeroDeVendedores()
        {
            _etl.ProcessarLinhas(_linha, _vendedores, _clientes, _vendasVazia);
            var numeroEsperado = 2;
            Assert.AreEqual(numeroEsperado, _vendedores.Count);
        }

        [Test]
        public void ProcessarLinhas_numeroDeVendas()
        {
            _etl.ProcessarLinhas(_linha, _vendedores, _clientes, _vendasVazia);
            var numeroEsperado = 2;
            Assert.AreEqual(numeroEsperado, _vendasVazia.Count);
        }
    }
}
