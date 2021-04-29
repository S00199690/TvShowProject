namespace TvShowProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveYearEnd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Shows", "YearEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shows", "YearEnd", c => c.Int());
        }
    }
}
