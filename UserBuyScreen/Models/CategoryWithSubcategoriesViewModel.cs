using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class CategoryWithSubcategoriesViewModel
    {
        public int productCategoryId { get; set; }
        public string productCategoryName { get; set; }
        public List<SubcategoryViewModel> Subcategories { get; set; }
    }
}

  