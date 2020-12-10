using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace BibiBro.Client.Telegram.Parser
{
    public class ParserAutoRuScheduler
    {
        //TODO: Разобраться как создавать задачу ограниченную по времени.
        /*
        public static async Task StartAsync()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ParserAutoRu>().Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInMinutes(1)          // через 1 минуту
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
        */
    }
}
