//
// 2005-08-01 - Stephen Lim - New class.
//


using System;

namespace GA.BDC.Core.LinkShare
{
	/// <summary>
	/// Summary description for LsTransEntry.
	/// </summary>
	public class LsTransEntry
	{

		#region Fields
		private int visitorLogId = 0;
		private string visitorId = "";
		private int promotionId = 0;
		private int orderId = 0;
		private string siteId = "";
		private DateTime timeEntered = DateTime.Now;
		private DateTime timeCompleted = DateTime.Now;
		private double totalOrder = 0;
		private double netAmount = 0;
		private DateTime dateStamp = DateTime.Now;
		private int totalQuantity = 0;
		private string sku = "";
		#endregion


		#region Constructors

		public LsTransEntry()
		{

		}
		#endregion

		#region Properties
		public int VisitorLogId
		{
			get { return this.visitorLogId; }
			set { this.visitorLogId = value; }
		}

		public string VisitorId
		{
			get { return this.visitorId; }
			set { this.visitorId = value; }
		}

		public int PromotionId
		{
			get { return this.promotionId; }
			set { this.promotionId = value; }
		}

		public int OrderId
		{
			get { return this.orderId; }
			set { this.orderId = value; }
		}

		public string Sku
		{
			get { return this.sku; }
			set { this.sku = value; }
		}

		public string SiteId
		{
			get { return this.siteId; }
			set { this.siteId = value; }
		}

		public DateTime TimeEntered
		{
			get { return this.timeEntered; }
			set { this.timeEntered = value; }
		}

		public DateTime TimeCompleted
		{
			get { return this.timeCompleted; }
			set { this.timeCompleted = value; }
		}

		public double TotalOrder
		{
			get { return this.totalOrder; }
			set { this.totalOrder = value; }
		}

		public double NetAmount
		{
			get { return this.netAmount; }
			set { this.netAmount = value; }
		}

		public DateTime DateStamp
		{
			get { return this.dateStamp; }
			set { this.dateStamp = value; }
		}

		public int TotalQuantity
		{
			get { return this.totalQuantity; }
			set { this.totalQuantity = value; }
		}
		#endregion


	}
}
