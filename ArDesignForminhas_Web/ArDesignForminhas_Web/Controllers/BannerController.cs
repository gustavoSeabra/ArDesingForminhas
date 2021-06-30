using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using System;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ArDesignForminhas_Web.Controllers
{
    public class BannerController : Controller
    {
        private IBannerRepositorio repositorio;
        private const string CaminhoBanner = "~/Content/Imagens/Home/";

        public BannerController(IBannerRepositorio _repositorio)
        {
            repositorio = _repositorio;
        }

        // GET: Banner
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

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Cadastrar(Banner objBanner)
        {
            var caminhoArquivo = Server.MapPath(CaminhoBanner);
            var nomeArquivo = string.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (Request.Files[0] != null && !string.IsNullOrEmpty(Request.Files[0].FileName))
                    {
                        var idBanner = repositorio.Adicionar(objBanner);
                        nomeArquivo = $"{idBanner}_BANNER.png";

                        Request.Files[0].SaveAs(caminhoArquivo + nomeArquivo);

                        objBanner.Codigo = idBanner;
                        objBanner.Caminho = CaminhoBanner + nomeArquivo;

                        this.repositorio.Editar(objBanner);
                    }

                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExcluirBanner(CaminhoBanner + nomeArquivo);

                return RedirectToAction("Index");
            }
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

        #endregion
    }
}