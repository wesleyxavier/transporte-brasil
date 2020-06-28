using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using src.core.entities;
using src.core.services.veiculo;
using src.infra.repositorys.veiculo;

namespace tests.core.services {
    [TestClass]
    public class VeiculoServiceTest {
        private Mock<IVeiculoRepository> mock;

        [TestInitialize]
        public void Setup () {
            mock = new Mock<IVeiculoRepository> ();
        }

        [TestMethod]
        public void FindAllTest () {
            mock.Setup (c => c.FindAll ())
                .Returns (Task.FromResult (new List<Veiculo> () {
                    new Veiculo (),
                        new Veiculo (),
                        new Veiculo ()
                }));
            var service = new VeiculoService (mock.Object);
            var result = service.FindAll ().Result;

            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (3, result.Count (), "Repository Veiculo está nullo");
        }
    }
}