using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 自动表格（会自动设置子项在表格中的索引）
    /// </summary>
    public class AutoGrid : Grid
    {
        /*https://github.com/budul100/WpfAutoGrid.Core*/

        #region 公共字段

        public static readonly DependencyProperty ChildHorizontalAlignmentProperty = DependencyProperty.Register(
            name: "ChildHorizontalAlignment",
            propertyType: typeof(HorizontalAlignment?),
            ownerType: typeof(AutoGrid),
            typeMetadata: new FrameworkPropertyMetadata((HorizontalAlignment?)null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnChildHorizontalAlignmentChanged)));

        public static readonly DependencyProperty ChildMarginProperty = DependencyProperty.Register(
            name: "ChildMargin",
            propertyType: typeof(Thickness?),
            ownerType: typeof(AutoGrid),
            typeMetadata: new FrameworkPropertyMetadata((Thickness?)null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnChildMarginChanged)));

        public static readonly DependencyProperty ChildVerticalAlignmentProperty = DependencyProperty.Register(
            name: "ChildVerticalAlignment",
            propertyType: typeof(VerticalAlignment?),
            ownerType: typeof(AutoGrid),
            typeMetadata: new FrameworkPropertyMetadata((VerticalAlignment?)null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnChildVerticalAlignmentChanged)));

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.RegisterAttached(
            name: "ColumnCount",
            propertyType: typeof(int),
            ownerType: typeof(AutoGrid),
            defaultMetadata: new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ColumnCountChanged)));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached(
            name: "Columns",
            propertyType: typeof(string),
            ownerType: typeof(AutoGrid),
            defaultMetadata: new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ColumnsChanged)));

        public static readonly DependencyProperty ColumnWidthProperty = DependencyProperty.RegisterAttached(
            name: "ColumnWidth",
            propertyType: typeof(GridLength),
            ownerType: typeof(AutoGrid),
            defaultMetadata: new FrameworkPropertyMetadata(GridLength.Auto, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(FixedColumnWidthChanged)));

        public static readonly DependencyProperty IsAutoIndexingProperty = DependencyProperty.Register(
            name: "IsAutoIndexing",
            propertyType: typeof(bool),
            ownerType: typeof(AutoGrid),
            typeMetadata: new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            name: "Orientation",
            propertyType: typeof(Orientation),
            ownerType: typeof(AutoGrid),
            typeMetadata: new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty RowCountProperty = DependencyProperty.RegisterAttached(
            name: "RowCount",
            propertyType: typeof(int),
            ownerType: typeof(AutoGrid),
            defaultMetadata: new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(RowCountChanged)));

        public static readonly DependencyProperty RowHeightProperty = DependencyProperty.RegisterAttached(
            name: "RowHeight",
            propertyType: typeof(GridLength),
            ownerType: typeof(AutoGrid),
            defaultMetadata: new FrameworkPropertyMetadata(GridLength.Auto, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(FixedRowHeightChanged)));

        public static readonly DependencyProperty RowsProperty = DependencyProperty.RegisterAttached(
            name: "Rows",
            propertyType: typeof(string),
            ownerType: typeof(AutoGrid),
            defaultMetadata: new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(RowsChanged)));

        #endregion

        #region 公共属性

        /// <summary>
        /// 所有子项水平对齐方式
        /// </summary>
        [Category("Layout"), Description("所有子项水平对齐方式。")]
        public HorizontalAlignment? ChildHorizontalAlignment
        {
            get { return (HorizontalAlignment?)GetValue(ChildHorizontalAlignmentProperty); }
            set { SetValue(ChildHorizontalAlignmentProperty, value); }
        }

        /// <summary>
        /// 所有子项边距
        /// </summary>
        [Category("Layout"), Description("所有子项边距。")]
        public Thickness? ChildMargin
        {
            get { return (Thickness?)GetValue(ChildMarginProperty); }
            set { SetValue(ChildMarginProperty, value); }
        }

        /// <summary>
        /// 所有子项垂直对齐方式
        /// </summary>
        [Category("Layout"), Description("所有子项垂直对齐方式。")]
        public VerticalAlignment? ChildVerticalAlignment
        {
            get { return (VerticalAlignment?)GetValue(ChildVerticalAlignmentProperty); }
            set { SetValue(ChildVerticalAlignmentProperty, value); }
        }

        /// <summary>
        /// 列的数量
        /// </summary>
        [Category("Layout"), Description("列的数量。")]
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        /// <summary>
        /// 列定义（用逗号分割，如“100,*,2*,auto”）
        /// </summary>
        [Category("Layout"), Description("用逗号分隔的列定义。")]
        public string Columns
        {
            get { return (string)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// 所有列的宽度
        /// </summary>
        [Category("Layout"), Description("所有列的宽度。")]
        public GridLength ColumnWidth
        {
            get { return (GridLength)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        /// <summary>
        /// 是否自动布局。
        /// <remarks>
        [Category("Layout"), Description("是否自动布局。")]
        public bool IsAutoIndexing
        {
            get { return (bool)GetValue(IsAutoIndexingProperty); }
            set { SetValue(IsAutoIndexingProperty, value); }
        }

        /// <summary>
        /// 索引方向。（水平：从左到右，从上到下；垂直：从上到下，从左到右）默认水平
        /// </summary>
        [Category("Layout"), Description("建立索引的方向。")]
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// 行的数量
        /// </summary>
        [Category("Layout"), Description("行的数量。")]
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        /// <summary>
        /// 所有行高
        /// </summary>
        [Category("Layout"), Description("所有行高。")]
        public GridLength RowHeight
        {
            get { return (GridLength)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        /// <summary>
        /// 行定义（用逗号分割，如“100,*,2*,auto”）
        /// </summary>
        [Category("Layout"), Description("用逗号分隔的行定义。")]
        public string Rows
        {
            get { return (string)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 列数量更改事件
        /// </summary>
        public static void ColumnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((int)e.NewValue < 0)
                return;

            var grid = d as AutoGrid;

            //查找高度的现有列定义
            var width = GridLength.Auto;
            if (grid.ColumnDefinitions.Count > 0)
                width = grid.ColumnDefinitions[0].Width;

            //清除和重建
            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < (int)e.NewValue; i++)
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = width });
        }

        /// <summary>
        /// 列更改事件
        /// </summary>
        public static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((string)e.NewValue))
                return;

            var grid = d as AutoGrid;
            grid.ColumnDefinitions.Clear();

            var defs = Parse((string)e.NewValue);
            foreach (var def in defs)
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = def });
        }

        /// <summary>
        /// 固定列宽度更改事件
        /// </summary>
        public static void FixedColumnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as AutoGrid;

            //如果没有，添加默认列
            if (grid.ColumnDefinitions.Count == 0)
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            //将所有现有列设置为此宽度
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
                grid.ColumnDefinitions[i].Width = (GridLength)e.NewValue;
        }

        /// <summary>
        /// 固定行高度更改事件
        /// </summary>
        public static void FixedRowHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as AutoGrid;

            //如果没有，添加默认行
            if (grid.RowDefinitions.Count == 0)
                grid.RowDefinitions.Add(new RowDefinition());

            //将所有现有行设置为此高度
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
                grid.RowDefinitions[i].Height = (GridLength)e.NewValue;
        }

        /// <summary>
        /// 解析逗号分隔文本的网格长度数组
        /// </summary>
        public static GridLength[] Parse(string text)
        {
            var tokens = text.Split(',');
            var definitions = new GridLength[tokens.Length];
            for (var i = 0; i < tokens.Length; i++)
            {
                var str = tokens[i];
                double value;

                //比例
                if (str.Contains('*'))
                {
                    if (!double.TryParse(str.Replace("*", ""), out value))
                        value = 1.0;

                    definitions[i] = new GridLength(value, GridUnitType.Star);
                    continue;
                }

                //固定
                if (double.TryParse(str, out value))
                {
                    definitions[i] = new GridLength(value);
                    continue;
                }

                //自动
                definitions[i] = GridLength.Auto;
            }
            return definitions;
        }

        /// <summary>
        /// 行数更改事件
        /// </summary>
        public static void RowCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((int)e.NewValue < 0)
                return;

            var grid = d as AutoGrid;

            //查找现有行以获取高度
            var height = GridLength.Auto;
            if (grid.RowDefinitions.Count > 0)
                height = grid.RowDefinitions[0].Height;

            //清理和重建
            grid.RowDefinitions.Clear();
            for (int i = 0; i < (int)e.NewValue; i++)
                grid.RowDefinitions.Add(
                    new RowDefinition() { Height = height });
        }

        /// <summary>
        /// 行更改事件
        /// </summary>
        public static void RowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((string)e.NewValue))
                return;

            var grid = d as AutoGrid;
            grid.RowDefinitions.Clear();

            var defs = Parse((string)e.NewValue);
            foreach (var def in defs)
                grid.RowDefinitions.Add(new RowDefinition() { Height = def });
        }

        #endregion

        #region 保护方法

        protected override Size MeasureOverride(Size constraint)
        {
            this.PerformLayout();
            return base.MeasureOverride(constraint);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 当[子水平对齐更改]时调用
        /// </summary>
        private static void OnChildHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as AutoGrid;
            foreach (UIElement child in grid.Children)
            {
                if (grid.ChildHorizontalAlignment.HasValue)
                    child.SetValue(FrameworkElement.HorizontalAlignmentProperty, grid.ChildHorizontalAlignment);
                else
                    child.SetValue(FrameworkElement.HorizontalAlignmentProperty, DependencyProperty.UnsetValue);
            }
        }

        /// <summary>
        /// 当[子布局更改]时调用。
        /// </summary>
        private static void OnChildMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as AutoGrid;
            foreach (UIElement child in grid.Children)
            {
                if (grid.ChildMargin.HasValue)
                    child.SetValue(FrameworkElement.MarginProperty, grid.ChildMargin);
                else
                    child.SetValue(FrameworkElement.MarginProperty, DependencyProperty.UnsetValue);
            }
        }

        /// <summary>
        /// 当[子垂直对齐更改]时调用。
        /// </summary>
        private static void OnChildVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as AutoGrid;
            foreach (UIElement child in grid.Children)
            {
                if (grid.ChildVerticalAlignment.HasValue)
                    child.SetValue(FrameworkElement.VerticalAlignmentProperty, grid.ChildVerticalAlignment);
                else
                    child.SetValue(FrameworkElement.VerticalAlignmentProperty, DependencyProperty.UnsetValue);
            }
        }

        /// <summary>
        /// 应用子边距和布局效果，如对齐
        /// </summary>
        private void ApplyChildLayout(UIElement child)
        {
            if (ChildMargin != null)
            {
                child.SetIfDefault(FrameworkElement.MarginProperty, ChildMargin.Value);
            }
            if (ChildHorizontalAlignment != null)
            {
                child.SetIfDefault(FrameworkElement.HorizontalAlignmentProperty, ChildHorizontalAlignment.Value);
            }
            if (ChildVerticalAlignment != null)
            {
                child.SetIfDefault(FrameworkElement.VerticalAlignmentProperty, ChildVerticalAlignment.Value);
            }
        }

        /// <summary>
        /// 将值限制在最大值
        /// </summary>
        private int Clamp(int value, int max)
        {
            return (value > max) ? max : value;
        }

        /// <summary>
        /// 执行行和列索引的网格布局
        /// </summary>
        private void PerformLayout()
        {
            var fillRowFirst = this.Orientation == Orientation.Horizontal;
            var rowCount = this.RowDefinitions.Count;
            var colCount = this.ColumnDefinitions.Count;

            if (rowCount == 0 || colCount == 0)
                return;

            var position = 0;
            var skip = new bool[rowCount, colCount];
            foreach (UIElement child in Children)
            {
                var childIsCollapsed = child.Visibility == Visibility.Collapsed;
                if (IsAutoIndexing && !childIsCollapsed)
                {
                    if (fillRowFirst)
                    {
                        var row = Clamp(position / colCount, rowCount - 1);
                        var col = Clamp(position % colCount, colCount - 1);
                        if (skip[row, col])
                        {
                            position++;
                            row = (position / colCount);
                            col = (position % colCount);
                        }

                        Grid.SetRow(child, row);
                        Grid.SetColumn(child, col);
                        position += Grid.GetColumnSpan(child);

                        var offset = Grid.GetRowSpan(child) - 1;
                        while (offset > 0)
                        {
                            skip[row + offset--, col] = true;
                        }
                    }
                    else
                    {
                        var row = Clamp(position % rowCount, rowCount - 1);
                        var col = Clamp(position / rowCount, colCount - 1);
                        if (skip[row, col])
                        {
                            position++;
                            row = position % rowCount;
                            col = position / rowCount;
                        }

                        Grid.SetRow(child, row);
                        Grid.SetColumn(child, col);
                        position += Grid.GetRowSpan(child);

                        var offset = Grid.GetColumnSpan(child) - 1;
                        while (offset > 0)
                        {
                            skip[row, col + offset--] = true;
                        }
                    }
                }

                ApplyChildLayout(child);
            }
        }

        #endregion
    }

}
