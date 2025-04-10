namespace TutoringProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateFormat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Session", "Time", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Student", "Birthday", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Session", "Time", c => c.DateTime(nullable: false));
        }
    }
}
