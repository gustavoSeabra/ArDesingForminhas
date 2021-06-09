using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArDesignForminhas_Web.Controllers
{
    public class ProdutoController : Controller
    {
        private ICategoriaRepositorio categoriaRepositorio;
        private IProdutoRepositorio repositorio;

        public ProdutoController(IProdutoRepositorio _repositorio, ICategoriaRepositorio _repositorioCategoria)
        {
            this.repositorio = _repositorio;
            this.categoriaRepositorio = _repositorioCategoria;
        }

        // GET: Produto
        public ActionResult Index() => View();
        
        [HttpGet]
        public ActionResult Listar()
        {
            return PartialView("_Listar", repositorio.Listar());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produto/Create
        public ActionResult Cadastrar()
        {
            PreencheViewBagCategoria();
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Cadastrar(Produto objProduto)
        {
            try
            {
                repositorio.Adicionar(objProduto);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repositorio.ObeterPorCodigo(id));
        }

        // POST: Produto/Edit/5
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

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produto/Delete/5
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

        private void PreencheViewBagCategoria()
        {
            var lista = categoriaRepositorio.Listar(string.Empty);

            ViewBag.Categoria = new SelectList(
                lista,
                "Codigo",
                "Nome");
        }
    }
}
