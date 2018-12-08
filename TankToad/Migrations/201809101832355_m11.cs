namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMS", "ParseStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMS", "ParseStatus");
        }
    }
}
