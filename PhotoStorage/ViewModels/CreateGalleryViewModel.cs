using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoStorage.ViewModels
{
    public class CreateGalleryViewModel
    {
        [DisplayName("Name")]
        [Required]
        [StringLength(100, ErrorMessage = "Gallery title maximum length is 100 characters")]
        public string GalleryName { get; set; }

        [DisplayName("Description")]
        [StringLength(1024, ErrorMessage = "Maximum length is 1024 characters")]
        public string Description{ get; set; }  

        [DisplayName("Date Created")]
        [Editable(false)]
        [Required]
        public DateTime DateCreated{ get; set; }
    }
}