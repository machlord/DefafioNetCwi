using System;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

using Desafio;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
            FileSystemWatcher watch = new Watcher();

            Console.WriteLine("Precionar para Fechar");
            Console.WriteLine(ConfigurationManager.AppSettings.Get("PathEntrada"));
            Console.ReadLine();
        }

    }
}
