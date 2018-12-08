var _SmsLogRender = {
    mainTableRendered: $().DataTable(),

    /** @type {DateBox}*/
    dateBox: null,

    DateBoxInit: async function (Id, label) {
        this.dateBox = new DateBox(
            Id,
            () => {
                this.MainTable();
                this.Chart.Render();
            },
            label
        );
        await this.dateBox.Render();
    },

    MainTable: async function () {
        let refreshIconRes = await fetch("/Content/svg/actions/refresh.svg");

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
                url: `/DataTables/SMS?minDateStr=${minDateStr}&maxDateStr=${maxDateStr}`,
                type: 'POST'
            },
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
                        return `<span style = "color :${data == "OK" ? "#28a745" : "#dc3545"}; ">${data}</span>`
                    }
                }
            ],
            order: [[1, 'desc']]
        });

       
    },

    Chart: {
        _divId: 'statDateChart',

        _potly: null,

        Render: async function () {
            const minDateStr = _SmsLogRender.dateBox.GetDateMin();
            const maxDateStr = _SmsLogRender.dateBox.GetDateMax();

           // Plotly.purge(this._divId);

            /** @type {Array}*/
            const resp = await GetSMSstat(minDateStr, maxDateStr);
            var dates = [];
            var counts = [];
            resp.forEach((r) => {
                dates.push(r['date']['Year'] + "-" + r['date']['Month'] + "-" + r['date']['Day']);
                counts.push(r['count']);
            })

            var CountByDate = {
                x: dates,
                y: counts,
                type: 'scatter',
                name: "SMS frequency"
            };
            var layout = {
                title: 'SMS frequency'
            }
            this._potly = Plotly.newPlot(this._divId, [CountByDate], layout);
        }
    }
}

async function GetSMSstat(minDateStr, maxDateStr) {
    var url = "/DataTables/SMSstat/";
    var parameters = { minDateStr: minDateStr, maxDateStr: maxDateStr }
    return new Promise((resolve) => {
        $.getJSON(url, parameters, (res) => {
            resolve(res);
        });
    }) 
}