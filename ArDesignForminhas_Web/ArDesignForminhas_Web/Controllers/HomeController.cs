using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArDesignForminhas_Web.Controllers
{
    public class HomeController : Controller
    {
        private IBannerRepositorio repositorioBanner;
        private IProdutoRepositorio repositorioProduto;

        public HomeController(IBannerRepositorio _repositorio, IProdutoRepositorio _repositorioProduto)
        {
            this.repositorioBanner = _repositorio;
            this.repositorioProduto = _repositorioProduto;
        }

        public ActionResult Index()
        {
            return View(PreencheViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private HomeViewModel PreencheViewModel()
        {
            var objViewModel = new HomeViewModel();

            objViewModel.ListaBanner = repositorioBanner.ListarHome();
            objViewModel.ListaProdutoDestaque = repositorioProduto.ListarDestaque();

            return objViewModel;
        }
    }
}