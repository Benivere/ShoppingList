using System.Web.Mvc;

namespace ShoppingList.Controllers
{
    public class ShoppingEventPageController : Controller
    {
        public ActionResult Index()
        {
            return View("ShoppingEventPage");
        }
    }
}