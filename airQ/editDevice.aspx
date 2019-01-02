<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="editDevice.aspx.cs" Inherits="airQ.editDevice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div align="center">
		 <caption><h2><ins><i>LISTA DE DISPOSITIVOS</i></ins></h2></caption>
	</div>
<div class="container">
	<div class="row">
		<div class="col-md-12">
			<div class="alert info">
		        <div style="margin-left:25%">
	                <asp:GridView runat="server" ID="gvDevices" DataSourceID="dsDevices"
		                EmptyDataText="No tienes aun dispositivos registrados!" AllowPaging="True" 
		                AllowSorting="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" 
		                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
		                GridLines="Vertical" AutoGenerateColumns ="false">
		                <Columns>
			                <asp:BoundField DataField="deviceID" HeaderText="Numero identificador" InsertVisible="False" 
				                ReadOnly="True" SortExpression="deviceID" Visible="False" />
			                <asp:HyperLinkField Text="modificar" DataNavigateUrlFields="deviceID" 
				                DataNavigateUrlFormatString="editDevice.aspx?deviceID={0}" />
			                <asp:HyperLinkField Text="desactivar" DataNavigateUrlFields="deviceID" 
				                DataNavigateUrlFormatString="editDevice.aspx?deviceIDArchiv={0}" />
			                <asp:BoundField DataField="deviceName" HeaderText="Nombre del dispositivo" SortExpression="deviceName" >
			                <HeaderStyle HorizontalAlign="Left" />
			                </asp:BoundField>
			                <asp:BoundField DataField="inTopic" HeaderText="Topico de entrada" 
				                SortExpression="inTopic" >
			                <HeaderStyle HorizontalAlign="Left" />
			                </asp:BoundField>
			                <asp:BoundField DataField="outTopic" HeaderText="Topico de salida" 
				                SortExpression="outTopic" >
			                <HeaderStyle HorizontalAlign="Left" />
			                </asp:BoundField>
                            <asp:BoundField DataField="deviceActiv" HeaderText="Dispositivo activo" 
				                SortExpression="deviceActiv" >
			                <HeaderStyle HorizontalAlign="Left" />
			                </asp:BoundField>
		                </Columns>
	                </asp:GridView>
                    <div runat="server" id="editDiv">
                        <div class="form-group">
                            <asp:label text="Nombre del dispositivo" runat="server" />
                            <asp:textbox runat="server" id="txtName" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:label text="Topico de entrada" runat="server" />
                            <asp:textbox runat="server" id="txtInTopic" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:label text="Topico de salida" runat="server" />
                            <asp:textbox runat="server" id="txtOutTopic" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:label text="Dispositivo activo" runat="server" />
                            <asp:CheckBox Text="" ID="deviceActiv" runat="server" CssClass="form"/>
                        </div>
                        <div class="form-group">
                            <asp:button style="position:fixed" CssClass="form-control btn btn-primary pull-right" text="Enviar" id="btnEnviar" onclick="btnEnviar_click" runat="server" />
                            <br />
                            <br />
                            <asp:button style="position:fixed" CssClass="form-control btn btn-primary pull-right" text="Descartar cambios" id="btnDescartar" onclick="btnDescartar_click" runat="server" />
                        </div>
                    </div>
			    </div>
		    </div>
		</div>
	</div>
</div>
    <script>
        try {
            document.querySelectorAll("[ID*=gvDevices]")[0].classList.add("table");
            document.querySelectorAll("[ID*=gvDevices]")[0].classList.add("table-striped");
            document.querySelectorAll("[ID*=gvDevices]")[0].classList.add("custab");
        }
        catch (err) {}
    </script>
<asp:SqlDataSource runat="server" ID="dsDevices" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>"
		ProviderName="<%$ ConnectionStrings:AirQConnectionString.ProviderName %>"
	SelectCommand="SELECT * FROM devices">
</asp:SqlDataSource>
</asp:Content>



