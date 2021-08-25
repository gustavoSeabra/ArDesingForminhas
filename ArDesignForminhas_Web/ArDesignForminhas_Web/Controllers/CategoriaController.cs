using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArDesignForminhas_Web.Controllers
{
    public class CategoriaController : Controller
    {
        private IProdutoRepositorio repositorioProduto;
        private ICategoriaRepositorio repositorioCategoria;

        public CategoriaController(IProdutoRepositorio _repositorioProduto, ICategoriaRepositorio _repositorioCategoria)
        {
            this.repositorioProduto = _repositorioProduto;
            this.repositorioCategoria = _repositorioCategoria;
        }


        // GET: Categoria
        public ActionResult index()
        {
            return View();
        }

        public ActionResult detalhes(int id)
        {
            var categoriaVM = new CategoriaViewModel();

            try
            {
                categoriaVM.objCategoria = repositorioCategoria.ObeterPorCodigo(id);
                categoriaVM.listaProdutos = repositorioProduto.ListarPorCategoria(id);
            }
            catch(Exception ex)
            {

            }

            return View(categoriaVM);
        }
    }
}