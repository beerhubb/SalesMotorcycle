using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMotorcycle.Models
{
    public class UseRegister
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name ="UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "PassWord")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Required]
        [Display(Name = "ชื่อ-นาสกุล")]
        public string Fname_Lname { get; set; }

        [Required]
        [Display(Name = "ว/ด/ป เกิด")]
        public DateTime Bdate { get; set; }

        [Required]
        [Display(Name = "เบอร์โทรที่สามารถติดต่อได้")]
        public string Phon { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "ที่อยู่ที่สามารถติดต่อได้")]
        public string Address { get; set; }

        [Required]
        public string Pictrue { get; set; }

    }
}
