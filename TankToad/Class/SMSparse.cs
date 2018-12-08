using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TankToad.Models;
using System.Text.RegularExpressions;
using TankToad.Controllers;

namespace TankToad.Class
{
    public class SMSparse
    {
        #region Var
        private Diagnostics _diagnostics;
        public Diagnostics Diagnostics { get { return _diagnostics; } }

        private List<Data> _data;
        public List<Data> Data { get { return _data; }}

        private List<string> _error;
        public List<string> ErrorList { get { return _error; }}

        private DeviceAttributes _editedDevice;
        public DeviceAttributes EditedDevice { get { return _editedDevice; } }

        private SMS _sms;

        private DeviceAttributes _device;
        
        
        private string measurementType;
        private string units;
        private int graceWindow;
        private int reportScale;
        private double systemVoltage;
        private int ADCscale;
        private double actualVoltage;
        #endregion

        private void InitDSC()
        {
            TankToadContext td = new TankToadContext();
            var DSC = td.DeviceSpecificConstants.FirstOrDefault();

            if (_device.HardwareVersion== "WM5E"||
                _device.HardwareVersion== "WM5F"||
                _device.HardwareVersion== "WM5G")
            {
                systemVoltage = DSC.WM_SystemVoltage;
                ADCscale = DSC.WM_ADCscale;
                reportScale = DSC.WM_ReportScale;
            }
            //Default
            else
            {
                systemVoltage = DSC.Default_SystemVoltage;
                ADCscale = DSC.Default_ADCscale;
                reportScale = DSC.Default_ReportScale;
            }

            //units
            if (_device.FirmwareName == "WM5E-Firmware")
            {
                if (_device.FirmwareBranch == "Master")
                {
                    measurementType = DSC.Master_MeasurementType;
                    units = DSC.Master_Units;
                }else
                if (_device.FirmwareBranch == "Operator")
                {
                    measurementType = DSC.Operator_MeasurementType;
                    units = DSC.Operator_Units;
                }
                else
                if (_device.FirmwareBranch == "Pressure")
                {
                    measurementType = DSC.Pressure_MeasurementType;
                    units = DSC.Pressure_Units;
                }
                else
                {
                    measurementType = DSC.Default_MeasurementType;
                    units = DSC.Default_Units;
                }
            }
            //Default
            else
            {
                measurementType = DSC.Default_MeasurementType;
                units = DSC.Default_Units;
            }

            //Grace Window
            graceWindow = DSC.GraceWindow;

        }

        public SMSparse(SMS sms)
        {
            TankToadContext td = new TankToadContext();

            _sms = sms;
            _error = new List<string>();
            _diagnostics = new Diagnostics();
            _data = new List<Data>();
            _device = td.DeviceAttributes
                .Where(d => d.CellNumber == sms.From)
                .FirstOrDefault();
            if (_device == null)
            {
                ErrorList.Add("No device");
                return;
            }

            InitDSC();
            
            Regex DATregex = new Regex(@"^DAT:");
            if (DATregex.Matches(_sms.Body).Count > 0)
                DAT();
            Regex RPTregex = new Regex(@"^RPT:");
            if (RPTregex.Matches(_sms.Body).Count > 0)
                RPT();

            bool ifDAT = DATregex.Matches(_sms.Body).Count > 0;
            bool ifRPT = RPTregex.Matches(_sms.Body).Count > 0;
            if (!(ifRPT || ifDAT))
            {
                ErrorList.Add("SMS type");
            }
        }

        private void DAT()
        {
            _diagnostics.DeviceAttributesId = _device.Id;
            _diagnostics.SMSId = _sms.Id;
            DateTime messageDate;
            
            Regex lines = new Regex(@"^(.+)$", RegexOptions.Multiline);
            var linesMatch = lines.Matches(_sms.Body);

            string head = linesMatch[0].Value;

            //Device type
            var deviceTypeReg1 = (new Regex(@":(\w+):"));
            var deviceTypeReg1Match = deviceTypeReg1.Matches(head);
            if (deviceTypeReg1Match.Count == 0)
            {
                _error.Add("device type");
                return;
            }
            string deviceTypeReg1Var = deviceTypeReg1Match[0].Value;
            _diagnostics.DeviceType = (new Regex(@"\w+")).Matches(deviceTypeReg1Var)[0].Value;

            //UP - the uptime number (daily cycle count) 
            var UPreg1 = (new Regex(@":UP(\d+)[A-Z]"));
            var UPmatch = UPreg1.Matches(head);
            if (UPmatch.Count == 0)
            {
                _error.Add("UP");
                return;
            }
                var UPvar = UPmatch[0].Value;
                string UPreg2 = (new Regex(@"\d+")).Matches(UPvar)[0].Value;
                _diagnostics.UptimeCount = int.Parse(UPreg2);

            //TM - the base message date, local time
            var BaseDateReg1 = (new Regex(@"TM(\d+)"));
            var BaseDateMatch = BaseDateReg1.Matches(head);
            if (BaseDateMatch.Count == 0)
            {
                _error.Add("TM");
                return;
            }

            string BaseDateVar = BaseDateMatch[0].Value;
            string BaseDateReg2 = (new Regex(@"\d+")).Matches(BaseDateVar)[0].Value;
            if (BaseDateReg2.Length != 10)
            {
                _error.Add("TM_length");
                return;
            }

            int year = int.Parse("20" + BaseDateReg2.Substring(0, 2));
            int month = int.Parse(BaseDateReg2.Substring(2, 2));
            int day = int.Parse(BaseDateReg2.Substring(4, 2));
            int hour = int.Parse(BaseDateReg2.Substring(6, 2));
            int minute = int.Parse(BaseDateReg2.Substring(8, 2));
            messageDate = new DateTime(year, month, day, hour, minute, 0);

            //Report time difference from desired local report time (minutes)
            _diagnostics.ReportTime = _sms.DateReceiving;

            if (String.IsNullOrWhiteSpace(_device.CurrentLocationTimeZone))
            {
                _error.Add("CurrentLocationTimeZone");
                return;
            }
            var timeDif = _diagnostics.ReportTime.AddHours(int.Parse(_device.CurrentLocationTimeZone))
                - messageDate;
            _diagnostics.TimeDifferenceFromDesiredLocalReportTime = (int)timeDif.TotalMinutes;


            //DATA
            if (linesMatch.Count <= 1)
            {
                _error.Add("Body");
                return;
            }
            string body = linesMatch[1].Value;

            if (body.Length % 7 != 0)
            {
                _error.Add("BodyLength");
                return;
            }

            var dataReg = (new Regex(@"\w{7}"));
            var dataMatch = dataReg.Matches(body);

            if (dataMatch.Count != body.Length/7)
            {
                _error.Add("Data");
                return;
            }

            if (!(measurementType == "bottom to top" || measurementType == "top to bottom"))
            {
                ErrorList.Add("DSC measurement type");
                return;
            }
            /*if (!(units == "ft" || units == "in"))
            {
                ErrorList.Add("DSC units");
                return;
            }*/

            actualVoltage = _device.BatteryLowLevel / (double)ADCscale;
     
     
            foreach (Match data in dataMatch)
            {
                string minutesStr ="0x"+ data.Value.Substring(0, 3);
                int mint = Convert.ToInt32(minutesStr, 16);
                DateTime timestamp = messageDate.AddMinutes(-mint);

                string waterLevelStr = "0x" + data.Value.Substring(3, 2);
                int waterL = Convert.ToInt32(waterLevelStr, 16);
                /*if (units == "ft")
                {
                    waterL = waterL / 12;
                }*/

                string batteryLevelStr = "0x" + data.Value.Substring(5, 2);
                int batteryL = Convert.ToInt32(batteryLevelStr, 16);

                _data.Add(new Data()
                {
                    Timestamp = timestamp,
                    BatteryLevel = batteryL,
                    WaterLevel = waterL,
                    DeviceAttributesId = _device.Id,
                    SMSId = _sms.Id
                });
                
                if (measurementType == "bottom to top" && waterL < _device.WaterLowLevel)
                    _diagnostics.LowLevel = "!";
                if (measurementType == "top to bottom" && waterL > _device.WaterLowLevel)
                    _diagnostics.LowLevel = "!";

                if (waterL == 0)
                    _diagnostics.Zeros = "!";

                if (_diagnostics.TimeDifferenceFromDesiredLocalReportTime > graceWindow)
                    _diagnostics.GraceWindowExceeded = "!";

                if ((double)batteryL/(double)reportScale < actualVoltage)
                    _diagnostics.LowBattery = "!";
            }

            _data.Reverse();
        }

        private void RPT()
        {
            TankToadContext td = new TankToadContext();

            var device = td.DeviceAttributes
                .Where(d => d.CellNumber == _sms.From)
                .FirstOrDefault();
            if(device == null)
            {
                ErrorList.Add("No Device");
                return;
            }

            Regex lines = new Regex(@"^(.+)$", RegexOptions.Multiline);
            var linesMatch = lines.Matches(_sms.Body);
            if (linesMatch.Count <= 1)
            {
                ErrorList.Add("No Body");
                return;
            }

            var body = linesMatch[1].Value;

            //user number
            var userNumberReg0 = (new Regex(@"ADD:\+(\d+)\D"));
            var userNumberStr0 = userNumberReg0.Match(body).Value;          
            var userNumberReg1 = (new Regex(@"\+(\d+)"));
            var userNumber = userNumberReg1.Match(userNumberStr0).Value;
            if (userNumber.Length == 0)
            {
                _error.Add("user number");
                return;
            }
            device.CustomerPhoneNumber = userNumber;

            //gateway number
            var gatewayNumberReg0 = (new Regex(@"ADM:\+(\d+)\D"));
            var gatewayNumberStr0 = gatewayNumberReg0.Match(body).Value;
            var gatewayNumberReg1 = (new Regex(@"\+(\d+)"));
            var gatewayNumber = gatewayNumberReg1.Match(gatewayNumberStr0).Value;
            if (gatewayNumber.Length == 0)
            {
                _error.Add("gateway number");
                return;
            }
            device.GatewayPhoneNumber = gatewayNumber;

            //WLL
            var WLL_Reg0 = (new Regex(@"WLL:(\d+)\D"));
            var WLL_Str0 = WLL_Reg0.Match(body).Value;
            var WLL_Reg1 = (new Regex(@"(\d+)"));
            var WLL_str1 = WLL_Reg1.Match(WLL_Str0).Value;
            if (WLL_str1.Length == 0)
            {
                _error.Add("WLL");
                return;
            }
            int WLL = int.Parse(WLL_str1); 
            device.WaterLowLevel = WLL;

            //WHL
            var WHL_Reg0 = (new Regex(@"WHL:(\d+)\D"));
            var WHL_Str0 = WHL_Reg0.Match(body).Value;
            var WHL_Reg1 = (new Regex(@"(\d+)"));
            var WHL_str1 = WHL_Reg1.Match(WHL_Str0).Value;
            if (WHL_str1.Length == 0)
            {
                _error.Add("WHL");
                return;
            }
            int WHL = int.Parse(WHL_str1);
            device.WaterHighLevel = WHL;

            //BLL
            var BLL_Reg0 = (new Regex(@"BLL:(\d+)\D"));
            var BLL_Str0 = BLL_Reg0.Match(body).Value;
            var BLL_Reg1 = (new Regex(@"(\d+)"));
            var BLL_str1 = BLL_Reg1.Match(BLL_Str0).Value;
            if (BLL_str1.Length == 0)
            {
                _error.Add("BLL");
                return;
            }
            int BLL = int.Parse(BLL_str1);
            device.BatteryLowLevel = BLL;

            //BSL
            var BSL_Reg0 = (new Regex(@"BSL:(\d+)\D"));
            var BSL_Str0 = BSL_Reg0.Match(body).Value;
            var BSL_Reg1 = (new Regex(@"(\d+)"));
            var BSL_str1 = BSL_Reg1.Match(BSL_Str0).Value;
            if (BSL_str1.Length == 0)
            {
                _error.Add("BSL");
                return;
            }
            int BSL = int.Parse(BSL_str1);
            device.BatteryShutdownLevel = BSL;

            //BTL
            var BTL_Reg0 = (new Regex(@"BTL:(\d+)\D"));
            var BTL_Str0 = BTL_Reg0.Match(body).Value;
            var BTL_Reg1 = (new Regex(@"(\d+)"));
            var BTL_str1 = BTL_Reg1.Match(BTL_Str0).Value;
            if (BTL_str1.Length == 0)
            {
                _error.Add("BTL");
                return;
            }
            int BTL = int.Parse(BTL_str1);
            device.BatteryTopLevel = BTL;

            //TOA
            var TOA_Reg0 = (new Regex(@"TOA:(\d+)/(\d+)/(\d+):(\d+):(\d+)\D"));
            var TOA_Str0 = TOA_Reg0.Match(body).Value;
            var TOA_Reg1 = (new Regex(@":(\d+):(\d+)\D"));
            var TOA_str1 = TOA_Reg1.Match(TOA_Str0).Value;
            var TOA_Reg2 = (new Regex(@"(\d+):(\d+)"));
            var TOA_str2 = TOA_Reg2.Match(TOA_str1).Value;
            if (TOA_str2.Length == 0)
            {
                _error.Add("TOA");
                return;
            }
            device.TimeOfAlert = TOA_str2;

            //CSP
            var CSP_Reg0 = (new Regex(@"CSP:(\d+)\D"));
            var CSP_Str0 = CSP_Reg0.Match(body).Value;
            var CSP_Reg1 = (new Regex(@"(\d+)"));
            var CSP_str1 = CSP_Reg1.Match(CSP_Str0).Value;
            if (CSP_str1.Length == 0)
            {
                _error.Add("CSP");
                return;
            }
            int CSP = int.Parse(CSP_str1);
            device.SleepPeriod = CSP;

            //ANS
            var ANS_Reg0 = (new Regex(@"ANS:(\d+)\D"));
            var ANS_Str0 = ANS_Reg0.Match(body).Value;
            var ANS_Reg1 = (new Regex(@"(\d+)"));
            var ANS_str1 = ANS_Reg1.Match(ANS_Str0).Value;
            if (ANS_str1.Length == 0)
            {
                _error.Add("ANS");
                return;
            }
            int ANS = int.Parse(ANS_str1);
            device.NumberOfSleepPeriods = ANS;

            //CEM
            var CEM_Reg0 = (new Regex(@"CEM:(\d+)\D"));
            var CEM_Str0 = CEM_Reg0.Match(body).Value;
            var CEM_Reg1 = (new Regex(@"(\d+)"));
            var CEM_str1 = CEM_Reg1.Match(CEM_Str0).Value;
            if (CEM_str1.Length == 0)
            {
                _error.Add("CEM");
                return;
            }
            int CEM = int.Parse(CEM_str1);
            device.EnergyMode = CEM;

            //COM
            var COM_Reg0 = (new Regex(@"COM:(\d+)\D"));
            var COM_Str0 = COM_Reg0.Match(body).Value;
            var COM_Reg1 = (new Regex(@"(\d+)"));
            var COM_str1 = COM_Reg1.Match(COM_Str0).Value;
            if (COM_str1.Length == 0)
            {
                _error.Add("COM");
                return;
            }
            int COM = int.Parse(COM_str1);
            device.OperationMode = COM;

            //VFC
            var VFC_Reg0 = (new Regex(@"VFC:(\d+)$"));
            var VFC_Str0 = VFC_Reg0.Match(body).Value;
            var VFC_Reg1 = (new Regex(@"(\d+)"));
            var VFC_str1 = VFC_Reg1.Match(VFC_Str0).Value;
            if (VFC_str1.Length == 0)
            {
                _error.Add("VFC");
                return;
            }
            int VFC = int.Parse(VFC_str1);
            device.VoltageFeedback = VFC;

            //smsId
            device.SMSId = _sms.Id;

            device.UpdateDate = DateTime.UtcNow;

            _editedDevice = device;
        }
    }
}