using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xamarin.Forms;

namespace MeApp.Models
{
    public class Present
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name should contains max 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200, ErrorMessage = "Description should contains max 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }

        [MaxLength]
        public ImageSource DataFiles { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }
        public string UserID { get; set; }
    }
}
