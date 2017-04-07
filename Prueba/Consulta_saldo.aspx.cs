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
    public partial class Consulta_saldo : System.Web.UI.Page
    {
        CadenaConexion conexion = new CadenaConexion();
        string CodUsuario;        
        protected void Page_Load(object sender, EventArgs e)
        {
            CodUsuario = Session["CodUsuario"].ToString();            
            Consultar_saldo(CodUsuario);
        }

        public bool Consultar_saldo(string cod_usuario) {
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "SELECt Usuario.CodUsuario ,Usuario.Nombre, Cuenta.Cuenta, Cuenta.Saldo FROM Cuenta,Usuario"+
            " WHERE Cuenta.Usuario_CodUsuario = (SELECT CodUsuario FROM Usuario  WHERE Usuario.CodUsuario  = "+cod_usuario+" ) AND Usuario.CodUsuario = "+cod_usuario+";";
            SqlCommand comando = new SqlCommand(consulta, cn);            

            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            cn.Close();
            if (valores.Rows.Count > 0)
            {
                try {
                    lblNombreCliente.Text = "Saldo de Cuenta: <br/> No. Cuenta: " + valores.Rows[0]["Cuenta"].ToString() + "<br/>" + "Propietario de la cuenta: " + valores.Rows[0]["Nombre"].ToString();
                    lblSaldo.Text = "Q " + valores.Rows[0]["Saldo"].ToString();
                }
                catch { }
                return true;
            }
            else
            {
                try {
                    lblNombreCliente.Text = "Error al consultar los datos del cliente";
                    lblSaldo.Text = "";
                }
                catch { }
                return false;                
            }            
        }

    }
}