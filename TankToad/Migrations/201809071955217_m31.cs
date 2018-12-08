namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeviceAttributes", "IMEI", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeviceAttributes", "IMEI", c => c.Int(nullable: false));
        }
    }
}
