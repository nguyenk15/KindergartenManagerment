using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KindergartentManagerment.Models
{
    public class IngredientViewModel
    {
        public DD_ThucPham DD_ThucPhamModel { get; set; }
        public DD_NhomThucPham DD_NhomThucPhamModel { get; set; }       
        public int ThucPhamID
        {
            get
            {
                return DD_ThucPhamModel.ThucPhamID;
            }
            set
            {
                DD_ThucPhamModel.ThucPhamID = value;
            }
        }
        public string TenThucPham
        {
            get
            {
                return DD_ThucPhamModel.TenThucPham;
            }
          set
            {
                DD_ThucPhamModel.TenThucPham = value;
            }
        }
        public string NhomThucPham
        {
            get
            {
                return DD_NhomThucPhamModel.TenNhomThucPham;
            }
            set
            {
                DD_NhomThucPhamModel.TenNhomThucPham = value;
            }
        }
        public bool NguonTuDongVat
        {
            get
            {
                return DD_ThucPhamModel.NguonTuDongVat.HasValue ? DD_ThucPhamModel.NguonTuDongVat.Value : false;
            }
            set
            {
                DD_ThucPhamModel.NguonTuDongVat = value;
            }
        }
        public string QuyDoiVeKg
        {
            get
            {
                return DD_ThucPhamModel.QuyDoiVeKg;
            }
            set
            {
                DD_ThucPhamModel.QuyDoiVeKg = value;
            }
        }
        public string DonViTinh
        {
            get
            {
                return DD_ThucPhamModel.DonViTinh;
            }
            set
            {
                DD_ThucPhamModel.DonViTinh = value;
            }
        }
        public double GiaThanh
        {
            get
            {
                return DD_ThucPhamModel.GiaThanh.HasValue ? DD_ThucPhamModel.GiaThanh.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.GiaThanh = value;
            }
        }
        public double TyLeHapThu
        {
            get
            {
                return DD_ThucPhamModel.TyLeHapThu.HasValue ? DD_ThucPhamModel.TyLeHapThu.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TyLeHapThu = value;
            }
        }
        public double TyLeThai
        {
            get
            {
                return DD_ThucPhamModel.TyLeThai.HasValue ? DD_ThucPhamModel.TyLeThai.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TyLeThai = value;
            }
        }
        public double NangLuongCalo
        {
            get
            {
                return DD_ThucPhamModel.NangLuongCalo.HasValue ? DD_ThucPhamModel.NangLuongCalo.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.NangLuongCalo = value;
            }
        }
        public double TphhNuoc
        {
            get
            {
                return DD_ThucPhamModel.TphhNuoc.HasValue ? DD_ThucPhamModel.TphhNuoc.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TphhNuoc = value;
            }
        }
        public double TphhLipid
        {
            get
            {
                return DD_ThucPhamModel.TphhLipid.HasValue ? DD_ThucPhamModel.TphhLipid.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TphhLipid = value;
            }
        }
        public double TphhProtid
        {
            get
            {
                return DD_ThucPhamModel.TphhProtid.HasValue ? DD_ThucPhamModel.TphhProtid.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TphhProtid = value;
            }
        }
        public double TphhGlucid
        {
            get
            {
                return DD_ThucPhamModel.TphhGlucid.HasValue ? DD_ThucPhamModel.TphhGlucid.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TphhGlucid = value;
            }
        }
        public double TphhCellulose
        {
            get
            {
                return DD_ThucPhamModel.TphhCellulose.HasValue ? DD_ThucPhamModel.TphhCellulose.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TphhCellulose = value;
            }
        }
        public double TphhCholesterol
        {
            get
            {
                return DD_ThucPhamModel.TphhCholesterol.HasValue ? DD_ThucPhamModel.TphhCholesterol.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.TphhCholesterol = value;
            }
        }
        public double MkCalci
        {
            get
            {
                return DD_ThucPhamModel.MkCalci.HasValue ? DD_ThucPhamModel.MkCalci.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.MkCalci = value;
            }
        }
        public double MkPhotpho
        {
            get
            {
                return DD_ThucPhamModel.MkPhotpho.HasValue ? DD_ThucPhamModel.MkPhotpho.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.MkPhotpho = value;
            }
        }
        public double MkSat
        {
            get
            {
                return DD_ThucPhamModel.MkSat.HasValue ? DD_ThucPhamModel.MkSat.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.MkSat = value;
            }
        }
        public double VitaminCaroten
        {
            get
            {
                return DD_ThucPhamModel.VitaminCaroten.HasValue ? DD_ThucPhamModel.VitaminCaroten.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.VitaminCaroten = value;
            }
        }
        public double VitaminA
        {
            get
            {
                return DD_ThucPhamModel.VitaminA.HasValue ? DD_ThucPhamModel.VitaminA.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.VitaminA = value;
            }
        }
        public double VitaminB1
        {
            get
            {
                return DD_ThucPhamModel.VitaminB1.HasValue ? DD_ThucPhamModel.VitaminB1.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.VitaminB1 = value;
            }
        }
        public double VitaminB2
        {
            get
            {
                return DD_ThucPhamModel.VitaminB2.HasValue ? DD_ThucPhamModel.VitaminB2.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.VitaminB2 = value;
            }
        }
        public double VitaminC
        {
            get
            {
                return DD_ThucPhamModel.VitaminC.HasValue ? DD_ThucPhamModel.VitaminC.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.VitaminC = value;
            }
        }
        public double VitaminPP
        {
            get
            {
                return DD_ThucPhamModel.VitaminPP.HasValue ? DD_ThucPhamModel.VitaminPP.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.VitaminPP = value;
            }
        }
        public string AuthStatus
        {
            get
            {
                return DD_ThucPhamModel.Auth_Status;//.HasValue ? DD_ThucPhamModel.VitaminPP.Value : 0;
            }
            set
            {
                DD_ThucPhamModel.Auth_Status = value;
            }
        }
        public IngredientViewModel()
        {
            DD_ThucPhamModel = new DD_ThucPham();
            DD_NhomThucPhamModel = new DD_NhomThucPham();
        }
    }
}