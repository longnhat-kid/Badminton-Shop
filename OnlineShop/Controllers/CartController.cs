using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        OnlineShopDBEntities db = new OnlineShopDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            ThanhVien user = Session["User"] as ThanhVien;
            List<SanPhamGioHang> listProducts = db.SanPhamGioHangs.Where(product => product.GioHang.MaTV == user.MaTV && product.MaGioHang == product.GioHang.MaGioHang).ToList();
            if(Session["Discount"] == null)
            {
                Session["Discount"] = 10;
            }
            return View(listProducts);
        }

        public GioHang GetGioHang(int maTV)
        {
            GioHang userCart = db.GioHangs.SingleOrDefault(cart => cart.MaTV == maTV);
            if(userCart == null)
            {
                userCart = new GioHang(maTV);
                db.GioHangs.Add(userCart);
                db.SaveChanges();
            }
            return userCart;
        }

        public List<SanPhamGioHang> GetSanPhamGioHang(int maGioHang)
        {
            List<SanPhamGioHang> listSanPham = db.SanPhamGioHangs.Where(product => product.MaGioHang == maGioHang).ToList();
            return listSanPham;
        }

        private void UpdateProduct(int maSP)
        {
            var productUpdate = db.SanPhams.Single(product => product.MaSP == maSP);
            productUpdate.LuotXem++;
            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult AddToCart(int maSP, FormCollection f)
        {
            if(Session["User"] == null)
            {
                return Content(@"<div class=""alert alert-danger alert-dismissible""><button type = ""button"" class=""close"" data-dismiss=""alert"" style=""top: 0;"">&times;</button><strong>Không thành công!</strong> Quý khách vui lòng <a href="""" data-toggle=""modal"" data-target=""#loginModal"">Đăng nhập</a> hoặc <a href=""/User/Register"">tạo tài khoản</a> để thêm sản phẩm vào giỏ hàng.</div><script type=""text/javascript"">$("".alert"").first().hide().slideDown(500).delay(4000).slideUp(""slow"")</script>");
            }
            else
            {
                int maTV = (Session["User"] as ThanhVien).MaTV;
                int SoLuong = int.Parse(f["quantity"].ToString());
                GioHang userCart = GetGioHang(maTV);
                UpdateProduct(maSP);
                List<SanPhamGioHang> listProducts = GetSanPhamGioHang(userCart.MaGioHang);
                SanPhamGioHang newProduct = new SanPhamGioHang(userCart.MaGioHang, maSP, SoLuong);
                if(listProducts == null)
                {
                    db.SanPhamGioHangs.Add(newProduct);
                    db.SaveChanges();
                }
                else
                {
                    SanPhamGioHang productCheck = listProducts.SingleOrDefault(product => product.MaSP == maSP);
                    if (productCheck == null)
                    {
                        db.SanPhamGioHangs.Add(newProduct);
                        db.SaveChanges();
                    }
                    else
                    {
                        productCheck.SoLuong+= SoLuong;
                        db.SaveChanges();
                    }
                }
                
            }
            return Content(@"<div class=""alert alert-success alert-dismissible""><button type = ""button"" class=""close"" data-dismiss=""alert"" style=""top: 0;"">&times;</button><strong>Thêm thành công!</strong> Quý khách vui lòng xem lại chi tiết <a href=""/Cart/Index"">Giỏ hàng</a></div><script type=""text/javascript"">$("".alert"").first().hide().slideDown(500).delay(4000).slideUp(""slow"")</script>");
        }

        
        public ActionResult UpdateCart(SanPhamGioHang product, FormCollection f)
        {
            int maTV = (Session["User"] as ThanhVien).MaTV;
            bool isDelete = f["delete"] == "false" ? false : true;
            GioHang userCart = GetGioHang(maTV);
            List<SanPhamGioHang> listProducts = GetSanPhamGioHang(userCart.MaGioHang);
            SanPhamGioHang productCheck = listProducts.SingleOrDefault(item => item.MaSP == product.MaSP);
            if (isDelete)
            {
                db.SanPhamGioHangs.Remove(productCheck);
                db.SaveChanges();
            }
            else
            {
                productCheck.SoLuong = product.SoLuong;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetDiscount(string discount_string)
        {
            int discount = int.Parse(discount_string);
            Session["Discount"] = discount;
            return RedirectToAction("Index");
        }

        public ActionResult GetDiscountCode(string discount_code)
        {
            var codeDiscount = db.MaCodes.SingleOrDefault(code => code.Code == discount_code);
            if(codeDiscount != null)
            {
                Session["Discount"] = codeDiscount.UuDai;
            }
            else
            {
                Session["Discount"] = 0;
            }            
            return RedirectToAction("Index");
        }
    }
}