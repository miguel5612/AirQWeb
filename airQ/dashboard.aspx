<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="airQ.dashboard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src="Scripts/dash.js"></script>
	<script src="Scripts/jquery.signalR-2.4.0.min.js"></script>
	<script src="signalr/hubs"></script>
	<script src="Scripts/websocket.js"></script>
	<style>	    
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:ScriptManager ID="smUP" runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="upDash" runat="server">		
		<ContentTemplate>	
	<div align="center"><caption><h2><ins><i><asp:Label ID="lblTittle" runat="server" Text=""></asp:Label></i></ins></h2></caption>
	</div>
	<div class="row meters" runat="server" id="divMeters">
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
					<h3>Metano - Alcohol</h3>
					<p>Partes por millon / Partes por millon</p>
					<div id="chart-divAlcoholes" style="margin-left:10rem; width: 400px; height: 120px;"></div>
					<a href="https://www.sparkfun.com/datasheets/Sensors/Biometric/MQ-4.pdf" class="btn btn-outline-light">Mas informacion</a>
				</div>
			</div>
		</div>
		<div class="col-sm-4 py-2">
			<div class="card h-100 card-body">
			   <h3>TVOC/CO2</h3>
				<p>Partes por billon / Partes por millon</p>
					<div id="chart-divTVOCCO2" style="width: 400px; height: 120px;"></div>
					<a href="https://learn.sparkfun.com/tutorials/ccs811-air-quality-breakout-hookup-guide/all" class="btn btn-outline-light">Mas informacion</a>
			</div>
		</div>        
		<div class="col-sm-4 py-2">
			<div class="card h-100 card-body">
			   <h3>NH4</h3>
				<p>Partes por millon</p>
					<div id="chart-divGasMetano" style="margin-left:10rem; width: 400px; height: 120px;"></div>
					<a href="http://www.galenusrevista.com/?Amoniaco-NH3-y-amonio-NH4" class="btn btn-outline-light">Mas informacion</a>
			</div>
		</div>
		<div class="col-sm-4 py-2">
			<div class="card bg-primary">
				<div class="card-body">
					 <h3>Reportes</h3>
					<asp:Button Text="Descargar reporte diario" style="margin-left: 2rem;width: 100%;margin-bottom: 1rem;" CssClass="btn btn-danger" ID="dayReport" runat="server" OnClick="dayReport_Click" /><br />
					<asp:Button Text="Descargar reporte semanal" style="margin-left: 2rem;width: 100%;margin-bottom: 1rem;" CssClass="btn btn-danger" ID="weekReport" runat="server" OnClick="weekReport_Click" /><br />
					<asp:Button Text="Descargar reporte mensual" style="margin-left: 2rem;width: 100%;margin-bottom: 1rem;" CssClass="btn btn-danger" ID="monthReport" runat="server" OnClick="monthReport_Click" /><br />
					<div style="text-align: center;">
						<img runat="server" style="border: solid white 5px;" id="imgQR"/>
					</div>
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
