﻿using com.libraries.qrgenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QrWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QrGenerator qr = new QrGenerator();

            return View();
        }

        [HttpPost]
        public ActionResult Index(String url)
        {
            return View();
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