using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TankToad.Models
{
    public class DeviceSpecificConstants
    {
        public int Id { get; set; }

        public int GraceWindow { get; set; }//120
        public double WM_SystemVoltage { get; set; }//4.92
        public int WM_ADCscale { get; set; }//1024
        public int WM_ReportScale { get; set; }//256
        public double Default_SystemVoltage { get; set; }//4.92
        public int Default_ADCscale { get; set; }//1024
        public int Default_ReportScale { get; set; }//256
        public string Master_MeasurementType { get; set; }//top to bottom
        public string Master_Units { get; set; }//in
        public string Operator_MeasurementType { get; set; }//top to bottom
        public string Operator_Units { get; set; }//in
        public string Pressure_MeasurementType { get; set; }//bottom to top
        public string Pressure_Units { get; set; }//ft
        public string Default_MeasurementType { get; set; }//top to bottom
        public string Default_Units { get; set; }//in
    }
}