using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
	/// <summary>
	/// Summary description for FeaturedGroupReport.
	/// </summary>
	public class FeaturedGroupReport 
	{
		private FeaturedGroupCollection featuredGroupCollection = new FeaturedGroupCollection();

		public FeaturedGroupReport()
		{
			
		}

		public static FeaturedGroupCollection CreateFeaturedGroupReport() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetFeaturedGroupReport();
		}

		public static FeaturedGroupCollection CreateFeaturedGroupMainPageReport() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetFeaturedGroupMainPageReport();
		}

		public void Sort() {
			featuredGroupCollection.FeaturedGroups.Sort();
		}

		public FeaturedGroupCollection FeaturedGroups {
			get { return featuredGroupCollection; }
			set { featuredGroupCollection = value; }
		}

	}
}
