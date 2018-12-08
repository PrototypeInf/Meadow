var _DeviceListRender = {
    mainTableRendered: null,

    MainTable: async function () {
        let editIconRes = await fetch("/Content/svg/actions/edit.svg");
        let deleteIconRes = await fetch("/Content/svg/actions/delete.svg");

        this.mainTableRendered = $('#mainTable').DataTable({
            scrollX: true,
            columnDefs: [
                { width: 200 }
            ],
            processing: true,
            serverSide: true,
            ajax: {
                url: "/DataTables/DeviceAttributes",
                type: 'POST'
            },
            rowId: "Id",
            columns: [
            //0
                {
                    data: null,
                    defaultContent: `<img data-type = "delete" title = "Delete" src="${deleteIconRes.url}" class = "icon"/>
                                     <img data-type = "edit" title = "Edit" src="${editIconRes.url}" class = "icon"/>`,
                    title: "Actions",
                    searchable: false,
                    orderable: false,
                    width: "90px",
                    className: "size-norm"
                },
            //1
                {
                    data: "Id",
                    title: "Device Id"
                },
                //2
                {
                    data: "Name",
                    title: "Name"
                },
                //3
                {
                    data: "CellNumber",
                    title: "Cell number"
                },
                //4
                {
                    data: "IMEI",
                    title: "IMEI"
                },
                //5
                {
                    data: "SIMtype",
                    title: "SIM type"
                },
                //6
                {
                    data: "SIMnumber",
                    title: "SIM number"
                },
                //7
                {
                    data: "HardwareVersion",
                    title: "Hardware version"
                },
                //8
                {
                    data: "FirmwareName",
                    title: "Firmware name"
                },
                //9
                {
                    data: "FirmwareBranch",
                    title: "Firmware branch"
                },
                //10
                {
                    data: "FirmwareCommit",
                    title: "Firmware commit"
                },
                //11
                {
                    data: "CurrentAssignedCustomerName",
                    title: "Current assigned customer name"
                },
                //12
                {
                    data: "Status",
                    title: "Status"
                },
                //13
                {
                    data: "CurrentLocationLatitude",
                    title: "Current location latitude"
                },
                //14
                {
                    data: "CurrentLocationLongitude",
                    title: "Current location longitude"
                },
                //15
                {
                    data: "CurrentLocationTimeZone",
                    title: "Current location time zone"
                },
                //16
                {
                    data: "Operator",
                    title: "Operator"
                },
                //17
                {
                    data: "SignalQuality",
                    title: "Signal quality"
                },
                //18
                {
                    data: "CustomerPhoneNumber",
                    title: "Customer phone number"
                },
                //19
                {
                    data: "GatewayPhoneNumber",
                    title: "Gateway phone number"
                },
                //20
                {
                    data: "WaterLowLevel",
                    title: "Water low level"
                },
                //21
                {
                    data: "WaterHighLevel",
                    title: "Water high level"
                },
                //22
                {
                    data: "BatteryLowLevel",
                    title: "Battery low level"
                },
                //23
                {
                    data: "BatteryShutdownLevel",
                    title: "Battery shutdown level"
                },
                //24
                {
                    data: "BatteryTopLevel",
                    title: "Battery top level"
                },
                //25
                {
                    data: "TimeOfAlert",
                    title: "Time of alert"
                },
                //26
                {
                    data: "SleepPeriod",
                    title: "Sleep period"
                },
                //27
                {
                    data: "NumberOfSleepPeriods",
                    title: "Number of sleep periods"
                },
                //28
                {
                    data: "EnergyMode",
                    title: "Energy mode"
                },
                //29
                {
                    data: "OperationMode",
                    title: "Operation mode"
                },
                //30
                {
                    data: "VoltageFeedback",
                    title: "Voltage feedback"
                },
                //31
                {
                    data: "NotesAboutTheDevice",
                    title: "Notes about the device",
                    className: "size-mega"
                },
                //32
                {
                    data: "UpdateDate",
                    title: "Update date",
                    className: "size-big",
                    render: function (data, type, row, meta) {
                        return DateToString(data);
                    }
                },
                //33
                {
                    data: "SMSId",
                    title: "SMS Id"
                }
            ],
            order: [[1, 'desc']]
        });
    },
    ModalADD: async function () {
        let $modal = $('#DeviceList-edit-form');
        $modal.find('[name="HardwareVersion"]').val("WM5E");
        $modal.find('[name="FirmwareName"]').val("WM5E-Firmware");
        $modal.find('[name="FirmwareBranch"]').val("Master"); 
        $modal.find('[name="SIMtype"]').val("Telit AT&T");
    }
}