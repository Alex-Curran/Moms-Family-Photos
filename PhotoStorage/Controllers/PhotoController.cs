using System.Net;
using System.Web.Mvc;
using PhotoStorage.Models;
using PhotoStorage.Services;
using PhotoStorage.ViewModels;
using System.Drawing;
using System.Web;
using PhotoStorage.DAL;
using System.Data;

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
        public ActionResult Index(int? id)
        {
            PhotoViewModel model = new PhotoViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            Photo photo = repository.GetById((int)id);

            model.Title = photo.Title;
            model.Description = photo.Description;
            model.PhotoPath = photo.FilePath;
            model.GalleryName = repository.GetGalleryName((int)id);
            model.PhotoId = photo.PhotoId;

            return View(model);
        }        
      
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new CreatePhotoViewModel {GalleryId = (int) id};

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePhotoViewModel model)
        {
         
            if (model.PhotoUpload == null || model.PhotoUpload.ContentLength == 0)
            {
                ModelState.AddModelError("PhotoUpload", "This field is required");
            }
           
            if (ModelState.IsValid)
            {
                var photo = new Photo
                {
                    Title = model.Title,
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
           
            return View();
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int galleryId = repository.GetGalleryId(id);

            var photo = repository.GetById(id);
            repository.Delete(photo);
            repository.Save();
            return RedirectToAction("Index", "Gallery", new { id = galleryId });
        }

        public ActionResult Edit(int id)
        {
            var model = repository.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title,Description,PhotoId")] Photo photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Update(photo, photo.PhotoId);
                    repository.Save();

                    return RedirectToAction("Index", "Photo", new { id = photo.PhotoId });

                }
            }
            catch (DataException  dex)
            {                   
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return RedirectToAction("Index", "Photo", new { id = photo.PhotoId });
        }

    }
}