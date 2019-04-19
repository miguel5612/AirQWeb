/*
Funcion SpinGeneric.js
Hecha el dia 23/02/2017 por Miguel Angel Califa

Esta funcion se ha hecho generica para añadir el spinner en cualquier parte
del software.

Si deseas que el spinner se active debes agregar dentro de dos etiquetas script las siguientes lineas.
<script>
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
</script>

El script manager debe tener un asyncpostbacktimeout alto!!.

    <div>
        <asp:ScriptManager AsyncPostBackTimeout="36000" ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

*/


function InitializeRequest(sender, args) {
    StartSpinner();
}
function EndRequestHandler(sender, args) {
    deleteSpinner();
}

function StartSpinner() {
    var opts = {
        lines: 11 // The number of lines to draw
        , length: 4 // The length of each line
        , width: 8 // The line thickness
        , radius: 26 // The radius of the inner circle
        , scale: 1 // Scales overall size of the spinner
        , corners: 1 // Corner roundness (0..1)
        , color: '#000' // #rgb or #rrggbb or array of colors
        , opacity: 0.3 // Opacity of the lines
        , rotate: 0 // The rotation offset
        , direction: 1 // 1: clockwise, -1: counterclockwise
        , speed: 1 // Rounds per second
        , trail: 60 // Afterglow percentage
        , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
        , zIndex: 2e9 // The z-index (defaults to 2000000000)
        , className: 'spinner' // The CSS class to assign to the spinner
        , top: '50%' // Top position relative to parent
        , left: '50%' // Left position relative to parent
        , shadow: false // Whether to render a shadow
        , hwaccel: false // Whether to use hardware acceleration
        , position: 'absolute' // Element positioning
    }
    var target = document.querySelectorAll("form")[0]
    var spinner = new Spinner(opts).spin(target);
    try {
        document.querySelectorAll("input[type='submit']")[0].style.display = "none";
    }
    catch (err) {
        console.log(err.message);
    }
}


function StartSpinnerWithTarget(target) {
    var opts = {
        lines: 11 // The number of lines to draw
        , length: 4 // The length of each line
        , width: 8 // The line thickness
        , radius: 26 // The radius of the inner circle
        , scale: 1 // Scales overall size of the spinner
        , corners: 1 // Corner roundness (0..1)
        , color: '#000' // #rgb or #rrggbb or array of colors
        , opacity: 0.3 // Opacity of the lines
        , rotate: 0 // The rotation offset
        , direction: 1 // 1: clockwise, -1: counterclockwise
        , speed: 1 // Rounds per second
        , trail: 60 // Afterglow percentage
        , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
        , zIndex: 2e9 // The z-index (defaults to 2000000000)
        , className: 'spinner' // The CSS class to assign to the spinner
        , top: '50%' // Top position relative to parent
        , left: '50%' // Left position relative to parent
        , shadow: false // Whether to render a shadow
        , hwaccel: false // Whether to use hardware acceleration
        , position: 'absolute' // Element positioning
    }
    
    var spinner = new Spinner(opts).spin(target);
    try {
        document.querySelectorAll("input[id*='cmdAusgabe']")[0].style.display = "none";
    }
    catch (err) {
        console.log(err.message);
    }
}

function deleteSpinner() {
    var elements = document.getElementsByClassName('spinner');
    while (elements.length > 0) {
        elements[0].parentNode.removeChild(elements[0]);
    }

    try {
        document.querySelectorAll("input[id*='cmdAusgabe']")[0].style.display = "initial";
    }
    catch (err) {
        console.log(err.message);
    }
}
