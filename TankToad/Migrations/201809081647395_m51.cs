namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m51 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeviceAttributes", "IMEI", c => c.String());
            AlterColumn("dbo.DeviceAttributesLogs", "IMEI", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeviceAttributesLogs", "IMEI", c => c.Int());
            AlterColumn("dbo.DeviceAttributes", "IMEI", c => c.Int());
        }
    }
}
