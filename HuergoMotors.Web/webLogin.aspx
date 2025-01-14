﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webLogin.aspx.cs" Inherits="HuergoMotors.Web.webLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container p-5">
            <div class="row justify-content-center p-2">
                <div class="col-6">
                    <img src="/Resources/HuergoMotorsLogo.png" class="mx-auto d-block" />
                </div>
            </div>

            <div class="row justify-content-center p-2">
                <div class="col-6">
                    <uc:CampoTexto ID="ctUsuario" runat="server" Text="Usuario" Propiedad="Username" />
                </div>
            </div>
            <div class="row justify-content-center p-2">
                <div class="col-6">
                    <uc:CampoTexto ID="ctPassword" Tipo="Password" runat="server" Text="Contraseña" Propiedad="Password" />
                </div>
            </div>
            <div class="row justify-content-center p-2">
                <div class="col-6">
                    <asp:Button ID="btnLogin" runat="server" Class="btn btn-primary btn-block" Text="Ingresar" OnClick="btnLogin_Click" />
                </div>
            </div>

            <asp:Label ID="lbMsg" runat="server"></asp:Label>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
