using ArDesignForminhas_Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArDesignForminhas_Web.Controllers
{
    public class BannerController : Controller
    {
        private IBannerRepositorio repositorio;

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

        public ActionResult Editar(int id)
        {
            return View();
        }
    }
}