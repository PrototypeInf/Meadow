namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Data", "DeviceAttributesName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Data", "DeviceAttributesName");
        }
    }
}
