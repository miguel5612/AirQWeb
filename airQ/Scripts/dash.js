google.charts.load('current', { 'packages': ['gauge'] });
google.charts.setOnLoadCallback(drawCharts);

var data1, chart1, options1,
data2, chart2, options2,
data3, chart3, options3,
data4, chart4, options4,
data5, chart5, options5,
temp = 0, hum = 0, presAt = 0, alcoholPPM = 0, TVOC= 0, CO2 = 0, Metano = 0;

function drawCharts() {
    chart1();
    chart2();
    chart3();
    chart4();
    chart5();
}

function chart1() {
     data1 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Temperatura', temp],
        ['Humedad', hum]
    ]);

     options1 = {
        width: 400, height: 120,
        redFrom: 50, redTo: 100,
        yellowFrom: 40, yellowTo: 50,
        minorTicks: 5
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
function chart2() {
     data2 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Presion atmosferica', presAt]
    ]);

     options2 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
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
function chart3() {
     data3 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
         ['Alcoholes', alcoholPPM]
    ]);

     options3 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
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
function chart4() {
     data4 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['TVOC', TVOC],
        ['CO2', CO2]
    ]);

     options4 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
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
function chart5() {
     data5 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Gas Metano', Metano]
    ]);

     options5 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
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

