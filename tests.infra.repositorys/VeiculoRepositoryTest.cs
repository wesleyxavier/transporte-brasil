using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using src.core.entities;
using src.infra.data;
using src.infra.repositorys.veiculo;

namespace tests.infra.repositorys {
    [TestClass]
    public class VeiculoRepositoryTest {
        private IVeiculoRepository repository;
        private AplicacaoContexto db;
        private Mock<IVeiculoRepository> mock;
        private Veiculo result;

        [TestInitialize]
        public void Setup () {
            var options = new DbContextOptionsBuilder<AplicacaoContexto> ()
                .UseInMemoryDatabase (databaseName: "testedb")
                .Options;
            db = new AplicacaoContexto (options);
        }

        [TestMethod]
        public void FindAllMockTest () {

            mock = new Mock<IVeiculoRepository> ();
            var resultado = new List<Veiculo> () {
                new Veiculo (), new Veiculo (),
            };
            mock.Setup (c => c.FindAll ())
                .Returns (Task.FromResult (resultado));

            repository = mock.Object;

            var result = repository.FindAll ().Result;

            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (2, result.Count (), "Repository Veiculo está nullo");
        }

        [TestMethod]
        public void MemoryArrayVazioTest () {
            repository = new VeiculoRepository (db);

            var result = repository.FindAll ().Result;

            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (0, result.Count (), "Repository Veiculo está nullo");
        }

        [TestMethod]
        public void MemoryAddTest () {
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;

            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (1, result.Id, "Repository Veiculo está nullo");

            result = repository.FindAll ().Result.FirstOrDefault ();
            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (1, result.Id, "Repository Veiculo está nullo");
        }

        [TestMethod]
        public void MemoryFindTest () {

            #region Add
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;

            #endregion

            result = repository.Find (1).Result;

            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (1, result.Id, "Repository Veiculo está nullo");
        }

        [TestMethod]
        public void MemoryUpdateTest () {
            #region Add
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;
            var id = result.Id;
            #endregion

            result.Nome = "Marcelo";
            result = repository.Update (result).Result;

            Assert.IsNotNull (result, "Repository Veiculo está nullo");
            Assert.AreEqual (id, result.Id, "Repository Veiculo está nullo");
            Assert.AreEqual ("Marcelo", result.Nome, "Repository Veiculo está nullo");
        }

        [TestMethod]
        public void MemoryRemoveTest () {
            repository = new VeiculoRepository (db);
            #region Add
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;
            var id = result.Id;
            #endregion

            result = repository.Delete (result).Result;

            result = repository.Find (id).Result;
            Assert.IsNull (result, "Repository Veiculo está nullo");
        }
    }
}