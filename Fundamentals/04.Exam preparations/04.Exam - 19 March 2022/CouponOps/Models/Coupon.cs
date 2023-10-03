﻿namespace CouponOps.Models
{
    public class Coupon
    {
        public Coupon(string code, int discountPercentage, int validity)
        {
            this.Code = code;
            this.DiscountPercentage = discountPercentage;
            this.Validity = validity;
        }

        public string Code { get; set; }
        public int DiscountPercentage { get; set; }
        public int Validity { get; set; }

        public Website Website { get; set; }

        public override bool Equals(object obj)
        {
            return Code == (obj as Coupon).Code;
        }
    }
}
