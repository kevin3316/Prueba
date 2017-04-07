using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prueba
{
    public partial class Pago_Servicios : System.Web.UI.Page
    {
        CadenaConexion conexion = new CadenaConexion();
        string cod_usuario;
        string cod_cuenta;
        protected void Page_Load(object sender, EventArgs e)
        {                        
            cod_usuario = Session["CodUsuario"].ToString();
            cod_cuenta = Session["CodCuenta"].ToString();
            if (!IsPostBack)
            {
                Cargar_Servicio();
            }
            else {
                btnPagoServicio.Click += new System.EventHandler(Pagar_Click);
            }
        }

        private void Cargar_Servicio() {
            
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "select CodServicio, Servicio from Servicio;";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataSet valores = new DataSet();
            datos.Fill(valores, "Servicio");
            this.dServicios.DataSource = valores.Tables["Servicio"].DefaultView;
            this.dServicios.DataTextField = "Servicio";
            this.dServicios.DataValueField = "CodServicio";
            this.dServicios.DataBind();            
            cn.Close();
            this.dServicios.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
        }

        protected void dServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dServicios.SelectedValue.ToString() == "-1") { txtMonto.Enabled = false; }
            else { txtMonto.Enabled = true; }            
        }

        private void Pagar_Click(object sender, System.EventArgs e) {
            DateTime fechaHoy = DateTime.Now;
            string fecha = fechaHoy.ToString();
            //Pagar_servicio(txtMonto.Text, fechaHoy.ToString(), cod_cuenta, dServicios.SelectedValue.ToString());
            Pagar_servicio2(txtMonto.Text, fechaHoy.ToString(), cod_cuenta, dServicios.SelectedValue.ToString(), cod_usuario);
        }
       
        public bool Pagar_servicio2(string monto, string fecha, string codCuenta, string codServicio, string cod_usuario)
        {
            if (Verificar_Monto(cod_usuario, monto))
            {
                SqlConnection cn = new SqlConnection(conexion.conexion());
                cn.Open();
                string consulta = "INSERT INTO Pago VALUES(" + monto + ", '" + fecha + "', " + codCuenta + ", " + codServicio + ");";
                SqlCommand comando = new SqlCommand(consulta, cn);
                comando.ExecuteNonQuery();
                cn.Close();
                Actualizar_Saldo(codCuenta, monto, cod_usuario);
                return true;
            }else
            {
                return false;
            }
        }

        private void Pagar_servicio(string monto, string fecha, string codCuenta, string codServicio){            
            if (Verificar_Monto(cod_usuario,monto)) {
                SqlConnection cn = new SqlConnection(conexion.conexion());
                cn.Open();
                string consulta = "INSERT INTO Pago VALUES("+monto+", '"+fecha+"', "+codCuenta+", "+codServicio+");";
                SqlCommand comando = new SqlCommand(consulta, cn);
                comando.ExecuteNonQuery();
                cn.Close();
                Actualizar_Saldo(codCuenta,monto,cod_usuario);
            }            
        }

        private bool Verificar_Monto(string codCuenta, string monto) {
            bool resultado = false;
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "SELECt Cuenta.Saldo FROM Cuenta,Usuario" +
            " WHERE Cuenta.Usuario_CodUsuario = (SELECT CodUsuario FROM Usuario  WHERE Usuario.CodUsuario = " + codCuenta + " ) AND Usuario.CodUsuario=" + codCuenta + ";";
            SqlCommand comando = new SqlCommand(consulta, cn);           
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            if (valores.Rows.Count > 0){
                string Cadsaldo = valores.Rows[0]["Saldo"].ToString();
                double saldo = Convert.ToDouble(Cadsaldo);
                double monto_Pagar = Convert.ToDouble(monto);
                if(saldo >= monto_Pagar){
                    resultado = true;
                }
            }
            cn.Close();
            return resultado;
        }

        private double Nuevo_saldo(string codCuenta, string monto) {
            double resultado = 0;
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "SELECt Cuenta.Saldo FROM Cuenta,Usuario" +
            " WHERE Cuenta.Usuario_CodUsuario = (SELECT CodUsuario FROM Usuario  WHERE Usuario.CodUsuario = " + codCuenta + " ) AND Usuario.CodUsuario=" + codCuenta + ";";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataAdapter datos = new SqlDataAdapter(comando);
            DataTable valores = new DataTable();
            datos.Fill(valores);
            if (valores.Rows.Count > 0){
                string Cadsaldo = valores.Rows[0]["Saldo"].ToString();
                double saldo = Convert.ToDouble(Cadsaldo);
                double monto_Pagar = Convert.ToDouble(monto);
                resultado = saldo - monto_Pagar;
                
            }
            cn.Close();
            return resultado;
        }

        private void Actualizar_Saldo(string CodCuenta,string monto,string cod_usuario) {
            double nuevo_saldo = Nuevo_saldo(cod_usuario, monto);
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "UPDATE Cuenta SET Saldo = "+nuevo_saldo+" WHERE CodCuenta = "+CodCuenta+";";
            SqlCommand comando = new SqlCommand(consulta, cn);
            comando.ExecuteNonQuery();
            cn.Close();
        }
    }
}