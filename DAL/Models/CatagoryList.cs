using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class CatagoryList
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Products")]
        public int PId { get; set; }

        [Key, Column(Order =1)]
        [ForeignKey("Catagories")]
        public int CId { get; set; }

        public Catagory Catagories { get; set; }
        public Product Products { get; set; }

        public bool IsActive { get; set; }
    }
}