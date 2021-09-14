using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ArDesignForminhas_Web.Areas.Administracao.Controllers
{
    [Authorize]
    public class BannerController : Controller
    {
        #region Atributos,  Propriedades e Construtor

        private IBannerRepositorio repositorio;
        private const string CaminhoBanner = "/Content/Imagens/Home/";

        #endregion

        public BannerController(IBannerRepositorio _repositorio)
        {
            repositorio = _repositorio;
        }

        // GET: Administracao/Banner
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return PartialView("_Listar", repositorio.Listar());
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Banner objBanner)
        {
            var caminhoArquivo = Server.MapPath(CaminhoBanner);
            var nomeArquivo = string.Empty;

            try
            {
                if (Request.Files[0] != null && !string.IsNullOrEmpty(Request.Files[0].FileName))
                {
                    var idBanner = repositorio.Adicionar(objBanner);
                    nomeArquivo = $"{idBanner}_BANNER.png";

                    using (TransactionScope scope = new TransactionScope())
                    {
                        Request.Files[0].SaveAs(caminhoArquivo + nomeArquivo);

                        objBanner.Codigo = idBanner;
                        objBanner.Caminho = CaminhoBanner + nomeArquivo;

                        this.repositorio.Editar(objBanner);
                        scope.Complete();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExcluirBanner(CaminhoBanner + nomeArquivo);

                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(int id)
        {
            var objBanner = repositorio.ObeterPorCodigo(id);
            PreencheViewBagCaminhoImagem(objBanner);
            return View(objBanner);
        }


        [HttpPost]
        public ActionResult Editar(int id, Banner collection)
        {
            try
            {
                var oldBanner = repositorio.ObeterPorCodigo(id);

                oldBanner.EhHome = collection.EhHome;

                this.repositorio.Editar(oldBanner);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        // POST: BAnner/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Resposta objResposta = new Resposta();

            objResposta.Acao = "Delete";

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var objBanner = repositorio.ObeterPorCodigo(id);

                    // Exclui arquivos fisicamente
                    ExcluirBanner(Server.MapPath(objBanner.Caminho));

                    // Exclui registros no banco
                    repositorio.Excluir(id);

                    scope.Complete();

                    objResposta.Status = HttpStatusCode.OK;
                    objResposta.Mensagem = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objResposta.Status = HttpStatusCode.BadRequest;
                objResposta.Mensagem = ex.Message;
            }

            return serializer.Serialize(objResposta);
        }

        #region Métodos internos

        private void ExcluirBanner(string caminhoBanner)
        {
            System.IO.File.Delete(caminhoBanner);
        }

        private void PreencheViewBagCaminhoImagem(Banner objBanner)
        {
            string dominio = $"{Request.Url.Scheme}://{Request.Url.Authority}/";

            ViewBag.Arquivo = $"'{dominio}{objBanner.Caminho.Substring(1)}'";
        }

        #endregion
    }
}