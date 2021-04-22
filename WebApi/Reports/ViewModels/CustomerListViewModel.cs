using System;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Reports.ViewModels
{
   public class CustomerListViewModel
    {
        public DateTime? SalesConfirmStart { get; set; }
        public DateTime? SalesConfirmEnd { get; set; }
    }
}