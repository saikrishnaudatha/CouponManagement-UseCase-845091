using CouponManagement;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using CouponManagement.Helper;
using CouponManagementTestCase.DATA;
using NUnit.Framework;
using System.Threading.Tasks;
using CouponManagementDBEntity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CouponManagementTestCase.Controller
{
    class CouponControllerTests
    {
        private CouponController couponController;
        private Mock<ICouponManagementHelper> mockCouponManagementHelper;
        private CouponData mockCouponData;
        [SetUp]
        public void Setup()
        {
            mockCouponManagementHelper = new Mock<ICouponManagementHelper>();
            mockCouponData= new CouponData();
            couponController = new CouponController(mockCouponManagementHelper.Object);
        }
        /// <summary>
        /// To The GetAllCoupons
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCoupons_Valid_Return()
        {
            mockCouponManagementHelper.Setup(d => d.GetAllCoupon(It.IsAny<int>())).ReturnsAsync(mockCouponData.couponDetails);
            var result = await couponController.GetAllCoupons(10);
            Assert.That(result, Is.Not.Null);
        }
       
        /// <summary>
        /// to test exception of Adding coupon
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddCoupon_Valid_Return()
        {
            mockCouponManagementHelper.Setup(d => d.AddCoupon(It.IsAny<CouponDetails>())).ReturnsAsync(new bool());
            var result = await couponController.AddCoupon(new CouponDetails()
            {
                CouponId = 40,
                CouponNumber = "df453st",
                CouponStatus = "valid",
                CouponStartDate = DateTime.Now,
                CouponExpiredDate = DateTime.Now.AddDays(32),
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = 10

            }) as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode,Is.EqualTo(200));
        }
        [Test]
        public async Task DeleteCoupon_Valid_Return()
        {
            mockCouponManagementHelper.Setup(d => d.GetAllCoupon(It.IsAny<int>())).ReturnsAsync(mockCouponData.couponDetails);
            var result = await couponController.DeleteCoupon(10) as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

        }

        ///// <summary>
        /// To Test for updateCoupon
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateUser_valid_Returns()
        {
            mockCouponManagementHelper.Setup(d => d.UpdateCoupon(It.IsAny<CouponDetails>())).ReturnsAsync(new Boolean());
            var result = await couponController.UpdateCoupon(new CouponDetails()
            {
                CouponId = 20,
                CouponNumber = "JHDF748N",
                CouponStatus = "valid",
                CouponStartDate = DateTime.Now,
                CouponExpiredDate = DateTime.Now.AddDays(29),
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = 10
            }) as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
        /// <summary>
        /// To Test the GetCouponById
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCouponById_Valid_Returns()
        {
            mockCouponManagementHelper.Setup(d => d.GetCouponById(It.IsAny<int>())).ReturnsAsync(new CouponDetails());
            var coupon = await couponController.GetCouponById(20) as OkObjectResult;
            Assert.That(coupon.StatusCode, Is.EqualTo(200));

        }
        /// <summary>
        /// To Test the GetCoupons
        /// </summary>
        [Test]
        public async Task GetCoupons_Valid_Returns()
        {
            mockCouponManagementHelper.Setup(d => d.GetCoupons()).ReturnsAsync(new List<CouponDetails>());
            var coupon = await couponController.GetCoupons() as OkObjectResult;
            Assert.That(coupon.StatusCode, Is.EqualTo(200));
        }
    }
}
