namespace TutoringProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false),
                        StudentId = c.Int(nullable: false),
                        TutorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Tutor", t => t.TutorId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TutorId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAccounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        Fname = c.String(nullable: false, maxLength: 50),
                        Lname = c.String(nullable: false, maxLength: 50),
                        Role = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tutor",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Delivery = c.String(nullable: false, maxLength: 100),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAccounts", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Session", "TutorId", "dbo.Tutor");
            DropForeignKey("dbo.Tutor", "Id", "dbo.UserAccounts");
            DropForeignKey("dbo.Session", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Student", "Id", "dbo.UserAccounts");
            DropIndex("dbo.Tutor", new[] { "Id" });
            DropIndex("dbo.Student", new[] { "Id" });
            DropIndex("dbo.Session", new[] { "TutorId" });
            DropIndex("dbo.Session", new[] { "StudentId" });
            DropTable("dbo.Tutor");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Student");
            DropTable("dbo.Session");
        }
    }
}
