using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Desafio
{
    class Program
    {
        static void Main()
        {
           
            //Iniciando o Builder com vonfigurações do JSON
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            //Iniciando o logger
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Build())
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .CreateLogger();
            Log.Logger.Information("Aplicação iniciada");


            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IDesafio, Desafio>();
                }).UseSerilog().Build();

            var svc = ActivatorUtilities.CreateInstance<Desafio>(host.Services);
            svc.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder) 
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

        }

    }
}
