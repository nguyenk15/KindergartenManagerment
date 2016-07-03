using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindergartentManagerment.Models
{
    public class DD_ThucPham
    {
        [Key]
        public int ThucPhamID { get; set; }
        public int NhomThucPhamID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "TenThucPham cannot be longer than 50 characters.")]
        public string TenThucPham { get; set; }
        public Nullable<bool> NguonTuDongVat { get; set; }

        [StringLength(10, ErrorMessage = "QuyDoiVeKg cannot be longer than 10 characters.")]
        public string QuyDoiVeKg { get; set; }

        [StringLength(10, ErrorMessage = "DonViTinh cannot be longer than 10 characters.")]
        public string DonViTinh { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<double> GiaThanh { get; set; }
        [Range(0,1)]
        public Nullable<double> TyLeHapThu { get; set; }
        [Range(0, 1)]
        public Nullable<double> TyLeThai { get; set; }
        public Nullable<double> NangLuongCalo { get; set; }
        public Nullable<double> TphhNuoc { get; set; }
        public Nullable<double> TphhProtid { get; set; }
        public Nullable<double> TphhLipid { get; set; }
        public Nullable<double> TphhGlucid { get; set; }
        public Nullable<double> TphhCellulose { get; set; }
        public Nullable<double> TphhCholesterol { get; set; }
        public Nullable<double> MkCalci { get; set; }
        public Nullable<double> MkPhotpho { get; set; }
        public Nullable<double> MkSat { get; set; }
        public Nullable<double> VitaminCaroten { get; set; }
        public Nullable<double> VitaminA { get; set; }
        public Nullable<double> VitaminB1 { get; set; }
        public Nullable<double> VitaminB2 { get; set; }
        public Nullable<double> VitaminC { get; set; }
        public Nullable<double> VitaminPP { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string Notes { get; set; }
        public string Record_Status { get; set; }
        public string Maker_ID { get; set; }
        public Nullable<System.DateTime> Create_DT { get; set; }
        public string Auth_Status { get; set; }
        public string Checker_ID { get; set; }
        public Nullable<System.DateTime> Approve_DT { get; set; }

        [ForeignKey("NhomThucPhamID")]
        public virtual DD_NhomThucPham NhomThucPham { get; set; }
        //public virtual SYS_AUTH_STATUS AuthStatus { get; set; }
    }
}