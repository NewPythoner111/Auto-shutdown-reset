using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAction
{
    public class TaskTime:System.Timers.Timer
    {
        /// <summary>  
        /// 构造  
        /// </summary>  
        /// <param name="executeTime">定时什么时候发送</param>  
        /// <param name="action">要执行什么任务，这是一个委托，string[]是参数，当然也可以用模型</param>  
        /// <param name="actionArgs">要执行任务的参数，与action的参数相对应</param>  
        /// <param name="callback">执行完的回调函数</param>  
        /// <param name="callbackArgs">回调函数的参数，与回调函数里的参数类型相对应</param>  
        public TaskTime(double interval, Action<string[]> action, string[] actionArgs, Action<string[]> callback, string[] callbackArgs)
        {
            //这里做下限制，不能超过最大值  
            if (interval >= int.MaxValue)
            {
                throw new ArgumentOutOfRangeException("不能超过最大值24天!");
            }
            base.Elapsed += (obj, e) => action(actionArgs);
            base.Elapsed += (obj, e) => callback(callbackArgs);
            base.AutoReset = false; //TODO: 循环执行暂不支持  
            base.Interval = interval > 0 && interval < int.MaxValue ? interval : 100;
            base.Enabled = true;
        }

    }
}
