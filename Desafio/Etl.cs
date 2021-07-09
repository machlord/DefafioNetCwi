﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entidades;
using Microsoft.Extensions.Logging;

namespace Desafio
{
    class Etl : IEtl
    {
        private readonly ILogger _logger;

        public Etl(ILogger logger)
        {
            _logger = logger;
        }


        public void AnalisarArquivo(string path)
        {
            try
            {
                //Lê o arquivo;
                using var sr = new StreamReader(path);

                //Separar a linha pelo caractere 'ç'
                IList<string> linha = sr.ReadToEnd().Replace("\r", "").Split(new string[] { "ç", "\n" }, StringSplitOptions.None);
                
                //Lista de Clientes - R1: Quantidade de clientes;
                IList<Cliente> listaCLientes = new List<Cliente>();
                
                //Lista de Vendedores - R2: Quantidade de vendedores;
                IList<Vendedor> listaVendedores = new List<Vendedor>();
                
                //Lista de Vendas - R3: Id da venda mais cara
                IList<Venda> listaVendas = new List<Venda>();
                
                //Dados do Pior vendedor:
                var piorVendedor = new { SalesmanName = "", total = 0f};
                
                //Processando linha a linha, 4 informações por vez
                for (int i = 0; i < (linha.Count - 1); i+=4)
                {
                    switch (linha[i])
                    {
                        case "001":
                            //Adiciona o Vendedor a lista de vendedores
                            listaVendedores.Add(
                                new Vendedor(
                                    i, 
                                    linha[i + 1], 
                                    linha[i + 2], 
                                    float.Parse(linha[i + 3])
                                ));
                            break;
                            //Adiciona o cliente a lista de clientes
                        case "002":
                            listaCLientes.Add(
                                new Cliente(
                                    i, 
                                    linha[i + 1], 
                                    linha[i + 2], 
                                    linha[i +3])
                                );
                            break;
                            //Adiciona a venda a lista de vendas
                        case "003":
                            //Divide as informações do campo 3(informações do Item)
                            IList<string> itemInfo = linha[i + 2]
                                .Replace("[", "")
                                .Replace("]", "")
                                .Split("-");

                            listaVendas.Add(
                                new Venda(
                                    i,
                                    int.Parse(linha[i + 1]),
                                    int.Parse(itemInfo[0]),
                                    int.Parse(itemInfo[1]), 
                                    float.Parse(itemInfo[2]), 
                                    linha[i + 3])
                                );
                            
                            break;
                        default:
                            throw new Exception($"Arquivo: {linha[i]}");
                    }
                    _logger.LogInformation($"Processando Item {((i + 1)/4 + 1)}", i);
                }
               
                //Analise da Compra mais cara
                Venda compraMaisCara = listaVendas.OrderBy(p => p.SaleTotal).First();
               
                //Analise do Pior Vendedor
                piorVendedor = listaVendas
                    .GroupBy(x => x.SalesmanName)
                    .Select(lv => new 
                    {
                        SalesmanName = lv.First().SalesmanName,
                        total = lv.Sum(s => s.SaleTotal)
                    })
                    .OrderBy(o => o.total)
                    .Last();

                //Sumarisar em Um texto ;
                Resultado resultado = new Resultado(    
                                            listaCLientes.Count,
                                            listaVendedores.Count,
                                            compraMaisCara.SaleId,
                                            piorVendedor.SalesmanName
                                        );
                _logger.LogInformation("{i}", ProcessarResultado(resultado));
                //Criar Arquivo de Saida
                //Remover Arquivo de Entrada
                _logger.LogInformation("Processo Terminado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
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
