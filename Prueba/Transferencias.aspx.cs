using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Prueba
{
    public partial class Transferencias : System.Web.UI.Page
    {
        CadenaConexion conexion = new CadenaConexion();
        string cod_usuario;
        string cod_cuenta;
        protected void Page_Load(object sender, EventArgs e){
            cod_usuario = Session["CodUsuario"].ToString();
            cod_cuenta = Session["CodCuenta"].ToString();
            if (IsPostBack) {
                btnTransferir.Click += new System.EventHandler(Pagar_Click);
            }
        }

        private void Pagar_Click(object sender, System.EventArgs e) { 
            DateTime fechaHoy = DateTime.Now;
            //Transferir(txtMonto.Text,fechaHoy.ToString(), cod_cuenta);
            Transferir2(txtMonto.Text, fechaHoy.ToString(), cod_cuenta, txtCuentaDestino.Text, cod_usuario);
        }

        public bool Transferir2(string monto, string fecha, string cuenta, string cuenta_destino, string cod_usuario)
        {
            if (VerificarCuenta(cod_usuario, monto, cuenta_destino))
            {
                SqlConnection cn = new SqlConnection(conexion.conexion());
                cn.Open();
                string consulta = "INSERT INTO Tranasferencia VALUES(" + monto + ", '" + fecha + "' , " + cuenta + ", " + Cuenta_Destino(cuenta_destino,monto) + ");";
                SqlCommand comando = new SqlCommand(consulta, cn);
                comando.ExecuteNonQuery();
                cn.Close();
                Actualizar_Saldo(cuenta,monto,cod_usuario);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Transferir(string monto, string fecha, string cuenta) {            
            if (VerificarCuenta(cod_usuario, txtMonto.Text, txtCuentaDestino.Text)) {
                SqlConnection cn = new SqlConnection(conexion.conexion());
                cn.Open();
                string consulta = "INSERT INTO Tranasferencia VALUES("+monto+", '"+fecha+"', "+cuenta+", "+Cuenta_Destino(txtCuentaDestino.Text,monto)+");";
                SqlCommand comando = new SqlCommand(consulta, cn);
                comando.ExecuteNonQuery();
                cn.Close();
                Actualizar_Saldo(cod_cuenta,monto,cod_usuario);
                return true;
            }else
            {
                return false;
            }
        }

        private bool VerificarCuenta(string codUsuario, string monto, string cuenta) {
            bool val1 = false;
            bool val2 = false;
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "select * from Cuenta where Cuenta.Cuenta = '"+cuenta+"';";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            if (valores.Rows.Count > 0){
                val1 = true;
            }
            cn.Close();

            cn.Open();
            string consulta2 = "SELECt Cuenta.Saldo FROM Cuenta,Usuario"+
            " WHERE Cuenta.Usuario_CodUsuario = (SELECT CodUsuario FROM Usuario  WHERE Usuario.CodUsuario  = "+codUsuario+" ) AND Usuario.CodUsuario ="+codUsuario+";";
            comando = new SqlCommand(consulta2, cn);
            datos = new SqlDataAdapter(comando);
            valores = new DataTable();
            datos.Fill(valores);
            if (valores.Rows.Count > 0)
            {
                string Cadsaldo = valores.Rows[0]["Saldo"].ToString().Replace(",",".");
                double saldo = Double.Parse(Cadsaldo, new CultureInfo("en-US"));
                double monto_Pagar = Double.Parse(monto, new CultureInfo("en-US"));
                if (saldo >= monto_Pagar)
                {
                    val2 = true;
                }
            }
            cn.Close();
            if (val1 == true && val2 == true){
                return true;
            }
            else {
                return false;
            }            
        }

        private string Cuenta_Destino(string numero_cuenta, string monto) {
            string r = "";
            string saldo_actual = "";
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "select * from Cuenta where Cuenta.Cuenta = '"+numero_cuenta+"';";
            SqlCommand comando = new SqlCommand(consulta, cn);            
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            if (valores.Rows.Count > 0){
                r = valores.Rows[0]["CodCuenta"].ToString();
                saldo_actual = valores.Rows[0]["Saldo"].ToString().Replace(",",".");
            }            
            cn.Close();

            cn.Open();
            double nuevo_saldo = Double.Parse(saldo_actual, new CultureInfo("en-US")) + Double.Parse(monto, new CultureInfo("en-US"));
            string consulta2 = "UPDATE Cuenta SET Saldo = "+nuevo_saldo.ToString().Replace(",",".")+" WHERE Cuenta.Cuenta = '"+numero_cuenta+"'";
            comando = new SqlCommand(consulta2, cn);
            comando.ExecuteNonQuery();
            cn.Close();
            return r;
        }

        private void Actualizar_Saldo(string CodCuenta, string monto, string cod_usuario)
        {
            double nuevo_saldo = Nuevo_saldo(cod_usuario, monto);
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "UPDATE Cuenta SET Saldo = " + nuevo_saldo.ToString().Replace(",",".") + " WHERE CodCuenta = " + CodCuenta + ";";
            SqlCommand comando = new SqlCommand(consulta, cn);
            comando.ExecuteNonQuery();
            cn.Close();
        }

        private double Nuevo_saldo(string codCuenta, string monto)
        {
            double resultado = 0;
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "SELECt Cuenta.Saldo FROM Cuenta,Usuario" +
            " WHERE Cuenta.Usuario_CodUsuario = (SELECT CodUsuario FROM Usuario  WHERE Usuario.CodUsuario = " + codCuenta + " ) AND Usuario.CodUsuario=" + codCuenta + ";";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            if (valores.Rows.Count > 0)
            {
                string Cadsaldo = valores.Rows[0]["Saldo"].ToString().Replace(",",".");
                double saldo = Double.Parse(Cadsaldo, new CultureInfo("en-US"));
                double monto_Pagar = Double.Parse(monto, new CultureInfo("en-US"));
                resultado = saldo - monto_Pagar;

            }
            cn.Close();
            return resultado;
        }
    }
}