using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMotorcycle.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ID_User { get; set; }

        [Required]
        [Display(Name = "ชื่อ-นาสกุล")]
        public string Fname_Lname { get; set; }

        [Required]
        [Display(Name = "ที่อยู่ที่สามารถติดต่อได้")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "เบอร์โทรที่สามารถติดต่อได้")]
        public string Phon { get; set; }

        public string ID_object { get; set; }

        [Required]
        [Display(Name = "ชื่อสินค้า")]
        public string Data_Name { get; set; }

        [Required]
        [Display(Name = "ราคาสินค้า")]
        public int Data_Price { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
        [Display(Name = "จำนวน")]
        public int Number_item { get; set; }

        [Required]
        [Display(Name = "รวมราคา")]
        public int Sum_Price { get; set; }

        [Required]
        [Display(Name = "สถานะการสั่งซื้อ")]
        public string STT_item { get; set; }
    }
}
