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

        public Desafio(ILogger<Desafio> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            _log.LogInformation($"Variavel {_config.GetValue<string>("UrlEntrada")}");

            //Inicia o watcher que ficará observando a pasta
            FileSystemWatcher watcher = new Watcher(_config.GetValue<string>("UrlEntrada"));

            
            _log.LogInformation($"Pressione para Fechar");
            Console.ReadLine();

        }
    }
}
