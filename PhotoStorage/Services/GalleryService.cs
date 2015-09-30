using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PhotoStorage.Models;

namespace PhotoStorage.Services
{
    public class GalleryService
    {
        public GalleryService()
        {

        }

        public Gallery CreateGallery(string Name, string Description)
        {
            Gallery gallery = new Gallery();
            gallery.DateCreated = System.DateTime.Now;
            gallery.GalleryName = Name;
            gallery.Description = Description;

            return gallery;
        }

        public void CreateDirectoryStructure(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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

    }
}