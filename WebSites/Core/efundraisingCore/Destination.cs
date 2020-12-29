using System;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.efundraisingCore.DataAccess;

namespace GA.BDC.Core.efundraisingCore
{
	/// <summary>
	/// Summary description for Destination.
	/// </summary>
	public class Destination : BusinessBase.BusinessBase
	{
		#region Constructors
		public Destination()
		{
		}
		#endregion

		#region Fields
		private int destinationId = 0;
		private int webSiteId = 0;
		private string url = "";
		#endregion

		#region Methods
		public static Destination GetDestination(int destinationId)
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetDestination(destinationId);
		}

		#endregion

		#region Properties
		public int DestinationId
		{
			get { return this.destinationId; }
			set { this.destinationId = value; }
		}

		public int WebSiteId
		{
			get { return this.webSiteId; }
			set { this.webSiteId = value; }
		}

		public string Url
		{
			get { return this.url; }
			set { this.url = value; }
		}

		#endregion
	}
}
