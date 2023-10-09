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


        public ActionResult newfilter(int? categoryid, int? subcategoryid, int? stateid, int? cityid, int? areaid, decimal? minprice, decimal? maxprice,int? advertiseId)
        {
            List<AdvertiseImagesModel> list = dataAccess.newFilter(categoryid, subcategoryid, stateid, cityid, areaid, minprice, maxprice, advertiseId);

            return View(list);
        }


        public ActionResult showcategorywithsubcategory()
        {
            DataAccess dataAccess = new DataAccess();
            List<CategoryWithSubcategoriesViewModel> categorywithsub = dataAccess.GetCategoriesWithSubcategories();

            return View(categorywithsub);
        }

        public ActionResult showcatsub()
        {
            DataAccess dataAccess = new DataAccess();
            List<ProductSubCategoryModel> categorywithsub = dataAccess.GetCategoryWithSubcategories();

            return View(categorywithsub);
        }

        public ActionResult shoeAdvertiseDetails(int advertiseId)
        {
            DataAccess dataAccess = new DataAccess();
            IEnumerable<AdvertiseImagesModel> advertise = dataAccess.GetAdvertiseById(advertiseId);

            return View(advertise);
        }

    }
}