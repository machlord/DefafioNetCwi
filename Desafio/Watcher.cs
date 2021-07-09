using Microsoft.Extensions.Logging;
using System.IO;

namespace Desafio
{
    class Watcher : FileSystemWatcher
    {
        private readonly ILogger<Desafio> _logger;
        private readonly IEtl _etl;

        public Watcher(string path, ILogger<Desafio> logger, IEtl etl)
        {
            _logger = logger;
            _etl = etl;

            try
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


                //Adiciona o metodo que será executado quando criado;
                Created += (sender, e) => OnCreated(sender, e, _logger, _etl);
                
                Filter = "*.txt";
                IncludeSubdirectories = true;
                EnableRaisingEvents = true;
            }
            catch(System.ArgumentException ex)
            {
                _logger.LogInformation("Caminho de Entrada não encontrado", ex);
            }
            
        }

        private static void OnCreated(object sender, FileSystemEventArgs e, ILogger logger, IEtl etl)
        {
            etl.AnalisarArquivo($"{e.FullPath}");
        }
    }
}
