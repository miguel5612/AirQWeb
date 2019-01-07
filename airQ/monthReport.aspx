<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="monthReport.aspx.cs" Inherits="airQ.monthReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:ScriptManager ID="scriptmanager1" runat="server">
	</asp:ScriptManager>
	<br />    
            <h3>Reporte mensual (<%: Session["day"].ToString() + "-" + Session["month"].ToString() + "-" + Session["year"].ToString() %>  / <%: DateTime.Parse( "1" + "/" +  (Convert.ToInt32( Session["month"]) + 1).ToString()  + "/" + Session["year"].ToString()).AddDays(-1).ToString("dd-MM-yyyy") %>) </h3>
            <p>En este reporte se muestran los registros apartir de la fecha seleccionada hasta final de mes.</p>
	<br />
	<div>        
		<asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
		<asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
					TargetControlID="txtDate" Format="dd/MM/yyyy" >
				</asp:CalendarExtender>
        <asp:Button Text="Calcular" CssClass="btn btn-success" ID="btnCalcular" OnClick ="btnCalcular_Click" runat="server" />
		<br />
		<br />

        <div class="container">
            <div class="row">
                <div>
                    <h3>Resultados y recomendaciones</h3>
                </div>
                <div>
                    <asp:GridView ID="GVResults" runat="server"></asp:GridView>
                </div>
            </div>
        </div>

		<div class="container">
            <div class="row">
                <div>
                    <h3>Valores promedio de las mediciones</h3>
                </div>
                <div>
                    <asp:GridView ID="GVProms" runat="server"></asp:GridView>
                </div>
            </div>
        </div>

		<div class="container">
	<div class="row">
        <div>
            <h3>Valores grabados en nuestros servidores</h3>
        </div>
		<div class="col-md-12">
			<div class="alert info">
				<div style="margin-left:0%">
					<asp:GridView runat="server" ID="gvReport" DataSourceID="dsDevices"
						EmptyDataText="No tienes registros actualmente!" AllowPaging="True" 
						AllowSorting="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" 
						BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
						GridLines="Vertical" AutoGenerateColumns ="false">
						<Columns>
							<asp:BoundField DataField="dataId" HeaderText="Numero identificador" InsertVisible="False" 
								ReadOnly="True" SortExpression="deviceID" Visible="False" />
							<asp:HyperLinkField HeaderText="Geo-Referencia" Text="ver ubicacion en el mapa" DataNavigateUrlFields="latitud,longitud" 
								DataNavigateUrlFormatString="https://www.google.com/maps/@{0},{1},20z" />       
                             <asp:boundfield HeaderText="Fecha de toma de la muestra" datafield="registerAt" htmlencode="false" />
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
                            							
							<asp:BoundField DataField="Metano" HeaderText="Metano" 
								SortExpression="Metano" >
							<HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                            							
							<asp:BoundField DataField="NH4" HeaderText="NH4" 
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

            document.querySelectorAll("[ID*=GVProms]")[0].classList.add("table");
            document.querySelectorAll("[ID*=GVProms]")[0].classList.add("table-striped");
            document.querySelectorAll("[ID*=GVProms]")[0].classList.add("custab");

            document.querySelectorAll("[ID*=GVResults]")[0].classList.add("table");
            document.querySelectorAll("[ID*=GVResults]")[0].classList.add("table-striped");
            document.querySelectorAll("[ID*=GVResults]")[0].classList.add("custab");
        }
        catch (err) {}
    </script>
        <asp:SqlDataSource runat="server" ID="dsDevices" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>"
		ProviderName="<%$ ConnectionStrings:AirQConnectionString.ProviderName %>"
	SelectCommand="SELECT * FROM measurements WHERE ([topic] = @iNTopic) And MONTH(registerAt) = @month  AND DAY(registerAt)>= @day AND DAY(registerAt)< @endDay AND YEAR(registerAt) = @year ORDER BY registerAt ASC">
            <selectparameters>
                <asp:SessionParameter Name="iNTopic" SessionField="iNTopic" Type="String" />
                <asp:SessionParameter Name="day" SessionField="day" Type="String" />
                <asp:SessionParameter Name="endDay" SessionField="endDay" Type="String" />
                <asp:SessionParameter Name="month" SessionField="month" Type="String" />
                <asp:SessionParameter Name="year" SessionField="year" Type="String" />
            </selectparameters>
</asp:SqlDataSource>
	</div>
</asp:Content>
