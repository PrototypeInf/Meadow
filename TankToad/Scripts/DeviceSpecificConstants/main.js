$(document).ready(
    async function () {
        //NotyfyI('Please wait for downloading Device-Specific Constants')
        await API.DSC.Get();
        await _DSCrender.MainList();
    }
);