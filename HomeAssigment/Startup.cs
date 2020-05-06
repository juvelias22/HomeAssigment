using HomeAssigment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(HomeAssigment.Startup))]
namespace HomeAssigment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndDefaultUsers();

            CreateQualiyTypes();



        }

        public void CreateQualiyTypes()
        {
            Quality q = new Quality();
            ApplicationDbContext db = new ApplicationDbContext();
            if (!db.Quality.Any())
            {
                q.QualityType = "Excellent";


                db.Quality.Add(q);

                db.SaveChanges();
                q.QualityType = "Good";
                db.Quality.Add(q);
                db.SaveChanges();
                q.QualityType = "Poor";
                db.Quality.Add(q);
                db.SaveChanges();
                q.QualityType = "Bad";
                db.Quality.Add(q);
                db.SaveChanges();
            }


        }


        private void createRolesAndDefaultUsers()
        {





            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                {
                    // check whether an Admin role already exists - if it does, do nothing
                    if (!roleManager.RoleExists("Admin"))
                    {

                        IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Admin";
                        roleManager.Create(role);
                    }

                    if (!roleManager.RoleExists("RegisteredUser"))
                    {

                        IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "RegisteredUser";
                        roleManager.Create(role);
                    }
                }

                // Now it is time to manage the users and assign roles to the user
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    // First, check if the admin user exists!
                    if (userManager.FindByName("administrator@yourEmailHost.com") == null)
                    {
                        // admin user does not exist - we can create it
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "administrator@yourEmailHost.com";
                        user.Email = "administrator@yourEmailHost.com";

                        string userPWD = "P@ssw0rd_1234";

                        IdentityResult chkUser = userManager.Create(user, userPWD);

                        //Add the admin user to the Admin role, if it was successfully created
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "Admin");

                            if (!chkRole.Succeeded)
                            {
                                // admin user was not assigned to role, something went wrong!
                                // Log this information and handle it
                                Console.Error.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
                                Console.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
                            }
                        }
                        else
                        {   // admin user was not created, something went wrong!
                            // Log this information and handle it
                            Console.Error.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
                            Console.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
                        }
                    }
                }
            }

        }

    }
}
