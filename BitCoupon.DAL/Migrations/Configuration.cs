namespace BitCoupon.DAL.Migrations
{
    using BitCoupon.DAL.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<BitCoupon.DAL.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }



        protected override void Seed(BitCoupon.DAL.Models.ApplicationDbContext context)
        {
            context.Avatars.AddOrUpdate(
                  x => x.Id,
                  new Models.Avatar() { Id = 1, Name = "James Bond", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445507266/uiaovjvzywkvibpettz9.png" },
                  new Models.Avatar() { Id = 2, Name = "Hockey Player", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558082/0_aiszts.png" },
                  new Models.Avatar() { Id = 3, Name = "Panda", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558064/0_bk7wvx.png" },
                  new Models.Avatar() { Id = 4, Name = "Monkey", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558046/1_qm7ovq.png" },
                  new Models.Avatar() { Id = 5, Name = "Cowboy", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Chris_vy6jbb.png" },
                  new Models.Avatar() { Id = 6, Name = "Stone", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Stone_gulhdu.png" },
                  new Models.Avatar() { Id = 7, Name = "Jennifer", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Jennifer_fgsyqs.png" },
                  new Models.Avatar() { Id = 8, Name = "Mike", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Mike_szrcx6.png" },
                  new Models.Avatar() { Id = 9, Name = "Clonie", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Clonie_hvgtx6.png" },
                  new Models.Avatar() { Id = 10, Name = "Beach Girl", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558026/1_zx10bd.png" },
                  new Models.Avatar() { Id = 11, Name = "Erik", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Erik_wncimi.png" },
                  new Models.Avatar() { Id = 12, Name = "Andy", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558026/Andy_rksodp.png" },
                  new Models.Avatar() { Id = 13, Name = "Donkey", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558602/1_epmrjo.png" },
                  new Models.Avatar() { Id = 14, Name = "Cat", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558696/1_pxzpt0.png" },
                  new Models.Avatar() { Id = 15, Name = "Dog", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558712/1_qpcuku.png" },
                  new Models.Avatar() { Id = 16, Name = "Stan", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809594/3_hvh3dl.png" },
                  new Models.Avatar() { Id = 17, Name = "Kyle", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809586/1_vxciik.png" },
                  new Models.Avatar() { Id = 18, Name = "Kenny", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809576/1_vzx4rw.png" },
                  new Models.Avatar() { Id = 19, Name = "Cartman (Confused)", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809561/3_udmncc.png" },
                  new Models.Avatar() { Id = 20, Name = "Cartman (Angry)", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809561/2_vjkb8i.png" },
                  new Models.Avatar() { Id = 21, Name = "Cartman (Happy)", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809561/1_ja2eab.png" },
                  new Models.Avatar() { Id = 22, Name = "Butters", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809542/2_zithmd.png" },
                  new Models.Avatar() { Id = 23, Name = "Ninja", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809339/2_tlyndz.png" },
                  new Models.Avatar() { Id = 24, Name = "Penguin", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809328/3_qpmzkw.png" },
                  new Models.Avatar() { Id = 25, Name = "Gandalf", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809316/0_aiwztj.png" },
                  new Models.Avatar() { Id = 26, Name = "Policeman", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809305/0_ilk9to.png" },
                  new Models.Avatar() { Id = 27, Name = "Pirat", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809288/0_w0eddo.png" },
                  new Models.Avatar() { Id = 28, Name = "Turtle", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809274/0_y2uvzi.png" },
                  new Models.Avatar() { Id = 29, Name = "Mummy", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809261/2_t2pdro.png" },
                  new Models.Avatar() { Id = 30, Name = "Mike Tyson", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809248/2_tbrilr.png" },
                  new Models.Avatar() { Id = 31, Name = "Frankenstein", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809236/1_vwbe65.png" },
                  new Models.Avatar() { Id = 32, Name = "Eelephant", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809225/0_jpsswf.png" },
                  new Models.Avatar() { Id = 33, Name = "Frog", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809209/2_cn1ta8.png" },
                  new Models.Avatar() { Id = 34, Name = "Shark", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809183/2_oixqdm.png" },
                  new Models.Avatar() { Id = 35, Name = "Sad Dog", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809168/0_aevlaq.png" },
                  new Models.Avatar() { Id = 36, Name = "Puddle", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809151/1_ntubgm.png" },
                  new Models.Avatar() { Id = 37, Name = "Clown", PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445809135/1_lpg8px.png" }
                  );

            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "SuperAdmin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Salesman"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Salesman" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Seller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Seller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Buyer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Buyer" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.Email == "benjo@benjo.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "benjo@benjo.com", FirstName = "Benjamin", LastName = "Talic", Location = "Sarajevo", Address = "Sanjo Zna bb", UserName = "benjo@benjo.com", EmailConfirmed = true, Role = "SuperAdmin", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445507266/uiaovjvzywkvibpettz9.png" };

                manager.Create(user, "Benjo@123");
                manager.AddToRole(user.Id, "SuperAdmin");
            }
            if (!context.Users.Any(u => u.Email == "seller@seller.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "seller@seller.com", FirstName = "Admir", LastName = "Tuzovic", Location = "Sarajevo", Address = "Tuza bb", UserName = "seller@seller.com", EmailConfirmed = true, Role = "Seller", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Erik_wncimi.png" };

                manager.Create(user, "Seller@123");
                manager.AddToRole(user.Id, "Seller");
            }


            if (!context.Users.Any(u => u.Email == "buyer@buyer.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "buyer@buyer.com", FirstName = "Naida", LastName = "Dervishalidovic", Location = "Sarajevo", Address = "BitCamp bb", UserName = "buyer@buyer.com", EmailConfirmed = true, Role = "Buyer", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Jennifer_fgsyqs.png" };

                manager.Create(user, "Buyer@123");
                manager.AddToRole(user.Id, "Buyer");
            }

            if (!context.Users.Any(u => u.Email == "salesman@salesman.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "salesman@salesman.com", FirstName = "Benjamin", LastName = "Kapetanovic", Location = "Sarajevo", Address = "Sales Street bb", UserName = "salesman@salesman.com", EmailConfirmed = true, Role = "Salesman", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558025/Chris_vy6jbb.png" };

                manager.Create(user, "Salesman@123");
                manager.AddToRole(user.Id, "Salesman");
            }

            if (!context.Users.Any(u => u.Email == "melisa@bitcoupon.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "melisa@bitcoupon.com", FirstName = "Melisa", LastName = "Ibric", Location = "Sarajevo", Address = "IUS bb", UserName = "melisa@bitcoupon.com", EmailConfirmed = true, Role = "Admin", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558696/1_pxzpt0.png" };

                manager.Create(user, "Melisa@123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "ferid@bitcoupon.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "ferid@bitcoupon.com", FirstName = "Ferid", LastName = "Catovic", Location = "Sarajevo", Address = "Dobrinjska bb", UserName = "ferid@bitcoupon.com", EmailConfirmed = true, Role = "Admin", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558712/1_qpcuku.png" };

                manager.Create(user, "Ferid@123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "nedim@bitcoupon.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "nedim@bitcoupon.com", FirstName = "Nedim", LastName = "Hadzialic", Location = "Sarajevo", Address = "Vogoscanska bb", UserName = "nedim@bitcoupon.com", EmailConfirmed = true, Role = "Admin", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558046/1_qm7ovq.png" };

                manager.Create(user, "Nedim@123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "eldin@bitcoupon.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "eldin@bitcoupon.com", FirstName = "Eldin", LastName = "Soljic", Location = "Sarajevo", Address = "Mojmilska bb", UserName = "eldin@bitcoupon.com", EmailConfirmed = true, Role = "Admin", AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558602/1_epmrjo.png" };

                manager.Create(user, "Eldin@123");
                manager.AddToRole(user.Id, "Admin");
            }

            var newUser = context.Users.Where(x => x.Email == "seller@seller.com").SingleOrDefault();

            context.Categories.AddOrUpdate(
                    x => x.Id,
                    new Models.Category() { Id = 1, Name = "All Categories", Approved = true },
                    new Models.Category() { Id = 2, Name = "Other", Approved = true },
                    new Models.Category() { Id = 3, Name = "Sport", Approved = true },
                    new Models.Category() { Id = 4, Name = "Food", Approved = true },
                    new Models.Category() { Id = 5, Name = "Shoes", Approved = true },
                    new Models.Category() { Id = 6, Name = "Vacations", Approved = true });

            context.Coupons.AddOrUpdate(
                x => x.CouponId,
                 new Models.Coupon()
                 {
                     CouponId = 1,
                     Name = "BitCamp",
                     Price = 998,
                     NewPrice = 500,
                     DescriptionOnCoupon = "BitCamp, Great Eduction",
                     DescriptionOnSellerPage = "Six-months intensive training designed for intelligent and ambitious people who are willing to invest a huge effort, acquire knowledge, gain experience, and to come up employment in leading IT company in Bosnia and Herzegovina",
                     ExpirationTime = new DateTime(2015, 12, 01),
                     RequiredNumberOfCoupons = 1,
                     TotalNumberOfCoupons = 50,
                     SellerUrl = "www.bitcamp.ba",
                     PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446562714/fmr8bss6bylb91oxgs14.jpg",
                     CategoryId = 2,
                     Acitve = true,
                     IsDeleted = false,
                     NuberOfCouponsToOfferManaged = 1,
                     ApplicationUserId = newUser.Id,
                     IsEdited = false,
                     Approved = true,
                     Priority = 3
                 },
                 
                  new Models.Coupon()
                  {
                      CouponId = 2,
                      Name = "Wyndham Garden - Puerto Rico",
                      Price = 2475,
                      NewPrice = 999,
                      DescriptionOnCoupon = "Stay for two, with optional all-inclusive package.",
                      DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum.Typi non habent claritatem insitam; est usus legentis in iis qui facit eorum claritatem.",
                      ExpirationTime = new DateTime(2015, 12, 05),
                      RequiredNumberOfCoupons = 2,
                      TotalNumberOfCoupons = 40,
                      SellerUrl = "www.Solazur.com",
                      PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446555175/kzm9kbu0msxjzxtwobl5.jpg",
                      CategoryId = 6,
                      Acitve = true,
                      IsDeleted = false,
                      NuberOfCouponsToOfferManaged = 2,
                      ApplicationUserId = newUser.Id,
                      IsEdited = false,
                      Approved = true,
                      Priority = 3
                  },
                  new Models.Coupon()
                  {
                      CouponId = 3,
                      Name = "Shopping Tour in Ljubljana",
                      Price = 321,
                      NewPrice = 159,
                      DescriptionOnCoupon = "Organized shopping tour in Ljubljana",
                      DescriptionOnSellerPage = "Emona, Laibach and Lubiana, they are all the same names for the capital of Slovenia, Ljubljana. It lies on the river Ljubljanica is characterized by magnificent bridges along the river, the legacy of Roman Emona, Baroque and Art Nouveau architectural creations and totally unique and original seal embossed in the first half of the 20th century by the famous architect Joze Plecnik. Ljubljana is one of the most beautiful European capitals.",
                      ExpirationTime = new DateTime(2015, 11, 25),
                      RequiredNumberOfCoupons = 2,
                      TotalNumberOfCoupons = 16,
                      SellerUrl = "www.BrillianceTours.com",
                      PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446557590/txbwox8vfhu0utwdo4ef.jpg",
                      CategoryId = 6,
                      Acitve = true,
                      IsDeleted = false,
                      NuberOfCouponsToOfferManaged = 2,
                      ApplicationUserId = newUser.Id,
                      IsEdited = false,
                      Approved = true,
                      Priority = 3
                  },
                new Models.Coupon()
                {
                    CouponId = 4,
                    Name = "Hotel Radin in Slovenia ",
                    Price = 255,
                    NewPrice = 188,
                    DescriptionOnCoupon = "Three nights for two persons on board.",
                    DescriptionOnSellerPage = "Hotel Radin is the central hotel of Terme Radenci, which offers its guests rich mineral water springs in the green landscape of wheat fields and vineyard hills beside the river Mura. The hotel provides comfort for a relaxing holiday with the family, for those who are alone or for two.Guests have the option of relaxing in the thermal bath and complex.",
                    ExpirationTime = new DateTime(2015, 12, 18),
                    RequiredNumberOfCoupons = 3,
                    TotalNumberOfCoupons = 15,
                    SellerUrl = "www.Solazur.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446558149/aewnqxmkgmnk1jh6afqm.jpg",
                    CategoryId = 6,
                    Acitve = true,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 3,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3
                },
                new Models.Coupon()
                {
                    CouponId = 5,
                    Name = "Discount on three facial treatment",
                    Price = 135,
                    NewPrice = 28,
                    DescriptionOnCoupon = "Three nights for two persons on board.",
                    DescriptionOnSellerPage = "Facial cleansing is fundamental to the maintenance of skin nurtured and rejuvenated face. Facial cleansing is the first step towards perfect skin. Why are these important treatments ? Because your skin is daily exposed to various external factors that create the dirt and lead to loss of hydration,elasticity and radiance of the skin.To maintain the youthful appearance of your skin, it is very important to apply preventive treatments that will help to maintain the natural balance.",
                    ExpirationTime = new DateTime(2015, 12, 25),
                    RequiredNumberOfCoupons = 2,
                    TotalNumberOfCoupons = 26,
                    SellerUrl = "www.facial.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446558839/bzgbzamdi69m97fzyjjj.jpg",
                    CategoryId = 6,
                    Acitve = true,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 2,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3
                },
                 new Models.Coupon()
                 {
                     CouponId = 6,
                     Name = "Best selfie with selfie stick!",
                     Price = 34,
                     NewPrice = 10,
                     DescriptionOnCoupon = "Omega selfie stick in blue or red",
                     DescriptionOnSellerPage = "Selfie stick is handy, foldable perfect for daily use, especially indispensable to socializing and traveling. Selfie stick is easy to use regardless of age.",
                     ExpirationTime = new DateTime(2015, 12, 25),
                     RequiredNumberOfCoupons = 1,
                     TotalNumberOfCoupons = 18,
                     SellerUrl = "www.benjo-selfie-stick.com",
                     PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446560915/e0qbzcm4rfsuec8xksyv.jpg",
                     CategoryId = 2,
                     Acitve = true,
                     IsDeleted = false,
                     NuberOfCouponsToOfferManaged = 1,
                     ApplicationUserId = newUser.Id,
                     IsEdited = false,
                     Approved = true,
                     Priority = 3
                 },
                  new Models.Coupon()
                  {
                      CouponId = 7,
                      Name = "Best bowling for the whole society!",
                      Price = 20,
                      NewPrice = 10,
                      DescriptionOnCoupon = "Pay only $10 instead of $20 for a term of bowling",
                      DescriptionOnSellerPage = "Gather your friends, colleagues or family and take them to great entertainment with bowling! This is a game that can be enjoyed by all generations of players, all you need is a sharp eye and skillful hand. Understand this challenge as a fun and recreation, but also a chance to show their competitive spirit. You do not have to think too much about the rules because your main goal is to knock down as many pins, and laughing and competition, have tons of fun!",
                      ExpirationTime = new DateTime(2015, 12, 05),
                      RequiredNumberOfCoupons = 1,
                      TotalNumberOfCoupons = 18,
                      SellerUrl = "www.bowling.com",
                      PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561314/q3hk8oqkgls5js7zfglw.jpg",
                      CategoryId = 3,
                      Acitve = true,
                      IsDeleted = false,
                      NuberOfCouponsToOfferManaged = 1,
                      ApplicationUserId = newUser.Id,
                      IsEdited = false,
                      Approved = true,
                      Priority = 3
                  },
                  new Models.Coupon()
                  {
                      CouponId = 8,
                      Name = "Delicious cake!",
                      Price = 35,
                      NewPrice = 25,
                      DescriptionOnCoupon = "Choose one of the five cakes to our taste",
                      DescriptionOnSellerPage = "Cakes are very popular treat you prepare for various occasions such as weddings, birthdays, anniversaries and many other events. There are various combinations of these sweets, so they are very popular cake with chocolate, fruit cakes, cheesecake, pecan cake, ice cream cake ...",
                      ExpirationTime = new DateTime(2015, 12, 01),
                      RequiredNumberOfCoupons = 1,
                      TotalNumberOfCoupons = 22,
                      SellerUrl = "www.great-cakes.com",
                      PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561852/tsgdr7z1imdbuff7r7il.jpg",
                      CategoryId = 4,
                      Acitve = true,
                      IsDeleted = false,
                      NuberOfCouponsToOfferManaged = 1,
                      ApplicationUserId = newUser.Id,
                      IsEdited = false,
                      Approved = true,
                      Priority = 3
                  },
                 new Models.Coupon()
                 {
                     CouponId = 9,
                     Name = "Bermuda This Summer: 4-Star Resort",
                     Price = 171,
                     NewPrice = 78,
                     DescriptionOnCoupon = "Immerse Yourself in the Magic of Cambodia!",
                     DescriptionOnSellerPage = "Immerse Yourself in the Magic of Cambodia with a Luxurious Eight Day/Seven Night Escape at Two of the World’s Finest Hotels!",
                     ExpirationTime = new DateTime(2015, 12, 02),
                     RequiredNumberOfCoupons = 2,
                     TotalNumberOfCoupons = 20,
                     SellerUrl = "www.Solazur.com",
                     PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446555175/kzm9kbu0msxjzxtwobl5.jpg",
                     CategoryId = 6,
                     Acitve = true,
                     IsDeleted = false,
                     NuberOfCouponsToOfferManaged = 2,
                     ApplicationUserId = newUser.Id,
                     IsEdited = false,
                     Approved = true,
                     Priority = 3
                 },
                   new Models.Coupon()
                   {
                       CouponId = 10,
                       Name = "Mounting Climbing",
                       Price = 100,
                       NewPrice = 36,
                       DescriptionOnCoupon = "Mounting Climbing in Nature",
                       DescriptionOnSellerPage = "Mountaineering or the sport of climbing mountains is simply one of the finest outdoor opportunities available to the lover of high places. Mountain climbing is all about challenge and perseverance, about putting hands and feet onto rocks and ice and snow and finally reaching a summit. There, high above the world of cities and civilization, the climber can pause and look across a natural world ruled by nature and her raw beauty.",
                       ExpirationTime = new DateTime(2015, 12, 08),
                       RequiredNumberOfCoupons = 1,
                       TotalNumberOfCoupons = 28,
                       SellerUrl = "www.great-cakes.com",
                       PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446563269/r9kzvdzrxv9tefi2kh6j.jpg",
                       CategoryId = 3,
                       Acitve = true,
                       IsDeleted = false,
                       NuberOfCouponsToOfferManaged = 1,
                       ApplicationUserId = newUser.Id,
                       IsEdited = false,
                       Approved = true,
                       Priority = 3
                   },
                new Models.Coupon()
                {
                    CouponId = 11,
                    Name = "Pro Gym, Best Gym",
                    Price = 85,
                    NewPrice = 29,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 45,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1442747275/ytowv7tbla07zokhnpq8.jpg",
                    CategoryId = 3,
                    Acitve = true,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 5,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3
                },
                new Models.Coupon()
                {
                    CouponId = 12,
                    Name = "Burger King",
                    Price = 5,
                    NewPrice = 2,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 45,
                    SellerUrl = "www.BurgerKing.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1443020685/kp2xazyzycijlbzzmzk7.jpg",
                    CategoryId = 4,
                    Acitve = true,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 5,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3
                },
                new Models.Coupon()
                {
                    CouponId = 13,
                    Name = "Gym of the Kings",
                    Price = 122,
                    NewPrice = 59,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 45,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/c_scale,h_480,w_720/v1441286188/fitness_ujckp5.jpg",
                    CategoryId = 3,
                    Acitve = true,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 5,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3
                },
                new Models.Coupon()
                {
                    CouponId = 14,
                    Name = "Bet Gym in the World",
                    Price = 39,
                    NewPrice = 21,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 0,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/c_scale,h_480,w_720/v1441286188/fitness_ujckp5.jpg",
                    CategoryId = 3,
                    Acitve = false,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 0,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3,
                    Purchase = 5
                },
                new Models.Coupon()
                {
                    CouponId = 15,
                    Name = "Fitness is the Key",
                    Price = 56,
                    NewPrice = 28,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 0,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/c_scale,h_480,w_720/v1441286188/fitness_ujckp5.jpg",
                    CategoryId = 3,
                    Acitve = false,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 0,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3,
                    Purchase = 5
                },
                new Models.Coupon()
                {
                    CouponId = 16,
                    Name = "Body Building",
                    Price = 48,
                    NewPrice = 23,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 0,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/c_scale,h_480,w_720/v1441286188/fitness_ujckp5.jpg",
                    CategoryId = 3,
                    Acitve = false,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 0,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3,
                    Purchase = 5
                },
                new Models.Coupon()
                {
                    CouponId = 17,
                    Name = "Our Gym is the Best",
                    Price = 37,
                    NewPrice = 23,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 0,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/c_scale,h_480,w_720/v1441286188/fitness_ujckp5.jpg",
                    CategoryId = 3,
                    Acitve = false,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 0,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3,
                    Purchase = 5
                },
                new Models.Coupon()
                {
                    CouponId = 18,
                    Name = "My Gym, my Rules",
                    Price = 34,
                    NewPrice = 20,
                    DescriptionOnCoupon = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    DescriptionOnSellerPage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam consectetur purus est, eleifend iaculis quam accumsan sed. Mauris vehicula felis sit amet tellus cursus imperdiet. Curabitur cras amet.",
                    ExpirationTime = new DateTime(2015, 12, 12),
                    RequiredNumberOfCoupons = 5,
                    TotalNumberOfCoupons = 0,
                    SellerUrl = "www.progym.com",
                    PictureUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/c_scale,h_480,w_720/v1441286188/fitness_ujckp5.jpg",
                    CategoryId = 3,
                    Acitve = false,
                    IsDeleted = false,
                    NuberOfCouponsToOfferManaged = 0,
                    ApplicationUserId = newUser.Id,
                    IsEdited = false,
                    Approved = true,
                    Priority = 3,
                    Purchase = 5
                });

            context.Images.AddOrUpdate(
                x => x.Id,
                new Models.Image()
                {
                    CouponId = 11,
                    Id = 1,
                    ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441912749/qyklub989m9ruyxzty26.jpg"


                },
                  new Models.Image()
                  {
                      CouponId = 11,
                      Id = 2,
                      ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441915189/suc4yqaljzdo1ex1cci0.jpg"


                  },
                  new Models.Image()
                  {
                      CouponId = 11,
                      Id = 3,
                      ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441915186/lgc9act7qx0akjr6rrkl.jpg"


                  },
                  new Models.Image()
                  {
                      CouponId = 11,
                      Id = 4,
                      ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441915109/q971zvoie9dalres3oxh.jpg"


                  },
                  new Models.Image()
                  {
                      CouponId = 11,
                      Id = 5,
                      ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441915193/pvhahoa5poffxt1wg06w.jpg"


                  },

                   // second coupon 
                   new Models.Image()
                   {
                       CouponId = 12,
                       Id = 6,
                       ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441803585/cmjxwu5vwxiyyctvnyv3.jpg"


                   },
                    new Models.Image()
                    {
                        CouponId = 12,
                        Id = 7,
                        ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441803586/d2vpluicn3wbqy9n4k7u.jpg"


                    },
                    new Models.Image()
                    {
                        CouponId = 12,
                        Id = 8,
                        ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441803588/mv2zgoeboctis9briqbl.jpg"


                    },
                    new Models.Image()
                    {
                        CouponId = 12,
                        Id = 9,
                        ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441803615/z9bhsvzbkvdqwxv7r85m.jpg"


                    },
                    new Models.Image()
                    {
                        CouponId = 12,
                        Id = 10,
                        ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441803618/czy1oetx6ignvmdzgurt.jpg"


                    },

                     new Models.Image()
                     {
                         CouponId = 13,
                         Id = 11,
                         ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441974503/ldkpkx81jq0b30oirdkb.jpg"


                     },
                     new Models.Image()
                     {
                         CouponId = 13,
                         Id = 12,
                         ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441974503/v3cmcfng3adrvywyawoc.jpg"


                     },
                     new Models.Image()
                     {
                         CouponId = 13,
                         Id = 13,
                         ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441974506/ildeehjdleilducjikk8.jpg"


                     },
                     new Models.Image()
                     {
                         CouponId = 13,
                         Id = 14,
                         ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441974507/hyechs5awfgqera0yykm.jpg"


                     },
                     new Models.Image()
                     {
                         CouponId = 13,
                         Id = 15,
                         ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1441974508/xzvzy9weknibt2ooa70w.jpg"

                     },
                      new Models.Image()
                      {
                          CouponId = 2,
                          Id = 16,
                          ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446555176/zyvcvfffbo7pjinqwiq2.jpg"

                      },
                       new Models.Image()
                       {
                           CouponId = 9,
                           Id = 17,
                           ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446555178/tnjc7b1owulsbzbkwsbp.jpg"

                       },
                        new Models.Image()
                        {
                            CouponId = 9,
                            Id = 18,
                            ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446555185/anl1u766bse8gagf2hyy.jpg"

                        },
                          new Models.Image()
                          {
                              CouponId = 2,
                              Id = 19,
                              ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446556744/loapz7lhdh44b4fembzy.jpg"

                          },
                            new Models.Image()
                            {
                                CouponId = 2,
                                Id = 20,
                                ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446556746/eipqchjl223xtgqgicwj.jpg"

                            },
                            new Models.Image()
                            {
                                CouponId = 2,
                                Id = 21,
                                ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446556747/jyyybujvlsee6szkromo.jpg"

                            },
                         new Models.Image()
                         {
                             CouponId = 2,
                             Id = 22,
                             ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446556750/lrig0hszdegjl1409vbr.jpg"

                         },
                          new Models.Image()
                          {
                              CouponId = 2,
                              Id = 23,
                              ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446556755/ch9ba6rktflvosshfqkp.jpg"

                          },
                         new Models.Image()
                         {
                             CouponId = 4,
                             Id = 24,
                             ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446558148/klodecku8bbdtrgjcg2m.jpg"

                         },
                         new Models.Image()
                         {
                             CouponId = 4,
                             Id = 25,
                             ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446558146/iailomz6bj4jin5mxjq6.jpg"

                         },

                          new Models.Image()
                          {
                              CouponId = 4,
                              Id = 26,
                              ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446558145/da2hknmivzghnaw61d1c.jpg"

                          },
                             new Models.Image()
                             {
                                 CouponId = 5,
                                 Id = 27,
                                 ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446558838/eqqypinachqg2ijzfm32.jpg"

                             },

                         new Models.Image()
                         {
                             CouponId = 6,
                             Id = 28,
                             ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446560913/w8vqx5dkpcvz4mqyzvqq.jpg"

                         },
                          new Models.Image()
                          {
                              CouponId = 7,
                              Id = 29,
                              ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561312/cfdkabwjlbyizbzgukl2.jpg"

                          },
                           new Models.Image()
                           {
                               CouponId = 7,
                               Id = 30,
                               ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561312/yxcaabty6dup4jwjiosk.jpg"

                           },
                       new Models.Image()
                       {
                           CouponId = 8,
                           Id = 31,
                           ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561850/t4ybyqv3sgimztvlzsrs.jpg"

                       },
                         new Models.Image()
                         {
                             CouponId = 8,
                             Id = 32,
                             ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561849/a3xuffnyjn5wtoeaj72k.jpg"

                         },
                           new Models.Image()
                           {
                               CouponId = 8,
                               Id = 33,
                               ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561848/ckwrhimafleualsfbaet.jpg"

                           },
                             new Models.Image()
                             {
                                 CouponId = 8,
                                 Id = 34,
                                 ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446561844/xhjgktgggu2fb5ce8ppu.png"

                             },
                               new Models.Image()
                               {
                                   CouponId = 1,
                                   Id = 35,
                                   ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446562715/godp3n9b6433ayxcheee.jpg"

                               },
                             new Models.Image()
                             {
                                 CouponId = 1,
                                 Id = 36,
                                 ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446562718/nut0hbvm3bwvrn5hkrtn.jpg"

                             },
                              new Models.Image()
                              {
                                  CouponId = 1,
                                  Id = 37,
                                  ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446562717/bmvijesvhemr1rfbizsu.jpg"

                              },
                               new Models.Image()
                               {
                                   CouponId = 10,
                                   Id = 38,
                                   ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446563264/adhqpzvgzjbn87oeas49.jpg"

                               },
                                new Models.Image()
                                {
                                    CouponId = 10,
                                    Id = 39,
                                    ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446563274/uofyx8thhfxwflkdvlsx.jpg"

                                },
                                 new Models.Image()
                                 {
                                     CouponId = 10,
                                     Id = 40,
                                     ImageUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1446563274/vr0brtvmd5o35xyilzjq.jpg"

                                 }


                );

            context.GoogleApis.AddOrUpdate(
                x => x.Id,

                new Models.GoogleApi()
                {
                    CouponId = 1,
                    Id = 1,
                    Long = "18,419952392578125",
                    Lang = "43,89789239125797"

                },
                new Models.GoogleApi()
                {
                    CouponId = 1,
                    Id = 2,
                    Long = "18,219952392578125",
                    Lang = "43,69789239125797"

                },
                new Models.GoogleApi()
                {
                    CouponId = 1,
                    Id = 3,
                    Long = "18,419952392578125",
                    Lang = "43,19289239125797"

                },
                 // break
                 new Models.GoogleApi()
                 {
                     CouponId = 2,
                     Id = 4,
                     Long = "18,229952392578125",
                     Lang = "43,33289239125797"

                 },
                 new Models.GoogleApi()
                 {
                     CouponId = 2,
                     Id = 5,
                     Long = "18,519952392578125",
                     Lang = "43,89289239125797"

                 },
                 new Models.GoogleApi()
                 {
                     CouponId = 2,
                     Id = 6,
                     Long = "18,419952392578125",
                     Lang = "43,19289239125797"

                 },
                  new Models.GoogleApi()
                  {
                      CouponId = 3,
                      Id = 7,
                      Long = "18,367767333984375",
                      Lang = "43,86819814895608"

                  },
                   new Models.GoogleApi()
                   {
                       CouponId = 3,
                       Id = 8,
                       Long = "18,267767333984375",
                       Lang = "43,76819814895608"

                   },
                    new Models.GoogleApi()
                    {
                        CouponId = 4,
                        Id = 9,
                        Long = "18,267767333984375",
                        Lang = "43,76819814895608"

                    },
                     new Models.GoogleApi()
                     {
                         CouponId = 4,
                         Id = 10,
                         Long = "18,267767333984375",
                         Lang = "43,76819814895608"

                     },
                      new Models.GoogleApi()
                      {
                          CouponId = 4,
                          Id = 11,
                          Long = "18,267767333984375",
                          Lang = "43,76819814895608"

                      },
                       new Models.GoogleApi()
                       {
                           CouponId = 5,
                           Id = 12,
                           Long = "18,267767333984375",
                           Lang = "43,76819814895608"

                       },
                        new Models.GoogleApi()
                        {
                            CouponId = 5,
                            Id = 13,
                            Long = "18,267767333984375",
                            Lang = "43,76819814895608"

                        },
                         new Models.GoogleApi()
                         {
                             CouponId = 5,
                             Id = 14,
                             Long = "18,267767333984375",
                             Lang = "43,76819814895608"

                         },
                          new Models.GoogleApi()
                          {
                              CouponId = 6,
                              Id = 15,
                              Long = "18,267767333984375",
                              Lang = "43,76819814895608"

                          },
                           new Models.GoogleApi()
                           {
                               CouponId = 6,
                               Id = 16,
                               Long = "18,267767333984375",
                               Lang = "43,76819814895608"

                           },
                            new Models.GoogleApi()
                            {
                                CouponId = 6,
                                Id = 17,
                                Long = "18,267767333984375",
                                Lang = "43,76819814895608"

                            },
                             new Models.GoogleApi()
                             {
                                 CouponId = 7,
                                 Id = 18,
                                 Long = "18,267767333984375",
                                 Lang = "43,76819814895608"

                             },
                              new Models.GoogleApi()
                              {
                                  CouponId = 7,
                                  Id = 19,
                                  Long = "18,267767333984375",
                                  Lang = "43,76819814895608"

                              },
                               new Models.GoogleApi()
                               {
                                   CouponId = 8,
                                   Id = 20,
                                   Long = "18,267767333984375",
                                   Lang = "43,76819814895608"

                               },
                                 new Models.GoogleApi()
                                 {
                                     CouponId = 9,
                                     Id = 21,
                                     Long = "18,267767333984375",
                                     Lang = "43,76819814895608"

                                 },
                                   new Models.GoogleApi()
                                   {
                                       CouponId = 9,
                                       Id = 22,
                                       Long = "18,267767333984375",
                                       Lang = "43,76819814895608"

                                   },
                                    new Models.GoogleApi()
                                    {
                                        CouponId = 10,
                                        Id = 23,
                                        Long = "18,267767333984375",
                                        Lang = "43,76819814895608"

                                    },
                                     new Models.GoogleApi()
                                     {
                                         CouponId = 10,
                                         Id = 24,
                                         Long = "18,267767333984375",
                                         Lang = "43,76819814895608"

                                     }

                );

            context.Mails.AddOrUpdate(
                x => x.Id,
                new Models.Mail()
                {
                    Id = 1,
                    Body = "Dear %name%, <br /> <br />Your payment is succesful. <br /> <br />You paid:<br />%coupon% <br /><br />Total amount you paid :  %totalPrice% USD. <br /> <br />Your paypal Payment ID is %paymentId% <br /> <br />All the best, <b>BitCoupon</b> Team.",
                    Subject = "Payment"
                },
                new Models.Mail()
                {
                    Id = 2,
                    Body = "Dear %name%, <br /> <br />Your refound is succesful. <br /> <br />You paid:<br />%coupon% <br /><br />Total amunt you refounded :  %totalPrice% USD. <br /> <br />Your paypal Payment ID is %paymentId% <br /> <br />All the best, <b>BitCoupon</b> Team.",
                    Subject = "Refound"
                },
                new Models.Mail()
                {
                    Id = 3,
                    Body = "Dear %name%, <br /> <br />  Your request is sent to admin team. Please wait for aproval <br /> <br /> All the best, <b>BitCoupon</b> Team.",
                    Subject = "Refund Notification"

                }
                ,
                new Models.Mail()
                {
                    Id = 4,
                    Body = "<p>Dear %name% ,</p><p>Your request for refound is %order% by admin.</p><p>All the best <strong style='font-size: 14px; line-height: 20px; color: #333333; font-family: 'Open Sans', sans-serif;'>BitCoupon</strong> Team.</p>",
                    Subject = "Admin Refund Response"
                }
                );
        }
    }
}
