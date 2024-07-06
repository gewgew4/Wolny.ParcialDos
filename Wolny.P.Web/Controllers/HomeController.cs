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

            var result = await LlamadaHttp<RecorridoModel>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        public async Task<ActionResult<List<RecorridoModel>>> Recorridos()
        {
            var url = "https://localhost:7168/api/Recorrido";

            var result = await LlamadaHttp<List<RecorridoModel>>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        public async Task<ActionResult<List<CamionModel>>> Camiones()
        {
            var url = "https://localhost:7168/api/Camion";

            var result = await LlamadaHttp<List<CamionModel>>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        public async Task<ActionResult<List<RecorridoModel>>> PuntoDos(string patente)
        {
            var url = $"https://localhost:7168/api/Recorrido/puntoDos?Top=10&Descending=true&Patente={patente}&Finalizado=true";

            var result = await LlamadaHttp<List<RecorridoModel>>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        public async Task<ActionResult<List<CamionModel>>> PuntoTres()
        {
            var url = "https://localhost:7168/api/Camion/PuntoTres?disponible=true";

            var result = await LlamadaHttp<List<CamionModel>>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        public async Task<ActionResult<PuntoCuatroModel>> PuntoCuatro()
        {
            var urlCamiones = "https://localhost:7168/api/Camion/PuntoTres?disponible=false";
            var urlCiudades = "https://localhost:7168/api/Ciudad";

            var resultCamiones = await LlamadaHttp<List<CamionModel>>(httpClientFactory, urlCamiones, HttpMethod.Get);
            var resultCiudades = await LlamadaHttp<List<CiudadModel>>(httpClientFactory, urlCiudades, HttpMethod.Get);

            var result = new PuntoCuatroModel
            {
                Camiones = resultCamiones,
                Ciudades = resultCiudades
            };

            return View(result);
        }

        public async Task<ActionResult<PuntoUnoModel>> PedidosSinRecorrido()
        {
            var urlPedidos = "https://localhost:7168/api/Pedido/SinRecorrido";
            var urlCiudades = "https://localhost:7168/api/Ciudad";

            var resultPedidos = await LlamadaHttp<List<PedidoModel>>(httpClientFactory, urlPedidos, HttpMethod.Get);
            var resultCiudades = await LlamadaHttp<List<CiudadModel>>(httpClientFactory, urlCiudades, HttpMethod.Get);

            var result = new PuntoUnoModel
            {
                Pedidos = resultPedidos,
                Ciudades = resultCiudades
            };
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSelectedPedidos(List<int> selectedPedidos, int selectedCiudad)
        {
            var url = "https://localhost:7168/api/Recorrido";

            var x = JsonContent.Create(new { pedidoIds = selectedPedidos, algoritmoEnum = 0, origenCiudadId = selectedCiudad });

            var resultPedidos = await LlamadaHttp<RecorridoModel>(httpClientFactory, url, HttpMethod.Post, x);

            // Log or process the payload as needed
            // For example, send it to an API, save to database, etc.

            return RedirectToAction("Index", "Home"); // Redirect back to the listing page
        }



        private static async Task<T> LlamadaHttp<T>(IHttpClientFactory httpClientFactory, string url, HttpMethod method, HttpContent? contentSend = null) where T : class
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var httpRequestMessage = new HttpRequestMessage(
                method,
                url)
            {
                Content = contentSend
            }
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
