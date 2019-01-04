﻿<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="monthReport.aspx.cs" Inherits="airQ.monthReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:ScriptManager ID="scriptmanager1" runat="server">
	</asp:ScriptManager>
	<br />
	<br />
	<div>
		<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
		<asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
					TargetControlID="txtDate">
				</asp:CalendarExtender>

		<br />
		<br />
		<div class="container">
	<div class="row">
		<div class="col-md-12">
			<div class="alert info">
				<div style="margin-left:25%">
					<asp:GridView runat="server" ID="gvReport" DataSourceID="dsDevices"
						EmptyDataText="No tienes aun dispositivos registrados!" AllowPaging="True" 
						AllowSorting="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" 
						BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
						GridLines="Vertical" AutoGenerateColumns ="false">
						<Columns>
							<asp:BoundField DataField="dataId" HeaderText="Numero identificador" InsertVisible="False" 
								ReadOnly="True" SortExpression="deviceID" Visible="False" />
							<asp:HyperLinkField Text="ver ubicacion en el mapa" DataNavigateUrlFields="latitud,longitud" 
								DataNavigateUrlFormatString="https://www.google.com/maps/@{0},{1},4.23z" />                            
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha de entrada" 
								SortExpression="Fecha"  DataFormatString="{dd-MM-yyyy}" HtmlEncode="false">
							<HeaderStyle HorizontalAlign="Left" />
							</asp:BoundField>
							<asp:BoundField DataField="topic" HeaderText="Topico de entrada" 
								SortExpression="topic" >
							<HeaderStyle HorizontalAlign="Left" />
							</asp:BoundField>
							<asp:BoundField DataField="temperatura" HeaderText="Temperatura" 
								SortExpression="temperatura" >
							<HeaderStyle HorizontalAlign="Left" />
							</asp:BoundField>							
							<asp:BoundField DataField="humedad" HeaderText="humedad" 
								SortExpression="humedad" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                            							
							<asp:BoundField DataField="presionAtmosferica" HeaderText="presion atmosferica" 
								SortExpression="presionAtmosferica" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                            							
							<asp:BoundField DataField="Alcohol" HeaderText="Alcohol" 
								SortExpression="Alcohol" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                            							
							<asp:BoundField DataField="TVOC" HeaderText="TVOC" 
								SortExpression="TVOC" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                            							
							<asp:BoundField DataField="CO2" HeaderText="CO2" 
								SortExpression="CO2" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                            							
							<asp:BoundField DataField="NH4" HeaderText="Metano" 
								SortExpression="NH4" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
						</Columns>
					</asp:GridView>					
				</div>
			</div>
		</div>
	</div>
</div>
         <script>
        try {
            document.querySelectorAll("[ID*=gvReport]")[0].classList.add("table");
            document.querySelectorAll("[ID*=gvReport]")[0].classList.add("table-striped");
            document.querySelectorAll("[ID*=gvReport]")[0].classList.add("custab");
        }
        catch (err) {}
    </script>
        <asp:SqlDataSource runat="server" ID="dsDevices" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>"
		ProviderName="<%$ ConnectionStrings:AirQConnectionString.ProviderName %>"
	SelectCommand="SELECT * FROM measurements WHERE ([topic] = @iNTopic)  ORDER BY registerAt DESC">
            <selectparameters>
                <asp:SessionParameter Name="iNTopic" SessionField="iNTopic" Type="String" />
            </selectparameters>
</asp:SqlDataSource>
		<asp:Button Text="Calcular" CssClass="btn btn-success" ID="btnCalcular" OnClick ="btnCalcular_Click" runat="server" />
	</div>
</asp:Content>