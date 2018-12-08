let editIconRes = fetch("/Content/svg/actions/edit.svg");
let deleteIconRes = fetch("/Content/svg/actions/delete.svg");
let refreshIconRes = fetch("/Content/svg/actions/refresh.svg");

Promise.all([editIconRes, deleteIconRes, refreshIconRes]).then((res) => {
    [editIconRes, deleteIconRes, refreshIconRes] = res;
});
   
var _MainRender = {

    MainTable: {
        /** @type {DataTables.Api} */
        mainTableRendered: null,

        Render: async function (selectProblemDevice = false) {
            return new Promise((resolve, reject) => {
                this.mainTableRendered = $('#mainTable').DataTable({
                    destroy: true,
                    scrollX: true,
                    columnDefs: [
                        {
                            width: 200,
                        }
                    ],
                    processing: true,
                    serverSide: true,
                    ajax: {
                        url: `/DataTables/Main?selectProblemDevice=${selectProblemDevice}`,
                        type: 'POST',
                        dataSrc: function (json) {
                            resolve();
                            return json.data;
                        }
                    },
                    headerCallback: function (nHead, aData, iStart, iEnd, aiDisplay) {
                        _MainRender.MainTable.mainTableRendered.columns().iterator('column', function (settings, column) {
                            if (settings.aoColumns[column].tooltip !== undefined) {
                                $(_MainRender.MainTable.mainTableRendered.column(column).header()).attr('title', settings.aoColumns[column].tooltip);
                            }
                        });
                    },
                    rowId: "DeviceId",
                    columns: [

                        {
                            data: "DeviceId",
                            title: "Device Id",
                            tooltip: "Identification number of the device"
                        },
                        {
                            data: "DeviceName",
                            title: "Device name",

                        },
                        {
                            data: "MissingReport",
                            title: "Missing report",
                            tooltip: "A device did not report in the last 36 hours",
                            render: function (data) {
                                return `<span class="issues"; ">${data ? data : ""}</span>`;
                            }
                        },
                        {
                            data: "Timestamp",
                            title: "Timestamp",
                            tooltip: "The last timestamp from the data report for the last 24 hours",
                            className: "size-big",
                            render: function (data, type, row, meta) {
                                return DateToString(data);
                            }
                        },
                        {
                            data: "WaterLevel",
                            tooltip: "The last water level from the data report for the last 24 hours",
                            title: "Water level"
                        },
                        {
                            data: "BatteryLevel",
                            tooltip: "The last battery level from the data report for the last 24 hours",
                            title: "Battery level"
                        },
                        {
                            data: "LowLevel",
                            title: "Low level",
                            tooltip: "A device reported a low level in the past 24 hours",
                            render: function (data) {
                                return `<span class="issues"; ">${data ? data : ""}</span>`;
                            }
                        },
                        {
                            data: "Zeros",
                            title: "Zeros",
                            tooltip: "When zeros were found in a report in the past 24 hours",
                            render: function (data) {
                                return `<span class="issues"; ">${data ? data : ""}</span>`;
                            }
                        },
                        {
                            data: "GraceWindowExceeded",
                            title: "Grace window exceeded",
                            tooltip: "A device missed a report by the Grace Period",
                            render: function (data) {
                                return `<span class="issues"; ">${data ? data : ""}</span>`;
                            }
                        },
                        {
                            data: "LowBattery",
                            title: "Low battery",
                            tooltip: "A device’s battery dropped below the Low Battery threshold in the past 24 hours",
                            render: function (data) {
                                return `<span class="issues"; ">${data ? data : ""}</span>`;
                            }
                        }
                    ],
                    order: [[3, 'desc']]
                });
            });
        },
    },

    DataLogTable: {
        DeviceId: null,

        /** @type {DateBox}*/
        dateBox: null,

        DateBoxInit: async function (Id, label) {
            this.dateBox = new DateBox(
                Id,
                () => {
                    this.Render(this.DeviceId);
                },
                label
            );
            await this.dateBox.Render();
        },

        tableId: "dataLogTable",

        /** @type {DataTables.Api} */
        dataLogTableRendered: null,

        /** @type {TableInTab} */
        tableInTab:null,

        Render: async function (DeviceId) {
            this.DeviceId = DeviceId;
            this.tableInTab = new TableInTab(this.tableId);
            this.tableInTab.Display();

            let minDateStr = this.dateBox.GetDateMin();
            let maxDateStr = this.dateBox.GetDateMax();

            return new Promise((resolve, reject) => {
                this.dataLogTableRendered = $('#' + this.tableId).DataTable({
                    ajax: {
                        url: `/DataTables/DatasByDevice?DeviceId=${DeviceId}&minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
                        type: 'POST',
                        dataSrc: function (json) {
                            _MainRender.DataLogTable.tableInTab.Hide();
                            resolve();
                            return json.data;
                        }
                    },
                    destroy: true,
                    scrollX: true,
                    columnDefs: [
                        { width: 200 }
                    ],
                    processing: true,
                    serverSide: true,
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
                            title: "Data Id"
                        },
                        {
                            data: "Timestamp",
                            title: "Timestamp",
                            render: function (data, type, row, meta) {
                                return DateToString(data);
                            }
                        },
                        {
                            data: "WaterLevel",
                            title: "Water level"
                        },
                        {
                            data: "BatteryLevel",
                            title: "Battery level"
                        },
                        {
                            data: "SMSId",
                            title: "SMS Id"
                        }
                    ],
                    order: [[1, 'desc']]
                });
            });
        },

        RelodaAjax: function (refreshTable = false) {
            if (refreshTable)
                this.tableInTab.Display();

            this.dataLogTableRendered.ajax.reload(() => {
                if (refreshTable)
                    this.tableInTab.Hide();
            }, false);

            
        }
    },

    DiagnosticLogTable: {
        DeviceId: null,

        /** @type {DateBox}*/
        dateBox: null,

        DateBoxInit: async function (Id, label) {
            this.dateBox = new DateBox(
                Id,
                () => {
                    this.Render(this.DeviceId);
                },
                label
            );
            await this.dateBox.Render();
        },

        tableId: "diagnosticLogTable",

        /** @type {DataTables.Api} */
        diagnosticLogTableRendered: null,

        /** @type {TableInTab} */
        tableInTab:null,

        Render: async function (DeviceId) {
            this.DeviceId = DeviceId;
            this.tableInTab = new TableInTab(this.tableId);
            this.tableInTab.Display();

            let minDateStr = this.dateBox.GetDateMin();
            let maxDateStr = this.dateBox.GetDateMax();

            this.diagnosticLogTableRendered = $('#' + this.tableId).DataTable({
                ajax: {
                    url: `/DataTables/DiagnosticByDevice?DeviceId=${DeviceId}&minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
                    type: 'POST',
                    dataSrc: function (json) {
                        _MainRender.DiagnosticLogTable.tableInTab.Hide();
                        return json.data;
                    }
                },
                destroy: true,
                scrollX: true,
                columnDefs: [
                    { width: 200 }
                ],
                processing: true,
                serverSide: true,
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
                        title: "Diagnostic Id"
                    },
                    {
                        data: "ReportTime",
                        title: "Report time",
                        render: function (data, type, row, meta) {
                            return DateToString(data);
                        }
                    },
                    {
                        data: "UptimeCount",
                        title: "Uptime count"
                    },
                    {
                        data: "TimeDifferenceFromDesiredLocalReportTime",
                        title: "Time difference"
                    },
                    {
                        data: "LowLevel",
                        title: "Low level",
                        render: function (data) {
                            return `<span class="issues"; ">${data ? data : ""}</span>`;
                        }
                    },
                    {
                        data: "Zeros",
                        title: "Zeros",
                        render: function (data) {
                            return `<span class="issues"; ">${data ? data : ""}</span>`;
                        }
                    },
                    {
                        data: "GraceWindowExceeded",
                        title: "Grace window exceeded",
                        render: function (data) {
                            return `<span class="issues"; ">${data ? data : ""}</span>`;
                        }
                    },
                    {
                        data: "LowBattery",
                        title: "Low battery",
                        render: function (data) {
                            return `<span class="issues"; ">${data ? data : ""}</span>`;
                        }
                    },
                    {
                        data: "SMSId",
                        title: "SMS Id"
                    }
                ],
                order: [[1, 'desc']]
            });
        },

        RelodaAjax: function (refreshTable = false) {
            if (refreshTable)
                this.tableInTab.Display();

            this.diagnosticLogTableRendered.ajax.reload(() => {
                if (refreshTable)
                    this.tableInTab.Hide();
            }, false);
        }
    },

    DeviceAttributesLog: {
        DeviceId: null,

        /** @type {DateBox}*/
        dateBox: null,

        DateBoxInit: async function (Id, label) {
            this.dateBox = new DateBox(
                Id,
                () => {
                    this.Render(this.DeviceId);
                },
                label
            );
            await this.dateBox.Render();
        },

        tableId: "DeviceAttributesLogTable",

        /** @type {DataTables.Api} */
        deviceAttributesLogTableRendered: null,

        /** @type {TableInTab} */
        tableInTab:null,

        Render: async function (DeviceId) {
            this.DeviceId = DeviceId;
            this.tableInTab = new TableInTab(this.tableId);
            this.tableInTab.Display();

            let minDateStr = this.dateBox.GetDateMin();
            let maxDateStr = this.dateBox.GetDateMax();

            this.deviceAttributesLogTableRendered = $('#' + this.tableId).DataTable({
                ajax: {
                    url: `/DataTables/DeviceAttributesLogsByDevice?DeviceId=${DeviceId}&minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
                    type: 'POST',
                    dataSrc: function (json) {
                        _MainRender.DeviceAttributesLog.tableInTab.Hide();
                        return json.data;
                    }
                },
                destroy: true,
                scrollX: true,
                columnDefs: [
                    { width: 200 }
                ],
                processing: true,
                serverSide: true,
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
                        render: function (data, type, row, meta) {
                            return DateToString(data);
                        }
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
        },

        RelodaAjax: function (refreshTable = false) {
            if (refreshTable)
                this.tableInTab.Display();

            this.deviceAttributesLogTableRendered.ajax.reload(() => {
                if (refreshTable)
                    this.tableInTab.Hide();
            }, false);
        }
    },

    SMSlogTable: {
        DeviceId: null,

        /** @type {DateBox}*/
        dateBox: null,

        DateBoxInit: async function (Id, label) {
            this.dateBox = new DateBox(
                Id,
                () => {
                    this.Render(this.DeviceId);
                },
                label
            );
            await this.dateBox.Render();
        },
        tableId: "SMSlogTable",

        /** @type {DataTables.Api} */
        SMSlogTableRendered: null,

        /** @type {TableInTab} */
        tableInTab:null,

        Render: async function (DeviceId) {
            this.DeviceId = DeviceId;
            this.tableInTab = new TableInTab(this.tableId);
            this.tableInTab.Display();

            let minDateStr = this.dateBox.GetDateMin();
            let maxDateStr = this.dateBox.GetDateMax();

            this.SMSlogTableRendered = $('#' + this.tableId).DataTable({
                ajax: {
                    url: `/DataTables/SMSByDevice?DeviceId=${DeviceId}&minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
                    type: 'POST',
                    dataSrc: function (json) {
                            _MainRender.SMSlogTable.tableInTab.Hide();
                        return json.data;
                    }
                },
                destroy: true,
                scrollX: true,
                columnDefs: [
                    { width: 200 }
                ],
                processing: true,
                serverSide: true,
                rowId: "Id",
                columns: [
                    {
                        data: null,
                        defaultContent: `<img data-type = "reparse" title = "Reparse" src="${refreshIconRes.url}" class = "icon"/>`,
                        title: "Actions",
                        searchable: false,
                        orderable: false,
                        width: "60px"
                    },
                    {
                        data: "Id",
                        title: "SMS Id"
                    },
                    {
                        data: "From",
                        title: "From"
                    },
                    {
                        data: "To",
                        title: "To"
                    },
                    {
                        data: "Body",
                        title: "Text",
                        className: "size-mega"
                    },
                    {
                        data: "DateReceiving",
                        title: "Date receiving",
                        render: function (data, type, row, meta) {
                            return DateToString(data);
                        }
                    },
                    {
                        data: "Status",
                        title: "Status",
                        render: function (data) {
                            return `<span style = "color :${data === "OK" ? "#28a745" : "#dc3545"}; ">${data}</span>`;
                        }
                    }
                ],
                order: [[1, 'desc']]
            });
        },

        RelodaAjax: function () {
            this.SMSlogTableRendered.ajax.reload(null, false);
        }
    }
};

'use strict';
class TableInTab{
    constructor(tableId) {
        this._tableId = tableId;
        this._$tab = $('#' + tableId).parents('.tab-pane');
    }

    Display() {
        this._$tab.addClass('active show');
    }

    Hide() {
        if ($(`#${this._tableId}-tab`).hasClass('active'))
            return;
        setTimeout(() => {
            this._$tab.removeClass('active show');
        }, 100);
    }
}