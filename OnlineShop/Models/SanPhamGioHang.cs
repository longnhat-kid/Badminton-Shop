//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class SanPhamGioHang
    {

        public SanPhamGioHang(int maGH, int maSP, int soLuong)
        {
            using(OnlineShopDBEntities db = new OnlineShopDBEntities())
            {
                SanPham product = db.SanPhams.Single(item => item.MaSP == maSP);
                this.MaGioHang = maGH;
                this.MaSP = maSP;
                this.SoLuong = soLuong;
                this.TenSP = product.TenSP;
                this.DonGia = product.DonGia;
                this.HinhAnh = product.HinhAnh;
            }
            
        }

        public SanPhamGioHang()
        {

        }
        public int MaSPGH { get; set; }
        public int MaGioHang { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public string HinhAnh { get; set; }
    
        public virtual GioHang GioHang { get; set; }
    }
}
