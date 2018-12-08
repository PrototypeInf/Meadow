namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DeviceAttributes", new[] { "Name" });
            AlterColumn("dbo.DeviceAttributes", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeviceAttributes", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.DeviceAttributes", "Name", unique: true);
        }
    }
}
