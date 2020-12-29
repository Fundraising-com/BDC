using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IRepresentativeRepository : IRepository<Representative>
   {
      /// <summary>
      /// Return the Representative by its Redirect
      /// </summary>
      /// <param name="redirect">Redirect</param>
      /// <returns>Representative</returns>
      Representative GetByRedirect(string redirect);
      /// <summary>
      /// Returns the Representative related to the Consultant for this Lead
      /// </summary>
      /// <param name="leadId">Lead Id</param>
      /// <returns>Representative</returns>
      Representative GetByLead(int leadId);
   }
}
