using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ping9719.WpfEx.Mvvm
{
    /// <summary>
    /// 委托一个命名
    /// </summary>
    /// <remarks>
    /// 代码列子：
    /// <code>
    /// //XML代码
    /// &lt;Button Command="{Binding MyCommand}" Content="按钮"/&gt;
    /// 
    /// //类
    /// public class MyClass : BindableBase
    /// {
    ///     public ICommand MyCommand { get; }
    ///     //构造函数
    ///     public MyClass()
    ///     {
    ///         MyCommand = new DelegateCommand(MyFun);
    ///     }
    ///     //执行方法
    ///     public void MyFun()
    ///     {
    ///         //code
    ///     }
    /// }
    /// </code>
    /// </remarks>
    public class DelegateCommand : ICommand
    {
        Action _executeMethod;
        Func<bool> _canExecuteMethod;

        /// <summary>
        /// 创建实列
        /// </summary>
        /// <param name="executeMethod">一个方法</param>
        public DelegateCommand(Action executeMethod) : this(executeMethod, () => true)
        {

        }

        /// <summary>
        /// 创建实列
        /// </summary>
        /// <param name="executeMethod">一个方法</param>
        /// <param name="canExecuteMethod">是否可以执行此方式。使用方式：() => true</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base()
        {
            if (executeMethod == null)
                throw new ArgumentNullException(nameof(executeMethod));
            if (canExecuteMethod == null)
                throw new ArgumentNullException(nameof(canExecuteMethod));

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
            return _canExecuteMethod();
        }

        public void Execute(object parameter)
        {
            _executeMethod();
        }
    }
}
