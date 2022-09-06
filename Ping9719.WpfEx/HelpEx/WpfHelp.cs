using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// wpf扩展帮助
    /// </summary>
    public static class WpfHelp
    {
        /// <summary>
        /// 保存到图片
        /// </summary>
        /// <param name="visual">用户控件，控件等，不建议窗体</param>
        /// <param name="fileName">全文件名</param>
        /// <param name="imageFormat">文件格式（针对png,jpg已做优化）</param>
        /// <param name="background">绘画画布颜色，png默认透明,jpg默认白色</param>
        public static void SaveToImg(this FrameworkElement visual, string fileName, ImageFormat imageFormat = ImageFormat.Png, Brush background = null)
        {
            if (imageFormat == ImageFormat.Png)
            {
                var encoder = new PngBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                using (var stream = File.Create(fileName))
                    encoder.Save(stream);
            }
            else if (imageFormat == ImageFormat.Jpeg)
            {
                var encoder = new JpegBitmapEncoder()
                {
                    QualityLevel = 100,
                };

                if (background == null)
                    background = Brushes.White;

                EncodeVisual(visual, encoder, background);

                using (var stream = File.Create(fileName))
                    encoder.Save(stream);
            }
            else if (imageFormat == ImageFormat.Bmp)
            {
                var encoder = new BmpBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                using (var stream = File.Create(fileName))
                    encoder.Save(stream);
            }
            else if (imageFormat == ImageFormat.Gif)
            {
                var encoder = new GifBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                using (var stream = File.Create(fileName))
                    encoder.Save(stream);
            }
            else if (imageFormat == ImageFormat.Tiff)
            {
                var encoder = new TiffBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                using (var stream = File.Create(fileName))
                    encoder.Save(stream);
            }
            else if (imageFormat == ImageFormat.Wmp)
            {
                var encoder = new WmpBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                using (var stream = File.Create(fileName))
                    encoder.Save(stream);
            }
        }

        /// <summary>
        /// 保存到图片到流
        /// </summary>
        /// <param name="visual">用户控件，控件等，不建议窗体</param>
        /// <param name="imageFormat">文件格式（针对png,jpg已做优化）</param>
        /// <param name="background">绘画画布颜色，png默认透明,jpg默认白色</param>
        public static MemoryStream SaveToImg(this FrameworkElement visual, ImageFormat imageFormat = ImageFormat.Png, Brush background = null)
        {
            MemoryStream memoryStream = new MemoryStream();

            if (imageFormat == ImageFormat.Png)
            {
                var encoder = new PngBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                encoder.Save(memoryStream);
            }
            else if (imageFormat == ImageFormat.Jpeg)
            {
                var encoder = new JpegBitmapEncoder()
                {
                    QualityLevel = 100,
                };

                if (background == null)
                    background = Brushes.White;

                EncodeVisual(visual, encoder, background);

                encoder.Save(memoryStream);
            }
            else if (imageFormat == ImageFormat.Bmp)
            {
                var encoder = new BmpBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                encoder.Save(memoryStream);
            }
            else if (imageFormat == ImageFormat.Gif)
            {
                var encoder = new GifBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                encoder.Save(memoryStream);
            }
            else if (imageFormat == ImageFormat.Tiff)
            {
                var encoder = new TiffBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                encoder.Save(memoryStream);
            }
            else if (imageFormat == ImageFormat.Wmp)
            {
                var encoder = new WmpBitmapEncoder();
                EncodeVisual(visual, encoder, background);

                encoder.Save(memoryStream);
            }
            return memoryStream;
        }

        static void EncodeVisual(FrameworkElement visual, BitmapEncoder encoder, Brush background)
        {
            if (visual.Parent == null && !(visual is Window))
            {
                var viewbox = new Viewbox();
                viewbox.Child = visual;
                viewbox.Measure(visual.RenderSize);
                viewbox.Arrange(new Rect(new Point(0, 0), visual.RenderSize));
                //myChart.Update(true, true);
                visual.UpdateLayout();
                viewbox.UpdateLayout();
            }

            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            if (background != null)
            {
                Rect rect = new Rect(0, 0, (int)visual.ActualWidth, (int)visual.ActualHeight);
                DrawingVisual visualBj = new DrawingVisual();
                DrawingContext context = visualBj.RenderOpen();
                context.DrawRectangle(background, null, rect);
                context.DrawImage(bitmap, rect);
                context.Close();
                bitmap.Render(visualBj);
            }

            bitmap.Render(visual);

            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
        }

    }
}
