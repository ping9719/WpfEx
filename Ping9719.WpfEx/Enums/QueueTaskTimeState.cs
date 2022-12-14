using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 简单定时任务队列执行器运行状态
    /// </summary>
    public enum QueueTaskTimeState
    {
        /// <summary>
        /// 开始，正在运行循环检测任务中
        /// </summary>
        Start,
        /// <summary>
        /// 正在执行循环任务中
        /// </summary>
        ForTask,
        /// <summary>
        /// 正在执行队列任务中
        /// </summary>
        QueueTask,
        /// <summary>
        /// 结束任务，并且没有遇到错误，并等待继续运行
        /// </summary>
        EndTaskOk,
        /// <summary>
        /// 结束任务，并且遇到错误，并等待继续运行
        /// </summary>
        EndTaskErr,
        /// <summary>
        /// 已暂停、停止
        /// </summary>
        Stop,
    }
}
