﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cukiernia.Controllers
{
    public class ProduktyController : Controller
    {
        // GET: Produkty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lista(string nazwaKategori)
        {
            return View();
        }
        public ActionResult Szczegoly(string id)
        {
            return View();
        }
    }
}