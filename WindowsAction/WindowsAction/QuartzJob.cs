using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAction
{
    public class QuartzJob
    {
        public void Math()
        {
            //从工厂中获取一个调度器实例化
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();       //开启调度器

            //==========例子1（简单使用）===========

            IJobDetail job1 = JobBuilder.Create<HelloJob>()  //创建一个作业
                .WithIdentity("作业名称", "作业组")
                .Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                                        .WithIdentity("触发器名称", "触发器组")
                                        .StartNow()                        //现在开始
                                        .WithSimpleSchedule(x => x         //触发时间，5秒一次。
                                            .WithIntervalInSeconds(5)
                                            .RepeatForever())              //不间断重复执行
                                        .Build();


            scheduler.ScheduleJob(job1, trigger1);      //把作业，触发器加入调度器。

            //==========例子2 (执行时 作业数据传递，时间表达式使用)===========

            IJobDetail job2 = JobBuilder.Create<DumbJob>()
                                        .WithIdentity("myJob", "group1")
                                        .UsingJobData("jobSays", "Hello World!")
                                        .Build();


            ITrigger trigger2 = TriggerBuilder.Create()
                                        .WithIdentity("mytrigger", "group1")
                                        .StartNow()
                                        .WithCronSchedule("/5 * * ? * *")    //时间表达式，5秒一次     
                                        .Build();


            scheduler.ScheduleJob(job2, trigger2);

            //scheduler.Shutdown();         //关闭调度器。
        }
    }

    /// <summary>
    /// 作业
    /// </summary>
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("作业执行!");
        }
    }

    public class DumbJob : IJob
    {
        /// <summary>
        ///  context 可以获取当前Job的各种状态。
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string content = dataMap.GetString("jobSays");

            Console.WriteLine("作业执行，jobSays:" + content);
        }
    }

}
