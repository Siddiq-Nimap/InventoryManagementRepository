using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models.Models
{
    public class Catagory
    {

        [Key]
        public int Id { get; set; }

        public string CatagoryName { get; set; }

        public bool IsActive { get; set; }
    }
}