using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class ProductCategoryModel
    {
        public int productCategoryId { get; set; }

        public string productCategoryName { get; set; }

        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }
}