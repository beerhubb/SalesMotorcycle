using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMotorcycle.Models
{
    public class Data_Motor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name ="ชื่อสินค้า")]
        public string Data_Name { get; set; }

        [Required]
        [Display(Name = "ราคาสินค้า")]
        public int Data_Price { get; set; }

        [Required]
        [Display(Name = "เวลาเพิ่มสินค้า")]
        public DateTime Data_Time { get; set; }

        [Required]
        [Display(Name = "รูปภาพสินค้า")]
        public string Data_Img { get; set; }

        [Required]
        [Display(Name = "สถานะสินค้า")]
        public string Status_item { get; set; }
    }
}
