using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            var listaImagem = new List<ImagemProduto>();
            var caminhoArquivo = Server.MapPath(CaminhoFotoProduto);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    var idProduto = repositorio.Adicionar(objProduto);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(Request.Files[i].FileName))
                        {
                            var nomeArquivo = $"{idProduto}_FOTO_{i + 1}.png";

                            listaImagem.Add(new ImagemProduto()
                            {
                                Caminho = CaminhoFotoProduto + nomeArquivo,
                                IdProduto = idProduto,
                                Nome = nomeArquivo
                            });

                            Request.Files[i].SaveAs(caminhoArquivo + nomeArquivo);
                        }
                    }

                    if (listaImagem.Any())
                        repositorio.AdicionarImagemProduto(listaImagem);

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExcluirImagensProduto(caminhoArquivo, listaImagem);

                return RedirectToAction("Index");
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Editar(int id)
        {
            var objProduto = repositorio.ObeterPorCodigo(id);

            PreencheViewBagCategoria();
            PreencheViewBagCaminhoImagem(objProduto);

            return View(objProduto);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            var listaImagem = new List<ImagemProduto>();
            var caminhoArquivo = Server.MapPath(CaminhoFotoProduto);
            Produto objProduto = this.repositorio.ObeterPorCodigo(id);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    objProduto.Nome = collection["Nome"];
                    objProduto.Valor = Convert.ToDecimal(collection["Valor"]);
                    objProduto.CodCategoria = Convert.ToInt32(collection["CodCategoria"]);
                    objProduto.Descricao = collection["Descricao"];

                    repositorio.Editar(objProduto);
                    // Excluindo fotos salvas no disco
                    ExcluirImagensProduto(caminhoArquivo, objProduto.Imagens);
                    repositorio.ExcluirImagemProduto(objProduto.Codigo);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var nomeArquivo = $"{objProduto.Codigo}_FOTO_{i + 1}.png";

                        listaImagem.Add(new ImagemProduto()
                        {
                            Caminho = CaminhoFotoProduto + nomeArquivo,
                            IdProduto = objProduto.Codigo,
                            Nome = nomeArquivo
                        });

                        Request.Files[i].SaveAs(caminhoArquivo + nomeArquivo);
                    }

                    repositorio.AdicionarImagemProduto(listaImagem);

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Excluindo fotos salvas no disco
                ExcluirImagensProduto(caminhoArquivo, objProduto.Imagens);

                return RedirectToAction("Index");
            }
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Resposta objResposta = new Resposta();

            objResposta.Acao = "Delete";

            try
            {
                var caminhoArquivo = Server.MapPath(CaminhoFotoProduto);
                using (TransactionScope scope = new TransactionScope())
                {
                    var objProduto = repositorio.ObeterPorCodigo(id);

                    // Exclui arquivos fisicamente
                    ExcluirImagensProduto(caminhoArquivo, objProduto.Imagens);

                    // Exclui registros no banco
                    repositorio.ExcluirImagemProduto(id);
                    repositorio.Excluir(id);

                    scope.Complete();

                    objResposta.Status = HttpStatusCode.OK;
                    objResposta.Mensagem = string.Empty;
                }
            }
            catch(Exception ex)
            {
                objResposta.Status = HttpStatusCode.BadRequest;
                objResposta.Mensagem = ex.Message;
            }

            return serializer.Serialize(objResposta);
        }

        #region Métodos internos

        private void PreencheViewBagCategoria()
        {
            var lista = categoriaRepositorio.Listar(string.Empty);

            ViewBag.Categoria = new SelectList(
                lista,
                "Codigo",
                "Nome");
        }

        private void PreencheViewBagCaminhoImagem(Produto objProduto)
        {
            List<string> lista = new List<string>();
            string dominio = $"{Request.Url.Scheme}://{Request.Url.Authority}";

            foreach (var obj in objProduto.Imagens)
            {
                lista.Add($"'{dominio}{CaminhoFotoProduto.Substring(1)}{obj.Nome}'");
            }

            ViewBag.Arquivos = lista;
        }

        private void ExcluirImagensProduto(string caminhoArquivo, List<ImagemProduto> listaImagem)
        {
            foreach (var objFoto in listaImagem)
                System.IO.File.Delete(caminhoArquivo + objFoto.Nome);
        }

        #endregion
    }
}
