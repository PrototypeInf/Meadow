const API = {
    apiURL: window.location.origin,

    Diagnostics: {
        Val: null,

        Delete: async function (Id) {
            let url = API.apiURL + '/api/Diagnostics/' + Id;
            var res = null;
            try {
                res = await $.ajax({
                    type: 'Delete',
                    url: url
                });
                Obj.Delete(this.Val, res);
                NotyfyS('Deleted');
            } catch (err) {
                NotyfyD("Error deleting");
                throw new Error("Error deleting");
            }
            return res;
        }
    },

    Data: {
        Val: null,

        Get: async function () {
            let url = API.apiURL + '/api/Data';
            var res = null;
            try {
                res = await $.get(url);
                this.Val = res;
            } catch (err) {
                NotyfyD("Error Data log downloading");
                throw new Error("Error Data log downloading");
            }
            return res;
        },

        Delete: async function (Id) {
            let url = API.apiURL + '/api/Data/' + Id;
            var res = null;
            try {
                res = await $.ajax({
                    type: 'Delete',
                    url: url
                });
                Obj.Delete(this.Val, res);
                NotyfyS('Deleted');
            } catch (err) {
                NotyfyD("Error deleting");
                throw new Error("Error deleting");
            }
            return res;
        }
    },

    DeviceAttributesLog: {
        Val: null,

        Get: async function () {
            let url = API.apiURL + '/api/DeviceAttributesLogs';
            var res = null;
            try {
                res = await $.get(url);
                this.Val = res;
            } catch (err) {
                NotyfyD("Error Device attributes log downloading");
                throw new Error("Error Device attributes log downloading");
            }
            return res;
        },

        Delete: async function (Id) {
            let url = API.apiURL + '/api/DeviceAttributesLogs/' + Id;
            var res = null;
            try {
                res = await $.ajax({
                    type: 'Delete',
                    url: url
                });
                Obj.Delete(this.Val, res);
                NotyfyS('Deleted');
            } catch (err) {
                NotyfyD("Error deleting");
                throw new Error("Error deleting");
            }
            return res;
        }
    },

    Device: {
        Val: null,

        Get: async function () {
            let url = API.apiURL + '/api/Device';
            var res = null;
            try {
                res = await $.get(url);
                this.Val = res;
            } catch (err) {
                NotyfyD("Error device downloading");
                throw new Error("Error device downloading");
            }
            return res;
        },
        GetCoords: async function () {
            let url = API.apiURL + '/api/Device/Coords';
            var res = null;
            try {
                res = await $.get(url);
                this.Val = res;
            } catch (err) {
                NotyfyD("Error device coordinates downloading");
                throw new Error("Error device coordinates downloading");
            }
            return res;
        },
        GetById: async function (Id) {
            let url = API.apiURL + '/api/Device/' + Id;
            var res = null;
            try {
                res = await $.get(url);
            } catch (err) {
                NotyfyD("Error device downloading");
                throw new Error("Error device downloading");
            }
            return res;
        },
        Add: async function (data) {
            NotyfyI('In process...');
            let url = API.apiURL + '/api/Device';
            var res = null;
            try {
                res = await $.ajax({
                    type: 'POST',
                    url: url,
                    data: data,
                    dataType: "json",
                });
                Obj.Add(this.Val, res);
                NotyfyS('Saved');
            } catch (err) {
                NotyfyD('Add error');
                throw new Error("Add error");
            }
            return res;
        },
        Edit: async function (id, data) {
            data = `Id=${id}&${data}`;
            let url = API.apiURL + '/api/Device/' + id;
            var res = null;
            try {
                res = await $.ajax({
                    type: 'PUT',
                    url: url,
                    data: data,
                    dataType: "json",
                });
                Obj.Edit(this.Val, res);
                NotyfyS('Saved');
            } catch (err) {
                NotyfyD('Edit error');
                throw new Error("Edit error");
            }
            return res;
        },
        Delete: async function (Id) {
            let url = API.apiURL + '/api/Device/' + Id;
            var res = null;
            try {
                res = await $.ajax({
                    type: 'Delete',
                    url: url
                });
                Obj.Delete(this.Val, res);
                NotyfyS('Device deleted');
            } catch (err) {
                NotyfyD("Error deleting device");
                throw new Error("Error deleting device");
            }
            return res;
        },
    },

    SMS: {
        Val: null,
        Get: async function () {
            let url = API.apiURL + '/api/SMSapi';
            var res = null;
            try {
                res = await $.get(url);
                this.Val = res;
            } catch (err) {
                NotyfyD("Error SMS downloading");
                throw new Error("Error SMS downloading");
            }
            return res;
        },
        GetById: async function (Id) {
            let url = API.apiURL + '/api/SMSapi/' + Id;
            var res = null;
            try {
                res = await $.get(url);
                Obj.Edit(this.Val, res);
            } catch (err) {
                NotyfyD("Error SMS downloading");
                throw new Error("Error SMS downloading");
            }
            return res;
        }
    },

    DSC: {
        Val: null,
        Get: async function () {
            let url = API.apiURL + '/api/DeviceSpecificConstants';
            var res = null;
            try {
                res = await $.get(url);
            } catch (err) {
                NotyfyD("Error Device-Specific Constants downloading");
                throw new Error("Error Device-Specific Constants downloading");
            }
            this.Val = res;
            return res;
        },
        Edit: async function (id, data) {
            let url = API.apiURL + '/api/DeviceSpecificConstants/' + id;
            var res = null;
            try {
                res = await $.ajax({
                    type: 'PUT',
                    contentType: "application/json; charset=utf-8",
                    url: url,
                    data: JSON.stringify(data),
                    dataType: "json", 
                });
                Obj.Edit(this.Val, res);
                NotyfyS('Saved');
            } catch (err) {
                NotyfyD('Edit error');
                throw new Error('Edit error');
            }
            return res;
        }
    }
}
var Obj = {
    Edit: function (arr, newData) {
        if (!!arr && !!newData)
        arr.forEach((a,i) => {
            if (newData['Id'] == a['Id'])
                arr[i] = newData;
        })
    },
    Add: function (arr, newData) {
        if (!!arr && !!newData)
        arr.push(newData);
    },
    Delete: function (arr, newData) {
        if (!!arr && !!newData)
        for (var i = 0; i < arr.length; i++) {
            if (newData['Id'] == arr[i]['Id']) {
                arr.splice(i, 1);
                break;
            }
        }
    }
}