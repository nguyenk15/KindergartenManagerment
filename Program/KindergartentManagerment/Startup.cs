using KindergartentManagerment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(KindergartentManagerment.Startup))]
namespace KindergartentManagerment
{
    public partial class Startup
    {
        //private KindergartentManagermentdb db = new KindergartentManagermentdb();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }


        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            
            

            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {
                var auth = new SYS_AUTH_STATUS();
                auth.AUTH_STATUS_NAME = "Đã duyệt";
                auth.AUTH_STATUS = "A";
                context.SYS_AUTH_STATUS.Add(auth);
                var auth1 = new SYS_AUTH_STATUS();
                auth1.AUTH_STATUS_NAME = "Chờ duyệt";
                auth1.AUTH_STATUS = "U";
                context.SYS_AUTH_STATUS.Add(auth1);
                context.SaveChanges();

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "nguyen.nah76@gmail.com";
                user.ImageURL1 = "/Content/themes/AdminLTE/img/" + "user2-160x160" + ".jpg";
                string userPWD = "@Admin123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role 
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role 
            if (!roleManager.RoleExists("Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Member";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Guest"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Guest";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "guest";
                user.Email = "nguyen.nah76@gmail.com";
                user.ImageURL1 = "/Content/profile/" + "user_160x160" + ".jpg";
                user.ImageURL1 = "/Content/profile/" + "user_128x128" + ".png";
                string userPWD = "@Guest123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Guest");
                }
            }
        }
    }
}