using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Parser;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BibiBro.Server.Scheduler
{
    [DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly IParserAutoRu _parser;
        public HelloWorldJob(ILogger<HelloWorldJob> logger, IParserAutoRu parser)
        {
            _logger = logger;
            _parser = parser;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _parser.Pars();
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }

    /*
     public class HelloWorldJob : IJob
{
    // Inject the DI provider
    private readonly IServiceProvider _provider;
    public HelloWorldJob( IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        // Create a new scope
        using(var scope = _provider.CreateScope())
        {
            // Resolve the Scoped service
            var service = scope.ServiceProvider.GetService<IScopedService>();
            _logger.LogInformation("Hello world!");
        }

        return Task.CompletedTask;
    }
}
     */
}
