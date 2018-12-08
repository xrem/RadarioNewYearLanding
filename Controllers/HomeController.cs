using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewYearLanding.Controllers {
    public class HomeController : Controller {
        // GET
        [AllowAnonymous]
        [Route("")] // Entry Point
        public IActionResult Index() {
            return Redirect("https://radario.ru");
        }
    }
}