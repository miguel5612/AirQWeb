<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard.Master" AutoEventWireup="true" CodeBehind="editDevice.aspx.cs" Inherits="airQ.editDevice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:GridView runat="server" ID="gvDevices" DataSourceID="dsDevices"
        EmptyDataText="No tienes aun dispositivos registrados!" AllowPaging="True" 
        AllowSorting="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
        GridLines="Vertical">
        <Columns>
            <asp:BoundField DataField="deviceID" HeaderText="Device ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="deviceID" Visible="False" />
            <asp:HyperLinkField Text="modificar" DataNavigateUrlFields="deviceID" 
                DataNavigateUrlFormatString="editDevice.aspx?deviceID={0}" />
            <asp:HyperLinkField Text="eliminar" DataNavigateUrlFields="deviceID" 
                DataNavigateUrlFormatString="editDevice.aspx?deviceIDArchiv={0}" />
            <asp:BoundField DataField="deviceName" HeaderText="Name" SortExpression="deviceName" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="inTopic" HeaderText="in Topic" 
                SortExpression="inTopic" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="outTopic" HeaderText="out Topic" 
                SortExpression="outTopic" >
            <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="dsDevices" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>"
         ProviderName="<%$ ConnectionStrings:AirQConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM devices">
    </asp:SqlDataSource>
</asp:Content>

