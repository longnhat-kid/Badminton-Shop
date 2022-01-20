using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        OnlineShopDBEntities db = new OnlineShopDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        private void UpdateProduct(int maSP, int SoLuong)
        {
            var productUpdate = db.SanPhams.Single(product => product.MaSP == maSP);
            productUpdate.SoLanMua += SoLuong;
            productUpdate.SoLuongTon -= SoLuong;
            db.SaveChanges();
        }

        public ActionResult ProcessOrder(DonDatHang orderInfor)
        {
            if (ModelState.IsValid)
            {
                ThanhVien user = Session["User"] as ThanhVien;
                GioHang userCart = db.GioHangs.Single(cart => cart.MaTV == user.MaTV);
                List<SanPhamGioHang> listSanPham = db.SanPhamGioHangs.Where(product => product.MaGioHang == userCart.MaGioHang).ToList();
                if (listSanPham.Count == 0)
                {
                    ViewBag.isOrderSuccess = false;
                    ModelState.Clear();
                    return View("~/Views/Order/Index.cshtml");
                }
                var lastInsertedOrder = db.DonDatHangs.Add(orderInfor);
                db.SaveChanges();
                foreach (var product in listSanPham)
                {
                    ChiTietDonDatHang newOrderProduct = new ChiTietDonDatHang(product, lastInsertedOrder.MaDDH);
                    UpdateProduct(product.MaSP, product.SoLuong);
                    db.ChiTietDonDatHangs.Add(newOrderProduct);
                    db.SanPhamGioHangs.Remove(product);
                    db.SaveChanges();
                }
                ViewBag.isOrderSuccess = true;
                ModelState.Clear();
                return View("~/Views/Order/Index.cshtml");
            }
            else
            {
                return View("~/Views/Order/Index.cshtml");
            }
        }

    }
}