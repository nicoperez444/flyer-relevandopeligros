using com.libraries.qrgenerator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using com.libraries.flyergenerator;

namespace QrWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.mensajeAviso = TempData["mensajeAviso"]; ;
            ViewBag.mensajeFracaso = TempData["mensajeFracaso"]; ;

            return View();
        }

        [HttpPost]
        public ActionResult Index(String url)
        {
            //FlyerGenerator.getFlyer(this, null, "nombre");

            try
            {
                //String url = "http://relevandopeligros.com/Peligro/InfoPeligro/253";

                QrGenerator qr = new QrGenerator();

                String id = url.Replace("http://relevandopeligros.org/Peligro/InfoPeligro/", "");

                Bitmap bitmap = qr.GetQr(url, "", 500, 100);

                qr.SaveQr("D:/", "QR_" + id, bitmap);

                TempData["mensajeAviso"] = "QR generado =)";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["mensajeFracaso"] = "No se pudo generar el QR :_(";
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}