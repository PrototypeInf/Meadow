/** Date to string format
 * @param {string} dateStr dateStr from .Net server
 */
function DateToString(dateStr) {
    if(!dateStr) return "";
    var regx = /-?\d+/;
    var regsRes = regx.exec(dateStr)[0];
    var nam = parseInt(regsRes);
    return new Date(nam).toLocaleString();
}

// format date
Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;
};
function getFormatData(a) {
    var d = new Date(a),
        dformat = [
            d.getFullYear(),
            (d.getMonth() + 1).padLeft(),
            d.getDate().padLeft(),
        ].join('-') +
            ' ' +
            [d.getHours().padLeft(),
            d.getMinutes().padLeft(),
            d.getSeconds().padLeft()].join(':');

    return dformat;
}

//#region notify
function NotyfyS(msg) {
    $.notifyDefaults({

    });
    var $notf = $.notify(msg + '', {
        animate: {
            enter: 'animated fadeInRight',
            exit: 'animated fadeOutRight'
        },
        type: 'success',
        placement: {
            from: "top",
            align: 'right'
        },
        delay: 2000,
        z_index: 10000
    });
}
function NotyfyI(msg) {
    var $notfI = $.notify(msg + '', {
        animate: {
            enter: 'animated fadeInRight',
            exit: 'animated fadeOutRight'
        },
        type: 'info',
        placement: {
            from: "top",
            align: 'right'
        },
        delay: 2000,
        //timer: 3000,
        z_index: 10000
    });
}
function NotyfyD(msg) {
    var $notfD = $.notify(msg + '', {
        animate: {
            enter: 'animated fadeInRight',
            exit: 'animated fadeOutRight'
        },
        type: 'danger',
        placement: {
            from: "top",
            align: 'right'
        },
        delay:3000,
        //timer: 5000,
        z_index: 10000
    });
}
//#endregion


'use strict';
class GenerateTable {
    constructor($table) {
        this.table = $table;
        this.table.html('');
        this.table.append('<thead><tr></tr></thead>');
        this.table.append('<tbody></tbody>');

        this.$head = this.table.find('thead tr');
        this.$body = this.table.find('tbody');
    }

    AddC(...names) {
        names.forEach((n) => {
            this.$head.append(`<th>${n}</th>`);
        })
    }
    AddCArr(arr = null) {
        arr.forEach((n) => {
            this.$head.append(`<th>${n}</th>`);
        })
    }

    AddR(...cols) {
        let $tr = $('<tr></tr>');
        cols.forEach((c) => {
            $tr.append(`<td>${c}</td>`);
        })
        this.table.append($tr);
    }
    AddArr(cols) {
        let $tr = $('<tr></tr>');
        cols.forEach((c) => {
            $tr.append(`<td>${c}</td>`);
        })
        this.table.append($tr);
    }
} 

async function Confirm(msg) {
    var confirmPromise = new Promise(function (resolve, reject) {
        bootbox.confirm({
            message: `<h4>${msg}</h4>`,
            buttons: {
                confirm: {
                    label: 'OK',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Cancel',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                resolve(result);
            }
        });
    });
    return confirmPromise;
}

async function Modal($modal = null,Id = null) {
    let modal = Id ? $('#' + Id) : $modal;
    let btnOK = modal.find('[data-btn="OK"]');
    let btnCancle = modal.find('[data-btn="Cancel"]');
    modal.modal('show');
    return new Promise((resolve, reject) => {
        btnOK.click(() => {
            modal.modal('hide');
            let data = modal.find('form').serialize();
            resolve(data);
        })
        btnCancle.click(() => {
            modal.modal('hide');
            resolve(false);
        })
        modal.on('hidden.bs.modal', function (e) {
            // do something...
        })
    })
}

'use strict';
class DateBox {
    constructor(boxId, btnOkOnClickFn, label = null) {
        this._Id = boxId;
        this._$box = $('#' + this._Id);
        this._btnOkOnClickFn = btnOkOnClickFn;
        this._label = label;
    }

    async Render(btnOk = true) {
        this._$box.addClass('input-group date-box');
        this._$box.html(
            `${this._label ?
                `<div class="input-group-prepend">
                    <span class="input-group-text">${this._label}</span>
                </div>`
                : ''
             }
            <input data-type="date" data-class="min" type="text" class="form-control">
           
            <div class="input-group-prepend">
                <span class="input-group-text">to</span>
            </div>
            <input data-type="date" data-class="max" type="text" class="form-control">
            ${btnOk ? `<button date-type ="date-box-btnOk" type="button" class="btn btn-outline-success">OK</button>` : ''}`
        ); 

        return new Promise(resolve => {
            setTimeout(() => {
                $('input[data-type="date"]').each(function () {
                    let minDate = new Date();
                    minDate.setDate(minDate.getDate() - 2);
                    //minDate.setMonth(minDate.getMonth() + 1);
                    let maxDate = new Date();
                    maxDate.setDate(maxDate.getDate() + 1);
                    //maxDate.setMonth(maxDate.getMonth() + 1);

                    let minDateStr = `${minDate.getFullYear()}/${minDate.getMonth()+1}/${minDate.getDate()}`;
                    let maxDateStr = `${maxDate.getFullYear()}/${maxDate.getMonth()+1}/${maxDate.getDate()}`;
                    $(this).datepicker({
                        uiLibrary: 'bootstrap4',
                        autoclose: true,
                        format: 'yyyy/mm/dd',
                    });
                    if ($(this).attr('data-class') == "min")
                        $(this).val(minDateStr);
                    if ($(this).attr('data-class') == "max")
                        $(this).val(maxDateStr);
                });
                if (this._btnOkOnClickFn) {
                    $(`#${this._Id} [date-type ="date-box-btnOk"]`).unbind('click');
                    $(`#${this._Id} [date-type ="date-box-btnOk"]`).bind('click', this._btnOkOnClickFn);
                }
                resolve();
            });
        });
    }

    GetDateMin() {
        return this._$box.find('[data-class="min"]').val();
    }

    GetDateMax() {
        return this._$box.find('[data-class="max"]').val();
    }
}