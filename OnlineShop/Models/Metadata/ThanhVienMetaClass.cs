using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetaClass))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetaClass
        {
            public int MaTV { get; set; }

            [DisplayName("Tên đăng nhập")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            [StringLength(100, ErrorMessage = "{0} phải từ {2} kí tự đến {1} kí tự", MinimumLength = 5)]
            public string TaiKhoan { get; set; }


            [DisplayName("Mật khẩu")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "{0} phải chứa ít nhất 8 kí tự, bao gồm cả chữ cái và số !")]
            public string MatKhau { get; set; }

            

            [DisplayName("Họ và tên")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            public string HoTen { get; set; }

            [DisplayName("Địa chỉ")]
            public string DiaChi { get; set; }

            [DisplayName("Địa chỉ email")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            [StringLength(100, ErrorMessage = "{0} phải từ {2} kí tự đến {1} kí tự !", MinimumLength = 5)]
            [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Địa chỉ email không hợp lệ !")]
            public string Email { get; set; }

            [DisplayName("Số điện thoại")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            [StringLength(11, ErrorMessage = "{0} không quá {1} kí tự !")]
            public string SoDienThoai { get; set; }
            public string CauHoi { get; set; }
            public string CauTraLoi { get; set; }
            public Nullable<int> MaLoaiTV { get; set; }

        }
    }
}