using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ping9719.WpfEx.Helps
{
    /// <summary>
    /// 简单定时任务队列执行器
    /// </summary>
    public class QueueTaskTime : IDisposable
    {
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsStart { get; private set; } = false;
        /// <summary>
        /// 是否允许不冲突。运行定期任务的时候不执行队列任务，运行队列任务的时候不执行定期任务
        /// </summary>
        public bool IsRunNoConflict { get; } = true;
        /// <summary>
        /// 等待任务数
        /// </summary>
        public int WaitCount => ForTask.Count + QueueTask.Count;
        /// <summary>
        /// 队列循环任务
        /// </summary>
        public Queue<Action> ForTask = new Queue<Action>();
        /// <summary>
        /// 队列任务
        /// </summary>
        public Queue<Tuple<object, object>> QueueTask = new Queue<Tuple<object, object>>();

        Task task = null;

        ///// <summary>
        ///// 空闲 0 运行循环任务 1 运行队列任务 2
        ///// </summary>
        //int runState = 0;

        /// <summary>
        /// 初始化简单定时任务队列执行器
        /// </summary>
        public QueueTaskTime()
        {
            task = Task.Run(() =>
            {
                while (true)
                {
                    if (!IsStart)
                        continue;

                    try
                    {
                        if (IsStart && ForTask.Count != 0)
                        {
                            var que = ForTask.Dequeue();
                            que.Invoke();
                        }

                        if (IsStart && QueueTask.Count != 0)
                        {
                            var que = QueueTask.Dequeue();
                            if (que.Item1 is Action act)
                                act.Invoke();
                            else if (que.Item1 is Action<object> actt)
                                actt.Invoke(que.Item2);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            });
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            IsStart = true;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Stop()
        {
            IsStart = false;
        }

        ///// <summary>
        ///// 移除循环任务
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <returns></returns>
        //public bool RemoveForTask(Guid guid)
        //{
        //    //if (Timers.ContainsKey(guid))
        //    //{
        //    //    var t = Timers[guid];
        //    //    t.Stop();
        //    //    t.Dispose();
        //    //    t = null;
        //    //    Timers.Remove(guid);
        //    //    return true;
        //    //}
        //    return false;
        //}

        /// <summary>
        /// 添加循环任务
        /// </summary>
        /// <param name="action">需要执行的方法</param>
        /// <param name="timeSpan">执行的间隔</param>
        /// <param name="isStartRun">启动是否马上就执行方法（不等待间隔时间）</param>
        public void AddForTask(Action action, TimeSpan timeSpan, bool isStartRun = true)
        {
            Task task = new Task((object obj) =>
            {
                var dt = (Tuple<Action, TimeSpan, bool>)obj;
                var ts = (timeSpan == null || timeSpan.TotalMilliseconds < 10) ? 10 : Convert.ToInt32(timeSpan.TotalMilliseconds);

                while (true)
                {
                    if (!IsStart)
                        continue;

                    if (!ForTask.Contains(dt.Item1))
                    {
                        if (!dt.Item3)
                            Thread.Sleep(ts);
                        if (!IsStart)
                            continue;

                        ForTask.Enqueue(dt.Item1);

                        if (dt.Item3)
                            Thread.Sleep(ts);
                        if (!IsStart)
                            continue;
                    }

                }
            }, Tuple.Create(action, timeSpan, isStartRun));

            task.Start();
        }

        /// <summary>
        /// 添加队列任务
        /// </summary>
        /// <param name="action">需要执行的方法</param>
        public void AddQueueTask(Action action)
        {
            QueueTask.Enqueue(new Tuple<object, object>(action, null));
        }

        /// <summary>
        /// 添加队列任务
        /// </summary>
        /// <param name="action">需要执行的方法</param>
        /// <param name="state">参数</param>
        public void AddQueueTask(Action<object> action, object state)
        {
            QueueTask.Enqueue(new Tuple<object, object>(action, state));
        }

        public void Dispose()
        {
            try
            {
                Stop();
                ForTask = new Queue<Action>();
                QueueTask = new Queue<Tuple<object, object>>();
                task = null;
            }
            catch (Exception)
            {

            }
        }
    }
}
