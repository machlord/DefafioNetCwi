using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Desafio
{
    class Etl : IEtl
    {
        public void AnalisarArquivo(string path)
        {
            try
            {
                
                using var sr = new StreamReader(path);
                // Read the stream as a string, and write the string to the console.
                Console.WriteLine(sr.ReadToEnd());
            }
            catch (IOException ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
