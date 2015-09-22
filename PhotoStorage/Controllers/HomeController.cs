using System.Linq;
using System.Web.Mvc;
using PhotoStorage.Models;
using System.IO;
using System.Text;

namespace PhotoStorage.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        { 
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}