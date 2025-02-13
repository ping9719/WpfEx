using HandyControl.Controls;
using Ping9719.WpfEx.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using Path = System.IO.Path;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 图像监听。可针对文件夹文件进行监听显示
    /// </summary>
    public partial class ImageListener : UserControl
    {
        ImageListenerModel viewModel = new ImageListenerModel();
        public FileSystemWatcher FileWatcher = new FileSystemWatcher();
        /// <summary>
        /// 当前加载的图像路径
        /// </summary>
        public string FullPath { get; private set; }
        public ImageListener()
        {
            InitializeComponent();
            this.DataContext = viewModel;

            //拖动
            img.MouseLeftButtonDown += Img_MouseLeftButtonDown;
            img.MouseLeftButtonUp += Img_MouseLeftButtonUp;
            img.MouseMove += Img_MouseMove;
            //鼠标滚轮
            this.MouseWheel += Img_MouseWheel;
            //监听
            FileWatcher.Changed += FileSystemWatcher;
        }

        /// <summary>
        /// 如果在选项卡中，是否自动切换到当前变化的项来。默认为false
        /// </summary>
        public bool IsAutoActiveTabItem
        {
            get { return (bool)GetValue(IsAutoActiveTabItemProperty); }
            set { SetValue(IsAutoActiveTabItemProperty, value); }
        }

        public static readonly DependencyProperty IsAutoActiveTabItemProperty =
            DependencyProperty.Register("IsAutoActiveTabItem", typeof(bool), typeof(ImageListener), new PropertyMetadata(false));

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="path">监听的目录或文件</param>
        /// <param name="filter">对目录的筛选值（文件无效）</param>
        /// <returns>是否监听成功</returns>
        public bool StartListener(string path, string filter = "*.jpg")
        {
            FileWatcher.EnableRaisingEvents = false;

            path = (path ?? "").Trim();
            if (string.IsNullOrEmpty(path))
                return false;

            if (Path.HasExtension(path))
            {
                var ext = Path.GetFileName(path);
                if (!string.IsNullOrEmpty(ext))
                    filter = ext;

                path = Path.GetDirectoryName(path);
            }

            try
            {
                Directory.CreateDirectory(path);

                FileWatcher.Path = path;
                FileWatcher.Filter = filter;
                FileWatcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.LastAccess;
                FileWatcher.IncludeSubdirectories = false;
                FileWatcher.EnableRaisingEvents = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void StopListener()
        {
            FileWatcher.EnableRaisingEvents = false;
        }

        string FullPathTop = string.Empty;
        private void FileSystemWatcher(object sender, FileSystemEventArgs e)
        {
            if (FullPathTop == e.FullPath)
                return;

            FullPathTop = e.FullPath;

            Thread.Sleep(400);//确保大图片的成功
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    viewModel.ImageSource = null;
                    viewModel.ImageSource = BitmapFrame.Create(new Uri(e.FullPath), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);
                    viewModel.ImageHeight = viewModel.ImageSource.Height;
                    viewModel.ImageWidth = viewModel.ImageSource.Width;

                    FullPath = e.FullPath;
                    viewModel.ImgAutoSize();

                    if (IsAutoActiveTabItem)
                    {
                        if (this.Parent is System.Windows.Controls.TabItem tabItem)
                        {
                            if (tabItem.Parent is System.Windows.Controls.TabControl tabControl)
                            {
                                tabControl.SelectedItem = tabItem;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    FullPathTop = string.Empty;
                }
            }));
        }

        #region 变换
        private bool mouseLeftDown = false;
        private Point mouseLeftXY;

        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseLeftDown = true;
            mouseLeftXY = e.GetPosition((FrameworkElement)this);
        }

        private void Img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mouseLeftDown = false;
        }

        private void Img_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLeftDown && e.LeftButton == MouseButtonState.Pressed)
            {
                var position = e.GetPosition((FrameworkElement)this);
                viewModel.TranslateX -= mouseLeftXY.X - position.X;
                viewModel.TranslateY -= mouseLeftXY.Y - position.Y;
                mouseLeftXY = position;
            }
        }

        private void Img_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta * 0.001;
            var scaleXY = viewModel.ScaleXY + delta;
            if (scaleXY < 0.05 || scaleXY > 5)
                return;

            viewModel.CenterX = img.ActualWidth / 2;
            viewModel.CenterY = img.ActualHeight / 2;

            //校正
            var point = Mouse.GetPosition(img);
            //图片外
            if (point.X < 0 || point.Y < 0 || point.X > img.ActualWidth || point.Y > img.ActualHeight)
            {
                //不需要校正
            }
            else
            {
                var lx = img.ActualWidth / 2.00 - point.X;
                var ly = img.ActualHeight / 2.00 - point.Y;
                var pyx = lx * scaleXY - lx * viewModel.ScaleXY;
                var pyy = ly * scaleXY - ly * viewModel.ScaleXY;

                viewModel.TranslateX += pyx;
                viewModel.TranslateY += pyy;
            }

            viewModel.ScaleXY = scaleXY;
        }
        #endregion

        #region 效果
        DateTime dtsj = DateTime.Now;
        private void previewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now - dtsj).TotalMilliseconds <= 300)
            {
                if (e.ChangedButton == MouseButton.Left && !string.IsNullOrEmpty(FullPath))
                    if (File.Exists(FullPath))
                        new ImageBrowser(new Uri(FullPath)).Show();
            }
            dtsj = DateTime.Now;
        }

        private void ckyt(object sender, RoutedEventArgs e)
        {
            if (File.Exists(FullPath))
                new ImageBrowser(new Uri(FullPath)).Show();
        }

        private void syck(object sender, RoutedEventArgs e)
        {
            viewModel.ImgAutoSize();
        }

        private void ytdx(object sender, RoutedEventArgs e)
        {
            viewModel.ScaleXY = viewModel.ImageWidth / this.ActualWidth;
            viewModel.CenterX = img.ActualWidth / 2;
            viewModel.CenterY = img.ActualHeight / 2;
            viewModel.TranslateX = 0;
            viewModel.TranslateY = 0;
        }
        #endregion

    }

    internal class ImageListenerModel : BindableBase
    {
        #region MyRegion
        private BitmapFrame ImageSource_;
        public BitmapFrame ImageSource { get => ImageSource_; set { SetProperty(ref ImageSource_, value); } }

        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }
        #endregion

        #region 变换
        private double ScaleXY_ = 1;

        public double ScaleXY { get => ScaleXY_; set { SetProperty(ref ScaleXY_, value); } }

        private double CenterX_;

        public double CenterX { get => CenterX_; set { SetProperty(ref CenterX_, value); } }

        private double CenterY_;

        public double CenterY { get => CenterY_; set { SetProperty(ref CenterY_, value); } }

        private double TranslateX_;

        public double TranslateX { get => TranslateX_; set { SetProperty(ref TranslateX_, value); } }

        private double TranslateY_;

        public double TranslateY { get => TranslateY_; set { SetProperty(ref TranslateY_, value); } }

        /// <summary>
        /// 自适应大小
        /// </summary>
        public void ImgAutoSize()
        {
            ScaleXY = 1;
            CenterX = 0;
            CenterY = 0;
            TranslateX = 0;
            TranslateY = 0;
        }
        #endregion
    }
}
