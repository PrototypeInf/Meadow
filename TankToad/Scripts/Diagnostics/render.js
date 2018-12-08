var _DiagnosticLogRender = {
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
                url: `/DataTables/Diagnostic?minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
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
                    title: "Diagnostic Id"
                },
                {
                    data: "DeviceAttributesName",
                    title: "Device name"
                },
                {
                    data: "ReportTime",
                    title: "Report time",
                    className: "size-big",
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
                        return `<span class="issues"; ">${data ? data:""}</span>`;
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
    }
}