using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CouponManagementDBEntity.Models;
using CouponManagementDBEntity.Repository;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SHR_Model.Models;
using UserManagement.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement
{
    [Route("api/v1")]
    [ApiController]
   // [Authorize]
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private readonly IUserManagementHelper _iUserManagementHelper;
        public UserController(IUserManagementHelper iUserManagementHelper)
        {
            _iUserManagementHelper = iUserManagementHelper;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        

        /// <summary>
        /// To Register the User Details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserRegister")]
       // [Produces( "application/json")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> UserRegister(UserDetails user)
        {
            log.Info("In UserController :  UserRegister(UserDetails user)");

            try
            {
                    //Register new user passing object as parameter 
                    await _iUserManagementHelper.UserRegister(user);
                  
                    return Ok("success");
                
            }
            catch (Exception e)
            {
                log.Error("Exception UserController: UserRegister(UserDetails user)" + e.Message);
                return NotFound(e.StackTrace);
            }
        }
        /// <summary>
        /// To validate whether the user is authorized or not
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogin")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> UserLogin(UserLogin user)
        {
            log.Info("In UserController : UserLogin(UserLogin user)");
            try
            {
                //null checking
               return Ok( await  _iUserManagementHelper.UserLogin(user));
               
            }
            catch (Exception e)
            {
                log.Error("Exception UserController: UserLogin(UserLogin user)" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// To view the profile of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(UserDetails))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetUser(int userId)
        {
            log.Info("In UserController : GetUser(int userId)");
            try
            {
                //Checking userid in user and retriveing appropritate data
                UserDetails user=   await _iUserManagementHelper.GetUser(userId);

                return Ok(user);
            }
            catch (Exception e)
            {
                log.Error("Exception UserController: GetUser(int userId)"+ e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// To update the user profile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> UpdateUser(UserDetails user)
        {
            log.Info("In UserController : UpdateUser(UserDetails user)");
            try
            {
                //to modify appropriate data of an user
               await _iUserManagementHelper.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                log.Error("Exception UserController:UpdateUser(UserDetails user)" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// To view the all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(List<UserDetails>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetAllUsers()
        {
            log.Info("In UserController :  GetAllUsers()");
            try
            {
                //retriveing all users present
               return Ok( await _iUserManagementHelper.GetAllUsers());
            }
            catch(Exception e)
            {
                log.Error("Exception UserController: GetAllUsers()" + e.Message);
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get Id by using userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIdByName/{userName}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> GetIdByName(string userName)
        {
            log.Info("In UserController : GetIdByName(string userName)");
            try
            {
                //to retrive data based on  username 
                return Ok(await _iUserManagementHelper.GetIdByName(userName));
            }
            catch(Exception e)
            {
                log.Error("Exception UserController: GetIdByName(string userName)" + e.Message);
                return NotFound(e.Message);
            }

        }
        
    }

   
}
