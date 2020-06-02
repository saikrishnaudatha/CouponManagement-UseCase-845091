using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_UI.Models
{
    public class CouponDetails
    {
        [Required]
        public int CouponId { get; set; }
        public string CouponNumber { get; set; }
        public string CouponStatus { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponExpiredDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UserId { get; set; }
    }
}
