using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aula1MVC.Context;
using Aula1MVC.Models;

namespace Aula1MVC.Controllers
{
    public class ClientesController : Controller
    {

        private Aula1Context db = new Aula1Context();

        // Podemos usar um cache de 30 em 30 segundos por exemplo para evitar multiplos requests de múltiplos clientes
        [OutputCache(Duration = 30)]
        public ContentResult Teste()
        {
            return Content(DateTime.Now.ToString());
        }

        // Cria um tipo de cache por cada parâmetro que vc passar, não precisa ser somente o id, para variar por todos basta colocar "*" no VaryByParam
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ContentResult Teste2(int id)
        {
            return Content(DateTime.Now.ToString());
        }

        // Permite receber qualquer tipo de dado seja ele perigoso ou não
        [ValidateInput(false)]
        public ContentResult Teste3(int id)
        {
            return Content(DateTime.Now.ToString());
        }

        // GET: Clientes
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // Se eu tentar alterar o token gerado dentro do form a cada novo cliente incluído vou ter uma mensagem impedindo a gravação
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Sobrenome,Email")] Cliente cliente)
        // public ActionResult Create(Cliente cliente), se a tabela tiver muitos campos não é necessário usar o bind
        {
            if (ModelState.IsValid)
            {
                if(!cliente.Email.Contains(".br"))
                {
                    ModelState.AddModelError(String.Empty, "E-mail não pode ser internacional");
                    return View(cliente);
                }
                cliente.DataCadastro = DateTime.Now;
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Sobrenome,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Apesar do nome desta action result ser DeleteConfirmed para que não tenhamos 2 assinaturas iguais, o ActionName permitirá chamar
        // esta action com o apelido dado de Delete
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
