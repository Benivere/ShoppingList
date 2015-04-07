using System.Web.Mvc;

namespace ShoppingList.Controllers
{
    public class ShoppingListPageController : Controller
    {
        // GET: ShoppingPage
        public ActionResult Index()
        {
            return View("ShoppingListPage");
        }
    }
}