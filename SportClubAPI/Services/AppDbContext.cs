using Microsoft.EntityFrameworkCore;
using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SportGood> SportGoods { get; set; }
        public DbSet<CustomerSportGood> CustomerSportGoods { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

                        modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                FirstName = "Tom",
                SecondName = "Jackson",
                Gender = Gender.Male,
                SportGoods = new List<SportGood>(),
                Address = "St.Johnes St, 57",
                SubscribeStatus = SubscribeStatus.SubscribeGeneral,
                Email = "tom-jackson.@gmail.com",
                RegistrationDate = DateTime.Now

            });
            
        }*/
    }
}
