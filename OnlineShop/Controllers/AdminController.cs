using OnlineShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        OnlineShopDBEntities db = new OnlineShopDBEntities();

        public ActionResult Index()
        {
            ThanhVien user = Session["User"] as ThanhVien;
            if(user != null && user.LoaiThanhVien.TenLoai == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ShowUserSystem()
        {
            var listUsers = db.ThanhViens;
            return PartialView(listUsers.OrderBy(user=>user.MaTV));
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] {
                "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă",
                "ắ", "ằ", "ẳ", "ẵ", "ặ","đ","é","è","ẻ","ẽ","ẹ","ê","ế","ề",
                "ể","ễ","ệ","í","ì","ỉ","ĩ","ị","ó","ò","ỏ","õ","ọ","ô","ố",
                "ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ","ú","ù","ủ","ũ","ụ",
                "ư","ứ","ừ","ử","ữ","ự","ý","ỳ","ỷ","ỹ","ỵ"
            };

            string[] arr2 = new string[] {
                "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "a", "a", "a", "a", "a","d","e","e","e","e","e","e","e","e",
                "e","e","e","i","i","i","i","i","o","o","o","o","o","o","o",
                "o","o","o","o","o","o","o","o","o","o","u","u","u","u","u",
                "u","u","u","u","u","u","y","y","y","y","y"
            };
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i]);
            }
            return text;
        }

        public ActionResult SearchUser(string keyword)
        {
            string sKeyword = RemoveUnicode(keyword);
            var listUsers = db.ThanhViens.Take(1);
            return PartialView(listUsers.OrderBy(user => user.MaTV));
        }

        public ActionResult ShowListProductsProcess()
        {
            var listProducts = db.ChiTietDonDatHangs.Where(product => product.DonDatHang.TinhTrangGiaoHang);
            return PartialView(listProducts);
        }

        public ActionResult ShowListProductsBestSell()
        {
            var listProducts = db.SanPhams.OrderByDescending(product => product.SoLanMua);
            return PartialView(listProducts);
        }

        public ActionResult ShowListProductsSelled()
        {
            var listProducts = db.SanPhams.Where(product => product.SoLanMua > 0).OrderBy(product => product.SoLanMua);
            return PartialView("~/Views/Admin/ShowListProductsBestSell.cshtml", listProducts);
        }
    }
}