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

        public async Task<ActionResult<ActualizarPlanesModel>> ActualizarPlanes(int id, string error = "")
        {
            ViewBag.Error = error;
            var urlRecorrido = $"https://localhost:7168/api/Recorrido/{id}";

            var resultRecorrido = await LlamadaHttp<RecorridoModel>(httpClientFactory, urlRecorrido, HttpMethod.Get);

            var urlCamiones = $"https://localhost:7168/api/Camion/{resultRecorrido.Camion.Id}";
            var resultCamiones = await LlamadaHttp<CamionModel>(httpClientFactory, urlCamiones, HttpMethod.Get);

            var listadoPlanRecorridoActulizado = new List<PlanRecorridoActulizadoModel>();

            foreach (var item in resultRecorrido.PlanRecorridos)
            {
                if (!item.Finalizado)
                {
                    var agregado = new PlanRecorridoActulizadoModel
                    {
                        PlanRecorridoId = item.Id,
                        FechaFin = item.FechaFin
                    };
                    listadoPlanRecorridoActulizado.Add(agregado);
                }
            }

            var result = new ActualizarPlanesModel
            {
                CamionId = resultCamiones.Id,
                CamionPatente = resultCamiones.Patente,
                PlanesRecorrido = listadoPlanRecorridoActulizado,
                RecorridoId = resultRecorrido.Id
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPlanes(ActualizarPlanesModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedPlanes = model.PlanesRecorrido
                    .Where(pr => pr.IsSelected)
                    .Select(pr => new PlanRecorridoActulizadoModel
                    {
                        PlanRecorridoId = pr.PlanRecorridoId,
                        FechaFin = pr.FechaFin.GetValueOrDefault()
                    }).ToList();

                var actualizarModel = new ActualizarPlanesModel
                {
                    CamionId = model.CamionId,
                    CamionPatente = model.CamionPatente,
                    RecorridoId = model.RecorridoId,
                    PlanesRecorrido = selectedPlanes
                };

                if (actualizarModel.PlanesRecorrido.Any(x=> x.FechaFin == DateTime.MinValue || x.FechaFin == default))
                {
                    return RedirectToAction("ActualizarPlanes", new { id = model.RecorridoId, error = "Fechas incorrectas" });

                }

                var urlActualizar = $"https://localhost:7168/api/PlanRecorrido/ActualizarPlanes";
                var x = JsonContent.Create(actualizarModel);

                var result = await LlamadaHttp<List<PlanRecorridoModel>>(httpClientFactory, urlActualizar, HttpMethod.Put, x);
                if (result == null)
                {

                    return RedirectToAction("ActualizarPlanes", new { id = model.RecorridoId, error = "Se eligieron incorrectamente a los tramos" });

                }
                return RedirectToAction("Recorridos");
            }

            return View(model);
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

        public async Task<ActionResult<AsignarModel>> AsignarleCamion(int id)
        {
            var urlCamiones = "https://localhost:7168/api/Camion/PuntoTres?disponible=true";
            var urlRecorrido = $"https://localhost:7168/api/Recorrido/{id}";

            var resultCamiones = await LlamadaHttp<List<CamionModel>>(httpClientFactory, urlCamiones, HttpMethod.Get);
            var resultRecorrido = await LlamadaHttp<RecorridoModel>(httpClientFactory, urlRecorrido, HttpMethod.Get);

            var result = new AsignarModel
            {
                Camion = resultCamiones,
                Recorrido = resultRecorrido
            };
            if (resultRecorrido.Camion != null)
            {
                throw new Exception("El recorrido ya tiene camión asignado");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<AsignarModel>> AsignarleCamion(AsignarModel entity)
        {
            var url = $"https://localhost:7168/api/Recorrido/AsignarleCamion";

            entity.FechaInicio = DateTime.UtcNow;
            var x = JsonContent.Create(new { RecorridoId = entity.Recorrido.Id, entity.CamionId, entity.FechaInicio });

            var result = await LlamadaHttp<RecorridoModel>(httpClientFactory, url, HttpMethod.Put, x);

            return RedirectToAction("Recorridos");
        }

        public async Task<ActionResult<List<CamionModel>>> Camiones()
        {
            var url = "https://localhost:7168/api/Camion";

            var result = await LlamadaHttp<List<CamionModel>>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        public async Task<ActionResult<CamionModel>> CambioCamion(int id)
        {
            var url = $"https://localhost:7168/api/Camion/{id}";

            var result = await LlamadaHttp<CamionModel>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<CamionModel>> CambioCamion(CamionModel entity)
        {
            var url = "https://localhost:7168/api/Camion";

            entity.Patente = "NoCambiará";

            var x = JsonContent.Create(entity);

            var result = await LlamadaHttp<CamionModel>(httpClientFactory, url, HttpMethod.Put, x);

            return RedirectToAction("Camiones");
        }

        public async Task<ActionResult<List<RecorridoModel>>> PuntoDos(string patente)
        {
            ViewBag.Patente = patente;

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

        public async Task<ActionResult<PuntoUnoModel>> PedidosSinRecorrido(string error = "")
        {
            ViewBag.Error = error;
            var urlPedidos = "https://localhost:7168/api/Pedido/SinRecorrido";
            var urlCiudades = "https://localhost:7168/api/Ciudad";

            var resultPedidos = await LlamadaHttp<List<PedidoModel>>(httpClientFactory, urlPedidos, HttpMethod.Get);
            var resultCiudades = await LlamadaHttp<List<CiudadModel>>(httpClientFactory, urlCiudades, HttpMethod.Get);

            var result = new PuntoUnoModel
            {
                Pedidos = resultPedidos.OrderBy(x => x.Id).ToList(),
                Ciudades = resultCiudades
            };
            return View(result);
        }

        public async Task<ActionResult<PedidoModel>> ListPedidos()
        {
            var url = "https://localhost:7168/api/Pedido";

            var result = await LlamadaHttp<List<PedidoModel>>(httpClientFactory, url, HttpMethod.Get);

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<PedidoModel>> AltaPedidos()
        {
            var urlCiudades = "https://localhost:7168/api/Ciudad";

            var resultCiudades = await LlamadaHttp<List<CiudadModel>>(httpClientFactory, urlCiudades, HttpMethod.Get);

            ViewBag.Ciudades = resultCiudades;

            return View();
        }

        [HttpPost]

        public async Task<ActionResult<PedidoModel>> AltaPedidos(PedidoModel entity)
        {
            var url = "https://localhost:7168/api/Pedido";

            var x = JsonContent.Create(new { CiudadId = entity.Ciudad.Id, entity.Entregado });

            var result = await LlamadaHttp<PedidoModel>(httpClientFactory, url, HttpMethod.Post, x);

            return RedirectToAction("ListPedidos");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSelectedPedidos(List<string> selectedPedidos, int selectedCiudad, int selectedAlgo)
        {
            var listaCiudades = new List<int>();
            var listaPedidos = new List<int>();

            // Validaciones
            if (selectedPedidos.Count < 1)
            {
                return RedirectToAction("PedidosSinRecorrido", new { error = "Tenés que seleccionar al menos un pedido" });
            }

            foreach (var item in selectedPedidos)
            {
                var cortado = item.Split(';');
                listaPedidos.Add(int.Parse(cortado[0]));
                listaCiudades.Add(int.Parse(cortado[1]));
            }
            listaCiudades = listaCiudades.Distinct().ToList();

            if (listaCiudades.Count == 1 && listaCiudades.First() == selectedCiudad)
            {
                return RedirectToAction("PedidosSinRecorrido", new { error = "La ciudad de origen es la misma que la de los pedidos" });
            }

            var url = "https://localhost:7168/api/Recorrido/PuntoUno";

            var x = JsonContent.Create(new { pedidoIds = listaPedidos, algoritmoEnum = selectedAlgo, origenCiudadId = selectedCiudad });

            var resultPedidos = await LlamadaHttp<ResultadoRecorridoModel>(httpClientFactory, url, HttpMethod.Post, x);

            return RedirectToAction("ResultadoRecorrido", "Home", resultPedidos);
        }

        public async Task<ActionResult<ResultadoRecorridoModel>> ResultadoRecorrido(ResultadoRecorridoModel resultPedidos)
        {
            return View(resultPedidos);
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


                var deserialized = await JsonSerializer.DeserializeAsync<Result<T>>(contentStream, options);
                return deserialized.Data;
            }
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return default(T);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
