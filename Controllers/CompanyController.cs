using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.DAL.RadarioSQL;
using NewYearLanding.Infrastructure;

namespace NewYearLanding.Controllers {
    public class CompanyResponse {
        public bool Success { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }

    public class CompanyController : BaseController {
        private readonly ICompaniesRepository _repository;
        private readonly string _webRootPath;

        public CompanyController(ICompaniesRepository repository,
                                 IHostingEnvironment env,
                                 IModelFacade modelFacade) : base(modelFacade) {
            _repository = repository;
            _webRootPath = env.WebRootPath;
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
                var company = await _repository.GetCompanyByGuid(guid);
                if (company == null) {
                    result.Error = "Company not found";
                } else {
                    result.Success = true;
                    result.Data = company;
                }
            }
            return new JsonResult(result, JsonSerializerSettings);
        }
    }
}