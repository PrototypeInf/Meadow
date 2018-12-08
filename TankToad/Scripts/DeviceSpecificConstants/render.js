var _DSCrender = {
    MainList: async function () {
        let $list = $('#mainList');
        var DSC = API.DSC.Val[0];
        for (var dsc in DSC) {
            let $li = $list.find(`[data-name="${dsc}"]`);
            let valConteiner = $li.find('[data-type="data"]');
            valConteiner.html('');
            valConteiner.append(DSC[dsc]);
        }
    }
}