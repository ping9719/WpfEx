using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Ping9719.WpfEx.Mvvm
{
    /// <summary>
    /// 表示一个动态数据集合，它可在添加、删除项目或刷新整个列表时提供通知。
    /// </summary>
    public class ObservableList<T> : ObservableCollection<T>
    {

        public ObservableList() : base() { }

        public ObservableList(T t) : base(new List<T>() { t }) { }

        public ObservableList(List<T> list) : base(list) { }

        public ObservableList(IEnumerable<T> collection) : base(collection) { }

        public new void Add(T item)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
                SynchronizationContext.Current.Post(pl =>
                {
                    base.Add(item);
                }, null);
            });
        }
    }
}
