$('body').on('click', '[data-type="delete"]', async function () {
    let conf = await Confirm("Confirmation of removal from the database");
    if (!conf) return;
    let $this = $(this);
    let Id = $this.parents('tr').attr('id');
    await API.Diagnostics.Delete(Id);
    _DiagnosticLogRender.mainTableRendered.ajax.reload(null, false);
})