using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TankToad.Models
{
    public class Diagnostics
    {
        public int Id { get; set; }
        public int? DeviceAttributesId { get; set; }
        public DeviceAttributes DeviceAttributes { get; set; }
        public int? SMSId { get; set; }
        public SMS SMS { get; set; }
        public string DeviceType { get; set; }
        public DateTime ReportTime  { get; set; }
        public int UptimeCount { get; set; }
        public int TimeDifferenceFromDesiredLocalReportTime { get; set; }
        public string LowLevel { get; set; }
        public string Zeros { get; set; }
        public string GraceWindowExceeded { get; set; }
        public string LowBattery { get; set; }
    }
}