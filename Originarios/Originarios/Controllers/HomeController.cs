﻿using System.Web.Mvc;

namespace Originarios.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Create", "Contatos");
        }
    }
}