using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoStorage.Models;
using System.Diagnostics;

namespace PhotoStorage.DAL
{
    public class PhotoRepository : IGenericRepository<Photo>
    {
        private readonly PhotoStorageDb db = new PhotoStorageDb();

        public Photo GetById(int id)
        {
            return db.Photos.Find(id);
        }

        public string GetGalleryName(int id)
        {
            Photo photo = db.Photos.Find(id);
            Gallery gallery = db.Galleries.Find(photo.GalleryId);
          
            return gallery.GalleryName;
        }

        public int GetGalleryId(int id)
        {
            Photo photo = db.Photos.Find(id);
            return photo.GalleryId;
        }

        public void Add(Photo newPhoto)
        {
            db.Photos.Add(newPhoto);
        }

        public void Delete(Photo photoToDelete)
        {
            db.Photos.Remove(photoToDelete);
        }

        public void Update(Photo photoToUpdate, int photoId)
        {
            Photo originalPhoto = GetById(photoToUpdate.PhotoId);

            photoToUpdate.FileName = originalPhoto.FileName;
            photoToUpdate.FilePath = originalPhoto.FilePath;
            photoToUpdate.GalleryId = originalPhoto.GalleryId;
            photoToUpdate.PhotoId = originalPhoto.PhotoId;
            photoToUpdate.ThumbnailName = originalPhoto.ThumbnailName;
            photoToUpdate.ThumbnailPath = originalPhoto.ThumbnailPath;

            var originalEntity = db.Photos.Find(photoToUpdate.PhotoId);
            db.Entry(originalEntity).CurrentValues.SetValues(photoToUpdate);
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private bool disposed; 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}