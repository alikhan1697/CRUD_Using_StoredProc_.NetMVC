using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ADO_Example_PRO.Models;

namespace ADO_Example_PRO.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();
       
        //Get All products
    
      public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
                
                using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["qty"]),
                        Remark = dr["Remark"].ToString()
                    });
                }


            }   

              
                return productList;   
        }
    
        //Insert Products

      public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProducts",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Price);
                command.Parameters.AddWithValue("@Remark", product.Remark);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close(); 
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }


        //Get Products by Product Id

        public List<Product> GetProductById(int ProductId)
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProductById";
                command.Parameters.AddWithValue("@ProductId",ProductId);            
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["qty"]),
                        Remark = dr["Remark"].ToString()
                    });
                }


            }


            return productList;
        }

        //Update Products

        public bool UpdateProduct(Product product)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Price);
                command.Parameters.AddWithValue("@Remark", product.Remark);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Product
        public string DeleteProduct(int productid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", productid);
                command.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                 
                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OutputMessage"].Value.ToString();
                connection.Close();
            }

                return result;
        }

    }
}