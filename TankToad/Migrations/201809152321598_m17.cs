namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSpecificConstants", "GraceWindow", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "WM_SystemVoltage", c => c.Double(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "WM_ADCscale", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "WM_ReportScale", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "Default_SystemVoltage", c => c.Double(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "Default_ADCscale", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "Default_ReportScale", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceSpecificConstants", "Master_MeasurementType", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Master_Units", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Operator_MeasurementType", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Operator_Units", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Pressure_MeasurementType", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Pressure_Units", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Default_MeasurementType", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Default_Units", c => c.String());
            DropColumn("dbo.DeviceSpecificConstants", "Name");
            DropColumn("dbo.DeviceSpecificConstants", "Value");
            DropColumn("dbo.DeviceSpecificConstants", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceSpecificConstants", "Type", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Value", c => c.String());
            AddColumn("dbo.DeviceSpecificConstants", "Name", c => c.String());
            DropColumn("dbo.DeviceSpecificConstants", "Default_Units");
            DropColumn("dbo.DeviceSpecificConstants", "Default_MeasurementType");
            DropColumn("dbo.DeviceSpecificConstants", "Pressure_Units");
            DropColumn("dbo.DeviceSpecificConstants", "Pressure_MeasurementType");
            DropColumn("dbo.DeviceSpecificConstants", "Operator_Units");
            DropColumn("dbo.DeviceSpecificConstants", "Operator_MeasurementType");
            DropColumn("dbo.DeviceSpecificConstants", "Master_Units");
            DropColumn("dbo.DeviceSpecificConstants", "Master_MeasurementType");
            DropColumn("dbo.DeviceSpecificConstants", "Default_ReportScale");
            DropColumn("dbo.DeviceSpecificConstants", "Default_ADCscale");
            DropColumn("dbo.DeviceSpecificConstants", "Default_SystemVoltage");
            DropColumn("dbo.DeviceSpecificConstants", "WM_ReportScale");
            DropColumn("dbo.DeviceSpecificConstants", "WM_ADCscale");
            DropColumn("dbo.DeviceSpecificConstants", "WM_SystemVoltage");
            DropColumn("dbo.DeviceSpecificConstants", "GraceWindow");
        }
    }
}
