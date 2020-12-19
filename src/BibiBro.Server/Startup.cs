using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using BibiBro.Client.Telegram;
using BibiBro.Client.Telegram.Collections;
using BibiBro.Client.Telegram.Helper;
using BibiBro.Client.Telegram.Parser;
using BibiBro.Client.Telegram.Services;
using BibiBro.Server.Scheduler;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace BibiBro.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IChatCollection, ChatCollection>();
            services.AddSingleton<ITelegramManager, TelegramManager>();
            services.AddSingleton<ITelegramService, TelegramService>();
            //services.AddSingleton<IParserAutoRu, ParserAutoRu>();
            services.AddSingleton<IParserAutoRu, ParserAutoRuTest>();

            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<ParserJob>();
            //services.AddSingleton(new JobSchedule(jobType: typeof(ParserJob), cronExpression: "0 0/1 * 1/1 * ? *")); //0 0/3 07-21 1/1 * ? *

            services.AddTransient<IReadWriteJson, ReadWriteJson>();

            services.AddHostedService<QuartzHostedService>();
            services.AddHttpClient<IAutoRuService, AutoRuService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifeTime)
        {
            appLifeTime.ApplicationStarted.Register(async () =>
            {
                try
                {
                    await app.ApplicationServices.GetService<ITelegramService>().StartAsync();
                }
                catch (Exception ex)
                {
                    app.ApplicationServices.GetService<ILogger<Startup>>().LogError(ex, ex.Message);
                }
            });

            appLifeTime.ApplicationStopping.Register(async () =>
            {
                try
                {
                    await app.ApplicationServices.GetService<ITelegramService>().StopAsync();
                }
                catch (Exception ex)
                {
                    app.ApplicationServices.GetService<ILogger<Startup>>().LogError(ex, ex.Message);
                }
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
