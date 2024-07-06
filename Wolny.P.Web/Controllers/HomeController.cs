using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Wolny.P.Application.Result;
using Wolny.P.Web.Models;

namespace Wolny.P.Web.Controllers
{
    public class HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<RecorridoModel>> Details(int id)
        {
            var url = $"https://localhost:7168/api/Recorrido/{id}";

            var result = await LlamadaHttp<RecorridoModel>(httpClientFactory, url);

            return View(result);
        }

        public async Task<ActionResult<List<RecorridoModel>>> Recorridos()
        {
            var url = "https://localhost:7168/api/Recorrido";

            var result = await LlamadaHttp<List<RecorridoModel>>(httpClientFactory, url);

            return View(result);
        }

        public async Task<ActionResult<List<CamionModel>>> Camiones()
        {
            var url = "https://localhost:7168/api/Camion";

            var result = await LlamadaHttp<List<CamionModel>>(httpClientFactory, url);

            return View(result);
        }

        public async Task<ActionResult<List<RecorridoModel>>> PuntoDos(string patente)
        {
            var url = $"https://localhost:7168/api/Recorrido/puntoDos?Top=10&Descending=true&Patente={patente}&Finalizado=true";

            var result = await LlamadaHttp<List<RecorridoModel>>(httpClientFactory, url);

            return View(result);
        }

        public async Task<ActionResult<List<CamionModel>>> PuntoTres()
        {
            var url = "https://localhost:7168/api/Camion/PuntoTres?disponible=true";

            var result = await LlamadaHttp<List<CamionModel>>(httpClientFactory, url);

            return View(result);
        }

        public async Task<ActionResult<List<CamionModel>>> PuntoCuatro()
        {
            var url = "https://localhost:7168/api/Camion/PuntoTres?disponible=false";

            var result = await LlamadaHttp<List<CamionModel>>(httpClientFactory, url);

            return View(result);
        }

        private static async Task<T> LlamadaHttp<T>(IHttpClientFactory httpClientFactory, string url) where T : class
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                url)
            //{
            //    Content = JsonContent.Create(new { to = currency, from = "ARS", amount = order.TotalCost })
            //}
            ;

            var httpClient = httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                var content = await httpResponseMessage.Content.ReadAsStringAsync();

                var deserialized = await JsonSerializer.DeserializeAsync<Result<T>>(contentStream, options);
                return deserialized.Data;
            }

            return default(T);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
