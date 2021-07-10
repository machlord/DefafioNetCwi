using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using Desafio.Interfaces;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
           
            //Iniciando o Builder com configurações do JSON
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            //Iniciando o logger
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Build())
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .CreateLogger();
            Log.Logger.Information("Aplicação iniciada");

            //Inicia o serviço de injeção de Dependências;
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices((context, services) =>
                           {
                               services.AddTransient<IDesafio, Desafio>();
                                   
                           }).UseSerilog().Build();

            //Inicia o Aplicativo;
            var svc = ActivatorUtilities.CreateInstance<Desafio>(host.Services);
            svc.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder) 
        {
            //Configura o Appsettings e pega, se existir, o Appsettings de produção;
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

        }

    }
}
