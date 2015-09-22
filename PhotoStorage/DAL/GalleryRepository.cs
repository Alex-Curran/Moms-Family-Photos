using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Owin.Security.Providers.EveOnline;
using PhotoStorage.Models;


namespace PhotoStorage.DAL
{
    public class GalleryRepository: IGenericRepository<Gallery>
    {
        private readonly PhotoStorageDb db = new PhotoStorageDb();

        public IList GetAll()
        {
            try
            {
                return db.Galleries.ToList();
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            return new List<Gallery>();

        }

        public Gallery GetById(int id)
        {
            return db.Galleries.Find(id);
        }

        public void Delete(Gallery galleryToDelete)
        {
            db.Galleries.Remove(galleryToDelete);
        }

        public void Update(Gallery galleryToUpdate, int id)
        {
            Gallery originalEntity = db.Galleries.Find(galleryToUpdate.Id);
            db.Entry(originalEntity).CurrentValues.SetValues(galleryToUpdate);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Add(Gallery newGallery)
        {
            db.Galleries.Add(newGallery);
            db.SaveChanges();
            
        }

        public ICollection<Photo> GetPhotos(int galleryId)
        {
            return (from photo in db.Photos
                    where photo.GalleryId == galleryId
                    select photo).ToList();
        }

        public void DeleteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(path, false);

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

        public void CreateDirectoryStructure(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}