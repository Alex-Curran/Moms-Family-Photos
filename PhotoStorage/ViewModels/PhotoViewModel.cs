using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoStorage.ViewModels
{
    public class PhotoViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int GalleryId { get; set; }

        public string Description { get; set; }
       
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PhotoUpload{ get; set; }
       
    }
}