using Microsoft.AspNetCore.Mvc;

namespace NewYearLanding.Controllers {
    public class HomeController : Controller {
        // GET
        [Route("")] // Entry Point
        public IActionResult Index() {
            return Redirect("https://radario.ru");
        }
    }
}