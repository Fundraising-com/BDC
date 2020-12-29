using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper.Contrib.Extensions;
using Dapper;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
	public class RouteMapperRepository : IRouteMapperRepository
	{
		private readonly DataProvider _dataProvider;

		public RouteMapperRepository(DataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}
		public string GetRedirect(string source)
		{
			if (source.Contains("autodiscover.xml") || source.EndsWith(".php", StringComparison.InvariantCultureIgnoreCase)) return string.Empty;
			var routes = _dataProvider.page_route_mappers.Where(p => p.enabled).ToList();
			var redirection = routes.FirstOrDefault(p => source.ToLower().StartsWith(p.source.ToLower()));
			if (redirection == null)
			{
				return "/";
			}
			return redirection.destination;
		}

		public string GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IList<string> GetAll()
		{
			throw new NotImplementedException();
		}

		public int Save(string model)
		{
			throw new NotImplementedException();
		}

		public void Update(string model)
		{
			throw new NotImplementedException();
		}

		public void Delete(string model)
		{
			throw new NotImplementedException();
		}
	}
}
