using PhotoStorage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoStorage.ViewModels
{
    public class PhotoViewModel
    {
        public PhotoViewModel(Photo photo, string galleryName)
        {
            Title = photo.Title;
            Description = photo.Description;
            PhotoPath = photo.FilePath;
            GalleryName = galleryName;
            GalleryId = photo.GalleryId;
            PhotoId = photo.PhotoId;
            Height = photo.Height;
            Width = photo.Width;
        }
        [Display(Name="Title")]
        public string Title { get; set; }

        [Display(Name="Gallery")]
        public string GalleryName { get; set; }

        public int GalleryId { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Required]
        public int PhotoId { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }
    }
}