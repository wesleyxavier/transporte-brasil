using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.app.webapi;
using src.core.entities;

namespace tests.app.webapi {
    [TestClass]
    public class VeiculoControllerTest {
        private TestServer testeServer;
        private HttpClient clienteHttp;
        [TestInitialize]
        public void Setup () {
            var webHost = new WebHostBuilder ().UseStartup<Startup> ();
            testeServer = new TestServer (webHost);
            clienteHttp = testeServer.CreateClient ();
        }

        [TestMethod]
        public async Task FindAllArrayVazioTest () {
            var response = await clienteHttp.GetAsync ("/api/Veiculo/Get");
            response.EnsureSuccessStatusCode ();
            var responseString = await response.Content.ReadAsStringAsync ();

            Assert.IsNotNull (responseString);

            var lista = JsonSerializer.Deserialize<List<Veiculo>> (responseString);

            Assert.IsNotNull (lista);

            Assert.AreEqual (0, lista.Count ());
        }

        [TestMethod]
        public async Task AddTest () {

            var veiculo = new Veiculo () {
                Nome = "Porsche"
            };

            var jsonContent = JsonSerializer.Serialize (veiculo);
            var contentString = new StringContent (jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

            var response = await clienteHttp.PostAsync ("/api/Veiculo/Add", contentString);

            Assert.AreNotEqual (HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual (HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync ();

            Assert.IsNotNull (responseString);

            var veiculoResult = JsonSerializer.Deserialize<Veiculo> (responseString);

            Assert.AreEqual (1, veiculoResult.Id);

            response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");
            responseString = await response.Content.ReadAsStringAsync ();
            var veiculoResultado = JsonSerializer.Deserialize<Veiculo> (responseString);

            // ALTERANDO NOME DO CARRO
            Assert.AreEqual ("Porsche", veiculoResultado.Nome);
        }

        [TestMethod]
        public async Task FindTest () {
            await AddTest ();

            var response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");

            Assert.AreNotEqual (HttpStatusCode.NoContent, response.StatusCode);
            Assert.AreEqual (HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync ();

            Assert.IsNotNull (responseString);

            var veiculo = JsonSerializer.Deserialize<Veiculo> (responseString);

            Assert.AreEqual (1, veiculo.Id);
        }

        [TestMethod]
        public async Task UpdateTest () {
            await AddTest ();

            var response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");
            var responseString = await response.Content.ReadAsStringAsync ();
            var veiculo = JsonSerializer.Deserialize<Veiculo> (responseString);

            // ALTERANDO NOME DO CARRO
            veiculo.Nome = "Ferrari";

            var jsonContent = JsonSerializer.Serialize (veiculo);
            var contentString = new StringContent (jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

            response = await clienteHttp.PutAsync ("/api/Veiculo/Update", contentString);
            response.EnsureSuccessStatusCode ();
            responseString = await response.Content.ReadAsStringAsync ();

            Assert.IsNotNull (responseString);

            var veiculoResult = JsonSerializer.Deserialize<Veiculo> (responseString);

            Assert.AreEqual (1, veiculoResult.Id);
            Assert.AreEqual ("Ferrari", veiculoResult.Nome);

            response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");
            responseString = await response.Content.ReadAsStringAsync ();
            var veiculoResultFind = JsonSerializer.Deserialize<Veiculo> (responseString);

            Assert.AreEqual ("Ferrari", veiculoResultFind.Nome);
        }

        [TestMethod]
        public async Task RemoveTest () {
            await AddTest ();

            var response = await clienteHttp.DeleteAsync ($"/api/Veiculo/Delete/?id={1}");

            Assert.AreNotEqual (HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual (HttpStatusCode.NoContent, response.StatusCode);

            await FindAllArrayVazioTest ();
        }
    }
}