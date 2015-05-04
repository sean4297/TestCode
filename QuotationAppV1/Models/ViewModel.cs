using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuotationAppV1.Models;

namespace QuotationAppV1.Models
{
    public class ViewModel
    {
        public string Quote { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        public int CategoryCount { get; set; }
        public int AuthorCount { get; set; }

        public List<ViewModel> stuff = new List<ViewModel>();

        

    }
}