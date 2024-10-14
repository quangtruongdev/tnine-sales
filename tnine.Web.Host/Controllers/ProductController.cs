using System.Web.Mvc;

namespace tnine.Web.Host.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}