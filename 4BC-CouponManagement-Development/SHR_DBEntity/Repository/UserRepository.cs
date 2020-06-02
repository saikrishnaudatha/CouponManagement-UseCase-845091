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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SHR_Model.Models;

namespace UserManagement.Helper
{
    public class UserRepository:IUserRepository
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CouponManagementContext _couponManagementContext;
        public UserRepository(CouponManagementContext couponManagementContext)
        {
            _couponManagementContext = couponManagementContext;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
        /// <summary>
        /// displays all the users
        /// </summary>
        /// <returns></returns>

        public async Task<List<UserDetails>> GetAllUsers()
        {
            log.Info("In UserRepository :   GetAllUsers()");
            try
            {
                return await _couponManagementContext.UserDetails.ToListAsync();
            }
            catch(Exception e)
            {
                log.Error("Exception UserRepository:  GetAllUsers()" + e.Message);
                throw;
            }
        }
        /// <summary>
        /// Get Id By userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<int> GetIdByName(string userName)
        {
            log.Info("In UserRepository : GetIdByName(string userName)");
            try
            {
                UserDetails user = await _couponManagementContext.UserDetails.SingleOrDefaultAsync(e => e.UserName == userName);
                return user.UserId;
            }
            catch (Exception e)
            {
                log.Error("Exception UserRepository:GetIdByName(string userName)" + e.Message);
                throw;
            }
        }

        /// <summary>
        /// To view the profile of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDetails> GetUser(int userId)
        {
            log.Info("In UserRepository :  GetUser(int userId)");
            try
            {
               // return _couponManagementContext.UserDetails.FromSqlRaw("EXEC dbo.spGetUsersById @UserId={0}", userId).ToListAsync().Result.FirstOrDefault();
                 return await _couponManagementContext.UserDetails.FindAsync(userId);

            }
            catch (Exception e)
            {
                log.Error("Exception UserRepository: GetUser(int userId)" + e.Message);
                throw;
            }
        }
        /// <summary>
        /// To update the user profile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task<bool> UpdateUser(UserDetails user1)
        {
            log.Info("In UserRepository :  UpdateUser(UserDetails user1)");
            try
            {
               
                _couponManagementContext.UserDetails.Update(user1);
                var id = await _couponManagementContext.SaveChangesAsync();
                if (id > 0)
                    return true;
                else return false;
            }
            catch (Exception e)
            {
                log.Error("Exception UserRepository:  UpdateUser(UserDetails user1)" + e.Message);
                throw;
            }
        }
        /// <summary>
        /// To validate whether the user is authorized or not
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDetails> UserLogin(UserLogin user)
        {
            log.Info("In UserRepository :  UserLogin(UserLogin user)");
            try
            {
                UserDetails userDetails = await _couponManagementContext.UserDetails.SingleOrDefaultAsync(e => e.UserName ==user.UserName && e.UserPassword ==user.UserPassword);
                    if(userDetails == null)
                    return null;
                else
                    return userDetails;
            }
            catch (Exception e)
            {
                log.Error("Exception UserRepository: UserLogin(UserLogin user)" + e.Message);
                throw;
            }
        }
        /// <summary>
        /// To Register the User Details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> UserRegister(UserDetails user)
        {
            log.Info("In UserRepository :  UserRegister(UserDetails user)");
            try
            {
               
                 _couponManagementContext.UserDetails.Add(user);
                var id = await _couponManagementContext.SaveChangesAsync();
                if (id > 0)
                    return "true";
                else
                    return "false";
            }
            catch (Exception e)
            {
                log.Error("Exception UserRepository: UserRegister(UserDetails user)" + e.Message);
                throw;
            }
        }
    }
}
