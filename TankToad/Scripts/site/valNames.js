var _ValNames = {
    DeviceAtrributes: {
        Array: [{ value: "Id", text: "Id" },
        { value: "Name", text: "Name" },
        { value: "CellNumber", text: "Cell number" },
        { value: "IMEI", text: "IMEI" },
        { value: "SIMtype", text: "SIM type" },
        { value: "SIMnumber", text: "SIM number" },
        { value: "HardwareVersion", text: "Hardware version" },
        { value: "FirmwareName", text: "Firmware name" },
        { value: "FirmwareBranch", text: "Firmware branch" },
        { value: "FirmwareCommit", text: "Firmware commit" },
        { value: "CurrentAssignedCustomerName", text: "Current assigned customer name" },
        { value: "Status", text: "Status" },
        { value: "CurrentLocationLatitude", text: "Current location latitude" },
        { value: "CurrentLocationLongitude", text: "Current location longitude" },
        { value: "CurrentLocationTimeZone", text: "Current location time zone" },
        { value: "Operator", text: "Operator" },
        { value: "SignalQuality", text: "Signal quality" },
        { value: "CustomerPhoneNumber", text: "Customer phone number" },
        { value: "GatewayPhoneNumber", text: "Gateway phone number" },
        { value: "WaterLowLevel", text: "Water low level" },
        { value: "WaterHighLevel", text: "Water high level" },
        { value: "BatteryLowLevel", text: "Battery low level" },
        { value: "BatteryShutdownLevel", text: "Battery shutdown level" },
        { value: "BatteryTopLevel", text: "Battery top level" },
        { value: "TimeOfAlert", text: "Time of alert" },
        { value: "SleepPeriod", text: "Sleep period" },
        { value: "NumberOfSleepPeriods", text: "Number of sleep periods" },
        { value: "EnergyMode", text: "Energy mode" },
        { value: "OperationMode", text: "Operation mode" },
        { value: "VoltageFeedback", text: "Voltage feedback" },
        { value: "NotesAboutTheDevice", text: "Notes about the device" },
            { value: "UpdateDate", text: "Update date" },
            { value: "SMSId", text: "SMS Id" }],

        GetNamesVal: () => {
            let namesVal = [];
            _ValNames.DeviceAtrributes.Array.forEach((d) => {
                namesVal.push(d['value']);
            })
            return namesVal;
        },
        GetNamesTxt: () => {
            let namesTxt = [];
            _ValNames.DeviceAtrributes.Array.forEach((d) => {
                namesTxt.push(d['text']);
            })
            return namesTxt;
        }

    },

    DeviceAtrributesLog: {
        Array: [
            { value: "Id", text: "Id" },
            { value: "UpdateDate", text: "Update date" },
            { value: "DeviceAttributesId", text: "Device attributes Id" },
            { value: "Name", text: "Name" },
            { value: "CellNumber", text: "Cell number" },
            { value: "IMEI", text: "IMEI" },
            { value: "SIMtype", text: "SIM type" },
            { value: "SIMnumber", text: "SIM number" },
            { value: "HardwareVersion", text: "Hardware version" },
            { value: "FirmwareName", text: "Firmware name" },
            { value: "FirmwareBranch", text: "Firmware branch" },
            { value: "FirmwareCommit", text: "Firmware commit" },
            { value: "CurrentAssignedCustomerName", text: "Current assigned customer name" },
            { value: "Status", text: "Status" },
            { value: "CurrentLocationLatitude", text: "Current location latitude" },
            { value: "CurrentLocationLongitude", text: "Current location longitude" },
            { value: "CurrentLocationTimeZone", text: "Current location time zone" },
            { value: "Operator", text: "Operator" },
            { value: "SignalQuality", text: "Signal quality" },
            { value: "CustomerPhoneNumber", text: "Customer phone number" },
            { value: "GatewayPhoneNumber", text: "Gateway phone number" },
            { value: "WaterLowLevel", text: "Water low level" },
            { value: "WaterHighLevel", text: "Water high level" },
            { value: "BatteryLowLevel", text: "Battery low level" },
            { value: "BatteryShutdownLevel", text: "Battery shutdown level" },
            { value: "BatteryTopLevel", text: "Battery top level" },
            { value: "TimeOfAlert", text: "Time of alert" },
            { value: "SleepPeriod", text: "Sleep period" },
            { value: "NumberOfSleepPeriods", text: "Number of sleep periods" },
            { value: "EnergyMode", text: "Energy mode" },
            { value: "OperationMode", text: "Operation mode" },
            { value: "VoltageFeedback", text: "Voltage feedback" },
            { value: "NotesAboutTheDevice", text: "Notes about the device" },
            { value: "SMSId", text: "SMS Id" }
        ],

        GetNamesVal: () => {
            let namesVal = [];
            _ValNames.DeviceAtrributes.Array.forEach((d) => {
                namesVal.push(d['value']);
            })
            return namesVal;
        },
        GetNamesTxt: () => {
            let namesTxt = [];
            _ValNames.DeviceAtrributes.Array.forEach((d) => {
                namesTxt.push(d['text']);
            })
            return namesTxt;
        }

    }
}