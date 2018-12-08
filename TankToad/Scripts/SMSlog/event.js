$('body').on('click', '[data-type="reparse"]', async function () {
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
    _SmsLogRender.mainTableRendered.ajax.reload(null, false);
    NotyfyS('Parsed');
});

$('body').on('click', '[data-type="ReparseAll"]', async function () {
    let conf = await Confirm("Pre-generated data will be deleted. Confirm the update");
    if (!conf) return;
    await API.SMS.Get();
    var SMS = API.SMS.Val;
    NotyfyI('Start parsing');
    var i = 0;
    Parse(i);
    
    function Parse(i) {
        if (i >= SMS.length) {
            NotyfyI('Parsed All');
            _SmsLogRender.mainTableRendered.ajax.reload(null, false);
            return;
        }
        $.get('/SMS/SMSparse/' + SMS[i]['Id'])
            .then(() => {
                NotyfyS('SMS Id - ' + SMS[i]['Id']);
                console.log(SMS[i]['Id']);
                i++;
                Parse(i);
            }).catch((e) => {
                NotyfyD('Some error');
                console.log(e);
                throw e;
            });
    }
});