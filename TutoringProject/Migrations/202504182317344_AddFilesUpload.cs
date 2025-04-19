namespace TutoringProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilesUpload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionMaterial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        UploadedAt = c.DateTime(nullable: false),
                        TutorId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentSubmission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        SubmittedAt = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentSubmission");
            DropTable("dbo.SessionMaterial");
        }
    }
}
