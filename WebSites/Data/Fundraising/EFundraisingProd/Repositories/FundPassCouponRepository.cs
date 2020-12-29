using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Data.Repositories;
using System.Data.Entity.Validation;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
    public class FundPassCouponRepository : IFundPassCouponRepositoryRepository
    {

        private readonly DataProvider _dataProvider;
        public FundPassCouponRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        void IFundPassCouponRepositoryRepository.Update(int id)
        {

            var code = _dataProvider.Database.Connection.Query<Fundpass_Coupons>("SELECT TOP 1 Code_ID, Fundraising_Code_Name, Fundraising_Code, Number_Of_Coupons, Coupon_Price, Coupon_Startdate, Coupon_Enddate, Lead_Id_Used, Coupon_Used, Create_Date FROM Fundpass_Coupons (NOLOCK) WHERE  Lead_Id_Used = 0 AND Coupon_Used = 0", new {}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
            
                try
                {
                    var toBeUpdated = _dataProvider.Fundpass_Coupons.Find(code.Code_ID);
                    toBeUpdated.Fundraising_Code_Name = code.Fundraising_Code_Name;
                    toBeUpdated.Fundraising_Code = code.Fundraising_Code;
                    toBeUpdated.Number_Of_Coupons = code.Number_Of_Coupons;
                    toBeUpdated.Coupon_Price = code.Coupon_Price;
                    toBeUpdated.Coupon_Startdate = code.Coupon_Startdate;
                    toBeUpdated.Coupon_Enddate = code.Coupon_Enddate;
                    toBeUpdated.Lead_Id_Used = id;
                    toBeUpdated.Coupon_Used = code.Coupon_Used;
                    toBeUpdated.Coupon_Used_Date = null;
                    toBeUpdated.Create_Date = code.Create_Date;

                    _dataProvider.SaveChanges();
                }
                catch (DbEntityValidationException dbEntityValidationException)
                {
                    foreach (var validationError in dbEntityValidationException.EntityValidationErrors.SelectMany(p => p.ValidationErrors))
                    {
                        dbEntityValidationException.Data.Add($"EntityValidationError - {validationError.PropertyName}", validationError.ErrorMessage);
                    }
                    throw dbEntityValidationException;
                }
          
        }

        void IFundPassCouponRepositoryRepository.Update(int id, bool used)
        {

            var code = _dataProvider.Database.Connection.Query<Fundpass_Coupons>("SELECT TOP 1 Code_ID, Fundraising_Code_Name, Fundraising_Code, Number_Of_Coupons, Coupon_Price, Coupon_Startdate, Coupon_Enddate, Lead_Id_Used, Coupon_Used, Create_Date FROM Fundpass_Coupons (NOLOCK) WHERE  Lead_Id_Used = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
            
            try
            {
                var toBeUpdated = _dataProvider.Fundpass_Coupons.Find(code.Code_ID);
                toBeUpdated.Fundraising_Code_Name = code.Fundraising_Code_Name;
                toBeUpdated.Fundraising_Code = code.Fundraising_Code;
                toBeUpdated.Number_Of_Coupons = code.Number_Of_Coupons;
                toBeUpdated.Coupon_Price = code.Coupon_Price;
                toBeUpdated.Coupon_Startdate = code.Coupon_Startdate;
                toBeUpdated.Coupon_Enddate = code.Coupon_Enddate;
                toBeUpdated.Lead_Id_Used = id;
                toBeUpdated.Coupon_Used = used;
                toBeUpdated.Coupon_Used_Date = DateTime.Now;
                toBeUpdated.Create_Date = code.Create_Date;

                _dataProvider.SaveChanges();

            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                foreach (var validationError in dbEntityValidationException.EntityValidationErrors.SelectMany(p => p.ValidationErrors))
                {
                    dbEntityValidationException.Data.Add($"EntityValidationError - {validationError.PropertyName}", validationError.ErrorMessage);
                }
                throw dbEntityValidationException;
            }
        }

        public IList<FundPassCoupon> GetAllRemaining()
        {

            var ids = _dataProvider.Database.Connection.Query<int>("SELECT Code_ID FROM Fundpass_Coupons (NOLOCK) WHERE Lead_Id_Used = 0 AND Coupon_Used = 0",
                null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();


        }

        public IList<FundPassCoupon> GetCodesToProcessAll()
        {

            var ids = _dataProvider.Database.Connection.Query<int>("SELECT Code_ID FROM Fundpass_Coupons (NOLOCK) WHERE Coupon_Used = 0 and Lead_Id_Used > 0",
                null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();


        }

        public FundPassCoupon GetById(int id)
        {
            var codeFound = _dataProvider.Database.Connection.Query<Fundpass_Coupons>("SELECT TOP 1 Code_ID, Fundraising_Code_Name, Fundraising_Code, Number_Of_Coupons, Coupon_Price, Coupon_Startdate, Coupon_Enddate, Lead_Id_Used, Coupon_Used, Create_Date FROM Fundpass_Coupons (NOLOCK) WHERE  Code_ID = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
            var result = FundPassCouponMapper.Hydrate(codeFound);
            return result;
          
        }



        public void Delete(FundPassCoupon model)
        {
            throw new NotImplementedException();
        }

        public IList<FundPassCoupon> GetAll()
        {

           throw new NotImplementedException();
        }

        
        public int Save(FundPassCoupon model)
        {
            throw new NotImplementedException();
        }

        
        void IRepository<FundPassCoupon>.Delete(FundPassCoupon model)
        {
            throw new NotImplementedException();
        }

        IList<FundPassCoupon> IRepository<FundPassCoupon>.GetAll()
        {
            //var codes = _dataProvider.Database.Connection.Query<int>("SELECT Code_ID FROM Fundpass_Coupons (NOLOCK)", null,
            //   _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            //    


            var ids = _dataProvider.Database.Connection.Query<int>("SELECT partner_id FROM partner (NOLOCK);", null,
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();


            throw new NotImplementedException();
        }

        FundPassCoupon IRepository<FundPassCoupon>.GetById(int id)
        {
            var promoFound = _dataProvider.Database.Connection.Query<Fundpass_Coupons>("SELECT TOP 1 Code_ID, Fundraising_Code_Name, Fundraising_Code, Number_Of_Coupons, Coupon_Price, Coupon_Startdate, Coupon_Enddate, Lead_Id_Used, Coupon_Used, Create_Date FROM Fundpass_Coupons (NOLOCK) WHERE  Lead_Id_Used = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).FirstOrDefault();
            if (promoFound != null)
            {
                var result = FundPassCouponMapper.Hydrate(promoFound);
                return result;
            }
            else
            {
                return null;
            }
        }

        int IRepository<FundPassCoupon>.Save(FundPassCoupon model)
        {
            throw new NotImplementedException();
        }

        

        void IRepository<FundPassCoupon>.Update(FundPassCoupon model)
        {
            throw new NotImplementedException();
        }
    }
}
