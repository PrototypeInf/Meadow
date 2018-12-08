$(document).ready(
    async function () {
        await _DiagnosticLogRender.DateBoxInit('dateBox', 'Report time');

        _DiagnosticLogRender.MainTable();
});