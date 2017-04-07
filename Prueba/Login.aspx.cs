using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Prueba
{
    public partial class Login : System.Web.UI.Page
    {
        //Cadena de conexion a la base de datos
        //Pascual y la pascualchica
        CadenaConexion conexion = new CadenaConexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) {
                btnLogin.Click += new System.EventHandler(Login_Click);
            }
        }

        private void Login_Click(Object sender, System.EventArgs e) {
            logearse(txtLoginUsuario.Text, txtLoginContra.Text,txtCodSeguridad.Text);
        }

        public bool logearse(string usuario, string contra, string cod)
        {
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "SELECt Usuario.CodUsuario ,Usuario.Usuario, Usuario.Nombre, Cuenta.Cuenta, Cuenta.Saldo, Cuenta.CodCuenta FROM Cuenta,Usuario " +
            " WHERE Cuenta.Usuario_CodUsuario = (SELECT CodUsuario FROM Usuario  WHERE Usuario.Usuario  = '" + usuario + "' ) AND Usuario.Usuario = '" + usuario + "' AND Usuario.Password = '" + contra +
            "' AND Usuario.CodUsuario =" + cod + ";";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            cn.Close();
            if (valores.Rows.Count > 0)
            {
                try
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Login exitoso')</script>");
                    Session["CodUsuario"] = valores.Rows[0]["CodUsuario"].ToString();
                    Session["Usuario"] = valores.Rows[0]["Usuario"].ToString();
                    Session["Cuenta"] = valores.Rows[0]["Cuenta"].ToString();
                    Session["Saldo"] = valores.Rows[0]["Saldo"].ToString();
                    Session["Nombre"] = valores.Rows[0]["Nombre"].ToString();
                    Session["CodCuenta"] = valores.Rows[0]["CodCuenta"].ToString();
                    Response.Redirect("Consulta_saldo.aspx");
                }
                catch { }

                return true;
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Error, datos incorrectos')</script>");
                return false;
            }

        }

    }
}