<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="airQ.dashboard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/dash.js"></script>
    <script src="Scripts/jquery.signalR-2.4.0.min.js"></script>
    <script src="signalr/hubs"></script>
    <script src="Scripts/websocket.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="smUP" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upDash" runat="server">
        <ContentTemplate>
    <div align="center"><caption><h2><ins><i><asp:Label ID="lblTittle" runat="server" Text=""></asp:Label></i></ins></h2></caption>
	</div>
    <div class="row" runat="server" id="divMeters">
        <div class="col-sm-4 py-2">
            <div class="card card-body h-100">
                <h3>Temperatura /Humedad </h3>
                <p>Grados centigrados, Porcentual</p>
                <div id="chart-divTemperaturaHumedad" style="width: 400px; height: 120px;"></div>
                    <a href="http://wiki.seeedstudio.com/Grove-TemperatureAndHumidity_Sensor/" class="btn btn-outline-light">Mas informacion</a>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card h-100 border-primary">
                <div class="card-body">
                    <h3>Presion atmosferica</h3>
                    <p>milibares / milimetro de mercurio</p>
                    <div id="chart-divPresionAtmosferica" style="margin-left:1rem; width: 400px; height: 120px;"></div>
                    <a href="http://wiki.seeedstudio.com/Grove-Barometer_Sensor-BMP180/" class="btn btn-outline-light">Mas informacion</a>
                </div>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card h-100 border-primary">
                <div class="card-body">
                    <h3>Alcoholes</h3>
                    <p>Partes por millon</p>
                    <div id="chart-divAlcoholes" style="margin-left:10rem; width: 400px; height: 120px;"></div>
                    <a href="https://www.sparkfun.com/datasheets/Sensors/Biometric/MQ-4.pdf" class="btn btn-outline-light">Mas informacion</a>
                </div>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card h-100 card-body">
               <h3>TVOC/CO2</h3>
                <p>Partes por millon / Partes por billon</p>
                    <div id="chart-divTVOCCO2" style="width: 400px; height: 120px;"></div>
                    <a href="https://learn.sparkfun.com/tutorials/ccs811-air-quality-breakout-hookup-guide/all" class="btn btn-outline-light">Mas informacion</a>
            </div>
        </div>        
        <div class="col-sm-4 py-2">
            <div class="card h-100 card-body">
               <h3>Gas Metano</h3>
                <p>Partes por millon</p>
                    <div id="chart-divGasMetano" style="margin-left:10rem; width: 400px; height: 120px;"></div>
                    <a href="https://www.olimex.com/Products/Components/Sensors/SNS-MQ135/resources/SNS-MQ135.pdf" class="btn btn-outline-light">Mas informacion</a>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card bg-primary">
                <div class="card-body">
                     <h3>Reportes</h3>
                    <asp:Button Text="Descargar reporte diario" ID="dayReport" runat="server" />
                    <asp:Button Text="Descargar reporte semanal" ID="weekReport" runat="server" />
                    <asp:Button Text="Descargar reporte mensual" ID="monthReport" runat="server" />
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
                </div>
            </div>
        </div>
    </div>
            <div style="visibility:hidden">
                <asp:TextBox runat="server" ID="txtReceived"/>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
