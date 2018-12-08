var _DataLogRender = {
    mainTableRendered: $().DataTable(),

    /** @type {DateBox}*/
    dateBox:null,

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
                url: `/DataTables/Datas?minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
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
                    title: "Data Id"
                },
                {
                    data: "DeviceAttributesId",
                    title: "Device Id"
                },
                {
                    data: "DeviceAttributesName",
                    title: "Device name"
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
    }
}