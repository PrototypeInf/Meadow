namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangetypeUnits : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeviceSpecificConstants", "Units", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeviceSpecificConstants", "Units", c => c.Int(nullable: false));
        }
    }
}
