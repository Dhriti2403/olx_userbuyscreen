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
        public List<ModelAdvertiseImages> newFilter(int? productCategoryId, int? productSubCategoryId, int? stateId, int? cityid, int? areaid, decimal? minprice, decimal? maxprice)
        {
            List<ModelAdvertiseImages> models = new List<ModelAdvertiseImages>();
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
                ModelAdvertiseImages model = new ModelAdvertiseImages()
                {
                    advertiseTitle = reader["advertiseTitle"].ToString(),
                    imageData= (byte[])reader["imageData"],

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

        public IEnumerable<ModelAdvertiseImages> GetProducts(int advertiseId)
        {
            List<ModelAdvertiseImages> products = new List<ModelAdvertiseImages>();
            //string query = "select img.imageData,ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice from " +
            //    "tbl_MyAdvertise ad join tbl_AdvertiseImages img on ad.advertiseId=img.advertiseId where " +
            //    "img.advertiseId=@advertiseId";
            SqlCommand cmd = new SqlCommand("DisplayAdDetail", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);
        
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


        //public list<categoryviewmodel> all()
        //{
        //    list<categoryviewmodel> list = new list<categoryviewmodel>();
        //    sqlcommand cmd = new sqlcommand("allcategories", _connection);
        //    cmd.commandtype = commandtype.storedprocedure;
        //    _connection.open();
        //    sqldatareader reader = cmd.executereader();
        //    while (reader.read())
        //    {
        //        categoryviewmodel categoryview = new categoryviewmodel()
        //        {
        //            category_id = convert.toint32(reader["category_id"]),
        //            category_name = reader["category_name"].tostring(),
        //            subcategories = reader["subcategories"].tostring()

        //        };
        //        list.add(categoryview);
        //    }
        //    _connection.close();
        //    return list;
        //}



        public List<ModelProductSubCategory> GetCategoryWithSubcategories()
        {
            List<ModelProductSubCategory> models = new List<ModelProductSubCategory>();

            string query = "\tselect pc.productCategoryId,pc.productCategoryName,sb.productSubCategoryId,sb.productSubCategoryName from tbl_ProductSubCategory sb\r\n\t\t\t\tjoin\r\n\t\t\t\ttbl_ProductCategory pc on pc.productCategoryId=sb.productCategoryId";

            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

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

