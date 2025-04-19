namespace TutoringProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseAndUpdateSesin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Session", "StudentId", "dbo.Student");
            DropIndex("dbo.Session", new[] { "StudentId" });
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Session", "IsOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.Session", "MaxParticipants", c => c.Int(nullable: false));
            AddColumn("dbo.Session", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "Session_Id", c => c.Int());
            CreateIndex("dbo.Session", "CourseId");
            CreateIndex("dbo.Student", "Session_Id");
            AddForeignKey("dbo.Session", "CourseId", "dbo.Course", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Student", "Session_Id", "dbo.Session", "Id");
            DropColumn("dbo.Session", "Subject");
            DropColumn("dbo.Session", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Session", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.Session", "Subject", c => c.String(nullable: false));
            DropForeignKey("dbo.Student", "Session_Id", "dbo.Session");
            DropForeignKey("dbo.Session", "CourseId", "dbo.Course");
            DropIndex("dbo.Student", new[] { "Session_Id" });
            DropIndex("dbo.Session", new[] { "CourseId" });
            DropColumn("dbo.Student", "Session_Id");
            DropColumn("dbo.Session", "CourseId");
            DropColumn("dbo.Session", "MaxParticipants");
            DropColumn("dbo.Session", "IsOnline");
            DropTable("dbo.Course");
            CreateIndex("dbo.Session", "StudentId");
            AddForeignKey("dbo.Session", "StudentId", "dbo.Student", "Id", cascadeDelete: true);
        }
    }
}
