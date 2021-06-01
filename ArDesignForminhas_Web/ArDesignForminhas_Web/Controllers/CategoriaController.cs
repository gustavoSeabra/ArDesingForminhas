using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ArDesignForminhas_Web.Controllers
{
    public class CategoriaController : Controller
    {
        private ICategoriaRepositorio repositorio;

        public CategoriaController(ICategoriaRepositorio _repositorio)
        {
            repositorio = _repositorio;
        }
        
        // GET: Categoria
        public ActionResult Index()
        {
            return View(repositorio.Listar(string.Empty));
        }

       // GET: Categoria/Create
        public ActionResult Cadastrar()
        {
            PreencheViewBagCategoriaPai();

            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Cadastrar(Categoria objCategoria)
        {
            try
            {
                repositorio.Adicionar(objCategoria);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Categoria/Edit/5
        public ActionResult Editar(int id)
        {
            PreencheViewBagCategoriaPai();

            return View(repositorio.ObeterPorCodigo(id));
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                var objCategoria = new Categoria();

                objCategoria.Codigo = id;
                
                if (string.IsNullOrEmpty(collection["CodigoPai"]))
                    objCategoria.CodigoPai = null;
                else
                    objCategoria.CodigoPai = Convert.ToInt32(collection["CodigoPai"]);

                objCategoria.Nome = collection["Nome"];
                objCategoria.Descricao = collection["Descricao"];
                
                this.repositorio.Editar(objCategoria);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // POST: Categoria/Delete/5
        [HttpPost]
        public string Delete(int id, FormCollection collection)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Resposta objResposta = new Resposta();

            objResposta.Acao = "Delete";

            try
            {
                this.repositorio.Excluir(id);

                objResposta.Status = HttpStatusCode.OK;
                objResposta.Mensagem = string.Empty;
            }
            catch(Exception ex)
            {
                objResposta.Status = HttpStatusCode.BadRequest;
                objResposta.Mensagem = ex.Message;
            }

            return serializer.Serialize(objResposta);
        }

        private void PreencheViewBagCategoriaPai()
        {
            var lista = repositorio.ListarCategoriaPai();

            ViewBag.CategoriaPai = new SelectList(
                lista,
                "CodigoPai",
                "Nome");
        }
    }
}
