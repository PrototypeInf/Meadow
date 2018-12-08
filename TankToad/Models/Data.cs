using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TankToad.Models;

namespace TankToad.Models
{
    public partial class Data
    {
        public int Id { get; set; }
        public int? DeviceAttributesId { get; set; }
        public DeviceAttributes DeviceAttributes { get; set; }
        public int? SMSId { get; set; }
        public SMS SMS { get; set; }
        public DateTime Timestamp { get; set; }
        public int WaterLevel { get; set; }
        public int BatteryLevel { get; set; }
    }
}