﻿var enc, mensaje, hub;


$(function () {
    var hub = $.connection.dashboardHub;
    

    hub.client.updateInfo = function (data, inTopic) {
        document.querySelectorAll("[ID*=txtReceived]")[0].value = data;
        temp, hum, presAt, alcoholPPM, TVOC, CO2, Metano;
        if (String(document.querySelectorAll("[ID*=txtData8]")[0].value) == inTopic) {
            try {
                mensaje = JSON.parse(data);
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
    }


    $.connection.hub.start();




});