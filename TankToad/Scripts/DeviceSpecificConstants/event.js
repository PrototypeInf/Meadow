$('body').on('click', '[data-type="edit"]', async function () {
    await _DSCrender.MainList();
    let $this = $(this);
    let valName = $this.parent('li').attr('data-name');

    if (!(!!API.DSC.Val)) {
        NotyfyD('Error - Device-Specific Constants not loaded');
        return;
    }
    let dsc = Object.assign({}, API.DSC.Val);
    var n = 0;
    let Id = dsc[n]['Id'];
    let DSCval = dsc[n][valName];

    let data = null;
    try {
        data = await _DSCmodal.Edit(DSCval);
        dsc[n][valName] = data;
        await API.DSC.Edit(Id, dsc[n]);
    } catch (err) {
        await API.DSC.Get();
    }
    _DSCrender.MainList();
})

$('body').on('mouseleave', 'li[data-name]', function () {
    let $this = $(this);
    let $img = $this.find('img');
    $img.hide();
})
$('body').on('mouseover', 'li[data-name]', function () {
    let $this = $(this);
    let $img = $this.find('img');
    $img.show();
})