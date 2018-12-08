namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Data", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropForeignKey("dbo.Data", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.DeviceAttributes", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.DeviceAttributesLogs", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.Diagnostics", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropForeignKey("dbo.Diagnostics", "SMSId", "dbo.SMS");
            DropIndex("dbo.Data", new[] { "DeviceAttributesId" });
            DropIndex("dbo.Data", new[] { "SMSId" });
            DropIndex("dbo.DeviceAttributes", new[] { "SMSId" });
            DropIndex("dbo.DeviceAttributesLogs", new[] { "SMSId" });
            DropIndex("dbo.Diagnostics", new[] { "DeviceAttributesId" });
            DropIndex("dbo.Diagnostics", new[] { "SMSId" });
            AlterColumn("dbo.Data", "DeviceAttributesId", c => c.Int());
            AlterColumn("dbo.Data", "SMSId", c => c.Int());
            AlterColumn("dbo.DeviceAttributes", "SMSId", c => c.Int());
            AlterColumn("dbo.DeviceAttributesLogs", "SMSId", c => c.Int());
            AlterColumn("dbo.Diagnostics", "DeviceAttributesId", c => c.Int());
            AlterColumn("dbo.Diagnostics", "SMSId", c => c.Int());
            CreateIndex("dbo.Data", "DeviceAttributesId");
            CreateIndex("dbo.Data", "SMSId");
            CreateIndex("dbo.DeviceAttributes", "SMSId");
            CreateIndex("dbo.DeviceAttributesLogs", "SMSId");
            CreateIndex("dbo.Diagnostics", "DeviceAttributesId");
            CreateIndex("dbo.Diagnostics", "SMSId");
            AddForeignKey("dbo.Data", "DeviceAttributesId", "dbo.DeviceAttributes", "Id");
            AddForeignKey("dbo.Data", "SMSId", "dbo.SMS", "Id");
            AddForeignKey("dbo.DeviceAttributes", "SMSId", "dbo.SMS", "Id");
            AddForeignKey("dbo.DeviceAttributesLogs", "SMSId", "dbo.SMS", "Id");
            AddForeignKey("dbo.Diagnostics", "DeviceAttributesId", "dbo.DeviceAttributes", "Id");
            AddForeignKey("dbo.Diagnostics", "SMSId", "dbo.SMS", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diagnostics", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.Diagnostics", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropForeignKey("dbo.DeviceAttributesLogs", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.DeviceAttributes", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.Data", "SMSId", "dbo.SMS");
            DropForeignKey("dbo.Data", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropIndex("dbo.Diagnostics", new[] { "SMSId" });
            DropIndex("dbo.Diagnostics", new[] { "DeviceAttributesId" });
            DropIndex("dbo.DeviceAttributesLogs", new[] { "SMSId" });
            DropIndex("dbo.DeviceAttributes", new[] { "SMSId" });
            DropIndex("dbo.Data", new[] { "SMSId" });
            DropIndex("dbo.Data", new[] { "DeviceAttributesId" });
            AlterColumn("dbo.Diagnostics", "SMSId", c => c.Int(nullable: false));
            AlterColumn("dbo.Diagnostics", "DeviceAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.DeviceAttributesLogs", "SMSId", c => c.Int(nullable: false));
            AlterColumn("dbo.DeviceAttributes", "SMSId", c => c.Int(nullable: false));
            AlterColumn("dbo.Data", "SMSId", c => c.Int(nullable: false));
            AlterColumn("dbo.Data", "DeviceAttributesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Diagnostics", "SMSId");
            CreateIndex("dbo.Diagnostics", "DeviceAttributesId");
            CreateIndex("dbo.DeviceAttributesLogs", "SMSId");
            CreateIndex("dbo.DeviceAttributes", "SMSId");
            CreateIndex("dbo.Data", "SMSId");
            CreateIndex("dbo.Data", "DeviceAttributesId");
            AddForeignKey("dbo.Diagnostics", "SMSId", "dbo.SMS", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Diagnostics", "DeviceAttributesId", "dbo.DeviceAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DeviceAttributesLogs", "SMSId", "dbo.SMS", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DeviceAttributes", "SMSId", "dbo.SMS", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Data", "SMSId", "dbo.SMS", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Data", "DeviceAttributesId", "dbo.DeviceAttributes", "Id", cascadeDelete: true);
        }
    }
}
