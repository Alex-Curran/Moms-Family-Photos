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

        private HashSet<int> GetRandomIds(int size)
        {
            HashSet<int> randomIds = new HashSet<int>();
            List<int> currentIds = GetCurrentIds();
            Random random = new Random();

            int PhotoCount = db.Photos.Count();
            if (PhotoCount > size)
            {
                while (randomIds.Count < size)
                {
                    randomIds.Add(currentIds.ElementAt(random.Next(0,currentIds.Count())));
                }
            }
            else
            {
                while (randomIds.Count < PhotoCount / 2)
                {
                    randomIds.Add(currentIds.ElementAt(random.Next(0, currentIds.Count())));
                }
            }

            return randomIds;
        }

        private List<int> GetCurrentIds()
        {
            List<int>currentIds = new List<int>();
            int GreatestId= db.Photos.Max(p => p.PhotoId);

            for (int i = 0; i < GreatestId; i++){
                if (db.Photos.Find(i) != null)
                {
                    currentIds.Add(i);
                }
            }

            return currentIds; 
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