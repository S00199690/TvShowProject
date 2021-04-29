namespace TvShowProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Seasons = c.Int(nullable: false),
                        Episodes = c.Int(nullable: false),
                        YearStart = c.Int(nullable: false),
                        YearEnd = c.Int(),
                        Description = c.String(),
                        ShowImage = c.String(),
                        Genre = c.String(),
                    })
                .PrimaryKey(t => t.ShowID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shows");
        }
    }
}
