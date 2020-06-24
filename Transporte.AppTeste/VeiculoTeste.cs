using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transporte.Entidades;

namespace Transporte.AppTeste
{
    [TestClass]
    public class VeiculoTeste
    {
        [TestMethod]
        public void TotalArrayVazio()
        {
            var lista = new List<Veiculo>();

            Assert.AreEqual(lista.Count(), 0);
        }
    }
}
