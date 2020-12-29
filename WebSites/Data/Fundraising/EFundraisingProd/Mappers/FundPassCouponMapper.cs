using System;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
    public static class FundPassCouponMapper
    {
        public static FundPassCoupon Hydrate(Fundpass_Coupons Fundpass_Coupons)
        {
            var result = new FundPassCoupon
            {
                  CodeId = Fundpass_Coupons.Code_ID,
                  FundraisingCodeName = Fundpass_Coupons.Fundraising_Code_Name,
                  FundraisingCode = Fundpass_Coupons.Fundraising_Code,
                  NumberOfCoupons = Fundpass_Coupons.Number_Of_Coupons,
                  CouponPrice = Fundpass_Coupons.Coupon_Price,
                  CouponStartdate = Fundpass_Coupons.Coupon_Startdate,
                  CouponEnddate = Fundpass_Coupons.Coupon_Enddate,
                  LeadIdUsed = Fundpass_Coupons.Lead_Id_Used,
                  CouponUsed = Fundpass_Coupons.Coupon_Used,
                CouponUsedDate = Fundpass_Coupons.Coupon_Used_Date,
                CreateDate = Fundpass_Coupons.Create_Date,
            };
         return result;
        }



        public static Fundpass_Coupons Dehydrate(FundPassCoupon FundPassCoupon)
        {
            return new Fundpass_Coupons
            {
                Code_ID = FundPassCoupon.CodeId,
                Fundraising_Code_Name = FundPassCoupon.FundraisingCodeName,
                Fundraising_Code = FundPassCoupon.FundraisingCode,
                Number_Of_Coupons = FundPassCoupon.NumberOfCoupons,
                Coupon_Price = FundPassCoupon.CouponPrice,
                Coupon_Startdate = FundPassCoupon.CouponStartdate,
                Coupon_Enddate = FundPassCoupon.CouponEnddate,
                Lead_Id_Used = FundPassCoupon.LeadIdUsed,
                Coupon_Used = FundPassCoupon.CouponUsed,
                Coupon_Used_Date = FundPassCoupon.CouponUsedDate,
                Create_Date = FundPassCoupon.CreateDate
            };
        }





    }
}
