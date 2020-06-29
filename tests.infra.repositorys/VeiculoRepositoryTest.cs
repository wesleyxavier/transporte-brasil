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
        public void FindAllMockRetornaListaItensTest () {

            mock = new Mock<IVeiculoRepository> ();
            var resultado = new List<Veiculo> () {
                new Veiculo (), new Veiculo (),
            };
            mock.Setup (c => c.FindAll ())
                .Returns (Task.FromResult (resultado));

            repository = mock.Object;

            var result = repository.FindAll ().Result;

            #region Assert
            Assert.IsNotNull (result, "Lista de Veiculo está nulo");
            Assert.AreEqual (2, result.Count (), "Lista de Veiculo está com a quantidade invalida");
            #endregion
        }

        [TestMethod]
        public void FindAllArrayVazioTest () {
            repository = new VeiculoRepository (db);

            var result = repository.FindAll ().Result;

            #region Assert
            Assert.IsNotNull (result, "Lista de Veiculo está nulo");
            Assert.AreEqual (0, result.Count (), "Lista de Veiculo está com a quantidade invalida");
            #endregion
        }

        [TestMethod]
        public void AddTest () {
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;

            #region Assert
            Assert.IsNotNull (result, "Add: Veiculo está nulo");
            Assert.AreNotEqual (0, result.Id, "Add: Veiculo com id incorreto");
            #endregion

            var id = result.Id;
            var resultFind = repository.Find (id).Result;

            #region Assert
            Assert.IsNotNull (resultFind, "Find: Veiculo está nulo");
            Assert.AreEqual (id, resultFind.Id, "Find:  Veiculo com id incorreto");
            #endregion
        }

        [TestMethod]
        public void FindTest () {

            #region Add
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;

            #endregion

            result = repository.Find (1).Result;

            #region Assert
            Assert.IsNotNull (result, "Veiculo está nulo");
            Assert.AreEqual (1, result.Id, " Veiculo com id incorreto");
            #endregion
        }

        [TestMethod]
        public void UpdateTest () {
            #region Add
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;
            #endregion

            var id = result.Id;
            result.Nome = "Marcelo";

            var resultUpdate = repository.Update (result).Result;

            #region Assert
            Assert.IsNotNull (resultUpdate, "Veiculo está nulo");
            Assert.AreEqual (id, resultUpdate.Id, "Veiculo com id incorreto");
            Assert.AreEqual ("Marcelo", resultUpdate.Nome, "Veiculo está com Nome incorreto");
            #endregion
        }

        [TestMethod]
        public void RemoveTest () {
            #region Add
            repository = new VeiculoRepository (db);

            Veiculo entity = new Veiculo () { Nome = "Wesley" };
            result = repository.Add (entity).Result;
            var id = result.Id;
            #endregion

            result = repository.Delete (result).Result;
            result = repository.Find (id).Result;

            #region Assert
            Assert.IsNull (result, "Veiculo não está nulo");
            #endregion
        }
    }
}