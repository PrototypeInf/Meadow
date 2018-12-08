namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeviceAttributes", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.DeviceAttributes", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.DeviceAttributes", new[] { "Name" });
            AlterColumn("dbo.DeviceAttributes", "Name", c => c.String());
        }
    }
}
