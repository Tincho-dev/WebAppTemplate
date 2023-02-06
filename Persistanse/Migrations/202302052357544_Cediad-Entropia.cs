namespace Persistanse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CediadEntropia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Empleadoes", "Id_RolServicio", "dbo.RolEmps");
            DropForeignKey("dbo.UserPorEmps", "IdUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPorEmps", "Legajo", "dbo.Empleadoes");
            DropIndex("dbo.Empleadoes", new[] { "Id_RolServicio" });
            DropIndex("dbo.UserPorEmps", new[] { "Legajo" });
            DropIndex("dbo.UserPorEmps", new[] { "IdUsuario" });
            CreateTable(
                "dbo.Fuentes",
                c => new
                    {
                        IdFuente = c.String(nullable: false, maxLength: 128),
                        N = c.Int(nullable: false),
                        CadenaFuente = c.String(),
                        EntropiaMaxima = c.Double(nullable: false),
                        CadenaCodificada = c.String(),
                        EntropiaDeLaFuente = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdFuente);
            
            CreateTable(
                "dbo.Letras",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Codigo = c.String(),
                        FrecuenciaDeAparicion = c.Int(nullable: false),
                        IdFuente = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fuentes", t => t.IdFuente)
                .Index(t => t.IdFuente);
            
            DropTable("dbo.Empleadoes");
            DropTable("dbo.RolEmps");
            DropTable("dbo.UserPorEmps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPorEmps",
                c => new
                    {
                        IdUserxEmp = c.Int(nullable: false, identity: true),
                        Legajo = c.Int(nullable: false),
                        IdUsuario = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdUserxEmp);
            
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
                .PrimaryKey(t => t.Legajo);
            
            DropForeignKey("dbo.Letras", "IdFuente", "dbo.Fuentes");
            DropIndex("dbo.Letras", new[] { "IdFuente" });
            DropTable("dbo.Letras");
            DropTable("dbo.Fuentes");
            CreateIndex("dbo.UserPorEmps", "IdUsuario");
            CreateIndex("dbo.UserPorEmps", "Legajo");
            CreateIndex("dbo.Empleadoes", "Id_RolServicio");
            AddForeignKey("dbo.UserPorEmps", "Legajo", "dbo.Empleadoes", "Legajo", cascadeDelete: true);
            AddForeignKey("dbo.UserPorEmps", "IdUsuario", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Empleadoes", "Id_RolServicio", "dbo.RolEmps", "Id_Rol", cascadeDelete: true);
        }
    }
}
