<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="airQ.info" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
	<div class="row">
    	<div class="col-md-12">
		<div class="aro-pswd_info">
			<div id="pswd_info">
				<h4>Analisis realizados</h4>
				<ul>
					<asp:GridView ID="GVResults" runat="server"></asp:GridView>
				</ul>
                <asp:Label Text="Ultima muestra recibida: " runat="server" />
                <asp:Label Text="" ID="lblFecha" runat="server" />
                </div>
			</div>
		</div>
	</div>
    <script>        
            document.querySelectorAll("[ID*=GVResults]")[0].classList.add("table");
            document.querySelectorAll("[ID*=GVResults]")[0].classList.add("table-striped");
            document.querySelectorAll("[ID*=GVResults]")[0].classList.add("custab");
    </script>
</div>

</asp:Content>
