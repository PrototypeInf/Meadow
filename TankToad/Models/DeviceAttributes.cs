using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TankToad.Models
{
    public class DeviceAttributes
    {
        public int Id { get; set; }
        public int? SMSId { get; set; }
        public SMS SMS { get; set; }
        public string Name  { get; set; }
        public string IMEI { get; set; }
        public string CellNumber { get; set; }
        public string SIMtype  { get; set; }
        public string SIMnumber { get; set; }
        public string HardwareVersion { get; set; }
        public string FirmwareName { get; set; }
        public string FirmwareBranch { get; set; }
        public string FirmwareCommit { get; set; }
        public string CurrentAssignedCustomerName { get; set; }
        public string Status { get; set; }
        public string CurrentLocationLatitude { get; set; }
        public string CurrentLocationLongitude { get; set; }
        public string CurrentLocationTimeZone  { get; set; }
        public string Operator { get; set; }
        public string SignalQuality { get; set; }
        public string CustomerPhoneNumber  { get; set; }
        public string GatewayPhoneNumber { get; set; }
        public int WaterLowLevel { get; set; }
        public int WaterHighLevel { get; set; }
        public int BatteryLowLevel { get; set; }
        public int BatteryShutdownLevel { get; set; }
        public int BatteryTopLevel { get; set; }
        public string TimeOfAlert { get; set; }
        public int SleepPeriod { get; set; }
        public int NumberOfSleepPeriods { get; set; }
        public int EnergyMode  { get; set; }
        public int OperationMode { get; set; }
        public int VoltageFeedback { get; set; }
        public string NotesAboutTheDevice { get; set; }
        public DateTime UpdateDate { get; set; }

        public DeviceAttributes()
        {
            UpdateDate = DateTime.UtcNow;
        }
    }
}