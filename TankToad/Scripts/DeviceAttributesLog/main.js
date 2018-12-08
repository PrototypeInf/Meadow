$(document).ready(
    async function () {
        await _DeviceAttributesLogRender.DateBoxInit('dateBox', 'Update date');

        _DeviceAttributesLogRender.MainTable();
    }
);