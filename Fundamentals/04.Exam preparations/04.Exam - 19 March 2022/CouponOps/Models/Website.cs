namespace CouponOps.Models
{
    using System.Collections.Generic;

    public class Website
    {
        public Website(string domain, int usersCount)
        {
            this.Domain = domain;
            this.UsersCount = usersCount;
        }

        public string Domain { get; set; }
        public int UsersCount { get; set; }

        public HashSet<Coupon> Coupons { get; set; } = new HashSet<Coupon>();

        public override bool Equals(object obj)
        {
            return Domain == (obj as Website).Domain;
        }
    }
}
