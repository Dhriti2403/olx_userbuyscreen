using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class DataAccess
    {
        private SqlConnection _connection;
        public DataAccess()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        }


        #region FILTER
        public List<AdvertiseImagesModel> newFilter(int? productCategoryId, int? productSubCategoryId, int? stateId, int? cityId,
            int? areaId, decimal? minprice, decimal? maxprice, int? advertiseId)
        {
            List<AdvertiseImagesModel> models = new List<AdvertiseImagesModel>();
            _connection.Open();
            SqlCommand cmd = new SqlCommand("newfilter", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productCategoryId", productCategoryId);
            cmd.Parameters.AddWithValue("@productsubCategoryId", productSubCategoryId);
            cmd.Parameters.AddWithValue("@stateId", stateId);
            cmd.Parameters.AddWithValue("@cityId", cityId);
            cmd.Parameters.AddWithValue("@areaId", areaId);
            cmd.Parameters.AddWithValue("@minprice", minprice);
            cmd.Parameters.AddWithValue("@maxprice", maxprice);
            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                bool isApproved = Convert.ToBoolean(reader["advertiseapproved"]);

                if (isApproved)
                {
                    AdvertiseImagesModel model = new AdvertiseImagesModel()
                    {
                        advertiseTitle = reader["advertiseTitle"].ToString(),
                        imageData = (byte[])reader["imageData"],
                        advertiseDescription = reader["advertiseDescription"].ToString(),
                        advertisePrice = Convert.ToInt32(reader["advertisePrice"]),
                        cityName = reader["cityName"].ToString(),
                        stateName = reader["statename"].ToString(),
                        areaName = reader["areaName"].ToString(),
                        advertiseId = Convert.ToInt32(reader["advertiseId"]),
                        productCategoryName= reader["productCategoryName"].ToString(),
                        productSubCategoryName= reader["productSubCategoryName"].ToString(),
                        productCategoryId = Convert.ToInt32(reader["productCategoryId"]),
                        productSubCategoryId = Convert.ToInt32(reader["productSubCategoryId"]),

                    };
                    models.Add(model);
                }
            }
            _connection.Close();
            return models;
        }

        #endregion




        //public List<CategoryWithSubcategoriesViewModel> GetCategoriesWithSubcategories()
        //{
        //    List<CategoryWithSubcategoriesViewModel> categoriesWithSubcategories = new List<CategoryWithSubcategoriesViewModel>();


        //    _connection.Open();

        //    string query = @"
        //    SELECT
        //        pc.productCategoryId AS CategoryId,
        //        pc.productCategoryName AS CategoryName,
        //        sb.productSubCategoryId AS SubcategoryId,
        //        sb.productSubCategoryName AS SubcategoryName
        //    FROM
        //        tbl_ProductCategory pc
        //    LEFT JOIN
        //        tbl_ProductSubCategory sb
        //    ON
        //        pc.productCategoryId = sb.productCategoryId
        //    ORDER BY
        //        pc.productCategoryId, sb.productSubCategoryId";

        //    using (SqlCommand command = new SqlCommand(query, _connection))
        //    using (SqlDataReader reader = command.ExecuteReader())
        //    {
        //        CategoryWithSubcategoriesViewModel currentCategory = null;

        //        while (reader.Read())
        //        {
        //            int productCategoryId = reader.GetInt32(reader.GetOrdinal("productCategoryId"));
        //            string productCategoryName = reader.GetString(reader.GetOrdinal("productCategoryName"));
        //            int productSubCategoryId = reader.GetInt32(reader.GetOrdinal("productSubCategoryId"));
        //            string productSubCategoryName = reader.GetString(reader.GetOrdinal("SubcategoryName"));

        //            if (currentCategory == null || currentCategory.productCategoryId != productCategoryId)
        //            {
        //                // Start a new category
        //                currentCategory = new CategoryWithSubcategoriesViewModel
        //                {
        //                    productCategoryId = productCategoryId,
        //                    productCategoryName = productCategoryName,
        //                    Subcategories = new List<SubcategoryViewModel>()
        //                };

        //                categoriesWithSubcategories.Add(currentCategory);
        //            }

        //            if (productSubCategoryId != 0) // Ensure there's a valid subcategory
        //            {
        //                currentCategory.Subcategories.Add(new SubcategoryViewModel
        //                {
        //                    productSubCategoryId = productSubCategoryId,
        //                    productSubCategoryName = productSubCategoryName
        //                });
        //            }
        //        }
        //    }

        //    _connection.Close();
        //    return categoriesWithSubcategories;
        //}


        public List<ProductSubCategoryModel> GetCategoryWithSubcategories()
        {
            List<ProductSubCategoryModel> models = new List<ProductSubCategoryModel>();

            SqlCommand cmd = new SqlCommand("CategoryWithSubcategory", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ProductSubCategoryModel subCategory = new ProductSubCategoryModel()
                {
                    productCategoryId = Convert.ToInt32(reader["productCategoryId"]),
                    productCategoryName = reader["productCategoryName"].ToString(),
                    productSubCategoryId = Convert.ToInt32(reader["productSubCategoryId"]),
                    productSubCategoryName = reader["productSubCategoryName"].ToString(),
                };
                models.Add(subCategory);
            }
            _connection.Close(); return models;
        }

        public IEnumerable<AdvertiseImagesModel> GetAdvertiseById(int advertiseId)
        {
            List<AdvertiseImagesModel> advertise = new List<AdvertiseImagesModel>();
            _connection.Open();
            SqlCommand cmd = new SqlCommand("DisplayAdDetail", _connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                AdvertiseImagesModel add = new AdvertiseImagesModel()
                {
                    advertiseTitle = dr["advertiseTitle"].ToString(),
                    advertiseDescription = dr["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(dr["advertisePrice"]),
                    cityName = dr["cityName"].ToString(),
                    stateName = dr["statename"].ToString(),
                    areaName = dr["areaName"].ToString(),
                    imageData = (byte[])dr["imageData"],
                    advertiseId = Convert.ToInt32(dr["advertiseId"]),
                    userId = Convert.ToInt32(dr["userId"]),
                    firstName = dr["firstName"].ToString(),
                };
                advertise.Add(add);
            }
            _connection.Close();
            return advertise;
        }
    }
}

