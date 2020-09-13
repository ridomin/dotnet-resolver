﻿using IoTModels.Resolvers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;

namespace dtdl2_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource(5000);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            
            ILogger<ModelValidationService> logger = LoggerFactory.Create(builder =>
                builder
                .AddConfiguration(config.GetSection("Logging"))
                .AddDebug()
                .AddConsole()
            ).CreateLogger<ModelValidationService>();

            var validator = new ModelValidationService(config, logger);
            validator.ExecuteAsync(cancellationTokenSource.Token).Wait();
        }


    }
}
