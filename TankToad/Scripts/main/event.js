$('body').on('click', '#mainTable tbody tr', function () {
    $('#mainTable tbody tr').removeClass('selected');
    $(this).addClass('selected');
    let Id = Number($(this).attr('id'));
    _MainRender.DataLogTable.Render(Id);
    _MainRender.DiagnosticLogTable.Render(Id);
    _MainRender.DeviceAttributesLog.Render(Id);
    _MainRender.SMSlogTable.Render(Id);

    _Map.SetCenterByDeviceId(Id);
});

$('body').on('click', '#dataLogTable [data-type="delete"]', async function () {
    let conf = await Confirm("Confirmation of removal from the database");
    if (!conf) return;
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    await API.Data.Delete(Id);
    _MainRender.DataLogTable.RelodaAjax();
});

$('body').on('click', '#diagnosticLogTable [data-type="delete"]', async function () {
    let conf = await Confirm("Confirmation of removal from the database");
    if (!conf) return;
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    await API.Diagnostics.Delete(Id);
    _MainRender.DiagnosticLogTable.RelodaAjax();
})

$('body').on('click', '#DeviceAttributesLogTable [data-type="delete"]', async function () {
    let conf = await Confirm("Confirmation of removal from the database");
    if (!conf) return;
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    await API.DeviceAttributesLog.Delete(Id);
    _MainRender.DeviceAttributesLog.RelodaAjax();
})

$('body').on('click', '#SMSlogTable [data-type="reparse"]', async function () {
    let conf = await Confirm("Pre-generated data will be deleted. Confirm the update");
    if (!conf) return;
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    try {
        await $.get('/SMS/SMSparse/' + Id);

    } catch (err) {
        NotyfyD('Error parsing');
        return;
    }
    _MainRender.SMSlogTable.RelodaAjax();
    _MainRender.DeviceAttributesLog.RelodaAjax(true);
    _MainRender.DiagnosticLogTable.RelodaAjax(true);
    _MainRender.DataLogTable.RelodaAjax(true);
    NotyfyS('Parsed');
});

$('body').on('change', '#SelectProblemDevices', async function () {
    var selectProblemDevice=this.checked;
    RenderTables(map = false, selectProblemDevice = selectProblemDevice);
});