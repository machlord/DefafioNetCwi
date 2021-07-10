using System;
using System.IO;
using Desafio.Interfaces;

namespace Desafio
{
    class Arquivo : IArquivo
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
                // Cria o FileInfo
                System.IO.FileInfo fi = new System.IO.FileInfo(caminhoArquivo);
                // Se o arquivo existir
                if (fi.Exists)
                {
                    // Renomear movendo ele
                    fi.MoveTo(@"C:\Temp\Mahesh.jpg");
                }
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
