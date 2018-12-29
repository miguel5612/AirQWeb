<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="registerDevice.aspx.cs" Inherits="airQ.registerDevice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Agregar dispositivo</h3>
    <asp:Label ID="lblNombre" runat="server" Text="Nombre del dispositivo: "></asp:Label><asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <asp:Label ID="lblInTopic" runat="server" Text="Topico de entrada: "></asp:Label><asp:TextBox ID="txtInTopic" runat="server"></asp:TextBox>
    <asp:Label ID="lblOutTopic" runat="server" Text="Topico de salida: "></asp:Label><asp:TextBox ID="txtOutTopic" runat="server"></asp:TextBox>
    <br />
    <asp:SqlDataSource ID="dsDevice" runat="server" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>" SelectCommand="SELECT * FROM [devices]"></asp:SqlDataSource>
    <asp:Button Text="Registrar" ID="btnRegister" runat="server" OnClick="btnRegister_Click" />
</asp:Content>
