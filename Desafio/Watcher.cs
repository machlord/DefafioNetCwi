using Microsoft.Extensions.Logging;
using System.IO;

namespace Desafio
{
    class Watcher : FileSystemWatcher
    {
        private readonly ILogger<Desafio> _logger;
        private readonly IEtl _etl;


        public Watcher(string pathEntrada, string pathSaida,ILogger<Desafio> logger, IEtl etl)
        {
            _logger = logger;
            _etl = etl;

            try
            {
                Path = pathEntrada;

                NotifyFilter = NotifyFilters.Attributes
                                       | NotifyFilters.CreationTime
                                       | NotifyFilters.DirectoryName
                                       | NotifyFilters.FileName
                                       | NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.Security
                                       | NotifyFilters.Size;


                //Adiciona o metodo que será executado quando criado;
                Created += (sender, e) => OnCreated(e, _etl, pathSaida);
                
                Filter = "*.txt";
                IncludeSubdirectories = true;
                EnableRaisingEvents = true;
            }
            catch(System.ArgumentException ex)
            {
                _logger.LogInformation("Caminho de Entrada não encontrado", ex);
            }
            
        }

        private static void OnCreated(FileSystemEventArgs e, IEtl etl, string pathSaida)
        {
            etl.AnalisarArquivo($"{e.FullPath}", pathSaida);
        }
    }
}
