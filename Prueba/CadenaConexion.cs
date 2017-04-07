using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba
{
    public class CadenaConexion
    {
        String cadena;
        public CadenaConexion() {
            cadena = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AD1_Practica2;Data Source=DAVID_PC\DAVIDMSSQLSERVER";
        }

        public String conexion(){
            return cadena;
        }
    }
}