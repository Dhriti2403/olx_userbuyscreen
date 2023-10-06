using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class ProductSubCategoryModel:ProductCategoryModel
    {
        public int productSubCategoryId { get; set; }
        public int productCategoryId { get; set; }
        public string productSubCategoryName { get; set; }
    }
}