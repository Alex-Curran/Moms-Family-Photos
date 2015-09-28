using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PhotoStorage.Models;

namespace PhotoStorage.ViewModels
{
    public class DeleteGalleryViewModel
    {
        [Required]
        public string GalleryName { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int GalleryId { get; set; }

        [Required]
        public string Description { get; set; }

    }
}