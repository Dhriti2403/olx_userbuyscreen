using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserBuyScreen.Models;

namespace UserBuyScreen.Controllers
{
    public class HomeController : Controller
    {
        DataAccess dataAccess = new DataAccess();


        public ActionResult newfilter(int? categoryid, int? subcategoryid, int? stateid, int? cityid, int? areaid, decimal? minprice, decimal? maxprice)
        {
            List<ModelAdvertiseImages> list = dataAccess.newFilter(categoryid, subcategoryid, stateid, cityid, areaid, minprice, maxprice);

            return View(list);
        }

        //public ActionResult ShowByProducts()
        //{
        //    DataAccess dataAccess = new DataAccess();
        //    IEnumerable<ModelAdvertiseImages> product = dataAccess.GetProducts();
           
        //    return View(product);
        //}

        
        //public ActionResult showcategorywithsubcategory()
        //{
        //    DataAccess dataAccess = new DataAccess();
        //    List<CategoryWithSubcategoriesViewModel> categorywithsub = dataAccess.GetCategoriesWithSubcategories();

        //    return View(categorywithsub);
        //}

        public ActionResult showcatsub()
        {
            DataAccess dataAccess = new DataAccess();
            List<ModelProductSubCategory> categorywithsub = dataAccess.GetCategoryWithSubcategories();

            return View(categorywithsub);
        }

        public ActionResult showAdvertise(int advertiseId)
        {
            DataAccess dataAccess = new DataAccess();
            IEnumerable<ModelAdvertiseImages> getadd = dataAccess.GetProducts(advertiseId);
            if (getadd != null)
            {
                return View(getadd);
            }

            return View(getadd);
        }
    }
}