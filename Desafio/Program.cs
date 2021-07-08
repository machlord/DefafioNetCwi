using System;
using System.IO;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
            string path = "C:\\Users\\machx\\Desktop\\in";
            FileSystemWatcher watcher = new Watcher(path);

            Console.WriteLine("Pressione para Fechar");
            Console.ReadLine();
        }

    }
}
