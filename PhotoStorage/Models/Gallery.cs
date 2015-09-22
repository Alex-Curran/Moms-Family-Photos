using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoStorage.Models
{
    public class Gallery
    {
        [Key]
        [Column("GalleryId")]
        public int Id { get; set; }
        public string GalleryName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated{ get; set; }
        public ICollection<Photo> Photos { get; set; }



    }

    
}