using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Entities;

namespace Desafio
{
    class Etl : IEtl
    {
        public void AnalisarArquivo(string path)
        {
            try
            {
                
                using var sr = new StreamReader(path);
                //Separar a linha pelo caractere 'ç'
                IList<string> linha = sr.ReadToEnd().Replace("\r", "").Split(new string[] { "ç", "\n" }, StringSplitOptions.None);
                //Preciso de :
                //Lista de Clientes - R1: Quantidade de clientes;
                IList<Cliente> listaCLientes = new List<Cliente>();
                //Lista de Vendedores - R2: Quantidade de vendedores;
                IList<Vendedor> listaVendedores = new List<Vendedor>();
                //Lista de Vendas - R3: Id da venda mais cara
                IList<Venda> listaVendas = new List<Venda>();
                //Nome do Pior vendedor:
                string piorVendedor;
                Console.WriteLine(linha.Count);
                //Processando linha a linha
                for (int i = 0; i < (linha.Count - 1); i+=4)
                {
                    switch (linha[i])
                    {
                        case "001":
                            //Adiciona o Vendedor a lista de vendedores
                            listaVendedores.Add(new Vendedor(i, linha[i + 1], linha[i + 2], float.Parse(linha[i + 3])));
                            break;
                            //Adiciona o cliente a lista de clientes
                        case "002":
                            listaCLientes.Add(new Cliente(i, linha[i + 1], linha[i + 2], linha[i +3]));
                            break;
                            //Adiciona a venda a lista de vendas
                        case "003":

                            break;
                        default:
                            throw new Exception($"Arquivo: {linha[i]}");
                    }
                    Console.WriteLine($" --> {i}");
                    Console.WriteLine($"A: {linha[i]} - B: {linha[i + 1]} - C: {linha[i + 2]} - D: {linha[i + 3]}");
                }
                Console.WriteLine("Processo terminado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
