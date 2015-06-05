using System;
using System.IO;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows;
using System.Drawing.Imaging;
using System.Drawing;
using Gma.QrCodeNet.Encoding.Windows.Controls;

namespace com.libraries.qrgenerator
{
    public class GeneradorQr
    {
        public GeneradorQr()
        {
            GenerarQr("D:/", "http://relevandopeligros.com/Peligro/InfoPeligro/282", 50);
        }

        private void GenerarQr(string dir, string content, int image_size)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = qrEncoder.Encode(content);

            Renderer renderer = new Renderer(image_size, Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ms, ImageFormat.Png);

            //renderer.WriteToStream(qrCode.Matrix, ms, ImageFormat.Png);

            var imageTemp = new Bitmap(ms);

            var image = new Bitmap(imageTemp, new Size(new Point(200, 200)));

            image.Save(dir + "qr.png", ImageFormat.Png);
        }
    }

}
