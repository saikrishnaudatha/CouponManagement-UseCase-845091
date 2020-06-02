using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CouponManagementDBEntity.Models;

namespace CouponManagementDBEntity.Repository
{
  public  interface ICouponRepository
    {
        Task<List<CouponDetails>> GetCoupons();
        Task<CouponDetails> GetCouponById(int couponId);
        Task<List<CouponDetails>> GetAllCoupon(int userId);
        Task<bool> AddCoupon(CouponDetails coupon);
        Task<bool> UpdateCoupon(CouponDetails coupon);
        Task<bool> DeleteCoupon(int couponId);
        Task<bool> Status();
    }
}
