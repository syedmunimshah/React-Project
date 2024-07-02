using EMedicineBE.Dto;
using EMedicineBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string Connection;


        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("EMedicine").ToString();
        }
        [HttpPost]
        [Route("registration")]
        public Response register(UsersModel usersModel) 
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
             response = dal.register(usersModel, connection);
            return response;
        }

        [HttpPost]
        [Route("login")]

        public Response login(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.Login(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.viewUser(users, connection);
            return response;
        }
    
        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users) 
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.updateProfile(users, connection);
            return response;

        }


    }
}
