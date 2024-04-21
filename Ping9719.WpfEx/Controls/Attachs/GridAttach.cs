using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// Grid 扩展属性
    /// </summary>
    public class GridAttach
    {
        /// <summary>
        /// 得到列字符串。如“*,2.5*,100,auto”
        /// </summary>
        public static string GetColumns(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnsProperty);
        }

        /// <summary>
        /// 设置列字符串。如“*,2.5*,100,auto”
        /// </summary>
        public static void SetColumns(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached("Columns", typeof(string), typeof(GridAttach), new PropertyMetadata("", ColumnsChanged));

        private static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Grid grid))
                return;

            grid.ColumnDefinitions.Clear();

            var columns = (e.NewValue as string).Split(',').Select(o => o.Trim());
            foreach (var row in columns)
            {
                var leng = ParseGridLength(row);
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = leng });
            }
        }

        /// <summary>
        /// 得到行字符串。如“*,2.5*,100,auto”
        /// </summary>
        public static string GetRows(DependencyObject obj)
        {
            return (string)obj.GetValue(RowsProperty);
        }

        /// <summary>
        /// 设置行字符串。如“*,2.5*,100,auto”
        /// </summary>
        public static void SetRows(DependencyObject obj, string value)
        {
            obj.SetValue(RowsProperty, value);
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached("Rows", typeof(string), typeof(GridAttach), new PropertyMetadata("", RowsChanged));

        private static void RowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Grid grid))
                return;

            grid.RowDefinitions.Clear();
            //grid.ColumnDefinitions.Clear();

            var rows = (e.NewValue as string).Split(',').Select(o => o.Trim());

            foreach (var row in rows)
            {
                var leng = ParseGridLength(row);
                grid.RowDefinitions.Add(new RowDefinition() { Height = leng });
            }
        }

        private static GridLength ParseGridLength(string text)
        {
            if (text.EndsWith("*"))
            {
                double star = 1;
                if (text.Length > 1)
                    star = Convert.ToDouble(text.Substring(0, text.Length - 1));
                return new GridLength(star, GridUnitType.Star);
            }
            else if (text.Equals("auto", StringComparison.OrdinalIgnoreCase))
            {
                return GridLength.Auto;
            }
            else if (double.TryParse(text, out var val1))
            {
                return new GridLength(val1);
            }

            return GridLength.Auto;
        }


        //public static bool GetShowBorder(DependencyObject obj)
        //{
        //    return (bool)obj.GetValue(ShowBorderProperty);
        //}
        //public static void SetShowBorder(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(ShowBorderProperty, value);
        //}
        //public static readonly DependencyProperty ShowBorderProperty =
        //    DependencyProperty.RegisterAttached("ShowBorder", typeof(bool), typeof(GridAttach), new PropertyMetadata(false, OnShowBorderChanged));


        //private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (!(d is Grid grid)) return;

        //    grid.Loaded += OnLoaded;
        //}


        //private static void OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    if (!(sender is Grid grid)) 
        //        return;

        //    var rowCount = grid.RowDefinitions.Count;
        //    var columnCount = grid.ColumnDefinitions.Count;
        //    var thickness = new Thickness(1);
        //    var bottomThickness = new Thickness(0, 0, 0, 1);
        //    var rightThickness = new Thickness(0, 0, 1, 0);
        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        Border border = new Border()
        //        {
        //            BorderBrush = Brushes.Black,
        //            BorderThickness = i == 0 ? thickness : bottomThickness,
        //            Background = Brushes.Transparent,
        //        };
        //        border.SetValue(Panel.ZIndexProperty, -1000);
        //        border.SetValue(Grid.RowProperty, i);
        //        border.SetValue(Grid.ColumnProperty, 0);
        //        border.SetValue(Grid.ColumnSpanProperty, columnCount);
        //        grid.Children.Add(border);
        //    }
        //    for (int i = 0; i < columnCount; i++)
        //    {
        //        Border border = new Border()
        //        {
        //            BorderBrush = Brushes.Black,
        //            BorderThickness = i == 0 ? thickness : rightThickness,
        //            Background = Brushes.Transparent,
        //        };
        //        border.SetValue(Panel.ZIndexProperty, -1000);
        //        border.SetValue(Grid.RowProperty, 0);
        //        border.SetValue(Grid.ColumnProperty, i);
        //        border.SetValue(Grid.RowSpanProperty, rowCount);
        //        grid.Children.Add(border);
        //    }
        //    grid.Loaded -= OnLoaded;
        //}

    }
}
