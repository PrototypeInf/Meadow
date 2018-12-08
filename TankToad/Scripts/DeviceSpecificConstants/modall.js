var _DSCmodal = {
    Edit: async function (Value) {
        let modal = $('#DSC-edit-form');
        let form = modal.find('form');
        form[0].reset();

        form.find('[data-type="value"]').val(Value);

        let btnOK = modal.find('[data-btn="OK"]');
        let btnCancle = modal.find('[data-btn="Cancel"]');
        modal.modal('show');

        let data = null;
        function OK() {
            modal.modal('hide');
            data = modal.find('[data-type="value"]').val();
        }
        return new Promise((resolve, reject) => {
            btnOK.click(() => {
                OK();
                resolve(data);
            })
            btnCancle.click(() => {
                modal.modal('hide');
                reject();
            })
            modal.on('hidden.bs.modal', function (e) {
                reject();
            })
            /*modal.on('submit', function (e) {
                OK();
                resolve(data);
            })*/
        })
    }
}