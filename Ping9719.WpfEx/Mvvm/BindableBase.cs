using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx.Mvvm
{
    /// <summary>
    /// 对 INotifyPropertyChanged 的实现
    /// </summary>
    /// <remarks>
    /// 代码列子：
    /// <code>
    /// //XML代码
    /// &lt;Button Content="{Binding Txt}"/&gt;
    /// 
    /// //在窗体中设置绑定数据源
    /// this.DataContext = new MainWindowViewModel()
    /// 
    /// //ViewModel类
    /// public class MainWindowViewModel : BindableBase
    /// {
    ///     private string _Txt = "按钮";
    ///     public string Title
    ///     {
    ///         get { return _Txt; }
    ///         set { SetProperty(ref _Txt, value); }
    ///     }
    /// }
    /// </code>
    /// </remarks>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 在属性值更改时发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 检查属性是否已经与所需的值匹配。 设置属性并仅在必要时通知侦听器。 
        /// </summary>
        /// <typeparam name="T">属性的类型</typeparam>
        /// <param name="storage">设置的属性，需要有get;set;</param>
        /// <param name="value">设置的值</param>
        /// <param name="propertyName"> 用于通知侦听器的属性的名称。 该值是可选的，可以在从支持CallerMemberName的编译器中调用时自动提供。 </param>
        /// <returns>如果值被更改，则为True，如果现有值与期望值匹配，则为false。 </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// 检查属性是否已经与所需的值匹配。 设置属性并仅在必要时通知侦听器。 
        /// </summary>
        /// <typeparam name="T">属性的类型</typeparam>
        /// <param name="storage">设置的属性，需要有get;set;</param>
        /// <param name="value">设置的值</param>
        /// <param name="onChanged">更改属性值后调用的操作。 </param>
        /// <param name="propertyName"> 用于通知侦听器的属性的名称。 该值是可选的，可以在从支持CallerMemberName的编译器中调用时自动提供。 </param>
        /// <returns>如果值被更改，则为True，如果现有值与期望值匹配，则为false。 </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// 引发此对象的PropertyChanged事件。
        /// </summary>
        /// <param name="propertyName">用于通知侦听器的属性的名称。 该值是可选的，可以在从支持<see cref="CallerMemberNameAttribute"/>的编译器中调用时自动提供。 </param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 引发此对象的PropertyChanged事件。
        /// </summary>
        /// <param name="propertyName">用于通知侦听器的属性的名称。</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 引发此对象的PropertyChanged事件。
        /// </summary>
        /// <param name="args">这是 PropertyChangedEventArgs</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

    }
}
