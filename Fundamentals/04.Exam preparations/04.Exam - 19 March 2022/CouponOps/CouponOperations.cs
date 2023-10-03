namespace CouponOps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CouponOps.Models;
    using Interfaces;

    public class CouponOperations : ICouponOperations
    {
        private Dictionary<string, Coupon> coupons = new Dictionary<string, Coupon>();
        private Dictionary<string, Website> websites = new Dictionary<string, Website>();

        public void RegisterSite(Website website)
            => websites.Add(website.Domain, website);

        public void AddCoupon(Website website, Coupon coupon)
        {
            if (!Exist(website))
            {
                throw new ArgumentException();
            }

            coupons.Add(coupon.Code, coupon);
            website.Coupons.Add(coupon);
            coupon.Website = website;
        }

        public bool Exist(Website website) => websites.ContainsKey(website.Domain);

        public bool Exist(Coupon coupon) => coupons.ContainsKey(coupon.Code);

        public Website RemoveWebsite(string domain)
        {
            if (!websites.ContainsKey(domain))
            {
                throw new ArgumentException();
            }

            var website = websites[domain];

            foreach (var coupon in website.Coupons)
            {
                coupons.Remove(coupon.Code);
            }

            websites.Remove(domain);

            return website;
        }

        public Coupon RemoveCoupon(string code)
        {
            if (!coupons.ContainsKey(code))
            {
                throw new ArgumentException();
            }

            var coupon = coupons[code];

            coupon.Website.Coupons.Remove(coupon);
            coupons.Remove(code);

            return coupon;
        }

        public void UseCoupon(Website website, Coupon coupon)
        {
            if (!Exist(website) || !Exist(coupon) || coupon.Website != website)
            {
                throw new ArgumentException();
            }

            website.Coupons.Remove(coupon);

            coupons.Remove(coupon.Code);
        }

        public IEnumerable<Coupon> GetCouponsForWebsite(Website website)
        {
            if (!Exist(website))
            {
                throw new ArgumentException();
            }

            return website.Coupons;
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
            => coupons.Values.OrderByDescending(c => c.Validity)
                            .ThenByDescending(c => c.DiscountPercentage);

        public IEnumerable<Website> GetSites() => websites.Values;

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
            => websites.Values.OrderBy(w => w.UsersCount)
                            .ThenByDescending(w => w.Coupons.Count);
    }
}
