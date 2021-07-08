using System;
using System.IO;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
            FileSystemWatcher watch = new Watcher();

            Console.WriteLine("Precionar para Fechar");
            Console.WriteLine();
            Console.ReadLine();
        }

    }
}
