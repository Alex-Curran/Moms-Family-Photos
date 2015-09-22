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

        public void Add(Photo newPhoto)
        {
            db.Photos.Add(newPhoto);
        }

        public void Delete(Photo photoToDelete)
        {
            db.Photos.Remove(photoToDelete);
        }

        public void Update(Photo photoToUpdate, int id)
        {
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