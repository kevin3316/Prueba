using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prueba;

namespace Prueba.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        //La hija y david aman a la misma mujer
        // [TestMethod]
        // public void LoginTest()
        // {
            // Login met = new Login();
            // Assert.AreEqual(true, met.logearse("kevin123", "123","2"));
        // }

        [TestMethod()]
        public void RegistroTest()
        {
            Inicio met = new Inicio();
            //Nombre, Nombre usuario, correo, contraseña
            // Si existe el usuario ya no lo registra y no existe lo registra
            // Pull automantico
            Assert.AreEqual(false, met.Registro("Christopher", "hijaaaa", "hijaaaa@gmail", "123"));
        }

        [TestMethod()]
        public void TransferirTest()
        {
            Transferencias met = new Transferencias();
            string monto = "20"; string fecha = "22/03/2017 8:03:00"; string cuenta = "2"; string cuenta_destino = "A-00001"; string cod_usuario = "2";
            Assert.AreEqual(true, met.Transferir2(monto, fecha, cuenta, cuenta_destino, cod_usuario));
        }

        [TestMethod]
        public void Realizar_CreditoTest()
        {
            Credito met = new Credito();
            string monto = "20"; string descripcion = "Por favor realizado"; string fecha = "22/03/2017 8:03:00"; string cuenta = "2"; string cuenta_destino = "A-00001"; string cod_usuario = "2";
            Assert.AreEqual(true, met.Realizar_Credito2(monto, descripcion, fecha, cuenta, cuenta_destino, cod_usuario));
        }

        [TestMethod]
        public void Realizar_DebitoTest()
        {
            Debito met = new Debito();
            string monto = "20"; string descripcion = "Porque te robo"; string fecha = "22/03/2017 8:03:00"; string cuenta = "2"; string cuenta_destino = "A-00001"; string cod_usuario = "2";
            Assert.AreEqual(true, met.Realizar_Debito2(monto, descripcion, fecha, cuenta, cuenta_destino, cod_usuario));
        }

        [TestMethod]
        public void Pago_ServiciosTest()
        {
            Pago_Servicios met = new Pago_Servicios();
            string monto = "30000"; string fecha = "22/03/2017 8:03:00"; string codCuenta = "1000"; string codServicio = "2"; string cod_usuario = "3";
            Assert.AreEqual(true, met.Pagar_servicio2(monto, fecha, codCuenta, codServicio, cod_usuario));
        }

        [TestMethod]
        public void Consultar_SaltoTest()
        {
            Consulta_saldo met = new Consulta_saldo();
            string usuario = "3";
            Assert.AreEqual(true, met.Consultar_saldo(usuario));
        }
    }
}


