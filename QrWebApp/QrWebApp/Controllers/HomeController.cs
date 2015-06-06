using com.libraries.qrgenerator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.libraries.flyergenerator;

namespace QrWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QrGenerator qr = new QrGenerator();

            //MODIFICAR LA URL NOMAS
            String url = "http://relevandopeligros.com/Peligro/InfoPeligro/282";





            String id = "QR_" + url.Replace("http://relevandopeligros.com/Peligro/InfoPeligro/", "");

            //CREA EL QR CON LOCO
            Bitmap bitmap = qr.GetQr(url, 500, 100);

            //GUARDO EL QR
            qr.SaveQr("C:/", id, bitmap);

            return View();
        }

        [HttpPost]
        public ActionResult Index(String url)
        {
           //FlyerGenerator.getFlyer(this, null, "nombre");

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