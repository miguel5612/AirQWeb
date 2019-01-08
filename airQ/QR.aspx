<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="QR.aspx.cs" Inherits="airQ.QR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/html5-qrcode.js"></script>
    <script src="Scripts/qr.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div ID="reader" style="width:320px;height:250px;"></div>
    <asp:TextBox runat="server" ID="txtData" />
</asp:Content>
