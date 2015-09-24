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

        public List<Photo> GetHomePagePhotos(int size)
        {
            List<Photo> homepagePhotos = new List<Photo>(size);
            HashSet<int> randomIds = GetRandomIds(size);
           
            foreach (var randomId in randomIds)
            {
                homepagePhotos.Add(GetById(randomId));
            }

            return homepagePhotos;
        }

        private HashSet<int> GetRandomIds(int size )
        {
            HashSet<int> randomIds = new HashSet<int>();
            Random random = new Random();

            int PhotoCount = db.Photos.Count();

            if (PhotoCount > size)
            {
                while (randomIds.Count < size)
                {
                    randomIds.Add(random.Next(1, PhotoCount));
                }
            }
            else
            {
                while (randomIds.Count < PhotoCount / 2)
                {
                    randomIds.Add(random.Next(0, PhotoCount));
                }
            }

            return randomIds;
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