using HomeAssigment.Controllers;
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
            CreateTests();


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

        public void CreateTests()
        {
            Categories c= new Categories();
            ItemType it = new ItemType();
            Items i = new Items();
            ApplicationDbContext db = new ApplicationDbContext();
         

            if (!db.Categories.Any())
            {
                c.Category = "Cars";           
                db.Categories.Add(c);
                db.SaveChanges();

                c.Category = "Phones";
                db.Categories.Add(c);
                db.SaveChanges();

                c.Category = "Laptops";
                db.Categories.Add(c);
                db.SaveChanges();

                c.Category = "Boats";
                db.Categories.Add(c);
                db.SaveChanges();

                c.Category = "Cameras";
                db.Categories.Add(c);
                db.SaveChanges();

            }


            if (!db.ItemTypes.Any())
            {

                for (int x = 1; x < 17; x++)
                {
                    it.CategoryId = 1;
                    it.ItemName = "Tesla Model " + x.ToString();
                    db.ItemTypes.Add(it);
                    db.SaveChanges();
                }

                it.CategoryId = 2;
                it.ItemName = "Xiaomi";
                db.ItemTypes.Add(it);
                db.SaveChanges();

                it.CategoryId = 3;
                it.ItemName = "msi";
                db.ItemTypes.Add(it);
                db.SaveChanges();

                it.CategoryId = 4;
                it.ItemName = "Titanic";
                db.ItemTypes.Add(it);
                db.SaveChanges();

                it.CategoryId = 5;
                it.ItemName = "Cannon";
                db.ItemTypes.Add(it);
                db.SaveChanges();


            }


            if (!db.Items.Any())
            {

                for (int x = 1; x < 26; x++)
                {
                  
                    i.ItemTypeId = 1;
                    i.ItemOwner = "test1@mail.com";
                    i.QualityId = 1;
                    i.ItemQuantity = x+1;
                    i.ItemPrice = x + 100.99;
                    i.ItemDate =  DateTime.Now.AddMinutes(x);
                    db.Items.Add(i);
                    db.SaveChanges();

                    i.ItemTypeId = 18;
                    i.ItemOwner = "test2@mail.com";
                    i.QualityId = 4;
                    i.ItemQuantity = x+4;
                    i.ItemPrice = x + 25.99;
                    i.ItemDate = DateTime.Now.AddMinutes(x);
                    db.Items.Add(i);
                    db.SaveChanges();


                    i.ItemTypeId = 19;
                    i.ItemOwner = "test3@mail.com";
                    i.QualityId = 2;
                    i.ItemQuantity = x+5;
                    i.ItemPrice = x + 85.99;
                    i.ItemDate = DateTime.Now.AddMinutes(x);
                    db.Items.Add(i);
                    db.SaveChanges();


                    i.ItemTypeId = 20;
                    i.ItemOwner = "test4@mail.com";
                    i.QualityId = 1;
                    i.ItemQuantity = x+10;
                    i.ItemPrice = x + 73.99;
                    i.ItemDate = DateTime.Now.AddMinutes(x);
                    db.Items.Add(i);
                    db.SaveChanges();

                }





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

                    if (userManager.FindByName("test1@mail.com") == null)
                    {
                        // admin user does not exist - we can create it
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "test1@mail.com";
                        user.Email = "test1@mail.com";

                        string userPWD = "Password_1234";

                        IdentityResult chkUser = userManager.Create(user, userPWD);

                        //Add the admin user to the Admin role, if it was successfully created
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "RegisteredUser");


                        }


                    }

                    if (userManager.FindByName("test2@mail.com") == null)
                        {
                            // admin user does not exist - we can create it
                            ApplicationUser user = new ApplicationUser();
                            user.UserName = "test2@mail.com";
                            user.Email = "test2@mail.com";

                            string userPWD = "Password_1234";

                            IdentityResult chkUser = userManager.Create(user, userPWD);

                            //Add the admin user to the Admin role, if it was successfully created
                            if (chkUser.Succeeded)
                            {
                                IdentityResult chkRole = userManager.AddToRole(user.Id, "RegisteredUser");


                            }
                          
                        
                        }

                    if (userManager.FindByName("test3@mail.com") == null)
                    {
                        // admin user does not exist - we can create it
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "test3@mail.com";
                        user.Email = "test3@mail.com";

                        string userPWD = "Password_1234";

                        IdentityResult chkUser = userManager.Create(user, userPWD);

                        //Add the admin user to the Admin role, if it was successfully created
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "RegisteredUser");

                          
                        }
                       
                    }

                    if (userManager.FindByName("test4@mail.com") == null)
                    {
                        // admin user does not exist - we can create it
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "test4@mail.com";
                        user.Email = "test4@mail.com";

                        string userPWD = "Password_1234";

                        IdentityResult chkUser = userManager.Create(user, userPWD);

                        //Add the admin user to the Admin role, if it was successfully created
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "RegisteredUser");

                           
                        }
                       
                    }



                    if (userManager.FindByName("test5@mail.com") == null)
                    {
                        // admin user does not exist - we can create it
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "test5@mail.com";
                        user.Email = "test5@mail.com";

                        string userPWD = "Password_1234";

                        IdentityResult chkUser = userManager.Create(user, userPWD);

                        //Add the admin user to the Admin role, if it was successfully created
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "RegisteredUser");

                        }
                        
                    }


                }
            }

        }

    }
}
