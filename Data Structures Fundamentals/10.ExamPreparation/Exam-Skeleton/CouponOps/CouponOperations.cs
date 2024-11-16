namespace CouponOps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CouponOps.Models;
    using Interfaces;

    public class CouponOperations : ICouponOperations
    {
        Dictionary<string, Website> websites = new Dictionary<string, Website>();
        Dictionary<string, Coupon> coupons = new Dictionary<string, Coupon>();

        public void AddCoupon(Website website, Coupon coupon)
        {
            if (!websites.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }

            coupons.Add(coupon.Code, coupon);
            websites[website.Domain].Coupons.Add(coupon);
            coupon.Website = website;
        }

        public bool Exist(Website website)
            => websites.ContainsKey(website.Domain);

        public bool Exist(Coupon coupon)
            => coupons.ContainsKey(coupon.Code);

        public IEnumerable<Coupon> GetCouponsForWebsite(Website website)
        {
            if(!websites.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }
            return coupons.Values.Where(c => c.Website == website);
        }


        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
            => coupons.Values.OrderByDescending(c => c.Validity)
                .ThenByDescending(c => c.DiscountPercentage);

        public IEnumerable<Website> GetSites()
            => websites.Values;

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
            => websites.Values.OrderBy(w => w.UsersCount)
                .ThenByDescending(w => w.Coupons.Count());

        public void RegisterSite(Website website)
        {
            if (websites.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }

            websites.Add(website.Domain, website);
        }

        public Coupon RemoveCoupon(string code)
        {
            if (!coupons.ContainsKey(code))
            {
                throw new ArgumentException();
            }

            Coupon coupon = coupons[code];
            coupons.Remove(code);

            return coupon;
        }

        public Website RemoveWebsite(string domain)
        {
            if (!websites.ContainsKey(domain))
            {
                throw new ArgumentException();
            }

            Website website = websites[domain];
            websites.Remove(domain);
            foreach (var coupon in website.Coupons)
            {
                coupons.Remove(coupon.Code);
            }

            return website;
        }

        public void UseCoupon(Website website, Coupon coupon)
        {
            if (!websites.ContainsKey(website.Domain)
                || !websites.ContainsKey(coupon.Code))
            {
                throw new ArgumentException();
            }

            if (!websites[website.Domain].Coupons.Contains(coupon))
            {
                throw new ArgumentException();
            }

            website.Coupons.Remove(coupon);
            coupons.Remove(coupon.Code);
        }
    }
}
