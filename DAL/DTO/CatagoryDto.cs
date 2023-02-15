using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CatagoryDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Catagory Name is required ")]
        [DisplayName("Catagory Name")]
        public string CatagoryName { get; set; }

        [DisplayName("Is-Active")]
        public bool IsActive { get; set; }
    }
}
