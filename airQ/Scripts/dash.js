var data1, options1,
    data2, options2,
    data3, options3,
    data4, options4,
    data5, options5,
    temp = 0, hum = 0, presAt = 0, presAtmmHg = 0, alcoholPPM = 0, TVOC = 0, CO2 = 0, Metano = 0, NH4 = 0;


function drawCharts() {
    gauge1();
    gauge2();
    gauge3();
    gauge4();
    gauge5();
}
function preLoadData() {
    temp = parseInt(document.querySelectorAll("[ID*=txtData1]")[0].value),
    hum = parseInt(document.querySelectorAll("[ID*=txtData2]")[0].value),
    presAt = parseInt(document.querySelectorAll("[ID*=txtData3]")[0].value),
    presAtmmHg = parseInt(presAt * 0.75006375541921),
    alcoholPPM = parseInt(document.querySelectorAll("[ID*=txtData4]")[0].value),
    TVOC = parseInt(document.querySelectorAll("[ID*=txtData5]")[0].value),
    CO2 = parseInt(document.querySelectorAll("[ID*=txtData6]")[0].value),
    NH4 = parseInt(document.querySelectorAll("[ID*=txtData7]")[0].value);
    Metano = parseInt(document.querySelectorAll("[ID*=txtData8]")[0].value);
    console.log("loaded");
}
$(document).ready(function () {

    preLoadData();
    google.charts.load('current', { 'packages': ['gauge'] });
    google.charts.setOnLoadCallback(drawCharts);

    //drawCharts();
    console.log("loaded");
});


function gauge1() {
    data1 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Temperatura', temp],
        ['Humedad', hum]
    ]);

    options1 = {
        width: 400, height: 120,
        redFrom: 80, redTo: 90,
        yellowFrom: 70, yellowTo: 80,
        max:90,
        minorTicks: 1
    };

    chart1 = new google.visualization.Gauge(document.getElementById('chart-divTemperaturaHumedad'));

    chart1.draw(data1, options1);

    /*
    setInterval(function () {
        data1.setValue(0, 1, document.querySelectorAll("[ID*=txtData1]")[0].value);
        chart1.draw(data1, options1);
    }, 5000);
    setInterval(function () {
        data1.setValue(1, 1, document.querySelectorAll("[ID*=txtData2]")[0].value);
        chart1.draw(data1, options1);
    }, 6000);
    */
}
function gauge2() {
    data2 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Presion(mb)', presAt],
        ['Presion(mmHg)', presAtmmHg]
    ]);

    options2 = {
        width: 400, height: 120,
        redFrom: 8000, redTo: 9000,
        yellowFrom: 7000, yellowTo: 8000,
        max: 9000,
        minorTicks: 5
    };

    chart2 = new google.visualization.Gauge(document.getElementById('chart-divPresionAtmosferica'));

    chart2.draw(data2, options2);
    /*
    setInterval(function () {
        data2.setValue(0, 1, document.querySelectorAll("[ID*=txtData3]")[0].value);
        chart2.draw(data2, options2);
    }, 7000);
    */
}
function gauge3() {
    data3 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Alcoholes', alcoholPPM],
        ['Metano', Metano]
    ]);

    options3 = {
        width: 400, height: 120,
        redFrom: 2500, redTo: 5000,
        yellowFrom: 2000, yellowTo: 2500,
        max: 5000,
        minorTicks: 5
    };

    chart3 = new google.visualization.Gauge(document.getElementById('chart-divAlcoholes'));

    chart3.draw(data3, options3);
    /*
    setInterval(function () {
        data3.setValue(0, 1, document.querySelectorAll("[ID*=txtData4]")[0].value);
        chart3.draw(data3, options3);
    }, 8000);
    */
}
function gauge4() {
    data4 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['TVOC', TVOC],
        ['CO2', CO2]
    ]);

    options4 = {
        width: 400, height: 120,
        redFrom: 1500, redTo: 5000,
        yellowFrom: 1000, yellowTo: 1500,
        max: 5000,
        minorTicks: 5
    };

    chart4 = new google.visualization.Gauge(document.getElementById('chart-divTVOCCO2'));

    chart4.draw(data4, options4);
    /*
    setInterval(function () {
        data4.setValue(0, 1, document.querySelectorAll("[ID*=txtData5]")[0].value);
        chart4.draw(data4, options4);
    }, 9000); setInterval(function () {
        data4.setValue(1, 1, document.querySelectorAll("[ID*=txtData6]")[0].value);
        chart4.draw(data4, options4);
    }, 9500);
    */
}
function gauge5() {
    data5 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Gas NH4', NH4]
    ]);

    options5 = {
        width: 400, height: 120,
        redFrom: 600, redTo: 1000,
        yellowFrom: 500, yellowTo: 600,
        max:1000,
        minorTicks: 5
    };

    chart5 = new google.visualization.Gauge(document.getElementById('chart-divGasMetano'));

    chart5.draw(data5, options5);
    /*
    setInterval(function () {
        data5.setValue(0, 1, document.querySelectorAll("[ID*=txtData7]")[0].value);
        chart5.draw(data5, options5);
    }, 9800);
    */
}

