using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PhotoStorage.DAL;
using PhotoStorage.Models;
using PhotoStorage.ViewModels;
using PhotoStorage.Services;

namespace PhotoStorage.Controllers
{
    public class GalleryController : Controller
    {
        private readonly GalleryRepository repository ;

        public GalleryController()
        {
            repository = new GalleryRepository();
        }

        public GalleryController(GalleryRepository galleryRepository)
        {
            repository = galleryRepository;
        }

        // GET: Galleries
        public ActionResult Index()
        {
            var galleries = repository.GetAll();
            List<GalleryListViewModel> model = new List<GalleryListViewModel>();
            foreach (Gallery gallery in galleries)
            {
                model.Add(
                    new GalleryListViewModel()
                    {
                        Name = gallery.GalleryName,
                        Id = gallery.Id,
                        ImageUrl = ("~/Content/images/empty_gallery.png")
                    }
                );

                gallery.Photos = repository.GetPhotos(gallery.Id);

                if (gallery.Photos.Count > 0)
                {
                    GalleryListViewModel galleryListViewModel = model.Last();
                    galleryListViewModel.ImageUrl = gallery.Photos.First().ThumbnailPath;
                    model.Remove(model.Last());
                    model.Add(galleryListViewModel);
                }
            } 

            return View(model);
        }

        // GET: Galleries/View/5
        public ActionResult ViewGallery(int id)
        {
            Gallery gallery = repository.GetById(id);
            GalleryViewModel model = new GalleryViewModel();

            if (gallery == null)
            {
                return HttpNotFound();
            }

            gallery.Photos = repository.GetPhotos(gallery.Id);

            model.Photos = gallery.Photos;
            model.GalleryId = gallery.Id;
            model.GalleryName = gallery.GalleryName;
            model.Description = gallery.Description;
            model.DateCreated = gallery.DateCreated;
            
            return View(model);
        }

        // GET: Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGalleryViewModel galleryViewModel)
        {
            if (ModelState.IsValid)
            {
                GalleryService galleryService = new GalleryService();
                PhotoUploader photoUploader = new PhotoUploader();

                Gallery gallery = galleryService.CreateGallery(galleryViewModel.GalleryName, galleryViewModel.Description);
                repository.Add(gallery);
                repository.Save();

                gallery.Path = Server.MapPath("/Photos/" + gallery.Id);
                galleryService.CreateDirectoryStructure(gallery.Path);

                List<Photo> photos = photoUploader.UploadMultiplePhotos(galleryViewModel.PhotoUpload, gallery.Id);

                repository.AddPhotos(photos);
                repository.Save();

                return RedirectToAction("ViewGallery", "Gallery", new {id = gallery.Id});
            }

            return View(new CreateGalleryViewModel());
        }

        // GET: Gallery/Edit/5
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
        public ActionResult Edit([Bind(Include = "Id,GalleryName,Description,Path")] Gallery gallery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Update(gallery, gallery.Id);
                    repository.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException  dex)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                Debug.WriteLine(dex.Message);
                
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Index","Gallery");
        }


        // GET: Gallery/Delete/5
        public ActionResult Delete(int id)
        {
            var gallery = repository.GetById(id);
            DeleteGalleryViewModel model = new DeleteGalleryViewModel();

            if (gallery == null)
            {
                return HttpNotFound();
            }
            var Photos = repository.GetPhotos(id);
           
            model.GalleryName = gallery.GalleryName;
            model.GalleryId = gallery.Id;
            model.Description = gallery.Description;
            
            return View(model);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = repository.GetById(id);
            GalleryService galleryService = new GalleryService();

            repository.Delete(gallery);
            galleryService.DeleteDirectory(gallery.Path);
            repository.Save();
            
            return RedirectToAction("Index");
        }
  
    }



}
