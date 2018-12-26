using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.Model;

namespace NewYearLanding.Controllers {
    public class HomeController : Controller {
        private const string InternalToken = "aW50ZXJuYWw6MDIzOWRjOTM1NzJiNDZjMTlhZGFjZmRhZTQ3ZDgyYTk=";

        private readonly ICompaniesRepository _hostRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;


        public HomeController(ICompaniesRepository hostRepo,
                              IHttpClientFactory httpClientFactory,
                              ILogger<HomeController> logger) {
            _hostRepo = hostRepo;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [AllowAnonymous]
        [Route("")] // Entry Point
        public IActionResult Index() {
            return Redirect("https://radario.ru/marketing/auth?marketingRedirectionType=newyear");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("oauth-callback")] // Entry Point
        public async Task<IActionResult> OAuthCallback(string code, string exchangeCodeUrl, string providerName) {
            var url = $"{exchangeCodeUrl}/internal/marketing/company?code={code}";
            try {
                using (var httpClient = _httpClientFactory.CreateClient()) {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", InternalToken);
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    var radarioHost = JsonConvert.DeserializeObject<RadarioHost>(responseMessage);
                    var host = await _hostRepo.GetCompanyById(radarioHost.Id);
                    if (host != null) {
                        return Redirect($"/{host.PublicGuid:D}");
                    }
                }
            } catch (Exception e) {
                _logger.LogError(e, "OAuth exception");
            }

            return RedirectToAction("Index");
        }
    }
}