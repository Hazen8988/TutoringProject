namespace TutoringProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsOnline = c.Boolean(nullable: false),
                        MaxParticipants = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TutorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.UserAccounts", t => t.TutorId)
                .Index(t => t.CourseId)
                .Index(t => t.TutorId);
            
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
                "dbo.StudentSession",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.SessionId })
                .ForeignKey("dbo.Session", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.UserAccounts", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Session", "TutorId", "dbo.UserAccounts");
            DropForeignKey("dbo.StudentSession", "SessionId", "dbo.UserAccounts");
            DropForeignKey("dbo.StudentSession", "StudentId", "dbo.Session");
            DropForeignKey("dbo.Session", "CourseId", "dbo.Course");
            DropIndex("dbo.StudentSession", new[] { "SessionId" });
            DropIndex("dbo.StudentSession", new[] { "StudentId" });
            DropIndex("dbo.Session", new[] { "TutorId" });
            DropIndex("dbo.Session", new[] { "CourseId" });
            DropTable("dbo.StudentSession");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Session");
            DropTable("dbo.Course");
        }
    }
}
