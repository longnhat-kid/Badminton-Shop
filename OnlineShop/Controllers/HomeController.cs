using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        OnlineShopDBEntities db = new OnlineShopDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NavigationPartial()
        {
            var listProduct = db.SanPhams;
            return PartialView(listProduct);
        }

        public ActionResult Footer()
        {
            var listProduct = db.SanPhams;
            return PartialView("~/Views/Shared/_FooterPartial.cshtml",listProduct);
        }
    }
}