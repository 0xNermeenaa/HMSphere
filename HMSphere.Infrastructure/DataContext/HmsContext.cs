using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace HMSphere.Infrastructure.DataContext
{
    public class HmsContext:DbContext
    {
        public HmsContext(DbContextOptions<HmsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
