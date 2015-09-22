using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoStorage.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int GalleryId { get; set; }
        
        public string FilePath { get; set; }

        public string ThumbnailPath { get; set; }

        public string ThumbnailName { get; set; }

        public string FileName { get; set; }

    }
}