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
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var result = new RecorridoModel();
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://localhost:7168/api/Recorrido/{id}")
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

                var deserialized = await JsonSerializer.DeserializeAsync<Result<RecorridoModel>>(contentStream, options);
                result = deserialized.Data;
            }

            return View(result);
        }

        public async Task<ActionResult<List<RecorridoModel>>> Recorridos()
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var result = new List<RecorridoModel>();
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://localhost:7168/api/Recorrido")
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

                var deserialized = await JsonSerializer.DeserializeAsync<Result<List<RecorridoModel>>>(contentStream, options);
                result = deserialized.Data;
            }

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
