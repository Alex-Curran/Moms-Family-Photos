using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PhotoStorage.Models;

namespace PhotoStorage.ViewModels
{
    public class GalleryViewModel
    {
        [DisplayName("Name")]
        [Required]
        public string GalleryName { get; set; }

        [Required]
        [Editable(false)]
        public int GalleryId { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Date Created")]
        [Editable(false)]
        [Required]
        public DateTime DateCreated { get; set; }

        [DisplayName("Photos")]
        public ICollection<Photo> Photos { get; set; }

    }

}