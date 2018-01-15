using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RevendaController : Controller
    {
        // GET: Revenda
        public ActionResult Index()
        {
            var revendas = new List<RevendaModels>();

            var revenda = new RevendaModels();
            revenda.Id = 1;
            revenda.CNPJ = "3333";
            revenda.RazaoSocial = "3333";
            revendas.Add(revenda);
            return View(revendas);
        }

        // GET: Revenda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Revenda/Create
        public ActionResult Create()
        {
            var revenda = new RevendaModels();

            HttpContext.Session["enderecos"] = new List<EnderecoModels>();
            return View(revenda);
        }

        [HttpPost]
        public ActionResult CadastroEndereco(RevendaModels model)
        {
            model.Enderecos = (List<EnderecoModels>)HttpContext.Session["enderecos"];
            if (model.Enderecos != null && model.Enderecos.Any(e => e.Id == model.IdEndereco))
            {
                var endereco = model.Enderecos.First(e => e.Id == model.IdEndereco);
                endereco.CEP = model.CEP;
                endereco.Logradouro = model.Logradouro;
                endereco.Numero = model.Numero;
            }
            else
                model.Enderecos.Add(new EnderecoModels { Id = model.IdEndereco, CEP = model.CEP, Logradouro = model.Logradouro, Numero = model.Numero });

            HttpContext.Session["enderecos"] = model.Enderecos;
            return PartialView("_GridEndereco", model);
        }

        [HttpGet]
        public ActionResult ExcluirEndereco(int id)
        {
             var enderecos = (List<EnderecoModels>)HttpContext.Session["enderecos"];
            var endereco = enderecos.FirstOrDefault(e => e.Id == id);
            enderecos.Remove(endereco);
            
            HttpContext.Session["enderecos"] = enderecos;
            return PartialView("_GridEndereco", new RevendaModels {Enderecos = enderecos});
        }


        // POST: Revenda/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var lista = HttpContext.Session["enderecos"];
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Revenda/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Revenda/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Revenda/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Revenda/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
