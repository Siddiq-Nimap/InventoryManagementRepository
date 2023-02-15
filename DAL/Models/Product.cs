using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductGenericName { get; set; }

        public string ProductDescription { get; set; }

        public string ProductTitle { get; set; }



        public int ProductWeight { get; set; }


        public int ProductPrice { get; set; }


        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [ForeignKey("Logins")]
        public int UserID { get; set; }

        public Logins Logins { get; set; }

    }
}