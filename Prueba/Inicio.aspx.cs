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
    public partial class Inicio : System.Web.UI.Page
    {
        //Cadena de conexion a la base de datos
        // Comentario de David en Registro 
        //David y la pascualchica se aman con amor
        // David vrs La hija guerra de mini gigantes
        CadenaConexion conexion = new CadenaConexion();
        protected void Page_Load(object sender, EventArgs e)
        {            
            this.txtNombre.Focus();
            if (IsPostBack) {                
                btnRegistrar.Click += new System.EventHandler(Ingresar_click);
            }
        }
       
        private void Ingresar_click(Object sender, System.EventArgs e) {
            Registro(txtNombre.Text, txtUsuario.Text, txtEmail.Text, txtContra.Text);                                   
        }
        
        public bool Registro(string nombre, string usuario, string correo, string pass) {
            bool BuscarUsuario = ExisteUsuario(usuario);
            if (BuscarUsuario == false) {                
                SqlConnection cn = new SqlConnection(conexion.conexion());
                cn.Open();
                string consulta = "insert into usuario values('" + nombre + "','" + usuario + "','" + correo + "', '" + pass + "');";
                SqlCommand comando = new SqlCommand(consulta, cn);                

                if (comando.ExecuteNonQuery() > 0)
                {
                                  
                    CrearCuenta();
                    LimpiarCampos();
                    cn.Close();
                    return true;
                }
                else
                {
                    
                    return false;                    
                }                
            }
            else {
                          
                LimpiarCampos();
                return false;
            }            
        }

        private void CrearCuenta() {            
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string codigo_usuario ="A-0000"+ cod_usuario();
            string consulta = "insert into Cuenta values('"+codigo_usuario+"',500,"+cod_usuario()+");";
            SqlCommand comando = new SqlCommand(consulta, cn);
            comando.ExecuteNonQuery();
            cn.Close();            
        }

        private string cod_usuario() {
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "select top 1 CodUsuario from usuario order by CodUsuario DESC";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataAdapter reader = new SqlDataAdapter(comando);
            DataTable resultado = new DataTable();
            reader.Fill(resultado);
            string cod = resultado.Rows[0]["CodUsuario"].ToString();            
            cn.Close();
            return cod;
        }

        private bool ExisteUsuario(string usuraio) { 
            SqlConnection cn = new SqlConnection(conexion.conexion());
            cn.Open();
            string consulta = "select Nombre, CodUsuario from usuario where Usuario = '"+usuraio+"';";
            SqlCommand comando = new SqlCommand(consulta, cn);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) {
                return true;
            }
            else {
                return false;
            }            
        }

        private void LimpiarCampos() {
            try {
                txtContra.Text = "";
                txtEmail.Text = "";
                txtNombre.Text = "";
                txtUsuario.Text = "";
            }
            catch { }                      
        }
    }   
}