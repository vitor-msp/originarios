using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Originarios.Models;

namespace Originarios.Controllers
{
    public class HomeController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        public ActionResult Index(int skip = 0)
        {
            List<Postagem> allPosts = db.Postagem.ToList();
            allPosts.Reverse();
            IEnumerable<Postagem> skipedPosts = allPosts.Skip(skip);
            List<Postagem> filteredPosts = new List<Postagem>();

            for (int i = 0; i < 8; i++)
            {
                try
                {
                    filteredPosts.Add(skipedPosts.ElementAt(i));
                }
                catch (Exception erro)
                {
                    continue;
                }
            }

            ViewBag.pre = skip - 8;
            ViewBag.pos = skip + 8 >= allPosts.Count ? -1 : skip + 8;
            return View(filteredPosts);


        }
        public ActionResult Contact()
        {
            return RedirectToAction("Create", "Contatos");
        }
    }
}