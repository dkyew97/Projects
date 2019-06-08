using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DDAC2.Models;

namespace DDAC2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DDAC2.Models.Reservation> Reservation { get; set; }
        public DbSet<DDAC2.Models.CarModel> CarModel { get; set; }
    }
}
