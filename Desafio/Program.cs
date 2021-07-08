using System;
using System.IO;
using Desafio;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
            FileSystemWatcher watch = new Watcher();

            Console.WriteLine("Precionar para Fechar");
            Console.ReadLine();
        }

    }
}
