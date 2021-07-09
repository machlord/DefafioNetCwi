﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
                var piorVendedor = new { SalesmanName = "", total = 0f};
                Console.WriteLine(linha.Count);
                //Processando linha a linha
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
                                    linha[i + 3]));
                            
                            break;
                        default:
                            throw new Exception($"Arquivo: {linha[i]}");
                    }
                    Console.WriteLine($" --> {i}");
                    Console.WriteLine($"A: {linha[i]} - B: {linha[i + 1]} - C: {linha[i + 2]} - D: {linha[i + 3]}");
                }
                Console.WriteLine("Processo terminado");
                Console.WriteLine($"Numero de clientes: {listaCLientes.Count}");
                Console.WriteLine($"Numero de Vendedores: {listaVendedores.Count}");
                //Pego com LINQ a venda mais cara;
                Venda compraMaisCara = listaVendas.OrderBy(p => p.SaleTotal).First();
                Console.WriteLine($"Numero de Vendedores: {compraMaisCara.SalesmanName} com o valor: {compraMaisCara.SaleTotal}");
                piorVendedor = listaVendas
                    .GroupBy(x => x.SalesmanName)
                    .Select(lv => new 
                    {
                        SalesmanName = lv.First().SalesmanName,
                        total = lv.Sum(s => s.SaleTotal)
                    })
                    .OrderBy(o => o.total)
                    .Last();

                Console.WriteLine($"Pior Vendedor: {piorVendedor.SalesmanName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
