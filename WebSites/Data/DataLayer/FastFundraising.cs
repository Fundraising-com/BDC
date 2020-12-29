using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Data.DataLayer
{
  public class FastFundraising
   {
      public static List<ffcart> GetCarts(string connectionString,  int cookieID)
      {

         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
     
                  return (from c in repo.ffcarts where c.cookieid == cookieID select c).ToList<ffcart>();
         }
         
         
      }

      public static ffitem LoadProductByProductID(string connectionString, int id)
      {
         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
           return (from f in repo.ffitems where f.itemid == id select f).FirstOrDefault();
           
         }

      }

      public static List<CartItem> GetCartItems(string connectionString, int? id)
      {
         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
            List<CartItem> output = new List<CartItem>();
            var temp =  from c in repo.ffcarts join i in repo.ffitems on c.itemid equals i.itemid where c.cookieid ==  id select new { c, i};
            foreach (var ie in temp)
            {
               output.Add(new CartItem { ItemNumber = ie.i.itemnmbr, Quantity = ie.c.qty, PriceApplied = ie.c.itempriceapplied, ItemName=  ie.c.itemname });
            }

            return output;

         }
      }


      public static List<CartItem> GetCartItemsWithShippingGroup(string connectionString, int? id)
      {
         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
            List<CartItem> output = new List<CartItem>();
            var temp =  from c in repo.ffcarts join i in repo.ffitems on c.itemid equals i.itemid
                        join cat in repo.categories on i.categoryid equals cat.categoryid
                        where c.cookieid == id
                        select new { c, i , cat};
            foreach (var ie in temp)
            {
               output.Add(new CartItem { ItemNumber = ie.i.itemnmbr, Quantity = ie.c.qty, PriceApplied = ie.c.itempriceapplied, ItemName=  ie.c.itemname, ShippingGroupId = ie.cat.shipping_group_id });
            }

            return output;

         }
      }

      public static List<shipping_group> GetShippingGroup(string connectionString, int cartId)
      {
         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
            return  (from c in repo.ffcarts
                         join i in repo.ffitems on c.itemid equals i.itemid
                         join cat in repo.categories on i.categoryid equals cat.categoryid
                         join sg in repo.shipping_groups on cat.shipping_group_id equals sg.shipping_group_id
                         where c.cookieid == cartId
                         select sg).Distinct().ToList<shipping_group>();
         }
      }

      public static List<shipping_fee> GetShippingFeesByShippingGroupId(string connectionString, int shippingGroupId)
      {
         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
            return (from sg in repo.shipping_groups join sf in repo.shipping_fees on sg.shipping_fee_id equals sf.shipping_fee_id where sg.shipping_group_id == shippingGroupId select sf).ToList<shipping_fee>();
         }
      }



      public static shipping_fee GetShippingFees(string connectionString, int shippingFeeId)
      {
         using (FastFundraisingDataContext repo = new FastFundraisingDataContext(connectionString))
         {
            return (from sf in repo.shipping_fees where sf.shipping_fee_id == shippingFeeId select sf).FirstOrDefault();
         }
      }


      public static decimal GetShippingFee(string connectionString, int cartId)
      {
         List<shipping_group> sg = FastFundraising.GetShippingGroup(connectionString, cartId);
         if (sg == null || sg.Count < 1)
         {
            return Decimal.Zero;
         }
         else
         {
            List<FastFundraising.CartItem> ci = FastFundraising.GetCartItemsWithShippingGroup(connectionString, cartId);
            if (ci == null || ci.Count < 1)
            {
               return Decimal.Zero;
            }
            else
            {
         
               var temp = from i in ci group i by i.ShippingGroupId into g select new { ShippingGroupId = g.Key, TotalItems = g.Sum(q => q.Quantity) };
               if (temp == null || temp.Count() < 1)
               {
                  return Decimal.Zero;

               }
               else
               {
                  decimal output = Decimal.Zero;
                  foreach (var item in temp)
                  {
                     List<shipping_group> shipGroup = sg.Where(a => a.shipping_group_id == item.ShippingGroupId).ToList<shipping_group>();
                     if (shipGroup != null && shipGroup.Count > 0)
                     {
                        List<shipping_fee> shipFeeList = new List<shipping_fee>();
                        foreach (shipping_group sgi in shipGroup)
                        {
                           shipFeeList.Add(GetShippingFees(connectionString, sgi.shipping_fee_id));

                        }
                        shipping_fee sf = (from s in shipFeeList where s.sale_amt_min <= item.TotalItems && s.sale_amt_max >= item.TotalItems select s).FirstOrDefault();
                        if (sf == null)
                        {
                           shipping_group sd = shipGroup.Where(p => p.@default == true).FirstOrDefault();
                           sf = GetShippingFees(connectionString, sd.shipping_fee_id);
                        }

                        if (sf != null)
                        {
                           output += sf.shipping_fee1??Decimal.Zero;
                        }
                     }


                 }
                  return output;
               }

            }
         }
      }

      public class CartItem
      {
         public string ItemNumber { get; set; }
         public int? Quantity { get; set; }
         public double? PriceApplied { get; set; }
         public string ItemName { get; set; }
         public int? ShippingGroupId { get; set; }
       }
   }
}
