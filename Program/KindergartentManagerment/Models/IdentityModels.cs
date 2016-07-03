using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KindergartentManagerment.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ImageURL { get; set; }
        public string ImageURL1 { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public DbSet<DD_ChiTietMonAnMau> DD_ChiTietMonAnMau { get; set; }
        public DbSet<DD_LoaiMonAnMau> DD_LoaiMonAnMau { get; set; }
        public DbSet<DD_LoaiSuatAn> DD_LoaiSuatAn { get; set; }
        public DbSet<DD_MonAnMau> DD_MonAnMau { get; set; }
        public DbSet<DD_NhomThucPham> DD_NhomThucPham { get; set; }
        public DbSet<DD_NhuCauDinhDuong> DD_NhuCauDinhDuong { get; set; }
        public DbSet<DD_NhuCauNangLuong> DD_NhuCauNangLuong { get; set; }
        public DbSet<DD_SuatAn> DD_SuatAn { get; set; }
        public DbSet<DD_SuatAnChiTiet> DD_SuatAnChiTiet { get; set; }
        public DbSet<DD_ThucDonNgay> DD_ThucDonNgay { get; set; }
        public DbSet<DD_ThucPham> DD_ThucPham { get; set; }
        public DbSet<DM_DEPARTMENTINFO> DM_DEPARTMENTINFO { get; set; }
        public DbSet<FM_FOODREPOSITORY> FM_FOODREPOSITORY { get; set; }
        public DbSet<GM_CLASSINFO> GM_CLASSINFO { get; set; }
        public DbSet<GM_GRADEINFO> GM_GRADEINFO { get; set; }
        public DbSet<KM_PHYSICALINFO> KM_PHYSICALINFO { get; set; }
        public DbSet<KM_STANDARDINDEXES> KM_STANDARDINDEXES { get; set; }
        public DbSet<KM_STUDENTOVERVIEW> KM_STUDENTOVERVIEW { get; set; }
        public DbSet<REM_IN> REM_IN { get; set; }
        public DbSet<REM_INOUT> REM_INOUT { get; set; }
        public DbSet<REM_OUT> REM_OUT { get; set; }
        public DbSet<REM_OUT_DETAIL> REM_OUT_DETAIL { get; set; }
        public DbSet<SCHOOLYEAR> SCHOOLYEARs { get; set; }
        public DbSet<SM_SALARY> SM_SALARY { get; set; }
        public DbSet<SM_STAFFINFO> SM_STAFFINFO { get; set; }
        public DbSet<SUPPLIERDETAIL> SUPPLIERDETAILs { get; set; }
        public DbSet<SYS_AUTH_STATUS> SYS_AUTH_STATUS { get; set; }
        public DbSet<SYS_EVENT> SYS_EVENT { get; set; }
        public DbSet<SYS_GROUP> SYS_GROUP { get; set; }
        public DbSet<TM_LESSONPROGRAM> TM_LESSONPROGRAM { get; set; }
        public DbSet<TM_TEACHSCHEDULE> TM_TEACHSCHEDULE { get; set; }
        public DbSet<TM_TOPIC> TM_TOPIC { get; set; }
        public DbSet<UM_ROLEDELIVERY> UM_ROLEDELIVERY { get; set; }
        public DbSet<UM_USERINFO> UM_USERINFO { get; set; }
    }
}