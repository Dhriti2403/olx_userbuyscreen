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
        public List<ModelMyAdvertise> newFilter(int? productCategoryId, int? productSubCategoryId, int? stateId, int? cityid, int? areaid, decimal? minprice, decimal? maxprice)
        {
            List<ModelMyAdvertise> models = new List<ModelMyAdvertise>();
            SqlCommand cmd = new SqlCommand("newfilter", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productCategoryId", productCategoryId);
            cmd.Parameters.AddWithValue("@productsubCategoryId", productSubCategoryId);
            cmd.Parameters.AddWithValue("@stateid", stateId);
            cmd.Parameters.AddWithValue("@cityid", cityid);
            cmd.Parameters.AddWithValue("@areaid", areaid);
            cmd.Parameters.AddWithValue("@minprice", minprice);
            cmd.Parameters.AddWithValue("@maxprice", maxprice);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ModelMyAdvertise model = new ModelMyAdvertise()
                {
                    advertiseTitle = reader["advertiseTitle"].ToString(),
                    advertiseDescription = reader["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(reader["advertisePrice"]),
                    cityName = reader["cityName"].ToString(),
                    stateName = reader["statename"].ToString(),

                };
                models.Add(model);
            }
            _connection.Close();
            return models;
        }
        #endregion

        #region ByProduct

        public IEnumerable<ModelAdvertiseImages> GetProducts()
        {
            List<ModelAdvertiseImages> products = new List<ModelAdvertiseImages>();
            string query = "select img.imageData,ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice from " +
                "tbl_MyAdvertise ad join tbl_AdvertiseImages img on ad.advertiseId=img.advertiseId where " +
                "ad.advertiseId=img.advertiseId ";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ModelAdvertiseImages advertise = new ModelAdvertiseImages()
                {
                    //imageData = Convert.ToBase64String((byte[])dr["imageData"]),
                    imageData = (byte[])dr["imageData"],
                    advertiseTitle = dr["advertiseTitle"].ToString(),
                    advertiseDescription = dr["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(dr["advertisePrice"])
                };
                products.Add(advertise);
            }
            _connection.Close();
            return products;
        }
        #endregion

       
        //public List<categoryViewModel> all()
        //{
        //    List<categoryViewModel> list = new List<categoryViewModel>();
        //    SqlCommand cmd = new SqlCommand("allcategories", _connection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    _connection.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        categoryViewModel categoryView = new categoryViewModel()
        //        {
        //            category_id = Convert.ToInt32(reader["category_id"]),
        //            category_name = reader["category_name"].ToString(),
        //            subcategories = reader["subcategories"].ToString()

        //        };
        //        list.Add(categoryView);
        //    }
        //    _connection.Close();
        //    return list;
        //}



        public List<ModelProductSubCategory> GetCategoryWithSubcategories()
        {
            List<ModelProductSubCategory> models=new List<ModelProductSubCategory>();
            
            string query = "select pc.productCategoryId,pc.productCategoryName,sb.productSubCategoryId," +
                "sb.productSubCategoryName from tbl_ProductSubCategory sb tbl_ProductCategory pc on " +
                "pc.productCategoryId=sb.productCategoryId";
               
            SqlCommand cmd= new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader reader= cmd.ExecuteReader();
            
            while (reader.Read())
            {
                ModelProductSubCategory subCategory = new ModelProductSubCategory()
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

