<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="registerDevice.aspx.cs" Inherits="airQ.registerDevice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h3>Agregar dispositivo</h3>
	<div class="form-group">
		<asp:Label ID="lblNombre" runat="server" Text="Nombre del dispositivo: " CssClass=""></asp:Label>
		<asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
	 </div>
	
	<div class="form-group">
	    <asp:Label ID="lblInTopic" runat="server" Text="Topico de entrada: " CssClass=""></asp:Label>
        <asp:TextBox ID="txtInTopic" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
	
	<div class="form-group">
	    <asp:Label ID="lblOutTopic" runat="server" Text="Topico de salida: " CssClass=""></asp:Label>
        <asp:TextBox ID="txtOutTopic" CssClass="form-control" runat="server"></asp:TextBox>
	</div>

	<br />
	<asp:SqlDataSource ID="dsDevice" runat="server" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>" SelectCommand="SELECT * FROM [devices]"></asp:SqlDataSource>
	
	<div class="form-group">
        <asp:Button style="position:fixed" CssClass="form-control btn btn-primary pull-right"  Text="Registrar" ID="btnRegister" runat="server" OnClick="btnRegister_Click" />
        <br />
    </div>
</asp:Content>
