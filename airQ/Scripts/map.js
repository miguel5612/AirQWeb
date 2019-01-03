$(document).ready(function () {
    //mapboxgl.accessToken = 'pk.eyJ1IjoibWlndWVsNTYxMiIsImEiOiJjanFndWVpdWMxdmtuNDJwdGxpMDkzdXo0In0.0IvcSGt-dMoJvLpW7XOkxw';
    /*
    var map = new mapboxgl.Map({
        container: 'map',
        zoom: 13,
        center: [7.8958656, -72.4989307],
        style: 'mapbox://styles/mapbox/streets-v9',
        hash: true,
        transformRequest: (url, resourceType) => {
            if (resourceType === 'Source' && url.startsWith('http://myHost')) {
                return {
                    url: url.replace('http', 'https'),
                    headers: { 'my-custom-header': true },
                    credentials: 'include'  // Include cookies for cross-origin requests
                }
            }
        }
    });
    var marker = mapboxgl.marker([51.5, -0.09]).addTo(mymap);
    marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();
    */
    var map = L.map('map').setView([51.505, -0.09], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 18,
        id: 'mapbox.streets',
        accessToken: 'pk.eyJ1IjoibWlndWVsNTYxMiIsImEiOiJjanFndWVpdWMxdmtuNDJwdGxpMDkzdXo0In0.0IvcSGt-dMoJvLpW7XOkxw'
    }).addTo(map);

    L.marker([7.8958656, -72.4989307]).addTo(map)
        .bindPopup('A pretty CSS3 popup.<br> Easily customizable.')
        .openPopup();
});