using CouponManagementDBEntity.Models;
using CouponManagementDBEntity.Repository;
using CouponManagementTestCase.DATA;
using Moq;
using NUnit.Framework;
using SHR_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace CouponManagementTestCase.Helper
{
    [TestFixture]
    class UserManagementHelperTests
    {
        private UserManagementHelper userManagementHelper;
        private Mock<IUserRepository> mockUserRepository;
        private UserDatas mockUserData;
        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUserData = new UserDatas();
            userManagementHelper = new UserManagementHelper(mockUserRepository.Object);
        }
        /// <summary>
        /// To Test the GetAllUsers 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllUsers_Valid_Returns()
        {
            mockUserRepository.Setup(d => d.GetAllUsers()).ReturnsAsync(mockUserData.userDetails);
            var result = await userManagementHelper.GetAllUsers();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.GreaterThan(0));
            Assert.That(result.Count, Is.EqualTo(2));
        }
        /// <summary>
        /// To Test Exception in GetAllUsers
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllUsers_InValid_ReturnsNull()
        {
            mockUserRepository.Setup(d => d.GetAllUsers()).ReturnsAsync((List<UserDetails>)(null));
            var result = await userManagementHelper.GetAllUsers();
            Assert.That(result, Is.Null);
        }
        /// <summary>
        /// To test get user by id
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetUser_Valid_Returns()
        {

            mockUserRepository.Setup(d => d.GetUser(It.IsAny<int>())).ReturnsAsync(new UserDetails());
            var result = await userManagementHelper.GetUser(18);
            Assert.That(result, Is.Not.Null);
          
        }
        /// <summary>
        /// to test user login is valid or not
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UserLogin_Valid_Returns()
        {
            mockUserRepository.Setup(d => d.UserLogin(It.IsAny<UserLogin>())).ReturnsAsync(new UserDetails());
            var result = await userManagementHelper.UserLogin(new UserLogin()
            {
                UserName = "hello",
                UserPassword = "hello"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("successfully logged in"));

        }
        /// <summary>
        /// to test whether the user can register or not
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UserRegister_valid_Returns()
        {
            mockUserRepository.Setup(d => d.UserRegister(It.IsAny<UserDetails>())).ReturnsAsync("success");
            var result = await userManagementHelper.UserRegister(new UserDetails()
            {
                UserId = 67,
                UserName = "Abc",
                EmailAddr = "Abc@gmail.com",
                UserPassword = "4545",
                UserAddress = "Ap",
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PhoneNumber = "9874563210",
                FirstName = "Abc",
                LastName = "Xyz"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("success"));
        }
        /// <summary>
        /// to test whether the user can update or not
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateUser_valid_Returns()
        {
            mockUserRepository.Setup(d => d.UpdateUser(It.IsAny<UserDetails>())).ReturnsAsync(true);
            var result = await userManagementHelper.UpdateUser(new UserDetails()
            {
                UserId = 10,
                UserName = "Abc1",
                EmailAddr = "Abc1@gmail.com",
                UserPassword = "4545",
                UserAddress = "Ap",
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PhoneNumber = "9874563210",
                FirstName = "Abc1",
                LastName = "Xyz"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(true));
        }
        /// <summary>
        /// To Test th GetIdByName
        /// </summary>
        [Test]
        public async Task GetIdByName_Valid_Returns()
        {
            mockUserRepository.Setup(d => d.GetIdByName(It.IsAny<string>()));
            var id =await userManagementHelper.GetIdByName("hello1");
            Assert.That(id, Is.Not.Null);
        }
    }


}
