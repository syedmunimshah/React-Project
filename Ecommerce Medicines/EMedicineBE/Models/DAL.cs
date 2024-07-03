using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EMedicineBE.Dto;
namespace EMedicineBE.Models

{
    public class DAL
    {
        public Response register(UsersModel usersModel, SqlConnection connection)
        {
            
                Response response = new Response();
            try
            {

            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", usersModel.FirstName);
            cmd.Parameters.AddWithValue("@LastName", usersModel.LastName);
            cmd.Parameters.AddWithValue("@Password", usersModel.Password);
            cmd.Parameters.AddWithValue("@Email", usersModel.Email);
            cmd.Parameters.AddWithValue("@Fund", 0); 
            cmd.Parameters.AddWithValue("@Type", "Users"); 
            cmd.Parameters.AddWithValue("@Status", 1); 
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now); 

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
           
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            }
            catch (Exception ex)
            {
                connection.Close();
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed: " + ex.Message;
            }

          
            return response;
        }
    
        public Response Login(LoginModel loginModel, SqlConnection connection) {
            SqlDataAdapter da = new SqlDataAdapter("sp_login",connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", loginModel.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", loginModel.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.Data = user;
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
            
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is Invalid";
                response.Data = null;
            }
            return response;
        }

        public Response viewUser(UsersModelID usersModelID, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", usersModelID.ID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            Response response = new Response();
            Users user = new Users();

            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);

                response.StatusCode = 200;
                response.StatusMessage = "User exists";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist";
                user = null; 
            }

            response.Data = user;
            return response;
        }



        public Response updateProfile(UsersModel usersModel, SqlConnection connection)
        {
            Response response = new Response();
            try
            {

            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", usersModel.ID);
            cmd.Parameters.AddWithValue("@FirstName", usersModel.FirstName);
            cmd.Parameters.AddWithValue("@LastName", usersModel.LastName);
            cmd.Parameters.AddWithValue("@Password", usersModel.Password);
            cmd.Parameters.AddWithValue("@Email", usersModel.Email);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

                response.StatusCode = 200;
                response.StatusMessage = "Record Updated Successfully";
            }
            catch (Exception ex)
            {
                connection.Close();
                response.StatusCode = 100;
                response.StatusMessage = "No record updated. User ID might not exist or no changes were made." + ex.Message;
            }
 
            return response;

        }


        public Response addToCart(CartModel cartModel, SqlConnection connection)
        {
                Response response=new Response();
            try
            {

            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cartModel.UserId);
            cmd.Parameters.AddWithValue("@UnitPrice", cartModel.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cartModel.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cartModel.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cartModel.TotalPrice);
            cmd.Parameters.AddWithValue("@MedicineID", cartModel.MedicineID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
          
                response.StatusCode = 200;
                response.StatusMessage = "Item addedd successfully";
            }
            catch (Exception ex)
            {
                connection.Close();
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added" +ex.Message;
            }
               
            

            return response;
        }


        public Response placeOrder(UsersModel usersModel, SqlConnection connection) 
        {
        Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder",connection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", usersModel.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed";
            }
            return response;
        }

        public Response orderList(UsersModelTYPEID usersmodeltYPEID, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList",connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", usersmodeltYPEID.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", usersmodeltYPEID.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);

                }
                if (listOrder.Count>0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                    response.Data = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available";
                    response.Data = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available";
                response.Data = null;
            }
            return response;
        }


        public Response addUpdateMedicine(MedicinesModel medicines, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_addUpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", medicines.Name);
            cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
            cmd.Parameters.AddWithValue("@ExpDate", medicines.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            cmd.Parameters.AddWithValue("@Type", medicines.Type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            if (i>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine inserted Successfully";
            }
            else{
                response.StatusCode = 100;
                response.StatusMessage = "Medicine did not save. try again.";
            }
            return response;
        }




        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_userList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Type = Convert.ToString(dt.Rows[i]["Type"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    listUsers.Add(user);

                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "user details fetched";
                    response.Data = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "user details are not available";
                    response.Data = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "user details are not available";
                response.Data = null;
            }
            return response;
        }








    }
}
