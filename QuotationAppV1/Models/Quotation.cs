﻿using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotationAppV1.Models
{
    public class Quotation
    {

        //public Quotation(){
        //this.DateAdded = DateTime.Now;
        //}

        public int QuotationID { get; set; }

        [Required(ErrorMessage = "Quotation is required!")]
        public string Quote { get; set; }
        

        [Required(ErrorMessage = "Author is required!")]
        public string Author { get; set; }

        public bool MyProperty { get; set; }

        public virtual Category Category { get; set; }

        public string CategoryName { get; set; }

        public int CategoryID { get; set; }

        //[DisplayFormat(DataFormatString="{0:MM/DD/YYYY}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "(0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }


    }
}