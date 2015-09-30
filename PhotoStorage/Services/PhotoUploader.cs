using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using PhotoStorage.Models;
using System.Drawing.Imaging;
using ImageResizer;

namespace PhotoStorage.Services
{
    public class PhotoUploader
    {
        public PhotoUploader()
        {

        }

        public List<Photo> UploadMultiplePhotos(IEnumerable<HttpPostedFileBase>Uploads, int galleryId)
        {
            List<Photo> PhotosAdded = new List<Photo>();
            foreach (var upload in Uploads)
            {
                Photo photo = new Photo();
                photo.Title = Path.GetFileNameWithoutExtension(upload.FileName);
                photo.GalleryId = galleryId;
                photo = SavePhotoToFileSystem(photo, upload);
                PhotosAdded.Add(photo);
            }

            return PhotosAdded;
        }


        public Photo SavePhotoToFileSystem(Photo photo, HttpPostedFileBase photoUpload)
        {
            var uploadDirectory = "/Photos/" + photo.GalleryId + "/";
            var imagePath = uploadDirectory + photoUpload.FileName;
            photoUpload.SaveAs(imagePath);
            photo.FilePath = imagePath;
            photo.ThumbnailPath = GenerateThumbnail(photo.FilePath);

            return photo;
        }

        private string GenerateThumbnail(string original)
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the versions to generate and their filename suffixes.
            versions.Add("_thumb", "width=100&height=100&crop=auto&format=jpg"); //Crop to square 
           
            string basePath = ImageResizer.Util.PathUtils.RemoveExtension(original);

            //To store the list of generated paths
            string thumbnail = null;

            //Generate each version
            foreach (string suffix in versions.Keys)
            {
                //Let the image builder add the correct extension based on the output file type
                thumbnail = ImageBuilder.Current.Build(original, basePath + suffix,
                new ResizeSettings(versions[suffix]), false, true);
            }

            return thumbnail;
        }
    }
}