using System.Linq;
using System.Web.Mvc;
using PhotoStorage.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PhotoStorage.DAL;
using PhotoStorage.ViewModels;

namespace PhotoStorage.Controllers
{
    public class HomeController : Controller
    {
        public readonly PhotoRepository repository; 

        public HomeController()
        {
            repository = new PhotoRepository();
        }

        public HomeController(PhotoRepository photoRepository)
        {
            repository = photoRepository;
        }
       
        public ActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel();
            int size = 20;

            List<Photo> HomepagePhotos = repository.GetHomePagePhotos(size);
            model.Photos = HomepagePhotos;

            return View(model);
           
        }

        public ActionResult About()
        { 
            return View();
        }

    }
}