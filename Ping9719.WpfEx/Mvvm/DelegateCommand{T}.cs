using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ping9719.WpfEx.Mvvm
{


    /// <summary>
    /// 委托一个带参数的命名
    /// </summary>
    /// <typeparam name="T">参数的类型，不能为null</typeparam>
    /// <remarks>
    /// 代码列子：
    /// <code>
    /// //XML代码
    /// &lt;Button CommandParameter="abc" Command="{Binding MyCommand}" Content="按钮"/&gt;
    /// //类
    /// public class MyClass : BindableBase
    /// {
    ///     public ICommand MyCommand { get; }
    ///     //构造函数
    ///     public MyClass()
    ///     {
    ///         MyCommand = new DelegateCommand&lt;string&gt;(MyFun);
    ///     }
    ///     //执行方法
    ///     public void MyFun(string obj)
    ///     {
    ///         //code
    ///     }
    /// }
    /// </code>
    /// </remarks>
    public class DelegateCommand<T> : ICommand
    {
        readonly Action<T> _executeMethod;
        Func<T, bool> _canExecuteMethod;

        /// <summary>
        /// 创建实列
        /// </summary>
        /// <param name="executeMethod">带参数的一个方法</param>
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, (o) => true)
        {
        }

        /// <summary>
        /// 创建实列
        /// </summary>
        /// <param name="executeMethod">带参数的一个方法</param>
        /// <param name="canExecuteMethod">带参数的一个方法并返回bool。使用方法：(obj) => true</param>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base()
        {
            if (executeMethod == null)
                throw new ArgumentNullException(nameof(executeMethod));
            if (canExecuteMethod == null)
                throw new ArgumentNullException(nameof(canExecuteMethod));

            //TypeInfo genericTypeInfo = typeof(T).GetTypeInfo();

            //// DelegateCommand允许object或Nullable<>。 
            //// 注意:Nullable<>是一个结构体，因此我们不能使用类约束。 
            //if (genericTypeInfo.IsValueType)
            //{
            //    if ((!genericTypeInfo.IsGenericType) || (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
            //    {
            //        throw new InvalidCastException(Resources.DelegateCommandInvalidGenericPayloadType);
            //    }
            //}

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod((T)parameter);
        }
    }
}
