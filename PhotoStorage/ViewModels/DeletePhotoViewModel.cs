using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoStorage.ViewModels
{
    public class DeletePhotoViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PhotoId { get; set; }
        public int GalleryId { get; set; }
    }
}