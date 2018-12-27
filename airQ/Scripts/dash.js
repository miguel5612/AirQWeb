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
    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Temperatura', 80],
        ['Humedad', 55]
    ]);

    var options = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('chart-divTemperaturaHumedad'));

    chart.draw(data, options);

    setInterval(function () {
        data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 13000);
    setInterval(function () {
        data.setValue(1, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 5000);
}
function chart2() {
    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Presion atmosferica', 80]
    ]);

    var options = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('chart-divPresionAtmosferica'));

    chart.draw(data, options);

    setInterval(function () {
        data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 13000);
}
function chart3() {
    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Alcoholes', 80]
    ]);

    var options = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('chart-divAlcoholes'));

    chart.draw(data, options);

    setInterval(function () {
        data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 13000);
}
function chart4() {
    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['TVOC', 80],
        ['CO2', 55]
    ]);

    var options = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('chart-divTVOCCO2'));

    chart.draw(data, options);

    setInterval(function () {
        data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 13000);
    setInterval(function () {
        data.setValue(1, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 5000);
}
function chart5() {
    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['Gas Metano', 80]
    ]);

    var options = {
        width: 400, height: 120,
        redFrom: 90, redTo: 100,
        yellowFrom: 75, yellowTo: 90,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('chart-divGasMetano'));

    chart.draw(data, options);

    setInterval(function () {
        data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 13000);
    setInterval(function () {
        data.setValue(1, 1, 40 + Math.round(60 * Math.random()));
        chart.draw(data, options);
    }, 5000);
}

