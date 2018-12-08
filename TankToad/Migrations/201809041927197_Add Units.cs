namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSpecificConstants", "Units", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceSpecificConstants", "Units");
        }
    }
}
