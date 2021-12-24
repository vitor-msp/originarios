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
    [Authorize]
    public class PostagensController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        private Usuario BuscaUsuarioLogado()
        {
            string email = User.Identity.GetUserName();
            return db.Usuario.SingleOrDefault(u => u.email.Equals(email));
        }

        // rota: Meus_Produtos
        // lista os produtos do usuário logado
        public ActionResult Index(char? msg = null)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Msg = msg == 'c' ? "Produto postado com sucesso!!" 
                        : msg == 'e' ? "Produto atualizado com sucesso!!"
                        : msg == 'd' ? "Produto deletado com sucesso!!"
                        : null;
            IQueryable<Postagem> postagens = db.Postagem.Where(p => p.usuario == usuarioLogado.id_usu);
            return View(postagens.ToList());
        }

        // GET: Postagens/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Postagem postagem = db.Postagem.Find(id);
        //    if (postagem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(postagem);
        //}

        // rota: Criar_Produto
        // chama view para criação de produto
        public ActionResult Create()
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.usuario = usuarioLogado.id_usu;
            return View();
        }

        // POST da view Create
        // cria produto no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create
        (
            [Bind(Include = "id_post,usuario,titulo,descricao,corpo,nm_img1,vb_img1,nm_img2,vb_img2,nm_img3,vb_img3,nm_img4,vb_img4")] Postagem postagem, 
            HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3, HttpPostedFileBase img4
        )
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> imagens = new List<HttpPostedFileBase>();
                imagens.Add(img1);
                imagens.Add(img2);
                imagens.Add(img3);
                imagens.Add(img4);
                postagem = AdicionaImgNaPostagem(postagem, imagens);

                db.Postagem.Add(postagem);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = 'c' });
            }
            return View(postagem);
        }

        // rota: Editar_Produto
        // chama view para edição do produto
        public ActionResult Edit(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario = usuarioLogado.id_usu;
            return View(postagem);
        }

        // POST da view Edit
        // edita produto no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit
        (
            [Bind(Include = "id_post,usuario,titulo,descricao,corpo,nm_img1,vb_img1,nm_img2,vb_img2,nm_img3,vb_img3,nm_img4,vb_img4")] Postagem postagem,
            HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3, HttpPostedFileBase img4
        )
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> imagens = new List<HttpPostedFileBase>();
                imagens.Add(img1);
                imagens.Add(img2);
                imagens.Add(img3);
                imagens.Add(img4);
                postagem = AdicionaImgNaPostagem(postagem, imagens);

                db.Entry(postagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = 'e' });
            }
            return View(postagem);
        }

        // rota: Deletar_Produto
        // chama view para remoção do produto
        public ActionResult Delete(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
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

        // POST da view Delete
        // remove produto no banco de dados
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            db.Postagem.Remove(postagem);
            db.SaveChanges();
            return RedirectToAction("Index", new { msg = 'd' });
        }

        // método que adiciona imagens ao objeto 'postagem'
        private Postagem AdicionaImgNaPostagem (Postagem postagem, List<HttpPostedFileBase> imagens)
        {
            List<byte[]> vbImagens = new List<byte[]>();
            
            foreach(HttpPostedFileBase imagem in imagens)
            {
                if (imagem != null)
                {
                    MemoryStream target = new MemoryStream();
                    imagem.InputStream.CopyTo(target);
                    byte[] vbImagem = target.ToArray();
                    vbImagens.Add(vbImagem);
                }
                else
                {
                    vbImagens.Add(null);
                }
            }

            postagem.vb_img1 = vbImagens[0];
            postagem.vb_img2 = vbImagens[1];
            postagem.vb_img3 = vbImagens[2];
            postagem.vb_img4 = vbImagens[3];
            return postagem;
        }

        // método que busca, renderiza e retorna imagem
        public FileContentResult ObterImgNaView(int id, int img)
        {
            Postagem postagem = db.Postagem.Find(id);
            byte[] vbImg = null;

            switch (img)
            {
                case 1:
                    vbImg = postagem.vb_img1;
                    break;
                case 2:
                    vbImg = postagem.vb_img2;
                    break;
                case 3:
                    vbImg = postagem.vb_img3;
                    break;
                case 4:
                    vbImg = postagem.vb_img4;
                    break;
                default:
                    break;
            }

            return vbImg != null ? new FileContentResult(vbImg, "image/jpeg") : null;
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
