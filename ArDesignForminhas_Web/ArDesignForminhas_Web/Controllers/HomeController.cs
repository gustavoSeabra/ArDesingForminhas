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

        public HomeController(IBannerRepositorio _repositorio)
        {
            this.repositorioBanner = _repositorio;
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

            return objViewModel;
        }
    }
}