using System.Threading.Tasks;
using BibiBro.Client.Telegram.Parser;
using Quartz;

namespace BibiBro.Server.Scheduler
{
    [DisallowConcurrentExecution]
    public class ParserJob : IJob
    {
        private readonly IParserAutoRu _parser;

        public ParserJob(IParserAutoRu parser)
        {
            _parser = parser;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _parser.Pars();
            return Task.CompletedTask;
        }
    }
}
