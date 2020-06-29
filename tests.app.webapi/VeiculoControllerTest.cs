using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
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

            Assert.IsNotNull (response, "Response está nulo");

            response.EnsureSuccessStatusCode ();
            var responseString = await response.Content.ReadAsStringAsync ();

            var lista = JsonSerializer.Deserialize<List<Veiculo>> (responseString);

            Assert.AreEqual (0, lista.Count (), "Lista de Veiculo está com a quantidade invalida");
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

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            Assert.AreNotEqual (HttpStatusCode.BadRequest, response.StatusCode, "Response Bad Request");
            Assert.AreEqual (HttpStatusCode.OK, response.StatusCode, "Response não está Ok");
            #endregion

            var responseString = await response.Content.ReadAsStringAsync ();
            var veiculoResult = JsonSerializer.Deserialize<Veiculo> (responseString);

            #region Assert
            Assert.AreEqual (1, veiculoResult.Id, "Response está com id inválido");
            #endregion

            response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            #endregion

            responseString = await response.Content.ReadAsStringAsync ();
            var veiculoResultado = JsonSerializer.Deserialize<Veiculo> (responseString);

            #region Assert
            Assert.AreEqual ("Porsche", veiculoResultado.Nome, "Veiculo está com Nome Inválido");
            #endregion
        }

        [TestMethod]
        public async Task FindTest () {
            #region  add
            var veiculo = new Veiculo () {
                Nome = "Porsche"
            };

            var jsonContent = JsonSerializer.Serialize (veiculo);
            var contentString = new StringContent (jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

            await clienteHttp.PostAsync ("/api/Veiculo/Add", contentString);
            #endregion

            var response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            Assert.AreNotEqual (HttpStatusCode.NoContent, response.StatusCode, "Response está nulo");
            Assert.AreEqual (HttpStatusCode.OK, response.StatusCode, "Response está nulo");
            #endregion

            var responseString = await response.Content.ReadAsStringAsync ();
            veiculo = JsonSerializer.Deserialize<Veiculo> (responseString);

            #region Assert
            Assert.AreEqual (1, veiculo.Id, "Response está nulo");
            #endregion
        }

        [TestMethod]
        public async Task UpdateTest () {
            #region  add
            var veiculo = new Veiculo () {
                Nome = "Porsche"
            };

            var jsonContent = JsonSerializer.Serialize (veiculo);
            var contentString = new StringContent (jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

            await clienteHttp.PostAsync ("/api/Veiculo/Add", contentString);
            #endregion

            var response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            #endregion

            var responseString = await response.Content.ReadAsStringAsync ();
            veiculo = JsonSerializer.Deserialize<Veiculo> (responseString);

            // ALTERANDO NOME DO CARRO
            veiculo.Nome = "Ferrari";

            jsonContent = JsonSerializer.Serialize (veiculo);
            contentString = new StringContent (jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

            response = await clienteHttp.PutAsync ("/api/Veiculo/Update", contentString);

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            #endregion

            response.EnsureSuccessStatusCode ();
            responseString = await response.Content.ReadAsStringAsync ();

            var veiculoResult = JsonSerializer.Deserialize<Veiculo> (responseString);

            #region Assert
            Assert.AreEqual (1, veiculoResult.Id, "Update: Veiculo está com id inválido");
            Assert.AreEqual ("Ferrari", veiculoResult.Nome, "Update: Veiculo está com nome inválido");
            #endregion

            response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            #endregion

            responseString = await response.Content.ReadAsStringAsync ();
            var veiculoResultFind = JsonSerializer.Deserialize<Veiculo> (responseString);

            #region Assert
            Assert.AreEqual ("Ferrari", veiculoResultFind.Nome, "Find: Veiculo está com nome inválido");
            #endregion
        }

        [TestMethod]
        public async Task RemoveTest () {
            #region  add
            var veiculo = new Veiculo () {
                Nome = "Porsche"
            };

            var jsonContent = JsonSerializer.Serialize (veiculo);
            var contentString = new StringContent (jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

            await clienteHttp.PostAsync ("/api/Veiculo/Add", contentString);
            #endregion

            var response = await clienteHttp.DeleteAsync ($"/api/Veiculo/Delete/?id={1}");

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            Assert.AreNotEqual (HttpStatusCode.NotFound, response.StatusCode, "Response está NotFound");
            Assert.AreEqual (HttpStatusCode.NoContent, response.StatusCode, "Response não está ok");
            #endregion

            response = await clienteHttp.GetAsync ($"/api/Veiculo/Find/?id={1}");

            #region Assert
            Assert.IsNotNull (response, "Response está nulo");
            Assert.AreEqual (HttpStatusCode.NoContent, response.StatusCode, "Response com Status innválido");
            #endregion
        }
    }
}