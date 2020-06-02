using CouponManagementDBEntity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouponManagementTestCase.DATA
{
   public class UserDatas
    {
        public List<UserDetails> userDetails { get; private set; }
        public UserDatas()
        {
            userDetails = new List<UserDetails>(){new  UserDetails() { UserId = 10,UserName = "hello", EmailAddr = "hello@gmail.com",UserPassword ="hello",
            UserAddress="tnl",CreateDate=DateTime.Now,UpdatedDate=DateTime.Now,PhoneNumber="6583920836",FirstName ="hello",LastName="hello"
           },

           new UserDetails()
            {
                UserId = 18,
                UserName = "hello1",
                EmailAddr = "hello@gmail.com",
                UserPassword = "hello1",
                UserAddress = "tnl",
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PhoneNumber = "6583920836",
                FirstName = "hello1",
                LastName = "hello1"

           }};
        
        }

    }
}
