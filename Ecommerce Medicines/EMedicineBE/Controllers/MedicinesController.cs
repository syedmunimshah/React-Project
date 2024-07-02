﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EMedicineBE.Models;
using System.Data.SqlClient;

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
        public Response addToCart(Cart cart) 
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.addToCart(cart, connection);
            return response;
                }

        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.placeOrder(users, connection);
            return response;

        }

        [HttpPost]
        [Route("orderList")]

        public Response orderList(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.orderList(users, connection);
            return response;
        }
    }
}
