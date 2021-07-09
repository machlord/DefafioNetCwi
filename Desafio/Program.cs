using System;
using System.IO;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
            //Inicia o watcher que ficará observando a pasta
            string path = "C:\\in";
            FileSystemWatcher watcher = new Watcher(path);

            
            Console.WriteLine("Pressione para Fechar");
            Console.ReadLine();
        }

    }
}
