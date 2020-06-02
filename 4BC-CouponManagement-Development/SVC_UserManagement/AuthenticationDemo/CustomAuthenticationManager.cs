using CouponManagementDBEntity.Models;
using System;

using System.Collections.Generic;
using System.Linq;

namespace UserManagement.AuthenticationDemo

{

    public interface ICustomAuthenticationManager

    {

        string UserLogin(string username, string password);

        IDictionary<string, string> Tokens { get; }

    }

    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly IDictionary<string, string> tokens = new Dictionary<string, string>();
        private readonly CouponManagementContext _couponManagementContext;
        public CustomAuthenticationManager(CouponManagementContext couponManagementContext)
        {
            _couponManagementContext = couponManagementContext;
        }
        public IDictionary<string, string> Tokens => tokens;

        public string UserLogin(string username, string password)
        {
            UserDetails userDetails = _couponManagementContext.UserDetails.SingleOrDefault(e => e.UserName == username && e.UserPassword == password);
            if (userDetails == null)
                return null;
            else
            {

                var token = Guid.NewGuid().ToString();

                tokens.Add(token, password);

                return token;
            }

        }

    }

}
