using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.DAL.RadarioSQL;
using NewYearLanding.Infrastructure;
using NewYearLanding.Model;

namespace NewYearLanding.Controllers {
    public class CompanyResponse {
        public bool Success { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }

    [AllowAnonymous]
    public class CompanyController : BaseController {
        private readonly ICompaniesRepository _hostRepo;
        private readonly IStatisticsRepository _statsRepo;
        private readonly string _webRootPath;
        private static readonly Random Random;

        public CompanyController(ICompaniesRepository hostRepo,
                                 IStatisticsRepository statsRepo,
                                 IHostingEnvironment env,
                                 IModelFacade modelFacade) : base(modelFacade) {
            _hostRepo = hostRepo;
            _statsRepo = statsRepo;
            _webRootPath = env.WebRootPath;
        }

        static CompanyController() {
            Random = new Random();
        }

        private Statistics GetStatisticsMockObject(Company company) {
            var date = DateTime.Now.AddDays(Random.Next(0, 30));
            var ruLocale = CultureInfo.GetCultureInfo("ru-RU");
            return new Statistics {
                Hostname = company.CompanyTitle,
                Logo = "https://images.radario.ru/images/webhostpageimage/9fb8d006a64240229a4e6e735d1989b2.png",
                BestDay = date.ToString("dddd", ruLocale),
                BestTime = date.ToString("hh:mm"),
                BestMonth = ruLocale.DateTimeFormat.GetMonthName(date.Month),
                BestChannel = "Виджет, касса и билетный стол",
                Year = new StatisticsYear {
                    Events = Random.Next(100, 1000),
                    Tickets = Random.Next(100000, 999999),
                    Money = Math.Round(Random.NextDouble() * Math.Pow(9, 8), 2)
                },
                EventsCovers = new List<string> {
                    "https://images.radario.ru/images/27b9a3b64d994117b5c2c4bdf20744da.jpg",
                    "https://images.radario.ru/images/2544ef644fc3480fa82a74c5a5d0128a.jpg",
                    "https://images.radario.ru/images/b4c519b85a76473facbfb69d9adfb28a.jpg",
                    "https://images.radario.ru/images/96d18ec2f955433d80a4a410fe5b8aa0.jpg",
                    "https://images.radario.ru/images/babf4141e0df456bbd0d631d7b611204.jpg",
                    "https://images.radario.ru/images/95fce4c9769c45098ae1f4eadc356fdb.jpg",
                    "https://images.radario.ru/images/62cc4b65063e4244be2afbb6ea201623.jpg",
                    "https://images.radario.ru/images/48c0d8593b5a4b0d812d8e6f26f3ed40.jpg",
                    "https://images.radario.ru/images/1fa19774e66340299328b951a3c9685d.jpg",
                    "https://images.radario.ru/images/0ccb500c0d5c4d79914b99d208ec8639.jpg",
                    "https://images.radario.ru/images/c6701ae695ca4e42a1a532edd7eb5ce4.jpg",
                    "https://images.radario.ru/images/fa0ae661b5a9463ea14f4bfda23a4676.png"
                }
            };
        }

        [HttpGet]
        [NoCache]
        [Route("/{req}")]
        public ActionResult ServeStatic(string req) {
            if (Guid.TryParse(req, out Guid guid)) {
                return PhysicalFile(Path.Combine(_webRootPath, "index.html"), "text/html");
            }
            if (req == "favicon.ico") {
                return PhysicalFile(Path.Combine(_webRootPath, "favicon.ico"), "image/x-icon");
            }

            return RedirectToAction("Index", "Home");
        }

        public DataViewModel Map(Company c, Statistics s, Guid g) {
            s.Year.Money = Math.Round(s.Year.Money, 0);
            return new DataViewModel {
                Logo = s.Logo,
                Year = s.Year,
                Hostname = s.Hostname,
                BestDay = s.BestDay,
                BestTime = s.BestTime,
                BestMonth = s.BestMonth,
                BestChannel = s.BestChannel,
                EventsCovers = s.EventsCovers,
                CompanyId = c.CompanyId,
                PublicGuid = c.PublicGuid,
                FullAccessGuid = c.PublicGuid == g ? Guid.Empty : c.FullAccessGuid
            };
        }

        [HttpPost]
        [NoCache]
        [Route("/{req}")]
        [Produces("application/json")]
        public async Task<JsonResult> Get(string req) {
            var result = new CompanyResponse {
                Success = false,
                Error = null,
                Data = null
            };
            if (!Guid.TryParse(req, out Guid guid)) {
                result.Error = "Guid not provided";
            } else {
                var company = await _hostRepo.GetCompanyByGuid(guid);
                if (company == null) {
                    result.Error = "Company not found";
                } else {
                    var statistics = await _statsRepo.GetStatisticsByCompanyId(company.CompanyId);
                    result.Data = Map(company, statistics, guid);
                    result.Success = true;
                }
            }
            return new JsonResult(result, JsonSerializerSettings);
        }
    }
}