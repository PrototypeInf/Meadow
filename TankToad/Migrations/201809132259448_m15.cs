namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Data", "DeviceAttributesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Data", "DeviceAttributesName", c => c.String());
        }
    }
}
