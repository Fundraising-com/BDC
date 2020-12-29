using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for TopSupporterCollection.
	/// </summary>
    [Serializable]
	public class TopSupporterCollection {
		private ArrayList supporters;

		public TopSupporterCollection() {
			supporters = new ArrayList();
		}

		public void Add(TopSupporter topSupporter) {
			supporters.Add(topSupporter);
		}

		public static TopSupporterCollection Load(Event myEvent) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetTopSupporterCollection(myEvent.EventID);
		}

		public ArrayList TopSupporters {
			get { return supporters; }
		}
	}
}
