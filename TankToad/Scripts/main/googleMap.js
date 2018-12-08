var _InitMapPromiseResolveFunc = null;
var _InitMapPromise = new Promise((resolve, reject) => {
    _InitMapPromiseResolveFunc = resolve;
});
var _Map = {
    /**google.maps.Map(...)
     * @type {google.maps}
     */
    _googleMap: null,

    /** @type {Marker[]}*/
    _markers: [],
    
    InitMap: function () {
        let mapContainer = document.getElementById('googleMap');
        this._googleMap = new google.maps.Map(
            mapContainer,
            {
                zoom: 15,
                center: { lat: 50.62045013422409, lng: 26.253711363484513 },
                draggableCursor: 'default'
            });

        _InitMapPromiseResolveFunc();
    },

    /** Set marks into map
     * @param {Device} device name with coords
     */
    SetMarker: function (device) {
        if (!this._googleMap) return;
        let mark = new google.maps.Marker({
            position: device.Coords,
            title: device.name,
            label: device.name
        });
        mark.setMap(this._googleMap);
        this._markers.push(new Marker(device, mark));
    },

    SetCenter: function (coords) {
        if (!this._googleMap) return;
        this._googleMap.setCenter(coords);
        //this._googleMap.setZoom(15);
    },

    /** @param {number} Id Id*/
    SetCenterByDeviceId: function (Id) {
        if (!this._googleMap) return;
        let device = null;
        this._markers.forEach(m => {
            if (m.Device.Id === Number(Id))
                device = m.Device;
        });
        if (device)
            this.SetCenter(device.Coords);
    }
};
/** @param {Device} Device*/
var Marker = function (Device, Mark) {
    /**@type {Device}*/
    this.Device= Device;
    this.Mark = Mark;
};
var Device = function () {
    this.Id = null;
    this.name = null;
    this.Coords = {
        /**@type {number}*/
        lat: null,
        /**@type {number}*/
        lng: null
    };
};