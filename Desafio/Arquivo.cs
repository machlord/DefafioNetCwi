using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Desafio
{
    class Arquivo
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
                    // Renomei movendo ele
                    fi.MoveTo(@"C:\Temp\Mahesh.jpg");
                }
            }
            catch (IOException ioExp)
            {

            }
        }
        
        public void CriarArquivoPeloTexto(string texto, string local) 
        {
            try
            {
                if (!File.Exists(local))
                {
                    // Cria o arquivo
                    using (StreamWriter sw = File.CreateText(local))
                    {
                        sw.WriteLine(texto);
                    }
                }

                // Abre o Arquivo se ele existir
                using (StreamReader sr = File.OpenText(local))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (System.ArgumentException ex)
            {

            }
           
        }
    }
}
