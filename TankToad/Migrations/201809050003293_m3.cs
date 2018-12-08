namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSpecificConstants", "Name", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Value", c => c.String());
            DropColumn("dbo.DeviceSpecificConstants", "HardwareVersion");
            DropColumn("dbo.DeviceSpecificConstants", "SoftwareSpecific");
            DropColumn("dbo.DeviceSpecificConstants", "Branch");
            DropColumn("dbo.DeviceSpecificConstants", "SystemVoltage");
            DropColumn("dbo.DeviceSpecificConstants", "ADCscale");
            DropColumn("dbo.DeviceSpecificConstants", "ReportScale");
            DropColumn("dbo.DeviceSpecificConstants", "MeasurementType");
            DropColumn("dbo.DeviceSpecificConstants", "GraceWindow");
            DropColumn("dbo.DeviceSpecificConstants", "Units");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceSpecificConstants", "Units", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "GraceWindow", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "MeasurementType", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "ReportScale", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "ADCscale", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "SystemVoltage", c => c.Double(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "Branch", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "SoftwareSpecific", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "HardwareVersion", c => c.String());
            DropColumn("dbo.DeviceSpecificConstants", "Value");
            DropColumn("dbo.DeviceSpecificConstants", "Name");
        }
    }
}
