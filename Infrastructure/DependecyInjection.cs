﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure
{
    public static class DependecyInjection
    {
        public static void AddInfrastrucutre(this IServiceCollection services) 
        {
            services.AddQuartz(options => 
            {
                var jobkey = JobKey.Create(nameof(EmailSendBg));
                options
                .AddJob<EmailSendBg>(jobkey)
                .AddTrigger(trigger =>
                trigger
                .ForJob(jobkey)
                .WithSimpleSchedule(schedule =>
                schedule.WithIntervalInSeconds(5).RepeatForever()));
            });
            services.AddQuartzHostedService();
        }
    }
}
