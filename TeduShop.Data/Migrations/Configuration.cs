﻿namespace TeduShop.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeduShop.Data.TeduShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlides(context);
        }

        private void CreateUser(TeduShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tedu",
                Email = "tedu.international@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Technology Education"
            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh", Alias="dien-lanh", Status=true, CreatedDate = DateTime.Now, CreatedBy = "Migration" },
                 new ProductCategory() { Name="Di động", Alias="di-dong", Status=true, CreatedDate = DateTime.Now, CreatedBy = "Migration" },
                  new ProductCategory() { Name="Đồ gia dụng", Alias="do-gia-dung", Status=true, CreatedDate = DateTime.Now, CreatedBy = "Migration" },
                   new ProductCategory() { Name="Điện máy", Alias="dien-may", Status=true, CreatedDate = DateTime.Now, CreatedBy = "Migration" }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreateFooter(TeduShopDbContext context)
        {
            if (context.Footers.Count(m => m.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "";
            }
        }

        private void CreateSlides(TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() { Name = "Slide 1", DisplayOrder = 1, Status = true, URL = "#", Image = "/Assets/client/images/bag.jpg", Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p><span class=""on-get"">GET NOW</span>" },
                    new Slide() { Name = "Slide 2", DisplayOrder = 2, Status = true, URL = "#", Image = "/Assets/client/images/bag1.jpg", Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on-get"">GET NOW</span>" },
                    new Slide() { Name = "Slide 3", DisplayOrder = 3, Status = true, URL = "#", Image = "/Assets/client/images/bag.jpg", Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on-get"">GET NOW</span>" }
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
    }
}
