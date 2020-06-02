using CouponManagementDBEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouponManagementDBEntity.Repository;
using SHR_Model.Models;
using log4net;
using System.Reflection;
using System.IO;
using log4net.Config;

namespace UserManagement.Helper
{
    public interface IUserManagementHelper
    {
        Task<string> UserRegister(UserDetails user);
        Task<string> UserLogin(UserLogin user);
        Task<bool> UpdateUser(UserDetails user);
        Task<UserDetails> GetUser(int userId);
        Task<List<UserDetails>> GetAllUsers();
        Task<int> GetIdByName(string userName);
    }
    public class UserManagementHelper : IUserManagementHelper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IUserRepository _iUserRepository;
        public UserManagementHelper(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
        /// <summary>
        /// displays all users
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDetails>> GetAllUsers()
        {
            log.Info("In UserManagementHelper :   GetAllUsers()");
            try
            {
                return await _iUserRepository.GetAllUsers();
            }
            catch(Exception e)
            {
                log.Error("Exception UserManagementHelper: GetAllUsers()" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// Get Id by using userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public  async Task<int> GetIdByName(string userName)
        {
            log.Info("In UserManagementHelper : GetIdByName(string userName) ");
            try
            {
                return await _iUserRepository.GetIdByName(userName);
            }
            catch (Exception e)
            {
                log.Error("Exception UserManagementHelper: GetIdByName(string userName)" + e.Message);
                throw;

            }
        }

        /// <summary>
        /// getting user by id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDetails> GetUser(int userId)
        {
            log.Info("In UserManagementHelper :  GetUser(int userId)");
            try
            {
                return await _iUserRepository.GetUser(userId);
               
            }
            catch (Exception e)
            {
                log.Error("Exception UserManagementHelper:GetUser(int userId)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// updating user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UserDetails user)
        {
            log.Info("In UserManagementHelper :  UpdateUser(UserDetails user)");
            try
            {
                bool id = await _iUserRepository.UpdateUser(user);
                if (id == true)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Error("Exception UserManagementHelper: UpdateUser(UserDetails user)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// validating user details for login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> UserLogin(UserLogin user)
        {
            log.Info("In UserManagementHelper :  UserLogin(UserLogin user)");
            try
            {
                UserDetails userDetails = await _iUserRepository.UserLogin(user);
                if (userDetails == null)
                    return "Invalid Crendentails";
                else return "successfully logged in";
            }
           
             catch (Exception e)
            {
                log.Error("Exception UserManagementHelper: UserLogin(UserLogin user)" + e.Message);
                throw;

            }
        }
/// <summary>
/// registering new user
/// </summary>
/// <param name="user"></param>
/// <returns></returns>
       

        public async Task<string> UserRegister(UserDetails user)
        {
            log.Info("In UserManagementHelper :   UserRegister(UserDetails user)");
            try
            {
              await _iUserRepository.UserRegister(user);
                return "success";
            }
            catch (Exception e)
            {
                log.Error("Exception UserManagementHelper:  UserRegister(UserDetails user)" + e.Message);
                throw;

            }
        }
    }
}
