google.charts.load('current', { 'packages': ['gauge'] });
google.charts.setOnLoadCallback(drawCharts);

function drawCharts() {
    chart1();
    chart2();
    chart3();
    chart4();
    chart5();
}

function chart1() {
    var data1 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Temperatura', 80],
        ['Humedad', 55]
    ]);

    var options1 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart1 = new google.visualization.Gauge(document.getElementById('chart-divTemperaturaHumedad'));

    chart1.draw(data1, options1);

    setInterval(function () {
        data1.setValue(0, 1, document.querySelectorAll("[ID*=txtData1]")[0].value);
        chart1.draw(data1, options1);
    }, 5000);
    setInterval(function () {
        data1.setValue(1, 1, document.querySelectorAll("[ID*=txtData2]")[0].value);
        chart1.draw(data1, options1);
    }, 6000);
}
function chart2() {
    var data2 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Presion atmosferica', 80]
    ]);

    var options2 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart2 = new google.visualization.Gauge(document.getElementById('chart-divPresionAtmosferica'));

    chart2.draw(data2, options2);

    setInterval(function () {
        data2.setValue(0, 1, document.querySelectorAll("[ID*=txtData3]")[0].value);
        chart2.draw(data2, options2);
    }, 7000);
}
function chart3() {
    var data3 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Alcoholes', 80]
    ]);

    var options3 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart3 = new google.visualization.Gauge(document.getElementById('chart-divAlcoholes'));

    chart3.draw(data3, options3);

    setInterval(function () {
        data3.setValue(0, 1, document.querySelectorAll("[ID*=txtData4]")[0].value);
        chart3.draw(data3, options3);
    }, 8000);
}
function chart4() {
    var data4 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['TVOC', 80],
        ['CO2', 55]
    ]);

    var options4 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart4 = new google.visualization.Gauge(document.getElementById('chart-divTVOCCO2'));

    chart4.draw(data4, options4);

    setInterval(function () {
        data4.setValue(0, 1, document.querySelectorAll("[ID*=txtData5]")[0].value);
        chart4.draw(data4, options4);
    }, 9000); setInterval(function () {
        data4.setValue(1, 1, document.querySelectorAll("[ID*=txtData6]")[0].value);
        chart4.draw(data4, options4);
    }, 9500);
}
function chart5() {
    var data5 = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Gas Metano', 80]
    ]);

    var options5 = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart5 = new google.visualization.Gauge(document.getElementById('chart-divGasMetano'));

    chart5.draw(data5, options5);

    setInterval(function () {
        data5.setValue(0, 1, document.querySelectorAll("[ID*=txtData7]")[0].value);
        chart5.draw(data5, options5);
    }, 9800);
}

