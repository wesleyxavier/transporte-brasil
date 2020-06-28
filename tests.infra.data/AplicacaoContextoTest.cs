using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.core.entities;
using src.infra.data;

namespace tests.infra.data {
    [TestClass]
    public class AplicacaoContextoTest : IDisposable {

        private AplicacaoContexto db;

        [TestInitialize]
        public void Setup () {
            DbContextOptions<AplicacaoContexto> options = null;

            options = new DbContextOptionsBuilder<AplicacaoContexto> ()
                .UseInMemoryDatabase (databaseName: "testedb")
                .Options;

            db = new AplicacaoContexto (options);
        }

        [TestMethod]
        public async Task TestConnectionInMemory () {

            var lista = await db.Veiculo.ToListAsync ();

            Assert.IsNotNull (lista);
            Assert.AreEqual (0, lista.Count ());
        }

        [TestMethod]
        public async Task TestConnectionSqlUmItem () {

            var veiculo = new Veiculo () {
                Nome = "Teste"
            };

            await db.Set<Veiculo> ().AddAsync (veiculo);
            await db.SaveChangesAsync ();

            var lista = await db.Set<Veiculo> ()?.ToListAsync ();

            Assert.IsNotNull (lista);
            Assert.AreEqual (1, lista.Count ());
            Assert.AreEqual (lista[0].Nome, "Teste");

            lista[0].Nome = "Novo Nome";
            db.Set<Veiculo> ().Update (lista[0]);
            await db.SaveChangesAsync ();
            lista = await db.Set<Veiculo> ()?.ToListAsync ();

            Assert.IsNotNull (lista);
            Assert.AreEqual (1, lista.Count ());
            Assert.AreEqual (lista[0].Nome, "Novo Nome");
            Assert.AreEqual (lista[0].Id, 1);

            db.Set<Veiculo> ().Remove (lista[0]);
            await db.SaveChangesAsync ();

            lista = await db.Set<Veiculo> ()?.ToListAsync ();

            Assert.IsNotNull (lista);
            Assert.AreEqual (0, lista.Count ());

            var item = db.Set<Veiculo> ().Find (1);

            Assert.IsNull (item);
        }

        public void Dispose () {
            GC.Collect ();
        }
    }
}