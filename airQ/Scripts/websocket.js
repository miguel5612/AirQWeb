var enc, mensaje, hub;


$(function () {
    var hub = $.connection.dashboardHub;
    

    hub.client.updateInfo = function (data, inTopic) {
        document.querySelectorAll("[ID*=txtReceived]")[0].value = data;
        temp, hum, presAt, alcoholPPM, TVOC, CO2, Metano;
        console.log("Topic: ", inTopic, "savedTopic: ", String(document.querySelectorAll("[ID*=txtData9]")[0].value), String(document.querySelectorAll("[ID*=txtData9]")[0].value) == inTopic);
        if (String(document.querySelectorAll("[ID*=txtData9]")[0].value) == inTopic) {
            try {
                mensaje = JSON.parse(data);
                temp = mensaje.D1;
                hum = mensaje.D2;
                presAt = mensaje.D3;
                presAtmmHg = presAt * 0.75006375541921;
                alcoholPPM = mensaje.D4;
                TVOC = mensaje.D5;
                CO2 = mensaje.D6;
                Metano = mensaje.D7;
                NH4 = mensaje.D8;
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