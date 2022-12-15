namespace Persistanse.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    enabled = c.Boolean(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                    Name = c.String(),
                    LastName = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Empleadoes",
                c => new
                {
                    Legajo = c.Int(nullable: false, identity: true),
                    Nombre = c.String(nullable: false, maxLength: 120),
                    Apellido = c.String(nullable: false, maxLength: 120),
                    DNI = c.Int(nullable: false),
                    FechaNacimiento = c.DateTime(nullable: false),
                    FechaIngreso = c.DateTime(nullable: false),
                    Telefono = c.Long(nullable: false),
                    Id_RolServicio = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Legajo)
                .ForeignKey("dbo.RolEmps", t => t.Id_RolServicio, cascadeDelete: true)
                .Index(t => t.Id_RolServicio);

            CreateTable(
                "dbo.RolEmps",
                c => new
                {
                    Id_Rol = c.Int(nullable: false, identity: true),
                    Nombre = c.String(nullable: false, maxLength: 120),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.Id_Rol);

            CreateTable(
                "dbo.UserPorEmps",
                c => new
                {
                    IdUserxEmp = c.Int(nullable: false, identity: true),
                    Legajo = c.Int(nullable: false),
                    IdUsuario = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.IdUserxEmp)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUsuario)
                .ForeignKey("dbo.Empleadoes", t => t.Legajo, cascadeDelete: true)
                .Index(t => t.Legajo)
                .Index(t => t.IdUsuario);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPorEmps", "Legajo", "dbo.Empleadoes");
            DropForeignKey("dbo.UserPorEmps", "IdUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Empleadoes", "Id_RolServicio", "dbo.RolEmps");
            DropIndex("dbo.UserPorEmps", new[] { "IdUsuario" });
            DropIndex("dbo.UserPorEmps", new[] { "Legajo" });
            DropIndex("dbo.Empleadoes", new[] { "Id_RolServicio" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.UserPorEmps");
            DropTable("dbo.RolEmps");
            DropTable("dbo.Empleadoes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
