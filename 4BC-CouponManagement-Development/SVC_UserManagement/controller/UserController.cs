using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouponManagementDBEntity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHR_Model.Models;
using UserManagement.AuthenticationDemo;

namespace UserManagement.controller
{
   // [Route("api/[controller]")]
    [ApiController]
  
    public class UserController : Controller
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;

        public UserController(ICustomAuthenticationManager customAuthenticationManager)

        {

            _customAuthenticationManager = customAuthenticationManager;

        }


        [AllowAnonymous]

        [HttpPost("UserLogin")]

        public IActionResult UserLogin([FromBody] UserLogin user)

        {

            var token = _customAuthenticationManager.UserLogin(user.UserName, user.UserPassword);

            if (token == null)

                return Unauthorized();

            return Ok(token);

        }

    }
}