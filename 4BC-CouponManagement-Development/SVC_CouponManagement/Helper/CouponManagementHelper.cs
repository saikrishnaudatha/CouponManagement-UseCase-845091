using CouponManagementDBEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouponManagementDBEntity.Repository;
using log4net;
using System.Reflection;
using System.IO;
using log4net.Config;

namespace CouponManagement.Helper
{
    public interface ICouponManagementHelper
    {
        Task<List<CouponDetails>> GetCoupons();
        Task<CouponDetails> GetCouponById(int couponId);
        Task<List<CouponDetails>> GetAllCoupon(int userId);
        Task<bool> AddCoupon(CouponDetails coupon);
        Task<bool> UpdateCoupon(CouponDetails coupon);
        Task<bool> DeleteCoupon(int couponId);
        Task<bool> Status();
    }
    public class CouponManagementHelper : ICouponManagementHelper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICouponRepository _iCoupounRepositoty;
        public CouponManagementHelper(ICouponRepository iCouponRepository)
        {
            _iCoupounRepositoty = iCouponRepository;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
        /// <summary>
        /// adding coupon
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public async Task<bool> AddCoupon(CouponDetails coupon)
        {
            log.Info("In CouponManagementHelper :   AddCoupon(CouponDetails coupon)");
            try
            {
                bool result = await _iCoupounRepositoty.AddCoupon(coupon);
                if (result == true)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper:  AddCoupon(CouponDetails coupon)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// deleting  coupon based on couponid
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCoupon(int couponId)
        {
            log.Info("In CouponManagementHelper :  DeleteCoupon(int couponId)");
            try
            {
                bool result = await _iCoupounRepositoty.DeleteCoupon(couponId);
                if (result == true)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper:  DeleteCoupon(int couponId)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// Getting all coupons of a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CouponDetails>> GetAllCoupon(int userId)
        {
            log.Info("In CouponManagementHelper : GetAllCoupon(int userId)");
            try
            {
                return await _iCoupounRepositoty.GetAllCoupon(userId);
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper:  GetAllCoupon(int userId)" + e.Message);
                throw;

            }
        }

        public async Task<CouponDetails> GetCouponById(int couponId)
        {
            log.Info("In CouponManagementHelper :    GetCouponById(int couponId)");
            try
            {
                return await _iCoupounRepositoty.GetCouponById(couponId);
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper:   GetCouponById(int couponId)" + e.Message);
                throw;

            }
        }

        public async Task<List<CouponDetails>> GetCoupons()
        {
            log.Info("In CouponManagementHelper :  GetCoupons()");
            try
            {
                return await _iCoupounRepositoty.GetCoupons();
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper: GetCoupons()" + e.Message);
                throw;

            }
        }

        /// <summary>
        /// updating coupon status
        /// </summary>
        /// <returns></returns>

        public async Task<bool> Status()
        {
            log.Info("In CouponManagementHelper :  Status()");
            try
            {
                return await _iCoupounRepositoty.Status();
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper: Status()" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// Updating coupon details
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>

        public async Task<bool> UpdateCoupon(CouponDetails coupon)
        {
            log.Info("In CouponManagementHelper :  UpdateCoupon(CouponDetails coupon)");
            try
            {
                bool result = await _iCoupounRepositoty.UpdateCoupon(coupon);
                if (result == true)
                    return true;
                else return false;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponManagementHelper:  UpdateCoupon(CouponDetails coupon)" + e.Message);
                throw;

            }
        }
    }
}
