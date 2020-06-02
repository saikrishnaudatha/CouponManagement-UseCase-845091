using CouponManagementDBEntity.Models;
using SHR_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementDBEntity.Repository
{
  public  interface IUserRepository
    {
        Task<string> UserRegister(UserDetails user);
        Task<UserDetails> UserLogin(UserLogin user);
        Task<bool> UpdateUser(UserDetails user);
        Task<UserDetails> GetUser(int userId);

        Task<List<UserDetails>> GetAllUsers();
        Task<int> GetIdByName(string userName);
    }
}
