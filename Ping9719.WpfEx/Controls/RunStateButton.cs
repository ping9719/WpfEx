﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 运行状态按钮
    /// </summary>
    public class RunStateButton : Control
    {
        static RunStateButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RunStateButton), new FrameworkPropertyMetadata(typeof(RunStateButton)));
        }

        /// <summary>
        /// 最近一次单击的按钮
        /// </summary>
        public RunStateButtonClick RunStateButtonClick { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var aa1 = this.Template.FindName("_start", this);
            var aa2 = this.Template.FindName("_pause", this);
            var aa3 = this.Template.FindName("_continue", this);
            var aa4 = this.Template.FindName("_stop", this);
            var aa5 = this.Template.FindName("_rset", this);

            if (aa1 is Button but1)
                but1.Click += (sen1, obj1) =>
                {
                    RunStateButtonClick = RunStateButtonClick.Start;
                    this.RaiseEvent(new RoutedEventArgs(ClickEvent, RunStateButtonClick));
                };
            if (aa2 is Button but2)
                but2.Click += (sen1, obj1) =>
                {
                    RunStateButtonClick = RunStateButtonClick.Pause;
                    this.RaiseEvent(new RoutedEventArgs(ClickEvent, RunStateButtonClick));
                };
            if (aa3 is Button but3)
                but3.Click += (sen1, obj1) =>
                {
                    RunStateButtonClick = RunStateButtonClick.Continue;
                    this.RaiseEvent(new RoutedEventArgs(ClickEvent, RunStateButtonClick));
                };
            if (aa4 is Button but4)
                but4.Click += (sen1, obj1) =>
                {
                    RunStateButtonClick = RunStateButtonClick.Stop;
                    this.RaiseEvent(new RoutedEventArgs(ClickEvent, RunStateButtonClick));
                };
            if (aa5 is Button but5)
                but5.Click += (sen1, obj1) =>
                {
                    RunStateButtonClick = RunStateButtonClick.Rset;
                    this.RaiseEvent(new RoutedEventArgs(ClickEvent, RunStateButtonClick));
                };
        }

        /// <summary>
        /// 单击按钮时
        /// </summary>
        public event RoutedEventHandler Click
        {
            add { this.AddHandler(ClickEvent, value); }
            remove { this.RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("ClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RunStateButton));

        /// <summary>
        /// 是否显示复位按钮
        /// </summary>
        public bool IsVisRset
        {
            get { return (bool)GetValue(IsVisRsetProperty); }
            set { SetValue(IsVisRsetProperty, value); }
        }

        public static readonly DependencyProperty IsVisRsetProperty =
            DependencyProperty.Register("IsVisRset", typeof(bool), typeof(RunStateButton), new PropertyMetadata(false));


        /// <summary>
        /// 是否自动控制复位按钮是否可用
        /// </summary>
        public bool IsAutoRset
        {
            get { return (bool)GetValue(IsAutoRsetProperty); }
            set { SetValue(IsAutoRsetProperty, value); }
        }

        public static readonly DependencyProperty IsAutoRsetProperty =
            DependencyProperty.Register("IsAutoRset", typeof(bool), typeof(RunStateButton), new PropertyMetadata(true));


        /// <summary>
        /// 运行状态
        /// </summary>
        public RunState RunState
        {
            get { return (RunState)GetValue(RunStateProperty); }
            set { SetValue(RunStateProperty, value); }
        }

        public static readonly DependencyProperty RunStateProperty =
            DependencyProperty.Register("RunState", typeof(RunState), typeof(RunStateButton), new PropertyMetadata(RunState.Stop));

        public event EventHandler CanExecuteChanged;


        #region buttonContent

        /// <summary>
        /// 启动按钮内容
        /// </summary>
        public object StartButContent
        {
            get { return (object)GetValue(StartButContentProperty); }
            set { SetValue(StartButContentProperty, value); }
        }

        public static readonly DependencyProperty StartButContentProperty =
            DependencyProperty.Register("StartButContent", typeof(object), typeof(RunStateButton), new PropertyMetadata((object)"启动"));



        /// <summary>
        /// 暂停按钮内容
        /// </summary>
        public object PauseButContent
        {
            get { return (object)GetValue(PauseButContentProperty); }
            set { SetValue(PauseButContentProperty, value); }
        }

        public static readonly DependencyProperty PauseButContentProperty =
            DependencyProperty.Register("PauseButContent", typeof(object), typeof(RunStateButton), new PropertyMetadata((object)"暂停"));


        /// <summary>
        /// 继续按钮内容
        /// </summary>
        public object ContinueButContent
        {
            get { return (object)GetValue(ContinueButContentProperty); }
            set { SetValue(ContinueButContentProperty, value); }
        }

        public static readonly DependencyProperty ContinueButContentProperty =
            DependencyProperty.Register("ContinueButContent", typeof(object), typeof(RunStateButton), new PropertyMetadata((object)"继续"));


        /// <summary>
        /// 停止按钮内容
        /// </summary>
        public object StopButContent
        {
            get { return (object)GetValue(StopButContentProperty); }
            set { SetValue(StopButContentProperty, value); }
        }

        public static readonly DependencyProperty StopButContentProperty =
            DependencyProperty.Register("StopButContent", typeof(object), typeof(RunStateButton), new PropertyMetadata((object)"停止"));


        /// <summary>
        /// 复位按钮内容
        /// </summary>
        public object RsetButContent
        {
            get { return (object)GetValue(RsetButContentProperty); }
            set { SetValue(RsetButContentProperty, value); }
        }

        public static readonly DependencyProperty RsetButContentProperty =
            DependencyProperty.Register("RsetButContent", typeof(object), typeof(RunStateButton), new PropertyMetadata((object)"复位"));

        #endregion
    }
}
