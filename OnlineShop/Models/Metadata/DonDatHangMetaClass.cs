using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [MetadataTypeAttribute(typeof(DonDatHangMetaClass))]
    public partial class DonDatHang
    {
        internal sealed class DonDatHangMetaClass
        {
            [DisplayName("Địa chỉ nhận hàng")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            public string DiaChiNhanHang { get; set; }

            [DisplayName("Địa chỉ phụ")]
            [Required(ErrorMessage = "{0} không được bỏ trống !")]
            public string DiaChiPhu { get; set; }
        }
    }
}