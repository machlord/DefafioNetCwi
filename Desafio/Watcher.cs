using System.IO;

namespace Desafio
{
    class Watcher : FileSystemWatcher
    {
        public Watcher(string path)
        {
            Path = path;
            NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.DirectoryName
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Security
                                   | NotifyFilters.Size;


            Created += OnCreated;
            Filter = "*.txt";
            IncludeSubdirectories = true;
            EnableRaisingEvents = true;
        }


        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            var etl = new Etl();
            etl.AnalisarArquivo($"{e.FullPath}");
        }
    }
}
