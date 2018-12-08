$(document).ready(async function () {
    Promise.all([
        _MainRender.DataLogTable.DateBoxInit('dataLogTable-dateBox', 'Timestamp'),
        _MainRender.DiagnosticLogTable.DateBoxInit('diagnosticLogTable-dateBox', 'Report time'),
        _MainRender.DeviceAttributesLog.DateBoxInit('DeviceAttributesLogTable-dateBox', 'Update date'),
        _MainRender.SMSlogTable.DateBoxInit('SMSlogTable-dateBox', 'Date receiving')
    ]);

    await RenderTables(map = true);
    setTimeout(() => {
        $('thead th[title]').tooltip({ show: { effect: "blind", duration: 300,delay:1000 }});
    })
});
/** @param {boolean} map render google map
    @param {boolean} 
    ProblemDevice for MainTabe*/
async function RenderTables(map = false, selectProblemDevice = false) {
    await _MainRender.MainTable.Render(selectProblemDevice);
    $($('#mainTable tbody tr')[0]).addClass('selected');

    let Id = $('#mainTable tbody tr.selected').attr('id');
    if (!Id) return;
    _MainRender.DataLogTable.Render(Id);
    _MainRender.DiagnosticLogTable.Render(Id);
    _MainRender.DeviceAttributesLog.Render(Id);
    _MainRender.SMSlogTable.Render(Id);


    if (map) {
        await SetDevicesOnMap();
    }
    _Map.SetCenterByDeviceId(Id);
}

async function SetDevicesOnMap() {
    /** @type {[]}*/
    let coordsDevice = await API.Device.GetCoords();
    await _InitMapPromise;
    coordsDevice.forEach((c) => {
        var lat = Number(c['CurrentLocationLatitude']);
        var lng = Number(c['CurrentLocationLongitude']);
        if (lat && lng) {
            let device = new Device();
            device.Coords = { lat: lat, lng: lng };
            device.name = c['Name'];
            device.Id = c['Id'];
            _Map.SetMarker(device);
        }
    });
}