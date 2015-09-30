using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoStorage.ViewModels
{
    public class CreatePhotoViewModel
    {
        [Required]
        [Display(Name="Title")]
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters")]
        public string Title { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int GalleryId { get; set; }

        [Display(Name="Description")]
        [StringLength(1024, ErrorMessage = "Maximum length is 1024 characters")]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase PhotoUpload { get; set; }
    }
}