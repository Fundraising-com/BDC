using System;
using System.Runtime.Serialization;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Magazine.
	/// </summary>
	/// 
	[Serializable]
	public class Magazine
	{
		private string sTitle;
		private string sProductCode;
		private int iMagInstance;
		private float fPrice;
		private int iTerm;
		private string sNewRenew;
		private int iProductType;


		public Magazine()
		{
		}
		public Magazine(string Title,int MagInstance,string ProductCode,float Price,int Term, int ProductType)
		{
			sTitle =Title;
			sProductCode= ProductCode;
			iMagInstance  = MagInstance;
			fPrice = Price;
			iTerm = Term;
			iProductType = ProductType;
		}
		public Magazine(string Title,int MagInstance,string ProductCode,float Price,int Term,string NewRenew, int ProductType)
		{
			sTitle =Title;
			sProductCode= ProductCode;
			iMagInstance  = MagInstance;
			fPrice = Price;
			iTerm = Term;
			sNewRenew = NewRenew;
			iProductType = ProductType;
		}

		public string Title
		{
			get
			{
				return sTitle;}
		}
		public int MagInstance
		{
			get
			{
				return iMagInstance;}
		}								  
		public string ProductCode
		{
			get
			{
				return sProductCode;}
		}
		public float Price
		{
			get
			{
				return fPrice;}
		}
		public int Term
		{
			get
			{
				return iTerm;}
		}
		public string NewRenew
		{
			get
			{
				return this.sNewRenew;
			}
		}
		public int ProductType
		{
			get
			{
				return iProductType;
			}
		}
	}
}
