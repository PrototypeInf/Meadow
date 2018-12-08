$('body').on('click', '#btnAdd',async function () {
    let $modal = $('#DeviceList-edit-form');
    $modal.find('[data-btnOK]').attr('data-btnOK', 'Add');
    let $form = $modal.find('form');
    $form[0].reset();

    await _DeviceListRender.ModalADD();
    let modelRes = await Modal($modal);
    let data = $form.serialize();
    await API.Device.Add(data);
    _DeviceListRender.mainTableRendered.ajax.reload(null, false);
})

$('body').on('click', '[data-type="edit"]', async function () {
    let $modal = $('#DeviceList-edit-form');
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    let device = await API.Device.GetById(Id);
    for (n in device) {
        $modal.find(`[name = "${n}"]`).val(device[n]);
    }
    let modelRes = await Modal($modal);
    if (!modelRes) return;

    await API.Device.Edit(Id, modelRes);
    _DeviceListRender.mainTableRendered.ajax.reload(null, false);
})

$('body').on('click', '[data-type="delete"]', async function () {
    let conf = await Confirm("Confirmation of removal from the database. Related data will be deleted");
    if (!conf) return;
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    await API.Device.Delete(Id);
    _DeviceListRender.mainTableRendered.ajax.reload(null, false);
})