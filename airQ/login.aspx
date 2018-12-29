<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" MasterPageFile="~/Site.Master" Inherits="airQ.login" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card-group">
            <div class="card p-4">
              <div class="card-body">
                <h1>Iniciar sesion</h1>
                <p class="text-muted">Inicia sesion en tu cuenta</p>
                <div class="input-group mb-3">
                  <div class="input-group-prepend">
                    <span class="input-group-text">
                      <i class="icon-user"></i>
                    </span>
                  </div>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Nombre de usuario" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                </div>
                <div class="input-group mb-4">
                  <div class="input-group-prepend">
                    <span class="input-group-text">
                      <i class="icon-lock"></i>
                    </span>
                  </div>
                  <asp:TextBox ID="txtPassword" cssClass="form-control" type="password" placeholder="Contraseña" runat="server"/>
                    <br />
                    <br />
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label><br />
                </div>
                  <br />
                <div class="row">
                  <div class="col-6" style="margin-left: 40px">
                    <asp:Button ID="btnLogin" CssClass="btn btn-primary px-4" runat="server" Text="Iniciar sesion" OnClick="btnLogin_Click" />
                  </div>
                  <div class="col-6 text-left">
                    <button class="btn btn-link px-0" type="button">Olvidaste tu clave?</button>
                  </div>
                </div>
              </div>
            </div>
            <div class="card text-white bg-primary py-5 d-md-down-none" style="width:44%">
              <div class="card-body text-center">
                <div>
                  <h2>Registrate ahora mismo</h2>
                  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                     <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary active mt-3" Text="Registrarse" OnClick="btnRegister_Click" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <asp:SqlDataSource ID="dsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:AirQConnectionString %>" ProviderName="<%$ ConnectionStrings:AirQConnectionString.ProviderName %>" SelectCommand="SELECT * FROM Users"></asp:SqlDataSource>
</asp:Content>
