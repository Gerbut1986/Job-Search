namespace JobSearch.PL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Experience", c => c.String());
            AddColumn("dbo.AspNetUsers", "Skills", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Skills");
            DropColumn("dbo.AspNetUsers", "Experience");
            DropColumn("dbo.AspNetUsers", "Photo");
        }
    }
}
