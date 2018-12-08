using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NewYearLanding.DAL.RadarioSQL;

namespace NewYearLanding.Controllers {
    public class BaseController : Controller {
        protected readonly IModelFacade mf;
        public BaseController(IModelFacade mf) {
            this.mf = mf;
        }

        protected JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Culture = CultureInfo.InvariantCulture,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}