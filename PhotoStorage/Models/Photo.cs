using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoStorage.Models
{
    public class Photo
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PhotoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [ScaffoldColumn(false)]
        [Editable(false)]
        public int GalleryId { get; set; }

        [ScaffoldColumn(false)]
        [Editable(false)]
        public string FilePath { get; set; }

        [ScaffoldColumn(false)]
        [Editable(false)]
        public string ThumbnailPath { get; set; }

        [ScaffoldColumn(false)]
        [Editable(false)]
        public string ThumbnailName { get; set; }

        [ScaffoldColumn(false)]
        [Editable(false)]
        public string FileName { get; set; }

    }
}