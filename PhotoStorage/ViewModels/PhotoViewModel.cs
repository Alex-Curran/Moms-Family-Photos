using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoStorage.ViewModels
{
    public class PhotoViewModel
    {
        [Display(Name="Title")]
        public string Title { get; set; }

        [Display(Name="Gallery")]
        public string GalleryName { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Required]
        public int PhotoId { get; set; }

        [Required]
        public string PhotoPath { get; set; }
    }
}