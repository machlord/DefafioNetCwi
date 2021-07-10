using System;
using System.IO;
using Desafio.Interfaces;

namespace Desafio
{
    public class Arquivo : IArquivo
    {
        public void RemoverArquivo(string caminhoArquivo) 
        {
            try
            {
                // Verifica se o arquivo existe   
                if (File.Exists(caminhoArquivo))
                {
                    // Se existir deleta
                    File.Delete(caminhoArquivo);
                }
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }
        
        public void RenomearArquivo(string caminhoArquivo) 
        {
            try
            {
                // Renomear movendo ele
                File.Move(caminhoArquivo, Path.Combine(caminhoArquivo, "_ERRO")); // Rename the oldFileName into newFileName
            }
            catch (IOException ioExp)
            {
                Console.WriteLine($"Erro: {ioExp.Message}");
            }
        }
        
        public void CriarArquivoPeloTexto(string texto, string local) 
        {
            try
            {
                var caminhoComExtensao = $"{local}\\resultado.txt";
                if (File.Exists(local)) return;
                
                // Cria o arquivo
                using var sw = File.CreateText(caminhoComExtensao);
                sw.WriteLine(texto);

            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
           
        }
    }
}
