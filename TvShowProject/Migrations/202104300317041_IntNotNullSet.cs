namespace TvShowProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntNullSet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shows", "Seasons", c => c.Int(nullable: false));
            AlterColumn("dbo.Shows", "Episodes", c => c.Int(nullable: false));
            AlterColumn("dbo.Shows", "YearStart", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shows", "YearStart", c => c.Int());
            AlterColumn("dbo.Shows", "Episodes", c => c.Int());
            AlterColumn("dbo.Shows", "Seasons", c => c.Int());
        }
    }
}
