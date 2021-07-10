using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Desafio.Interfaces;

namespace Desafio
{
    class Desafio : IDesafio
    {
        private readonly ILogger<Desafio> _log;
        private readonly IEtl _etl;
        private readonly Arquivo _arquivo;

        public Desafio(ILogger<Desafio> log)
        {
            _log = log;
            _arquivo = new Arquivo();
            _etl = new Etl(_log, _arquivo);
        }

        public void Run()
        {
            //Informações
            _log.LogInformation($"Local de Entrada {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "in")}");

            //Inicia o watcher que ficará observando a pasta;
            FileSystemWatcher watcher = new Watcher(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "in"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "out"),
                    _log, 
                    _etl
                );
            
            //Mantem o console aberto;
            Console.ReadLine();
        }
    }
}
