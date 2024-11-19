using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcWebApp.Models;
using NuGet.Protocol;

namespace MvcWebApp.Data
{
    public class MvcWebAppContext : DbContext
    {
        public MvcWebAppContext(DbContextOptions<MvcWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<MvcWebApp.Models.UserInfoModel> UserInfoModel { get; set; } = default!;
        public DbSet<MvcWebApp.Models.UserCredentials> UserCredentials { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfoModel>()
            .Property(e => e.Roles)
            .HasConversion(
                v => v.ToJson(Newtonsoft.Json.Formatting.None),
                v => v.FromJson<string[]>()
            );
        }
    }
}
