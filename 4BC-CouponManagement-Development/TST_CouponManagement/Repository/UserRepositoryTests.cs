using Castle.Core.Internal;
using CouponManagementDBEntity.Models;
using CouponManagementDBEntity.Repository;
using CouponManagementTestCase.DATA;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SHR_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace CouponManagementTestCase.Repository
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private IUserRepository userRepository;
        private CouponManagementContext mockCouponManagementContext;
        private UserDatas mockUserDatas;
        [SetUp]
        public void Setup()
        {
            mockCouponManagementContext = new Sqlite().CreateSqliteConnection();
            userRepository = new UserRepository(mockCouponManagementContext);
            mockUserDatas = new UserDatas();
        }
        /// <summary>
        /// To Test the Get all users
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllUsers_Valid_Returns()
        {
            mockCouponManagementContext.UserDetails.AddRange(mockUserDatas.userDetails);
            await mockCouponManagementContext.SaveChangesAsync();
            var getAllUser = await userRepository.GetAllUsers();
            Assert.That(getAllUser, Is.Not.Null);
            Assert.That(getAllUser.Count, Is.EqualTo(2));

        }
        /// <summary>
        /// To the User Register
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UserRegister_valid_Returns()
        {
            mockCouponManagementContext.UserDetails.AddRange(mockUserDatas.userDetails);
            await mockCouponManagementContext.SaveChangesAsync();

            var getUserById = await userRepository.UserRegister(new UserDetails()
            {
                UserId = 12,
                FirstName = "sai",
                LastName = "manasa",
                UserName = "sree",
                UserPassword = "manasa",
                EmailAddr = "manasa@gmail.com",
                PhoneNumber = "7660912345",
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserAddress = "chennai"

            });
            Assert.That(getUserById, Is.Not.Null);
            Assert.That(getUserById, Is.EqualTo("true"));

        }
        /// <summary>
        ///     To test Get User 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetUser_Valid_Returns()
        {
            mockCouponManagementContext.UserDetails.AddRange(mockUserDatas.userDetails);
            await mockCouponManagementContext.SaveChangesAsync();
            var getUserById = await userRepository.GetUser(10);
            Assert.That(getUserById, Is.Not.Null);
            Assert.That(getUserById.UserId, Is.EqualTo(10));
        }
        /// <summary>
        /// To Test the UpdateUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Test]
        public async Task UpdateUser_Valid_Returns()
        {
            mockCouponManagementContext.UserDetails.AddRange(mockUserDatas.userDetails);
            await mockCouponManagementContext.SaveChangesAsync();
            var result = await userRepository.UpdateUser(new UserDetails()
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
            Assert.AreEqual(result, true);
        }
        /// <summary>
        /// To Test the User Login
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UserLogin_Valid()
        {
           
                mockCouponManagementContext.UserDetails.AddRange(mockUserDatas.userDetails);
                await mockCouponManagementContext.SaveChangesAsync();
                var user = new UserLogin { UserName = "hello1", UserPassword = "hello1" };
                var result = userRepository.UserLogin(user);
                Assert.NotNull(result);   
        }
        /// <summary>
        /// To Test the GetIdByName
        /// </summary>
       [Test]
       public async Task GetIdByName()
        {
            mockCouponManagementContext.UserDetails.AddRange(mockUserDatas.userDetails);
            await mockCouponManagementContext.SaveChangesAsync();
            var id = userRepository.GetIdByName("hello1");
            Assert.That(id, Is.Not.Null);

        }
    }

}



