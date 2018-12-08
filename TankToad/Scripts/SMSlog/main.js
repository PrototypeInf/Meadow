$(document).ready(
    async function () {
        await _SmsLogRender.DateBoxInit('dateBox', 'Date receiving');

        _SmsLogRender.Chart.Render();
        _SmsLogRender.MainTable();
    });