using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Desafio.Interfaces;
using Entidades;
using Microsoft.Extensions.Logging;

namespace Desafio
{
    public class Etl : IEtl
    {
        private readonly ILogger _logger;
        private readonly IArquivo _arquivo;

        public Etl() { }

        public Etl(ILogger logger, IArquivo arquivo)
        {
            _logger = logger;
            _arquivo = arquivo;
        }

        public void AnalisarArquivo(string path, string pathSaida)
        {
            //Lê o arquivo;
            using (var sr = new StreamReader(path))
            {
                try
                {
                    //Separar a linha pelo caractere 'ç'
                    var linha = DividirStream(sr.ReadToEnd());

                    //Lista de Clientes - R1: Quantidade de clientes;
                    var clientes = new List<Cliente>();

                    //Lista de Vendedores - R2: Quantidade de vendedores;
                    var vendedores = new List<Vendedor>();

                    //Lista de Vendas - R3: Id da venda mais cara
                    var vendas = new List<Venda>();

                    //Processando linha a linha
                    ProcessarLinhas(linha, vendedores, clientes, vendas);

                    //Avisando processamento
                    _logger.LogInformation($"Itens Processados: {((linha.Count + 1) / 4 + 1)}");

                    //Analise da compra mais cara
                    var compraMaisCara = VendaMaisCara(vendas);

                    //Analise do pior vendedor
                    var piorVendedor = PiorVendedor(vendas);

                    //Criar Arquivo de Saída
                    _arquivo.CriarArquivoPeloTexto(ProcessarResultado(new Resultado(
                        clientes.Count,
                        vendedores.Count,
                        compraMaisCara.SaleId,
                        piorVendedor.Name
                    )), pathSaida);

                }
                catch (Exception ex)
                {
                    sr.Close();
                    _logger.LogWarning("O arquivo não conseguiu ser importado por conter erros");
                }
            }

            //Remover Arquivo de Entrada
            _arquivo.RemoverArquivo(path);
            _logger.LogInformation("Processo Terminado");
        }


        public IList<string> DividirStream(string sr)
        {
            return sr
                     .Replace("\r", "")
                     .Split(new string[] { "ç", "\n" }, 
                        StringSplitOptions.None);
        }

        public Venda VendaMaisCara(IList<Venda> vendas)
        {
            return vendas.OrderByDescending(p => p.SaleTotal).First();
        }

        public Vendedor PiorVendedor(IList<Venda> vendas)
        {
            var piorVendedor=  vendas
                .GroupBy(x => x.SalesmanName)
                .Select(lv => new
                {
                    name = lv.First().SalesmanName,
                    total = lv.Sum(s => s.SaleTotal)
                })
                .OrderByDescending(o => o.total)
                .Last();
            return new Vendedor(piorVendedor.name);
        }

        public void ProcessarLinhas(IList<string> linha, IList<Vendedor> vendedores, IList<Cliente> clientes, IList<Venda> vendas)
        {
            for (var i = 0; i < (linha.Count - 1); i += 4)
            {
                switch (linha[i])
                {
                    case "001":
                        //Adiciona o Vendedor a lista de vendedores
                        vendedores.Add(
                            new Vendedor(
                                i,
                                linha[i + 1],
                                linha[i + 2],
                                float.Parse(linha[i + 3])
                            ));
                        break;
                    //Adiciona o cliente a lista de clientes
                    case "002":
                        clientes.Add(
                            new Cliente(
                                i,
                                linha[i + 1],
                                linha[i + 2],
                                linha[i + 3])
                        );
                        break;
                    //Adiciona a venda a lista de vendas
                    case "003":
                        //Cria a Classe de Vendas
                        var venda = new Venda(i, int.Parse(linha[i + 1]), linha[i + 3]);

                        //Divide as informações do campo 3(Lista de informações do Item)
                        IList<string> itemInfo = linha[i + 2]
                            .Replace("[", "")
                            .Replace("]", "")
                            .Split(",");

                        //Percorre a lista de itens
                        foreach (var item in itemInfo)
                        {
                            //Divide o item em partes
                            IList<string> itemAdd = item.Split("-");
                            //Adiciona o item a venda
                            venda.AddItemVenda(
                                new ItemVenda(
                                    int.Parse(itemAdd[0]),
                                    int.Parse(itemAdd[1]),
                                    float.Parse(itemAdd[2])
                                    )
                            );
                        }
                        //Finalmente adiciona a venda;
                        vendas.Add(venda);

                        break;
                    default:
                        throw new Exception($"Arquivo: {linha[i]}");
                }
                
            }
        }

        public string ProcessarResultado(Resultado resultado)
        {
            string analise = "Resultado:\n";

            analise += $"Quantidade de Clientes: {resultado.NumeroClientes}\n";
            analise += $"Quantidade de Vendedores: {resultado.NumeroVendedores}\n";
            analise += $"ID da venda mais cara: {resultado.CompraMaisCara}\n";
            analise += $"O pior Vendedor: {resultado.PiorVendedor}\n";

            return analise;
        }
    }
}
