using CouponManagementDBEntity.Models;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementDBEntity.Repository
{
  public  class CouponRepository:ICouponRepository
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CouponManagementContext _couponManagementContext;
        public CouponRepository(CouponManagementContext couponManagementContext)
        {
            _couponManagementContext = couponManagementContext;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
        /// <summary>
        /// To add a new coupon
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public async Task<bool> AddCoupon(CouponDetails coupon)
        {
            log.Info("In CouponRepository :   AddCoupon(CouponDetails coupon)");
            try
            {
                _couponManagementContext.CouponDetails.Add(coupon);
                int result = await _couponManagementContext.SaveChangesAsync();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository:  AddCoupon(CouponDetails coupon)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// For deleting coupon based on couponid
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCoupon(int couponId)
        {
            log.Info("In CouponRepository :   DeleteCoupon(int couponId)");
            try
            {
                CouponDetails coupon = _couponManagementContext.CouponDetails.Find(couponId);
                _couponManagementContext.Remove(coupon);
                int result = await _couponManagementContext.SaveChangesAsync();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository:  DeleteCoupon(int couponId)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// Viewing all coupons of a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CouponDetails>> GetAllCoupon(int userId)
        {
            log.Info("In CouponRepository :   GetAllCoupon(int userId)");
            try
            {
                return await _couponManagementContext.CouponDetails.Where(e => e.UserId == userId).ToListAsync();
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository:  GetAllCoupon(int userId)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// view the details of coupon
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<CouponDetails> GetCouponById(int couponId)
        {
            log.Info("In CouponRepository :  GetCouponById(int couponId)");
            try
            {
                CouponDetails couponDetails = await _couponManagementContext.CouponDetails.FindAsync(couponId);
                return couponDetails;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository:  GetCouponById(int couponId)" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// To view the list Of coupons
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponDetails>> GetCoupons()
        {
            log.Info("In CouponRepository :   GetCoupons()");
            try
            {
                return await _couponManagementContext.CouponDetails.ToListAsync();
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository: GetCoupons()" + e.Message);
                throw;

            }
        }

        /// <summary>
        /// Updaating coupon status 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Status()
        {
            log.Info("In CouponRepository :   Status()");
            try
            {
                List<CouponDetails> couponDetails = _couponManagementContext.CouponDetails.ToList();
                foreach (var coupon in couponDetails)
                {
                    int result = DateTime.Compare(coupon.CouponExpiredDate, DateTime.Now);
                    if (result < 0)
                    {
                        coupon.CouponStatus = "Invalid";
                        _couponManagementContext.CouponDetails.Update(coupon);
                        await _couponManagementContext.SaveChangesAsync();


                    }
                    else
                    {
                        coupon.CouponStatus = "valid";
                        _couponManagementContext.CouponDetails.Update(coupon);
                        await _couponManagementContext.SaveChangesAsync();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository:  Status()" + e.Message);
                throw;

            }
        }
        /// <summary>
        /// Updating coupon Details 
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCoupon(CouponDetails coupon1)
        {
            log.Info("In CouponRepository :  UpdateCoupon(CouponDetails coupon1)");
            try
            {
                _couponManagementContext.CouponDetails.Update(coupon1);
                int result = await _couponManagementContext.SaveChangesAsync();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Error("Exception CouponRepository:  UpdateCoupon(CouponDetails coupon1)" + e.Message);
                throw;

            }



        }
    }
}
