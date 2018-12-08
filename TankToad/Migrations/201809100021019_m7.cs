namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Data", "SMSId", c => c.Int(nullable: true));
            AddColumn("dbo.DeviceAttributes", "SMSId", c => c.Int(nullable: true));
            AddColumn("dbo.DeviceAttributesLogs", "SMSId", c => c.Int(nullable: true));
            AddColumn("dbo.Diagnostics", "SMSId", c => c.Int(nullable: true));
            CreateIndex("dbo.Data", "SMSId");
            CreateIndex("dbo.DeviceAttributes", "SMSId");
            CreateIndex("dbo.DeviceAttributesLogs", "SMSId");
            CreateIndex("dbo.Diagnostics", "SMSId");
            AddForeignKey("dbo.DeviceAttributes", "SMSId", "dbo.SMS", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Data", "SMSId", "dbo.SMS", "Id", cascadeDelete: false);
            AddForeignKey("dbo.DeviceAttributesLogs", "SMSId", "dbo.SMS", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Diagnostics", "SMSId", "dbo.SMS", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diagnostics", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.DeviceAttributesLogs", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.Data", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.DeviceAttributes", "SMSId", "dbo.SMS");
            DropIndex("dbo.Diagnostics", new[] { "SMSId" });
            DropIndex("dbo.DeviceAttributesLogs", new[] { "SMSId" });
            DropIndex("dbo.DeviceAttributes", new[] { "SMSId" });
            DropIndex("dbo.Data", new[] { "SMSId" });
            DropColumn("dbo.Diagnostics", "SMSId");
            DropColumn("dbo.DeviceAttributesLogs", "SMSId");
            DropColumn("dbo.DeviceAttributes", "SMSId");
            DropColumn("dbo.Data", "SMSId");
        }
    }
}
