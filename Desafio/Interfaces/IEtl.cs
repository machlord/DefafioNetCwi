using System.Collections.Generic;
using System.IO;
using Entidades;

namespace Desafio.Interfaces
{
    public interface IEtl
    {
        public void AnalisarArquivo(string path, string pathSaida);
        public IList<string> DividirStream(StreamReader sr);
        public Venda VendaMaisCara(IList<Venda> vendas);
        public Vendedor PiorVendedor(IList<Venda> vendas);

        public void ProcessarLinhas(IList<string> linha, IList<Vendedor> vendedores, IList<Cliente> clientes,
            IList<Venda> vendas);

        public string ProcessarResultado(Resultado resultado);
    }
}
