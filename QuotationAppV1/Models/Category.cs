using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotationAppV1.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public virtual List<Quotation> Q { get; set; }
    }
}