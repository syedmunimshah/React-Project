using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EMedicineBE.Models;
using System.Data.SqlClient;
using EMedicineBE.Dto;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string Connection;

        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("EMedicine").ToString();
        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(CartModel cartModel) 
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.addToCart(cartModel, connection);
            return response;
                }

        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(UsersModel usersModel)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.placeOrder(usersModel, connection);
            return response;

        }

        [HttpPost]
        [Route("orderList")]

        public Response orderList(UsersModel usersModel)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.orderList(usersModel, connection);
            return response;
        }
    }
}
