var _DeviceAttributesLogRender = {
    mainTableRendered: null,

    /** @type {DateBox}*/
    dateBox: null,

    DateBoxInit: async function (Id, label) {
        this.dateBox = new DateBox(
            Id,
            () => {
                this.MainTable();
            },
            label
        );
        await this.dateBox.Render();
    },

    MainTable: async function () {
        let deleteIconRes = await fetch("/Content/svg/actions/delete.svg");

        let minDateStr = this.dateBox.GetDateMin();
        let maxDateStr = this.dateBox.GetDateMax();

        this.mainTableRendered = $('#mainTable').DataTable({
            destroy: true,
            scrollX: true,
            columnDefs: [
                { width: 200 }
            ],
            processing: true,
            serverSide: true,
            ajax: {
                url: `/DataTables/DeviceAttributesLogs?minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
                type: 'POST'
            },
            rowId: "Id",
            columns: [
                {
                    data: null,
                    defaultContent: `<img data-type = "delete" title = "Delete" src="${deleteIconRes.url}" class = "icon"/>`,
                    title: "Actions",
                    searchable: false,
                    orderable: false,
                    width: "60px"
                },
                {
                    data: "Id",
                    title: "Attribute Id"
                },
                {
                    data: "UpdateDate",
                    title: "Update date",
                    className: "size-big",
                    render: function (data, type, row, meta) {
                        return DateToString(data);
                    }
                },
                {
                    data: "DeviceAttributesId",
                    title: "Device Id"
                },
                {
                    data: "Name",
                    title: "Name"
                },
                {
                    data: "CellNumber",
                    title: "Cell number"
                },
                {
                    data: "IMEI",
                    title: "IMEI"
                },
                {
                    data: "SIMtype",
                    title: "SIM type"
                },
                {
                    data: "SIMnumber",
                    title: "SIM number"
                },
                {
                    data: "HardwareVersion",
                    title: "Hardware version"
                },
                {
                    data: "FirmwareName",
                    title: "Firmware name"
                },
                {
                    data: "FirmwareBranch",
                    title: "Firmware branch"
                },
                {
                    data: "FirmwareCommit",
                    title: "Firmware commit"
                },
                {
                    data: "CurrentAssignedCustomerName",
                    title: "Current assigned customer name"
                },
                {
                    data: "Status",
                    title: "Status"
                },
                {
                    data: "CurrentLocationLatitude",
                    title: "Current location latitude"
                },
                {
                    data: "CurrentLocationLongitude",
                    title: "Current location longitude"
                },
                {
                    data: "CurrentLocationTimeZone",
                    title: "Current location time zone"
                },
                {
                    data: "Operator",
                    title: "Operator"
                },
                {
                    data: "SignalQuality",
                    title: "Signal quality"
                },
                {
                    data: "CustomerPhoneNumber",
                    title: "Customer phone number"
                },
                {
                    data: "GatewayPhoneNumber",
                    title: "Gateway phone number"
                },
                {
                    data: "WaterLowLevel",
                    title: "Water low level"
                },
                {
                    data: "WaterHighLevel",
                    title: "Water high level"
                },
                {
                    data: "BatteryLowLevel",
                    title: "Battery low level"
                },
                {
                    data: "BatteryShutdownLevel",
                    title: "Battery shutdown level"
                },
                {
                    data: "BatteryTopLevel",
                    title: "Battery top level"
                },
                {
                    data: "TimeOfAlert",
                    title: "Time of alert"
                },
                {
                    data: "SleepPeriod",
                    title: "Sleep period"
                },
                {
                    data: "NumberOfSleepPeriods",
                    title: "Number of sleep periods"
                },
                {
                    data: "EnergyMode",
                    title: "Energy mode"
                },
                {
                    data: "OperationMode",
                    title: "Operation mode"
                },
                {
                    data: "VoltageFeedback",
                    title: "Voltage feedback"
                },
                {
                    data: "NotesAboutTheDevice",
                    title: "Notes about the device",
                    className: "size-mega"
                },
                {
                    data: "SMSId",
                    title: "SMS Id"
                }
            ],
            order: [[1, 'desc']]
        });
    }
}