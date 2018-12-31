$(function () {
    var hub = $.connection.dashboardHub;
    var userName = "lol";

    hub.client.updateInfo = function (data) {
        document.querySelectorAll("[ID*=txtReceived]")[0].value = data;
        temp, hum, presAt, alcoholPPM, TVOC, CO2, Metano;
        try {
            var enc = new TextDecoder("utf-8");
            var mensaje = JSON.parse(enc.decode(data.mensaje).toString())
            var messageSplitted = [];
            temp = mensaje.D1;
            hum = mensaje.D2;
            presAt = mensaje.D3;
            alcoholPPM = mensaje.D4;
            TVOC = mensaje.D5;
            CO2 = mensaje.D6;
            Metano = mensaje.D7;
        }
        catch (err) {
            console.log("Lecture failed: ", err);
        }
        finally {
            drawCharts();
        }
    }

    $.connection.hub.start();
});