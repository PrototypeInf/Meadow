namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Data",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceAttributesId = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        WaterLevel = c.Int(nullable: false),
                        BatteryLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceAttributes", t => t.DeviceAttributesId, cascadeDelete: true)
                .Index(t => t.DeviceAttributesId);
            
            CreateTable(
                "dbo.DeviceAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IMEI = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceSpecificConstants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HardwareVersion = c.String(),
                        SoftwareSpecific = c.String(),
                        Branch = c.String(),
                        SystemVoltage = c.Double(nullable: false),
                        ADCscale = c.Int(nullable: false),
                        ReportScale = c.Int(nullable: false),
                        MeasurementType = c.String(),
                        GraceWindow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Diagnostics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceAttributesId = c.Int(nullable: false),
                        DeviceType = c.String(),
                        ReportTime = c.DateTime(nullable: false),
                        UptimeCount = c.Int(nullable: false),
                        TimeDifferenceFromDesiredLocalReportTime = c.Int(nullable: false),
                        LowLevel = c.Boolean(nullable: false),
                        Zeros = c.Boolean(nullable: false),
                        GraceWindowExceeded = c.Boolean(nullable: false),
                        LowBattery = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceAttributes", t => t.DeviceAttributesId, cascadeDelete: true)
                .Index(t => t.DeviceAttributesId);
            
            CreateTable(
                "dbo.SMS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateReceiving = c.DateTime(nullable: false),
                        Status = c.String(),
                        DateSent = c.DateTime(),
                        DateUpdated = c.DateTime(),
                        DateCreated = c.DateTime(),
                        Body = c.String(),
                        ApiVersion = c.String(),
                        AccountSid = c.String(),
                        ErrorMessage = c.String(),
                        From = c.String(),
                        MessagingServiceSid = c.String(),
                        NumMedia = c.String(),
                        NumSegments = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PriceUnit = c.String(),
                        Sid = c.String(),
                        ErrorCode = c.Int(),
                        To = c.String(),
                        Uri = c.String(),
                        Direction = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diagnostics", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropForeignKey("dbo.Data", "DeviceAttributesId", "dbo.DeviceAttributes");
            DropIndex("dbo.Diagnostics", new[] { "DeviceAttributesId" });
            DropIndex("dbo.Data", new[] { "DeviceAttributesId" });
            DropTable("dbo.SMS");
            DropTable("dbo.Diagnostics");
            DropTable("dbo.DeviceSpecificConstants");
            DropTable("dbo.DeviceAttributes");
            DropTable("dbo.Data");
        }
    }
}
