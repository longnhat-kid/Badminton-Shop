using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using PagedList;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        OnlineShopDBEntities db = new OnlineShopDBEntities();

        [ChildActionOnly]
        public ActionResult ProductPartial()
        {
            return PartialView();
        }

        public ActionResult LoadNewProduct(int? page)
        {
            ViewBag.isNewProduct = true;
            var listNewProduct = db.SanPhams.Where(product => product.Moi == true);
            int pageNumber = page ?? 1;
            int pageSize = 4;
            return PartialView("~/Views/Product/ProductPartial.cshtml", listNewProduct.OrderBy(product => product.MaSP).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LoadOldProduct(int? page)
        {
            ViewBag.isNewProduct = false;
            var listOldProduct = db.SanPhams.Where(product => product.Moi == false);
            int pageNumber = page ?? 1;
            int pageSize = 4;
            return PartialView("~/Views/Product/ProductPartial.cshtml", listOldProduct.OrderBy(product => product.MaSP).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult ProductDetail(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham productRes = db.SanPhams.Where(product => product.MaSP == id && !product.DaXoa).FirstOrDefault();

            if (productRes == null)
            {
                return HttpNotFound();
            }

            return View(productRes);
        }

        public ActionResult LoadProducts(int maNSX, int maLoaiSP)
        {
            if (maNSX == null || maLoaiSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listProductsRes = db.SanPhams.Where(product => product.MaNSX == maNSX && product.MaLoaiSP == maLoaiSP);
            if (listProductsRes.Count() == 0)
            {
                return HttpNotFound();
            }
            ViewBag.maNSX = maNSX;
            ViewBag.maLoaiSP = maLoaiSP;
            return View(listProductsRes.OrderBy(product => product.MaSP));
        }

        public ActionResult SortLoadProduct(string sortBy, int maNSX, int maLoaiSP)
        {
            var listProductSorted = new List<SanPham>();
            var listProductsRes = db.SanPhams.Where(product => product.MaNSX == maNSX && product.MaLoaiSP == maLoaiSP);
            switch (sortBy)
            {
                case "1":
                    listProductSorted = listProductsRes.OrderBy(product => product.TenSP).ToList();
                    break;
                case "2":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.TenSP).ToList();
                    break;
                case "3":
                    listProductSorted = listProductsRes.OrderBy(product => product.DonGia).ToList();
                    break;
                case "4":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.DonGia).ToList();
                    break;
                case "5":
                    listProductSorted = listProductsRes.Where(product => product.DonGia < 1000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "6":
                    listProductSorted = listProductsRes.Where(product => product.DonGia >= 1000000 && product.DonGia <= 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "7":
                    listProductSorted = listProductsRes.Where(product => product.DonGia > 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                default:
                    break;
            }
            return PartialView("~/Views/Product/SortSearchResult.cshtml", listProductSorted);
        }

        public ActionResult LoadProductsByMaLoaiSP(int maLoaiSP, int? page)
        {
            if (maLoaiSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listProductsRes = db.SanPhams.Where(product => product.MaLoaiSP == maLoaiSP);
            if (listProductsRes.Count() == 0)
            {
                return HttpNotFound();
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.maLoaiSP = maLoaiSP;
            return View(listProductsRes.OrderBy(product => product.MaSP).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SortLoadProductByMaLoaiSP(string sortBy, int maLoaiSP)
        {
            var listProductSorted = new List<SanPham>();
            var listProductsRes = db.SanPhams.Where(product => product.MaLoaiSP == maLoaiSP);
            switch (sortBy)
            {
                case "1":
                    listProductSorted = listProductsRes.OrderBy(product => product.TenSP).ToList();
                    break;
                case "2":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.TenSP).ToList();
                    break;
                case "3":
                    listProductSorted = listProductsRes.OrderBy(product => product.DonGia).ToList();
                    break;
                case "4":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.DonGia).ToList();
                    break;
                case "5":
                    listProductSorted = listProductsRes.Where(product => product.DonGia < 1000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "6":
                    listProductSorted = listProductsRes.Where(product => product.DonGia >= 1000000 && product.DonGia <= 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "7":
                    listProductSorted = listProductsRes.Where(product => product.DonGia > 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                default:
                    break;
            }
            return PartialView("~/Views/Product/SortSearchResult.cshtml", listProductSorted);
        }

        public ActionResult LoadRelatedProducts(int maLoaiSP, int maNSX)
        {
            var listRelatedProducts = db.SanPhams.Where(product => product.MaLoaiSP == maLoaiSP || product.MaNSX == maNSX);
            return PartialView(listRelatedProducts);
        }

        public ActionResult LoadProductsByMaNSX(int maNSX)
        {
            if (maNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listProductsRes = db.SanPhams.Where(product => product.MaNSX == maNSX);
            if (listProductsRes.Count() == 0)
            {
                return HttpNotFound();
            }
            ViewBag.maNSX = maNSX;
            return View(listProductsRes.OrderBy(product => product.MaSP));
        }

        public ActionResult SortLoadProductByMaNSX(string sortBy, int maNSX)
        {
            var listProductSorted = new List<SanPham>();
            var listProductsRes = db.SanPhams.Where(product => product.MaNSX == maNSX);
            switch (sortBy)
            {
                case "1":
                    listProductSorted = listProductsRes.OrderBy(product => product.TenSP).ToList();
                    break;
                case "2":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.TenSP).ToList();
                    break;
                case "3":
                    listProductSorted = listProductsRes.OrderBy(product => product.DonGia).ToList();
                    break;
                case "4":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.DonGia).ToList();
                    break;
                case "5":
                    listProductSorted = listProductsRes.Where(product => product.DonGia < 1000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "6":
                    listProductSorted = listProductsRes.Where(product => product.DonGia >= 1000000 && product.DonGia <= 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "7":
                    listProductSorted = listProductsRes.Where(product => product.DonGia > 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                default:
                    break;
            }
            return PartialView("~/Views/Product/SortSearchResult.cshtml", listProductSorted);
        }

        public ActionResult LoadProducers()
        {
            var listProducts = db.SanPhams;
            return PartialView(listProducts);
        }

        public ActionResult LoadCategorys()
        {
            var listProducts = db.SanPhams;
            return PartialView(listProducts);
        }

        public ActionResult LoadRandomProduct()
        {
            var listProducts = db.SanPhams;
            return PartialView(listProducts);
        }

        public ActionResult LoadBestSellerProducts()
        {
            var listProducts = db.SanPhams.OrderByDescending(product => product.SoLanMua).Take(3);
            return PartialView(listProducts);
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

        [HttpGet]
        public ActionResult SearchResult(string sKeyword)
        {
            var listProductsRes = db.SanPhams.Where(product => product.TenSP.Contains(sKeyword));
            ViewBag.sKeyword = sKeyword;
            return View(listProductsRes.OrderBy(product => product.TenSP));
        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            string sKeyword = RemoveUnicode(keyword);
            return RedirectToAction("SearchResult", new { @sKeyword = sKeyword });
        }

        public ActionResult SortSearchResult(string sortBy, string sKeyword)
        {
            var listProductSorted = new List<SanPham>();
            var listProductsRes = db.SanPhams.Where(product => product.TenSP.Contains(sKeyword));
            switch (sortBy)
            {
                case "1":
                    listProductSorted = listProductsRes.OrderBy(product => product.TenSP).ToList();
                    break;
                case "2":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.TenSP).ToList();
                    break;
                case "3":
                    listProductSorted = listProductsRes.OrderBy(product => product.DonGia).ToList();
                    break;
                case "4":
                    listProductSorted = listProductsRes.OrderByDescending(product => product.DonGia).ToList();
                    break;
                case "5":
                    listProductSorted = listProductsRes.Where(product => product.DonGia < 1000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "6":
                    listProductSorted = listProductsRes.Where(product => product.DonGia >= 1000000 && product.DonGia <= 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                case "7":
                    listProductSorted = listProductsRes.Where(product => product.DonGia > 2000000).OrderBy(product => product.DonGia).ToList();
                    break;
                default:
                    break;
            }
            return PartialView("~/Views/Product/SortSearchResult.cshtml", listProductSorted);
        }
    }
}