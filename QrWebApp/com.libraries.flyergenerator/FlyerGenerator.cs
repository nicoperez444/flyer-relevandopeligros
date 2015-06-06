using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace com.libraries.flyergenerator
{
    public abstract class FlyerGenerator
    {

        // esquema html por defecto 
        private static string flyerImage = @"Design\\flyer-bg.png";
        
        /// <summary>
        /// Permite generar el Flyer en base a los paramatros pasados, los path deben ser completos con
        /// nombre de archivo y extension inclusive.
        /// Metodo que permite validar los path pasados, validaciones de negocio, unico con visibilidad publica
        /// </summary>
        /// <param name="qrPath">Path de la imagen QR generada previamente</param>
        /// <param name="outPutFilePath">Path donde se almacenara el pdf generado</param>
        /// <returns>Path del pdf creado</returns>
        public static String generateNewFlyer(String qrPath, String outPutFilePath)
        {
            // se realizaron todas las validaciones, se manda a generar el archivo
            return createNewPdfFile(qrPath, outPutFilePath);
        }

        /// <summary>
        ///  Genera genera el pdf en base a la imagen del flyer y del qr pasado por parametro
        /// </summary>
        /// <param name="qrPath">Path de la imagen QR generada previamente</param>
        /// <param name="outPutFilePath">Path donde se almacenara el pdf generado</param>
        /// <returns>Path del pdf creado</returns>
        private static string createNewPdfFile(String qrPath, String outPutFilePath)
        {
            // generar un nombre para la imagen compuesta
            String mergeImage = @"mergeImage_"+DateTime.Now.Ticks.ToString()+".png";

            System.Drawing.Image flyerImageImg = System.Drawing.Image.FromFile(flyerImage);
            System.Drawing.Image qrImageImg = System.Drawing.Image.FromFile(qrPath);

            // tamanos necesarios del flyer completo
            int compWidth = 840;
            int compHeight = 768;

            // cambiar tamaño del codigo QR
            qrImageImg = resizeImage(qrImageImg, new System.Drawing.Size(260, 260));

            // hacer un merge de del backgroun y del codigo QR
            Bitmap bmpMerged = new Bitmap(compWidth, compHeight);

            Graphics g = Graphics.FromImage(bmpMerged);

            g.Clear(Color.White);

            // Background
            g.DrawImage(flyerImageImg, new Point(0, 0));

            // QR
            g.DrawImage(qrImageImg, new Point(489, 155));

            g.Dispose();
            flyerImageImg.Dispose();
            qrImageImg.Dispose();

            bmpMerged.Save(mergeImage, System.Drawing.Imaging.ImageFormat.Png);
            bmpMerged.Dispose();

            // path de destino del archivo generado
            string dstPdfPath = outPutFilePath + "Flyer_" + DateTime.Now.Ticks.ToString() + ".pdf";

            // convertir a pdf la imagen creada
            ConvertImageToPdf(mergeImage, dstPdfPath);

            // retornar la url creada
            return dstPdfPath;
        }

        /// <summary>
        /// Convirte una imagen en pdf
        /// </summary>
        /// <param name="srcFilename">El path del archivo origen</param>
        /// <param name="dstFilename">El path del archivo destino</param>
        private static void ConvertImageToPdf(string srcFilename, string dstFilename)
        {
            iTextSharp.text.Rectangle pageSize = null;

            using (var srcImage = new Bitmap(srcFilename))
            {
                pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
            }
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                var image = iTextSharp.text.Image.GetInstance(srcFilename);
                document.Add(image);
                document.Close();

                File.WriteAllBytes(dstFilename, ms.ToArray());
            }
        }

        /// <summary>
        /// Permite cambiar el tamaño de la imagen
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            return (System.Drawing.Image)(new Bitmap(imgToResize, size));
        }
    }
}
