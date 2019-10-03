using Microsoft.VisualStudio.TestTools.UnitTesting;
using verificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verificacion.Tests
{
    [TestClass()]
    public class ValidacionClassTests
    {
        [TestMethod()]
        public void RutInvalido()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaRut("14356900", "5");
            Assert.AreEqual(false, resp);
        }
        [TestMethod()]
        public void RutValido2()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaRut("14356900", "4");
            Assert.AreEqual(true, resp);
        }
        [TestMethod()]
        public void RutValido1()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaRut("13345477", "2");
            Assert.AreEqual(true, resp);
        }

        [TestMethod()]
        public void SsccValido1()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaSscc("178046293023231294");
            Assert.AreEqual(true, resp);
        }
        [TestMethod()]
        public void SsccValido2()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaSscc("178046293023231232");
            Assert.AreEqual(true, resp);
        }
        [TestMethod()]
        public void SsccInvalido1()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaSscc("178046293023231090");
            Assert.AreEqual(false, resp);
        }
        [TestMethod()]
        public void SsccInvalido2()
        {
            var clase = new ValidacionClass();
            var resp = clase.ValidaSscc("178046293023230460");
            Assert.AreEqual(false, resp);
        }
        [TestMethod()]
        public void TarjetaValidaMastercard()
        {
            var clase = new ValidacionClass();
            var resp = clase.Validar10("5416781202580496");
            Assert.AreEqual(true, resp);
        }
        [TestMethod()]
        public void TarjetaValidaVisa()
        {
            var clase = new ValidacionClass();
            var resp = clase.Validar10("4564771001249453");
            Assert.AreEqual(true, resp);
        }
        [TestMethod()]
        public void TarjetaInvalida()
        {
            var clase = new ValidacionClass();
            var resp = clase.Validar10("5416781202580493");
            Assert.AreEqual(false, resp);
        }
        [TestMethod()]
        public void RutErrorLargo()
        {
            Exception exception = null;
            try
            {
                var clase = new ValidacionClass();
                var resp = clase.ValidaRut("", "");
            }

            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
                Assert.AreEqual("Error largo", exception.Message);
        }

    }
}