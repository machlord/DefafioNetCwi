using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Desafio
{
    class Watcher : FileSystemWatcher
    {
        public Watcher()
        {
            this.Path = ConfigurationManager.AppSettings.Get("PathEntrada");

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
            try
            {
                // Open the text file using a stream reader.
                using var sr = new StreamReader($"{e.FullPath}");
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
