using System.Net;
using System.Web.Mvc;
using PhotoStorage.Models;
using PhotoStorage.Services;
using PhotoStorage.ViewModels;
using System.Drawing;
using System.Web;
using PhotoStorage.DAL;

namespace PhotoStorage.Controllers
{
    public class PhotoController : Controller
    {
        public readonly PhotoRepository repository; 

          public PhotoController()
        {
            repository = new PhotoRepository();
        }

        public PhotoController(PhotoRepository photoRepository)
        {
            repository = photoRepository;
        }

        // GET: Photo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new PhotoViewModel {GalleryId = (int) id};

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoViewModel model)
        {
          
            if (model.PhotoUpload == null || model.PhotoUpload.ContentLength == 0)
            {
                ModelState.AddModelError("PhotoUpload", "This field is required");
            }
           
            if (ModelState.IsValid)
            {
                var photo = new Photo
                {
                    Title = model.Name,
                    Description = model.Description,
                    GalleryId = model.GalleryId,
                    FileName = model.PhotoUpload.FileName
                    
                };

                if (model.PhotoUpload != null && model.PhotoUpload.ContentLength > 0)
                {
                    PhotoUploader.SavePhotoToFileSystem(photo, model.PhotoUpload);
                    photo.ThumbnailPath = PhotoUploader.GenerateThumbnail(photo.FilePath);
                    repository.Add(photo);
                    repository.Save();
                }

                return RedirectToAction("ViewGallery", "Gallery", new {id = model.GalleryId});

            }
            return View(model);
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var photo = db.Photos.Find(id);
            //if (photo == null)
            //{
              //  return HttpNotFound();
            //}
            return View();
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var photo = repository.GetById(id);
            repository.Delete(photo);
            return RedirectToAction("Index");
        }
    }
}