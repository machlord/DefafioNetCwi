using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Desafio
{
    class Watcher : FileSystemWatcher
    {
        public Watcher()
        {
            this.Path = @"C:\Users\machx\Desktop\in";

            this.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.DirectoryName
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Security
                                   | NotifyFilters.Size;


            this.Created += OnCreated;
            this.Filter = "*.txt";
            this.IncludeSubdirectories = true;
            this.EnableRaisingEvents = true;
        }


        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Console.WriteLine(value);
        }
    }
}
