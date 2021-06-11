using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace ArDesignForminhas_Web.Controllers
{
    public class ProdutoController : Controller
    {
        private ICategoriaRepositorio categoriaRepositorio;
        private IProdutoRepositorio repositorio;
        private const string CaminhoFotoProduto = "~/Content/Imagens/Produto/";

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
                using (TransactionScope scope = new TransactionScope())
                {
                    var caminhoArquivo = Server.MapPath(CaminhoFotoProduto);
                    var proximoID = repositorio.ObterProximoID();
                    var contador = 1;
                    var listaImagem = new List<ImagemProduto>();

                    foreach (HttpPostedFileBase objFoto in Request.Files)
                    {
                        var nomeArquivo = $"{proximoID}_FOTO_{contador}";

                        listaImagem.Add(new ImagemProduto()
                        {
                            Caminho = CaminhoFotoProduto + nomeArquivo,
                            IdProduto = proximoID,
                            Nome = nomeArquivo
                        });

                        objFoto.SaveAs(caminhoArquivo + nomeArquivo);
                        contador++;
                    }

                    objProduto.Imagens = listaImagem;

                    repositorio.Adicionar(objProduto);
                    
                    scope.Complete();
                }


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
