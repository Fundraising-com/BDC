using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CachingHastable : System.Collections.Hashtable
	{
		System.TimeSpan timeSpan;
		public Hashtable logTime = new Hashtable();

		public CachingHastable()
		{
			timeSpan = new TimeSpan(0, 0, 10);
		}

		
		public CachingHastable(int Hour, int Min, int Sec)
		{
			timeSpan = new TimeSpan(Hour, Min, Sec);
		}

		public override object this[object key]
		{
			get
			{
				if (base[key] != null)
				{
					DateTime logDT = DateTime.MinValue;
					try
					{
						if (logTime[key] != null)
							logDT = (DateTime)logTime[key];
					}
					catch (InvalidCastException)
					{
						logDT = DateTime.MinValue;
					}

					if (logDT != DateTime.MinValue)
					{
						if (DateTime.Now.Subtract(logDT) > timeSpan)
						{
							base.Remove(key);
							logTime.Remove(key);
							return null;
						}
						else
							return base[key];
					}
					else
					{
						base.Remove(key);
						logTime.Remove(key);
						return null;
					}
				}
				return null;
			}
			set
			{
				logTime[key] = DateTime.Now;
				base[key] = value;
			}
		}

		public override void Add(object key, object value)
		{
			this[key] = value;
		}


	}

	public class Package: EFundraisingCRMDataObject {

		private int packageId;
		private string description;
		private string comments;
		private string packageImage;
		private string packageProfit;
		private string packageWebDesc;
		private string packageTitleImage;
		private int isDisplayable;
		private decimal profitMin;
		private decimal profitMax;
		private decimal profitDefault;
		private int productClassId;
		private Hashtable scratchBookCollection = new Hashtable();
		public System.Data.DataTable profitList = null;


		public Package() : this(int.MinValue) { }
		public Package(int packageId) : this(packageId, null) { }
		public Package(int packageId, string description) : this(packageId, description, null) { }
		public Package(int packageId, string description, string comments) : this(packageId, description, comments, null) { }
		public Package(int packageId, string description, string comments, string packageImage) : this(packageId, description, comments, packageImage, null) { }
		public Package(int packageId, string description, string comments, string packageImage, string packageProfit) : this(packageId, description, comments, packageImage, packageProfit, null) { }
		public Package(int packageId, string description, string comments, string packageImage, string packageProfit, string packageWebDesc) : this(packageId, description, comments, packageImage, packageProfit, packageWebDesc, null) { }
		public Package(int packageId, string description, string comments, string packageImage, string packageProfit, string packageWebDesc, string packageTitleImage) : this(packageId, description, comments, packageImage, packageProfit, packageWebDesc, packageTitleImage, int.MinValue) { }
		public Package(int packageId, string description, string comments, string packageImage, string packageProfit, string packageWebDesc, string packageTitleImage, int isDisplayable) {
			this.packageId = packageId;
			this.description = description;
			this.comments = comments;
			this.packageImage = packageImage;
			this.packageProfit = packageProfit;
			this.packageWebDesc = packageWebDesc;
			this.packageTitleImage = packageTitleImage;
			this.isDisplayable = isDisplayable;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Package>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<PackageImage>" + System.Web.HttpUtility.HtmlEncode(packageImage) + "</PackageImage>\r\n" +
			"	<PackageProfit>" + System.Web.HttpUtility.HtmlEncode(packageProfit) + "</PackageProfit>\r\n" +
			"	<PackageWebDesc>" + System.Web.HttpUtility.HtmlEncode(packageWebDesc) + "</PackageWebDesc>\r\n" +
			"	<PackageTitleImage>" + System.Web.HttpUtility.HtmlEncode(packageTitleImage) + "</PackageTitleImage>\r\n" +
			"	<IsDisplayable>" + isDisplayable + "</IsDisplayable>\r\n" +
			"</Package>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageImage")) {
					SetXmlValue(ref packageImage, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageProfit")) {
					SetXmlValue(ref packageProfit, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageWebDesc")) {
					SetXmlValue(ref packageWebDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageTitleImage")) {
					SetXmlValue(ref packageTitleImage, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDisplayable")) {
					SetXmlValue(ref isDisplayable, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Package[] GetPackages() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackages();
		}

		public static Package GetPackageByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackageByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPackage(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePackage(this);
		}

		
		public static System.Data.DataTable GetDefaultProfit(int packageId, ref decimal defaultProfit, Package p, decimal incrPercent)
		{
			defaultProfit = decimal.Zero;
			System.Data.DataTable dtResult = null;
			System.Data.DataTable dt = new System.Data.DataTable();
			dt.Columns.Add("profit", typeof(decimal));
			//
			System.Data.DataRow row = null;
			if (p != null && p.ProfitMin >= 0 && p.ProfitMax < decimal.MaxValue)
			{
				defaultProfit = p.profitDefault;
				int i = 0;
				decimal decTmp = p.ProfitMin;
				for (; decTmp <= p.ProfitMax && i < 1000; i++)
				{
					row = dt.NewRow();
					row["profit"] = decTmp;
					decTmp += incrPercent;//(decimal)0.05;
					dt.Rows.Add(row);
				}

				System.Data.DataRow[] rows = dt.Select(string.Format("profit={0}", defaultProfit.ToString()) );
				if (rows == null || rows.Length < 1)
				{
					row = dt.NewRow();
					row["profit"] = defaultProfit;
					dt.Rows.Add(row);
					dtResult = dt.Clone();
					rows = dt.Select("", "profit asc");
					for (i=0; i < rows.Length; i++)
						dtResult.ImportRow(rows[i]);
					
					return dtResult;
				}
				else
					return dt;
			}
			else
				return dt;
		}
		#endregion

		#region Properties
		public int PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public string PackageImage {
			set { packageImage = value; }
			get { return packageImage; }
		}

		public string PackageProfit {
			set { packageProfit = value; }
			get { return packageProfit; }
		}

		public string PackageWebDesc {
			set { packageWebDesc = value; }
			get { return packageWebDesc; }
		}

		public string PackageTitleImage {
			set { packageTitleImage = value; }
			get { return packageTitleImage; }
		}

		public int IsDisplayable {
			set { isDisplayable = value; }
			get { return isDisplayable; }
		}

		public decimal ProfitMin
		{
			get { return profitMin;}
			set { profitMin = value;}
		}

		public decimal ProfitMax
		{
			get { return profitMax;}
			set { profitMax = value;}
		}

		public decimal ProfitDefault
		{
			get { return profitDefault;}
			set { profitDefault = value;}
		}

		public int ProductClassId
		{
			get { return productClassId;}
			set { productClassId = value;}
		}

		public Hashtable ScratchBookCollection
		{
			get {return scratchBookCollection;}
		}
		#endregion
	}
}
