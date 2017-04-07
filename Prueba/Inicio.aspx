<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Prueba.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset = "UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimun-scale=1.0"/>
    <title>Registro de usuarios</title>
    <link rel="stylesheet"  href="Boot/css/bootstrap.css"/>
    <link rel ="stylesheet" href="Boot/css/estilos.css"/>
</head>
<body>
    <script src = "Boot/js/jquery.js"></script>
    <script src ="Boot/js/bootstrap.min.js"></script>    

    <header>
      <nav class="navbar navbar-default" role="navigation">
        <!-- El logotipo y el icono que despliega el menú se agrupan
             para mostrarlos mejor en los dispositivos móviles -->
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse"
                  data-target=".navbar-ex1-collapse">
            <span class="sr-only">Desplegar navegación</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">BANCAR-VIRTUAL</a>
        </div>
   
        <!-- Agrupar los enlaces de navegación, los formularios y cualquier
             otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">
          <ul class="nav navbar-nav">
            <li><a href="Login.aspx">Ingreso de usuarios</a></li>
            <li class="active"><a href="Inicio.aspx">Registro de usuarios</a></li>          
          </ul>
   
          <form class="navbar-form navbar-left" role="search">
            <div class="form-group">
              <input type="text" class="form-control" placeholder="Buscar"/>
            </div>
            <button type="submit" class="btn btn-default">Enviar</button>
          </form>          
        </div>
      </nav>
    </header>

    <div class="container"> 
        <div class="row"> 
            <div class="col-md-4 col-md-offset-4"> 
                <form id="form2" runat="server" class="desplegable"> 
                    <h1><b>REGISTRO</b></h1>
                    <div class="form-group" >
                        <asp:Label ID="Label1" runat="server"> NOMBRE COMPLETO </asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass ="form-control"></asp:TextBox>
                    </div>
                    <div class ="form-group">
                        <asp:Label ID="Label2" runat="server"> NOMBRE DE USUARIO </asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass ="form-control"></asp:TextBox> 
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server"> CORREO ELECTRONICO </asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass ="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server"> PASSWORD </asp:Label>
                        <asp:TextBox ID="txtContra" runat="server" TextMode="Password" CssClass ="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" class="btn btn-primary"/>
                    </div>
                </form>                
            </div>
        </div>
    </div>   
    <footer class="clase-general">
        <p>Implementado y desarrollado por Kevin y Pascual</p>
    </footer>          
</body>
</html>
