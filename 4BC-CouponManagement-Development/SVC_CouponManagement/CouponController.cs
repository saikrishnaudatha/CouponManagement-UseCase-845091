using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CouponManagement.Helper;
using CouponManagementDBEntity.Models;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CouponManagement
{
    [Route("api/v2")]
    [ApiController]
    public class CouponController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICouponManagementHelper _iCouponManagementHelper;
        public CouponController(ICouponManagementHelper iCouponManagementHelper)
        {
            _iCouponManagementHelper = iCouponManagementHelper;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
        /// <summary>
        /// Adding coupon of particular user
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCoupon")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> AddCoupon(CouponDetails coupon)
        {
            log.Info("In CouponController :   AddCoupon(CouponDetails coupon)");
            try
            {
              //Adding new coupon to the CouponDetails 
                return Ok(await _iCouponManagementHelper.AddCoupon(coupon));
            }
            catch (Exception e)
            {
                log.Error("Exception CouponController :  AddCoupon(CouponDetails coupon)" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        ///  Deleting coupon based upon couponid
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteCoupon/{couponId}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            log.Info("In CouponController :  DeleteCoupon(int couponId)");
            try
            {
             //Removing a particular Coupon from CouponDetails based on CouponId
                return Ok(await _iCouponManagementHelper.DeleteCoupon(couponId));
            }
            catch (Exception e)
            {
                log.Error("Exception CouponController :  DeleteCoupon(int couponId)" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// displays all coupons of  the particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCoupons/{userId}")]
        [ProducesResponseType(200, Type = typeof(CouponDetails))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> GetAllCoupons(int userId)
        {
            log.Info("In CouponController :   GetAllCoupons(int userId)");
            try
            {
                //Retrieving All Coupons of a Specific User based on UserId
                return Ok(await _iCouponManagementHelper.GetAllCoupon(userId));
            }
            catch (Exception e)
            {
                log.Error("Exception CouponController : GetAllCoupons(int userId)" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// update coupon details
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateCoupon")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> UpdateCoupon(CouponDetails coupon)
        {
            log.Info("In CouponController :   UpdateCoupon(CouponDetails coupon)");
            try
            {
             //Updates the newly edited details
                return Ok(await _iCouponManagementHelper.UpdateCoupon(coupon));
            }
            catch (Exception e)
            {
                log.Error("Exception CouponController : UpdateCoupon(CouponDetails coupon)" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        ///  updating the status of the coupon
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("StatusUpdate")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> Statusupdate()
        {
            log.Info("In CouponController :   Statusupdate()");
            try
            {
             //Implictly Updates CouponStatus
                return Ok(await _iCouponManagementHelper.Status());
            }
            catch (Exception e)
            {
                log.Error("Exception CouponController :  Statusupdate()" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// displays all the coupons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCoupons")]
        [ProducesResponseType(200, Type = typeof(List<CouponDetails>))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> GetCoupons()
        {
            log.Info("In CouponController :    GetCoupons()");
            try
            {
                //Retrieving all coupons 
                return Ok(await _iCouponManagementHelper.GetCoupons());
            }
            catch(Exception e)
            {
                log.Error("Exception CouponController :   GetCoupons()" + e.Message);
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// displays coupons based on the coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCouponById/{couponId}")]
        [ProducesResponseType(200, Type = typeof(CouponDetails))]
        [ProducesResponseType(404, Type = typeof(string))]

        public async Task<IActionResult> GetCouponById(int couponId)
        {
            log.Info("In CouponController :  GetCouponById(int couponId)");
            try
            {
                //Getting Coupons based on CouponId 
                return Ok(await _iCouponManagementHelper.GetCouponById(couponId));
            }
            catch(Exception e)
            {
                log.Error("Exception CouponController :  GetCouponById(int couponId)" + e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
