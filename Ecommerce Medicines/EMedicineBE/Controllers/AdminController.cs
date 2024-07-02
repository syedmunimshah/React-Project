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
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string Connection;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("EMedicine").ToString();
        }

        [HttpPost]
        [Route("addUpdateMedicine")]
        public Response addUpdateMedicine(Medicines medicines)
        {


            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.addUpdateMedicine(medicines,connection);
            return response;

        }

        [HttpPost]
        [Route("userList")]
        public Response userList()
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(Connection);
            Response response = dal.userList(connection);
            return response;

        }
    }
}
