<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Debito.aspx.cs" Inherits="Prueba.Debito" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset = "UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimun-scale=1.0"/>
    <title>Realizar Debito a cuenta</title>
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
          <a class="navbar-brand" href="#">BANCA-VIRTUAL</a>
        </div>
   
        <!-- Agrupar los enlaces de navegación, los formularios y cualquier
             otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">
          <ul class="nav navbar-nav">
            <li><a href="Consulta_saldo.aspx">Consultar Saldo</a></li>
            <li><a href="Pago_Servicios.aspx">Pago de Servicios</a></li>
            <li><a href="Transferencias.aspx">Transferencias Electronicas</a></li>
            <li><a href="Credito.aspx">Realizar Credito</a></li>
            <li class="active"><a href="Debito.aspx">Realizar Debito</a></li>
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
            <form id="form1" runat="server" class="desplegable">            
            <h1>DEBITO</h1>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server"> NUMERO DE CUENTA A DEBITAR </asp:Label>
                    <asp:TextBox ID="txtCuentaDestino" runat="server" CssClass ="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server"> MONTO A DEBITAR </asp:Label>
                    <asp:TextBox ID="txtMonto" runat="server" CssClass ="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server"> DESCRIPCION </asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass ="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnDebito" runat="server" Text="Realizar Credito" class="btn btn-primary"/>
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
