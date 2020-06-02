using System;
using System.Collections.Generic;
using System.Text;
using CouponManagementDBEntity.Models;

namespace CouponManagementTestCase.DATA
{
    class CouponData
    {
        public List<CouponDetails> couponDetails { get; private set; }
        public List<UserDetails> userDetails { get; private set; }
        public CouponData()
        {

            userDetails = new List<UserDetails>()
            {
                new  UserDetails()
                { 
                    UserId = 10,
                    UserName = "hello",
                    EmailAddr = "hello@gmail.com",
                    UserPassword ="hello",
                    UserAddress="tnl",
                    CreateDate=DateTime.Now,
                    UpdatedDate=DateTime.Now,
                    PhoneNumber="6583920836",
                    FirstName ="hello",
                    LastName="hello"
           } };


            couponDetails = new List<CouponDetails>()
            {
                new CouponDetails()
                {
                    CouponId=20,
                    CouponNumber="se345df",
                    CouponStatus="valid",
                    CouponStartDate=DateTime.Now,
                    CouponExpiredDate=DateTime.Now.AddDays(10),
                    CreateDate=DateTime.Now,
                    UpdatedDate=DateTime.Now,
                   UserId=10
                }
            };
        }


    }
}
