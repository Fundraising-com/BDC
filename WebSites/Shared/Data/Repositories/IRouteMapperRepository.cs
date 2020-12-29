namespace GA.BDC.Shared.Data.Repositories
{
   public interface IRouteMapperRepository : IRepository<string>
   {
      string GetRedirect(string source);
   }
}
