using System;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows;
using System.Drawing.Imaging;
using Gma.QrCodeNet.Encoding.Windows.Controls;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Drawing.Drawing2D;

namespace com.libraries.qrgenerator
{
    public class QrGenerator
    {
        public void SaveQr(string savePath, string fileName, Bitmap imageQr)
        {
            var image = imageQr;

            image.Save(savePath + fileName + ".png", ImageFormat.Png);
        }

        public Bitmap GetQr(string content, int sizeQr, int sizeLogo)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);

            QrCode qrCode = qrEncoder.Encode(content);

            Renderer renderer = new Renderer(100, Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ms, ImageFormat.Png);

            var imageTemp = new Bitmap(ms);

            var image = new Bitmap(imageTemp, new Size(new Point(sizeQr, sizeQr)));

            String direccion = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/"), "logoQr.png");

            Bitmap watermarkImage = ResizeImg((Bitmap)Bitmap.FromFile(direccion), sizeLogo);

            Bitmap clone = new Bitmap(watermarkImage.Width, watermarkImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(watermarkImage, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            using (Graphics imageGraphics = Graphics.FromImage(image))
            {
                watermarkImage.SetResolution(imageGraphics.DpiX, imageGraphics.DpiY);

                int x = ((image.Width - watermarkImage.Width) / 2);
                int y = ((image.Height - watermarkImage.Height) / 2);

                imageGraphics.DrawImage(watermarkImage, x, y, watermarkImage.Width, watermarkImage.Height);
            }

            return image;
        }

        private Bitmap ResizeImg(Bitmap image, int size)
        {
            var thumbnailBitmap = new Bitmap(size, size);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, size, size);

            thumbnailGraph.DrawImage(image, imageRectangle);

            return thumbnailBitmap;

        }
    }

}
