using System;
using System.Collections.Generic;

namespace CouponManagementDBEntity.Models
{
    public partial class UserDetails
    {
        public UserDetails()
        {
            CouponDetails = new HashSet<CouponDetails>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPassword { get; set; }
        public string EmailAddr { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserAddress { get; set; }

        public virtual ICollection<CouponDetails> CouponDetails { get; set; }
    }
}
