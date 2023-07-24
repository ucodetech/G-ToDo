using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G_ToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace  G_ToDo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ToDo> Todos { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Status> Status { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                new Category  {CategoryId = "work", Name="Work", Icon = ""},
                new Category  {CategoryId = "home", Name="Home", Icon = ""},
                new Category  {CategoryId = "ex", Name="Exercise", Icon = ""},
                new Category  {CategoryId = "shop", Name="Shop", Icon = ""},
                new Category  {CategoryId = "call", Name="Contact", Icon = ""}
            );

             builder.Entity<Status>().HasData(
                new Status  {StatusId = "open", Name="Open"},
                new Status  {StatusId = "closed", Name="Close"}
              
            );
        }
    }
}