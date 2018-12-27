<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="airQ.dashboard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/dash.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-4 py-2">
            <div class="card card-body h-100">
                <h3>Temperatura/Humedad</h3>
                <div id="chart-divTemperaturaHumedad" style="width: 400px; height: 120px;"></div>
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card h-100 text-white bg-danger">
                <div class="card-body">
                    <h3>Presion atmosferica</h3>
                    <div id="chart-divPresionAtmosferica" style="width: 400px; height: 120px;"></div>
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
                </div>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card h-100 border-primary">
                <div class="card-body">
                    <h3>Alcoholes</h3>
                    <div id="chart-divAlcoholes" style="width: 400px; height: 120px;"></div>
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
                </div>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card h-100 card-body">
               <h3>TVOC/CO2</h3>
                    <div id="chart-divTVOCCO2" style="width: 400px; height: 120px;"></div>
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
            </div>
        </div>        
        <div class="col-sm-4 py-2">
            <div class="card h-100 card-body">
               <h3>Gas Metano</h3>
                    <div id="chart-divGasMetano" style="width: 400px; height: 120px;"></div>
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
            </div>
        </div>
        <div class="col-sm-4 py-2">
            <div class="card text-white bg-primary">
                <div class="card-body">
                     <h3>Parametros de calibracion</h3>
                    <asp:Label ID="lblTemp" runat="server" Text="Temperatura: "></asp:Label><asp:TextBox ID="txtTemperatura" runat="server"></asp:TextBox>
                    <asp:Label ID="lblHum" runat="server" Text="Humedad: "></asp:Label><asp:TextBox ID="txtHumedad" runat="server"></asp:TextBox>
                    <asp:Label ID="lblPres" runat="server" Text="Presion Atmosferica: "></asp:Label><asp:TextBox ID="txtPresionAtmos" runat="server"></asp:TextBox>
                    <asp:Label ID="lblAlcohol" runat="server" Text="Alcohol: "></asp:Label><asp:TextBox ID="txtAlcohol" runat="server"></asp:TextBox>
                    <asp:Label ID="lblCO2" runat="server" Text="CO2: "></asp:Label><asp:TextBox ID="txtCO2" runat="server"></asp:TextBox>
                    <asp:Label ID="lblTVOC" runat="server" Text="TVOC: "></asp:Label><asp:TextBox ID="txtTVOC" runat="server"></asp:TextBox>
                    <asp:Label ID="lblMetano" runat="server" Text="Metano: "></asp:Label><asp:TextBox ID="txtMetano" runat="server"></asp:TextBox>
                    <a href="#" class="btn btn-outline-light">Mas informacion</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
