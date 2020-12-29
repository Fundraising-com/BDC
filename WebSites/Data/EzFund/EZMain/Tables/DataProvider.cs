using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    public class DataProvider : DbContext
    {
        public DataProvider()
            : base("name=EZMain")
        {
        }
		  public virtual DbSet<page_route_mapper> page_route_mappers { get; set; }
		  public virtual DbSet<site_testml_tbl> testimonials { get; set; }
        public virtual DbSet<lead> leads { get; set; }
        public virtual DbSet<pros_pdct_tbl> lead_products { get; set; }
        public virtual DbSet<ar_trns_tbl> ar_trns_tbl { get; set; }

    }
}
