using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Identity;

namespace HBOnlineTyresApp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var servicesScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicesScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Manufacturers
                if (!context.Manufacturers.Any())
                {
                    context.Manufacturers.AddRange(new List<Manufacturer>()
                    {
                        new Manufacturer()
                        {
                            Name = "Maxxis",
                            LogoURL = "https://drive.google.com/uc?id=127JsQbJqZbG0LKmmSOSM3_GwgBJ9h2h0",
                            Description = "Rest"
                        },
                        new Manufacturer()
                        {
                            Name = "hankook",
                            LogoURL = "https://drive.google.com/uc?id=1xr-xP1oLrtzVgQO7iI26ExoWbA5SYeUa",
                            Description = "Rest"
                        },
                        new Manufacturer()
                        {
                            Name = "Michelin",
                            LogoURL = "https://drive.google.com/uc?id=1mzhnLUhR8pPgKZf-dSLIasKug2qlC7Cu",
                            Description = "Rest"
                        },
                        new Manufacturer()
                        {
                            Name = "Good Year",
                            LogoURL = "https://drive.google.com/uc?id=1-ApNNcAUxreWNZ7772jW8BFZDrcbmTWu",
                            Description = "Rest"

                        },

                    });
                    context.SaveChanges();
                }
                //Categories
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Passenger Car/Crossover",
                      
                        },
                        new Category()
                        {
                            Name = "Light Truck/SUV",
                          
                        },
                        new Category()
                        {
                            Name = "Extreme Off Road",
                       
                        },
                        new Category()
                        {
                            Name = "Compitition",
                     
                        },

                    });
                    context.SaveChanges();
                }


                //Tyres
                if (!context.Tyres.Any())
                {
                    context.Tyres.AddRange(new List<Tyre>()
                    {
                        new Tyre()
                        {
                            Name= "Bravo HP M3",
                            CategoryId = 1,
                            Description = "An all-season premium touring tire with passenger car, crossover" +
                            " and SUV fitments, the Bravo HP-M3 was designed as an all-around performer in dry " +
                            "and wet conditions as well as light snow. The HP-M3 features cross-hatch sipes across the " +
                            "tread for added traction on slippery surfaces, while the four circumferential grooves improve " +
                            "hydroplaning resistance ",
                            ImageURL = "https://drive.google.com/uc?id=1v9BE4fkwwx6quz0Z0TlbBrODeCG9R1MY",
                            ManufacturerId = 1
                        },
                        new Tyre()
                        {
                            Name= "Defender T Plus H",
                              CategoryId = 1,
                            Description = "More miles. With the safety you expect. Designed for the doers, " +
                            "Michelin's longest-lasting tire keeps you on the go longer because life never " +
                            "stops moving. Ultimate Tread life, Safety Quiet & Comfortable Ride",
                            ImageURL = "https://drive.google.com/uc?id=1nXSPnNDmhygmw6mUGKjcrelY5UiWv4-x",
                            ManufacturerId = 3
                        },
                        new Tyre()
                        {
                            Name= "EfficientGrip Performance",
                              CategoryId = 2,
                            Description = "Efficientgrip Performance is ideal for anyone looking for performance, efficiency and control." +
                            "Tread compound developed to optimize grip on dry and wet floors." +
                            "ActiveBraking technology providing greater contact of the tire with the ground in the brakes.",
                            ImageURL = "https://drive.google.com/uc?id=1voGo-MCMXO3QuLldZtB5d7KsxudI4BLn",
                            ManufacturerId = 4

                        },
                        new Tyre()
                        {
                            Name= "Dynapro MT2 RT05",
                              CategoryId = 3,
                            Description = "The All-terrain tire designed for the drivers of pickup trucks, full-size SUVs, " +
                            "Jeeps and other capable all-terrain vehicles that demand off-road traction, but also require " +
                            "reliable on-road performance.",
                            ImageURL = "https://drive.google.com/uc?id=1cQrlGjIE3sGA_talNoieCIniaovRC67m",
                            ManufacturerId = 2
                        }
                    });
                    context.SaveChanges();

                }

                //Specifications
                if (!context.Specifications.Any())
                {
                    context.Specifications.AddRange(new List<Specification>()
                    {
                        new Specification()
                        {
                            TyreId = 1,
                            RimSize = "16 inch",
                            Size = "195/55/R16",
                            ServiceDescription = "87V",
                            SideWall = "BSW",
                            Diameter = "24.4 inches",
                            MaxPSI = "44 lbs",
                            SectionWidth = "7 inches",
                            MaxLoad = "1200 lbs",
                            Weight = "20.23 inches",
                            ThreadDept = "10/32 inches",
                            AprovedRimWidth = "5.50-[6.00]-7.00 inches",
                            Cost = 15500
                        },
                        new Specification()
                        {
                            TyreId = 1,
                            RimSize = "16",
                            Size = "205/50/R16",
                            ServiceDescription = "87V",
                            SideWall = "BSW",
                            Diameter = "24.1 inches",
                            MaxPSI = "44 lbs",
                            SectionWidth = "8.31 inches",
                            MaxLoad = "1200 lbs",
                            Weight = "21.4 lbs",
                            ThreadDept = "10/32 inches",
                            AprovedRimWidth = "5.50-[6.00]-7.50 inches",
                            Cost = 16000
                        },
                         new Specification()
                        {
                            TyreId = 2,
                            RimSize = "17 inches",
                            Size = "235/50/R17",
                            ServiceDescription = "96H",
                            SideWall = "BSW",
                            Diameter = "26.3 inches",
                            MaxPSI = "44 lbs",
                            SectionWidth = "9.7 inches",
                            MaxLoad = "1,565 lbs",
                            Weight = "21 lbs",
                            ThreadDept = "10/32 inches",
                            AprovedRimWidth = "6.50-8.50 inches",
                            Cost = 18000
                        },
                          new Specification()
                        {
                            TyreId = 2,
                            RimSize = "18 inches",
                            Size = "235/60/R18",
                            ServiceDescription = "103H",
                            SideWall = "BSW",
                            Diameter = "29.1 inches",
                            MaxPSI = "44 lbs",
                            SectionWidth = "9.7 inches",
                            MaxLoad = "1,565 lbs",
                            Weight = "28 lbs",
                            ThreadDept = "10/32 inches",
                            AprovedRimWidth = "6.50-8.50 inches",
                            Cost = 19000
                        },
                           new Specification()
                        {
                            TyreId = 3,
                            RimSize = "18 inches",
                            Size = "225/45/R18",
                            ServiceDescription = "91V",
                            SideWall = "BSW",
                            Diameter = "25.9 inches",
                            MaxPSI = "51 PSI",
                            SectionWidth = "8.9 inches",
                            MaxLoad = "1,356 lbs",
                            Weight = "27 lbs",
                            ThreadDept = "10/32 inches",
                            AprovedRimWidth = "7.0-8.50 inches",
                            Cost = 30000
                        },
                              new Specification()
                        {
                            TyreId = 3,
                            RimSize = "18 inches",
                            Size = "255/40/R18",
                            ServiceDescription = "95Y",
                            SideWall = "BSW",
                            Diameter = "26 inches",
                            MaxPSI = "51 PSI",
                            SectionWidth = "10.2 inches",
                            MaxLoad = "1,521 lbs",
                            Weight = "28 lbs",
                            ThreadDept = "11/32 inches",
                            AprovedRimWidth = "8.5 - 10 inches",
                            Cost = 38000
                        },
                        new Specification()
                        {
                            TyreId = 3,
                            RimSize = "19 inches",
                            Size = "235/45/R19",
                            ServiceDescription = "95V",
                            SideWall = "BSW",
                            Diameter = "27.4 inches",
                            MaxPSI = "51 PSI",
                            SectionWidth = "9.3 inches",
                            MaxLoad = "1,521 lbs",
                            Weight = "28 lbs",
                            ThreadDept = "10/32 inches",
                            AprovedRimWidth = "7.5 - 9 inches",
                            Cost = 39000
                        },
                         new Specification()
                        {
                            TyreId = 4,
                            RimSize = "17 inches",
                            Size = "315/70/R17",
                            ServiceDescription = "121/118Q",
                            SideWall = "BSW",
                            Diameter = "34.6 inches",
                            MaxPSI = "50 PSI",
                            SectionWidth = "9.3 inches",
                            MaxLoad = "3,195/2,910 lbs",
                            Weight = "72.9 lbs",
                            ThreadDept = "18/32 inches",
                            AprovedRimWidth = "8 - 10.5 inches",
                            Cost = 66000
                        },
                          new Specification()
                        {
                            TyreId = 4,
                            RimSize = "15 inches",
                            Size = "215/75/R15",
                            ServiceDescription = "100/97Q",
                            SideWall = "BSW",
                            Diameter = "27.8 inches",
                            MaxPSI = "50 PSI",
                            SectionWidth = "9.3 inches",
                            MaxLoad = "1,764/1,609 lbs",
                            Weight = "72.9 lbs",
                            ThreadDept = "15.5/32 inches",
                            AprovedRimWidth = "5.5 - 7 inches",
                            Cost = 55000
                        }
                    });
                    context.SaveChanges();

                }

                //Inventories
                if (!context.Inventories.Any())
                {
                    context.Inventories.AddRange(new List<Inventory>()
                    {
                        new Inventory()
                        {
                            SpecsId = 1,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 2,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 3,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 4,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 5,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 6,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 7,
                            Quantity = 80
                        },
                        new Inventory()
                        {
                            SpecsId = 8,
                            Quantity = 40
                        },
                        new Inventory()
                        {
                            SpecsId = 9,
                            Quantity = 40
                        },

                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
                  await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = await userManager.FindByEmailAsync("admin@hbonlinetyres.com");
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FName = "Administrator User",
                        UserName = "Administrator",
                        Email = "admin@hbonlinetyres.com",
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "Aug@2022");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
                var appUser = await userManager.FindByEmailAsync("user@hbonlinetyres.com");
                if(appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FName = "Application User",
                        UserName = "Test-User",
                        Email = "user@hbonlinetyres.com",
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Aug@2022");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}