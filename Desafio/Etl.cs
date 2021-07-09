using System;
using System.Collections.Generic;
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
                IList<string> linha = sr.ReadToEnd().Split("ç");
                //Preciso de :
                //Lista de Clientes - R1: Quantidade de clientes;
                var clientes = new CLiente();
                //Lista de Vendedores - R2: Quantidade de vendedores;
                //Lista de Vendas - R3: Id da venda mais cara
                //Nome do Pior vendedor:


                //Processando linha a linha
                for (int i = 0; i < (linha.Count - 1); i+=3)
                {
                    Console.WriteLine($" --> {i}");
                    Console.WriteLine($"A: {linha[i]} - B: {linha[i + 1]} - C: {linha[i + 2]} - D: {linha[i + 3]}");
                }
            }

            catch (IOException ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
