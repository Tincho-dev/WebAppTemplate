using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Model.Domain;
using System.Data.Entity;

namespace Persistanse
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRol> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        
        public DbSet<RolEmp> RolEmpleado { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<UserPorEmp> UserPorEmp { get; set; }
        

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
