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
        public List<AdvertiseImagesModel> newFilter(int? productCategoryId, int? productSubCategoryId, int? stateId, int? cityId, int? areaId, decimal? minprice, decimal? maxprice,int? advertiseId)
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
                AdvertiseImagesModel model = new AdvertiseImagesModel()
                {
               
                    advertiseTitle = reader["advertiseTitle"].ToString(),
                    imageData= (byte[])reader["imageData"],
                    advertiseDescription = reader["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(reader["advertisePrice"]),
                    cityName = reader["cityName"].ToString(),
                    stateName = reader["statename"].ToString(),
                    areaName=reader["areaName"].ToString(), 
                };
                models.Add(model);
            }
            _connection.Close();
            return models;
        }
        #endregion

       


        public List<CategoryWithSubcategoriesViewModel> GetCategoriesWithSubcategories()
        {
            List<CategoryWithSubcategoriesViewModel> categoriesWithSubcategories = new List<CategoryWithSubcategoriesViewModel>();


            _connection.Open();

            string query = @"
            SELECT
                pc.productCategoryId AS CategoryId,
                pc.productCategoryName AS CategoryName,
                sb.productSubCategoryId AS SubcategoryId,
                sb.productSubCategoryName AS SubcategoryName
            FROM
                tbl_ProductCategory pc
            LEFT JOIN
                tbl_ProductSubCategory sb
            ON
                pc.productCategoryId = sb.productCategoryId
            ORDER BY
                pc.productCategoryId, sb.productSubCategoryId";

            using (SqlCommand command = new SqlCommand(query, _connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                CategoryWithSubcategoriesViewModel currentCategory = null;

                while (reader.Read())
                {
                    int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                    string categoryName = reader.GetString(reader.GetOrdinal("CategoryName"));
                    int subcategoryId = reader.GetInt32(reader.GetOrdinal("SubcategoryId"));
                    string subcategoryName = reader.GetString(reader.GetOrdinal("SubcategoryName"));

                    if (currentCategory == null || currentCategory.CategoryId != categoryId)
                    {
                        // Start a new category
                        currentCategory = new CategoryWithSubcategoriesViewModel
                        {
                            CategoryId = categoryId,
                            CategoryName = categoryName,
                            Subcategories = new List<SubcategoryViewModel>()
                        };

                        categoriesWithSubcategories.Add(currentCategory);
                    }

                    if (subcategoryId != 0) // Ensure there's a valid subcategory
                    {
                        currentCategory.Subcategories.Add(new SubcategoryViewModel
                        {
                            SubcategoryId = subcategoryId,
                            SubcategoryName = subcategoryName
                        });
                    }
                }
            }

            _connection.Close();
            return categoriesWithSubcategories;
        }


        public List<ProductSubCategoryModel> GetCategoryWithSubcategories()
        {
            List<ProductSubCategoryModel> models = new List<ProductSubCategoryModel>();

            string query = "\tselect pc.productCategoryId,pc.productCategoryName,sb.productSubCategoryId,sb.productSubCategoryName from tbl_ProductSubCategory sb\r\n\t\t\t\tjoin\r\n\t\t\t\ttbl_ProductCategory pc on pc.productCategoryId=sb.productCategoryId";

            SqlCommand cmd = new SqlCommand(query, _connection);
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
    }
}

