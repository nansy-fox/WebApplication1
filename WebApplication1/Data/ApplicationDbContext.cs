﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Good> Goods { get; set; }
    }


}
