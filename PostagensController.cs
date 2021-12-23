using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Originarios.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Originarios.Controllers
{
    public class PostagensController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        // GET: Postagens
        public ActionResult Index()
        {
            //var postagem = db.Postagem.Include(p => p.Usuario1);
            return View(db.Postagem.ToList());
        }

        // GET: Postagens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
            //return View(db.Postagem.ToList());
        }

        // GET: Postagens/Create
        public ActionResult Create()
        {
            ViewBag.usuario = new SelectList(db.Usuario, "id_usu", "nome");
            return View();
        }

        // POST: Postagens/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_post,usuario,titulo,descricao,corpo,nm_img1,vb_img1,nm_img2,vb_img2,nm_img3,vb_img3,nm_img4,vb_img4")] Postagem postagem, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3, HttpPostedFileBase img4)
        {
            if (ModelState.IsValid)
            {
                /////////////////////////////////////////////////////////////
                if (img1.FileName != null)
                {

                    MemoryStream target = new MemoryStream();
                    img1.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    postagem.vb_img1 = data;

                }

                /////////////////////////////////////////////////////////////
                db.Postagem.Add(postagem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuario = new SelectList(db.Usuario, "id_usu", "nome", postagem.usuario);
            return View(postagem);
        }

        /////////////////////////////////////////////////////////////
        public FileContentResult getImg1(int id)
        {
            byte[] array = db.Postagem.Find(id).vb_img1;
            return array != null ? new FileContentResult(array, "image/jpeg") : null;
        }
        /////////////////////////////////////////////////////////////

        // GET: Postagens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario = new SelectList(db.Usuario, "id_usu", "nome", postagem.usuario);
            return View(postagem);
        }

        // POST: Postagens/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_post,usuario,titulo,descricao,corpo,nm_img1,vb_img1,nm_img2,vb_img2,nm_img3,vb_img3,nm_img4,vb_img4")] Postagem postagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuario = new SelectList(db.Usuario, "id_usu", "nome", postagem.usuario);
            return View(postagem);
        }

        // GET: Postagens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // POST: Postagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Postagem postagem = db.Postagem.Find(id);
            db.Postagem.Remove(postagem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
