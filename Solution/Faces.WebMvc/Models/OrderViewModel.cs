using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace Faces.WebMvc.Models
{
    public class OrderViewModel
    {
        [Display(Name ="Order Id")]
        public Guid OrderId { get; set; }

        [Display(Name ="Email")]
        public string UserEmail { get; set; }

        [Display(Name ="Image File")]
        public IFormFile File { get; set; }

        [Display(Name ="ImageUrl")]
        public string ImageUrl { get; set; }

        [Display(Name ="Order Status")]
        public string StatusString { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageString { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}