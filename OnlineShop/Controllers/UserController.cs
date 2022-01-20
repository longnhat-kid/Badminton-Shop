using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        OnlineShopDBEntities db = new OnlineShopDBEntities();

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.CauHoi = new SelectList(LoadSecretQuestions());
            return View();
        }

        [HttpPost]
        public ActionResult Register(ThanhVien newUser)
        {
            ViewBag.CauHoi = new SelectList(LoadSecretQuestions());
            if (this.IsCaptchaValid("Captcha is not valid") && ModelState.IsValid)
            {
                bool isExist = db.ThanhViens.SingleOrDefault(user => user.TaiKhoan == newUser.TaiKhoan) == null ? false : true;
                if (isExist)
                {
                    ViewBag.Message = "Tên đăng nhập đã tồn tại, vui lòng chọn tên đăng nhập khác !!";
                }
                else
                {
                    ViewBag.Message = "Đăng kí tài khoản thành công !!";
                    newUser.MaLoaiTV = 1;
                    db.ThanhViens.Add(newUser);
                    db.SaveChanges();
                    ModelState.Clear();
                }
                return View();
            }
            else
            {
                ViewBag.Message = "Đăng kí tài khoản thất bại, vui lòng kiếm tra lại !!";
                return View();
            }
        }

        private List<String> LoadSecretQuestions()
        {
            List<String> listSecretQuestions = new List<string>() {
                "Cầu thủ bóng đá bạn yêu thích là gì ?",
                "Ca sĩ bạn yêu thích là gì ?",
                "Người yêu cũ bạn thương yêu nhất là ai ?",
                "Bạn chọn tình yêu hay sự nghiệp ?",
                "Tâm sự...",
            };

            return listSecretQuestions;
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string username = f["username"].ToString();
            string password = f["password"].ToString();
            ThanhVien userRes = db.ThanhViens.SingleOrDefault(user => user.TaiKhoan == username && user.MatKhau == password);
            if (userRes != null)
            {
                Session["User"] = userRes;
                return Content("<script>document.location.href='/';</script>");
            }
            return Content("Tên đăng nhập hoặc mật khẩu không đúng !!");
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SubmitComment(int maSP, FormCollection f) {

            if (Session["User"] == null)
            {
                return Content(@"<div class=""alert alert-danger alert-dismissible""><button type = ""button"" class=""close"" data-dismiss=""alert"" style=""top: 0;"">&times;</button><strong>Không thành công!</strong> Quý khách vui lòng <a href="""" data-toggle=""modal"" data-target=""#loginModal"">Đăng nhập</a> hoặc <a href=""/User/Register"">tạo tài khoản</a> để gửi bình luận về sản phẩm.</div><script type=""text/javascript"">$("".alert"").first().hide().slideDown(500).delay(4000).slideUp(""slow"")</script>");
            }
            else
            {
                ThanhVien userCurrent = Session["User"] as ThanhVien;
                BinhLuan newComment = new BinhLuan(userCurrent.MaTV, maSP, f["comment"].ToString());
                db.BinhLuans.Add(newComment);
                db.SaveChanges();
                return Content(@"<div class=""alert alert-success alert-dismissible""><button type = ""button"" class=""close"" data-dismiss=""alert"" style=""top: 0;"">&times;</button><strong>Thêm thành công!</strong> Quý khách vui lòng xem chi tiết về bình luận sản phẩm ở bên dưới !");
            }
        }

        public ActionResult UserAccount()
        {
            return View();
        }

        public ActionResult LoadProductsProcessByUserId()
        {
            ThanhVien user = Session["User"] as ThanhVien;
            var listProductsProcess = db.SanPhamGioHangs.Where(product => product.GioHang.MaTV == user.MaTV);
            return PartialView(listProductsProcess);
        }

        public ActionResult LoadProductsOrderedByUserId()
        {
            ThanhVien user = Session["User"] as ThanhVien;
            var listProductsProcess = db.ChiTietDonDatHangs.Where(product => product.DonDatHang.MaTV == user.MaTV);
            return PartialView(listProductsProcess);
        }

        public ActionResult LoadLikeListByUserId()
        {
            ThanhVien user = Session["User"] as ThanhVien;
            var listProducts = db.BoSuuTaps.Where(product => product.MaTV == user.MaTV);
            return PartialView(listProducts);
        }

        public ActionResult AddToLikeList(int maSP)
        {
            if (Session["User"] == null)
            {
                return Content(@"<div class=""alert alert-danger alert-dismissible""><button type = ""button"" class=""close"" data-dismiss=""alert"" style=""top: 0;"">&times;</button><strong>Không thành công!</strong> Quý khách vui lòng <a href="""" data-toggle=""modal"" data-target=""#loginModal"">Đăng nhập</a> hoặc <a href=""/User/Register"">tạo tài khoản</a> để thêm sản phẩm vào bộ sưu tập.</div><script type=""text/javascript"">$("".alert"").first().hide().slideDown(500).delay(4000).slideUp(""slow"")</script>");
            }
            else
            {
                int maTV = (Session["User"] as ThanhVien).MaTV;
                BoSuuTap newProduct = new BoSuuTap(maTV, maSP);
                if(db.BoSuuTaps.SingleOrDefault(product => product.MaSP == maSP && product.MaTV == maTV) == null)
                {
                    db.BoSuuTaps.Add(newProduct);
                    db.SaveChanges();
                }
                return Content(@"<div class=""alert alert-success alert-dismissible""><button type = ""button"" class=""close"" data-dismiss=""alert"" style=""top: 0;"">&times;</button><strong>Thêm thành công!</strong> Quý khách vui lòng xem lại chi tiết <a href=""/User/UserAccount"">Bộ sưu tập</a></div><script type=""text/javascript"">$("".alert"").first().hide().slideDown(500).delay(4000).slideUp(""slow"")</script>");

            }
        }

        public ActionResult RemoveItemLikeList(BoSuuTap item)
        {
            db.BoSuuTaps.Remove(db.BoSuuTaps.Single(product => product.MaSP == item.MaSP && product.MaTV == item.MaTV));
            db.SaveChanges();
            return View("~/Views/User/UserAccount.cshtml");
        }
    }
}