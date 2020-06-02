using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using CouponManagement.Helper;
using CouponManagementDBEntity.Models;
using CouponManagementDBEntity.Repository;
using CouponManagementTestCase.DATA;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CouponManagementTestCase.Helper
{
    class CouponManagementHelperTests
    {
        private CouponManagementHelper couponManagementHelper;
        private Mock<ICouponRepository> mockCouponRepository;
        private CouponData mockCouponData;
        [SetUp]
        public void Setup()
        {
            mockCouponRepository = new Mock<ICouponRepository>();
            mockCouponData = new CouponData();
            couponManagementHelper = new CouponManagementHelper(mockCouponRepository.Object);
        }
        /// <summary>
        /// To Test the GetAllCoupons
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCoupon_Valid_Returns()
        {
            mockCouponRepository.Setup(d => d.GetAllCoupon(It.IsAny<int>())).ReturnsAsync(mockCouponData.couponDetails);
            var result = await couponManagementHelper.GetAllCoupon(10);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.GreaterThan(0));
            Assert.That(result.Count, Is.EqualTo(1));
        }
        /// <summary>
        /// To Test the Exception of GetAllCoupon
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCoupon_InValid_RetunsNull()
        {
            mockCouponRepository.Setup(d => d.GetAllCoupon(It.IsAny<int>())).ReturnsAsync((List<CouponDetails>)(null));
            var result = await couponManagementHelper.GetAllCoupon(10);
            Assert.That(result, Is.Null);
        }
        /// <summary>
        /// to test the   exception of  AddCoupon
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddCoupon_Valid_Return()
        {
            mockCouponRepository.Setup(d => d.AddCoupon(It.IsAny<CouponDetails>())).ReturnsAsync(true);
            var result = await couponManagementHelper.AddCoupon(new CouponDetails()
            {
                CouponId = 29,
                CouponNumber = "HGH76",
                CouponStatus = "valid",
                CouponStartDate = DateTime.Now,
                CouponExpiredDate = DateTime.Now.AddDays(20),
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = 10
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(true));
        }
        /// <summary>
        /// to test the exception of deletecopon
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteCoupon_Valid_Return()
        {
            mockCouponRepository.Setup(d => d.DeleteCoupon(It.IsAny<int>())).ReturnsAsync(true);
            var result = await couponManagementHelper.DeleteCoupon(10);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(true));
          
            
        }
        /// <summary>
        /// to test whether the coupon can update or not
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateUser_valid_Returns()
        {
            mockCouponRepository.Setup(d => d.UpdateCoupon(It.IsAny<CouponDetails>())).ReturnsAsync(true);
            var result = await couponManagementHelper.UpdateCoupon(new CouponDetails()
            {
                CouponId = 20,
                CouponNumber = "JHDF748N",
                CouponStatus = "valid",
                CouponStartDate = DateTime.Now,
                CouponExpiredDate = DateTime.Now.AddDays(29),
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = 10
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(true));
        }
        /// <summary>
        /// To Test the GetCouponById
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCouponById()
        {
            mockCouponRepository.Setup(d => d.GetCouponById(It.IsAny<int>())).ReturnsAsync(new CouponDetails());
            var coupon = await couponManagementHelper.GetCouponById(20);
            Assert.That(coupon, Is.Not.Null);
            
        }
        /// <summary>
        /// To Test the GetCoupons
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCoupons()
        {
            mockCouponRepository.Setup(d => d.GetCoupons()).ReturnsAsync(new List<CouponDetails>());
            var coupon = await couponManagementHelper.GetCoupons();
            Assert.That(coupon, Is.Not.Null);
        }
    }
}
