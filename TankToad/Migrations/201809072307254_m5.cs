namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceAttributesLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceAttributesId = c.Int(nullable: false),
                        Name = c.String(),
                        IMEI = c.Int(),
                        CellNumber = c.String(),
                        SIMtype = c.String(),
                        SIMnumber = c.String(),
                        HardwareVersion = c.String(),
                        FirmwareName = c.String(),
                        FirmwareBranch = c.String(),
                        FirmwareCommit = c.String(),
                        CurrentAssignedCustomerName = c.String(),
                        Status = c.String(),
                        CurrentLocationLatitude = c.String(),
                        CurrentLocationLongitude = c.String(),
                        CurrentLocationTimeZone = c.String(),
                        Operator = c.String(),
                        SignalQuality = c.String(),
                        CustomerPhoneNumber = c.String(),
                        GatewayPhoneNumber = c.String(),
                        WaterLowLevel = c.Int(nullable: false),
                        WaterHighLevel = c.Int(nullable: false),
                        BatteryLowLevel = c.Int(nullable: false),
                        BatteryShutdownLevel = c.Int(nullable: false),
                        BatteryTopLevel = c.Int(nullable: false),
                        TimeOfAlert = c.String(),
                        SleepPeriod = c.Int(nullable: false),
                        NumberOfSleepPeriods = c.Int(nullable: false),
                        EnergyMode = c.Int(nullable: false),
                        OperationMode = c.Int(nullable: false),
                        VoltageFeedback = c.Int(nullable: false),
                        NotesAboutTheDevice = c.String(),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceAttributes", t => t.DeviceAttributesId, cascadeDelete: true)
                .Index(t => t.DeviceAttributesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceAttributesLogs", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropIndex("dbo.DeviceAttributesLogs", new[] { "DeviceAttributesId" });
            DropTable("dbo.DeviceAttributesLogs");
        }
    }
}
