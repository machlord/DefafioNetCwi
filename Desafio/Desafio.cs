using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Desafio
{
    class Desafio : IDesafio
    {
        private readonly ILogger<Desafio> _log;
        private readonly IConfiguration _config;
        private readonly Etl _etl;

        public Desafio(ILogger<Desafio> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            _etl = new Etl(_log);
        }

        public void Run()
        {
            //Informações
            _log.LogInformation($"Local de Entrada {_config.GetValue<string>("UrlEntrada")}");

            //Inicia o watcher que ficará observando a pasta;
            FileSystemWatcher watcher = new Watcher(_config.GetValue<string>("UrlEntrada"), _log, _etl);
            
            //Mantem o console aberto;
            Console.ReadLine();
        }
    }
}
