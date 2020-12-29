using System;
using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface IFundPassCouponRepositoryRepository : IRepository<FundPassCoupon>
    {
        /// <summary>
        /// Insert lead id in fundpass table
        /// </summary>
        /// <param name="lead id"></param>
        void Update(int id);
        /// <summary>
        /// update fund pass table code used = true
        /// </summary>
        /// <param name="lead id"></param>
        void Update(int id, bool used);
        /// <summary>
        /// get coupons still available
        /// </summary>
        /// <param name=""></param>
        IList<FundPassCoupon> GetAllRemaining();
        // <summary>
        /// get coupons to process for taskexecuter
        /// </summary>
        /// <param name=""></param>
        IList<FundPassCoupon> GetCodesToProcessAll();
    }
}