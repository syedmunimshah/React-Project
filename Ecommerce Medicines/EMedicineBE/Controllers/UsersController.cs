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

        public Response login(UsersModel usersModel)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.Login(usersModel, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(UsersModel usersModel)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.viewUser(usersModel, connection);
            return response;
        }
    
        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(UsersModel usersModel) 
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.updateProfile(usersModel, connection);
            return response;

        }


    }
}
