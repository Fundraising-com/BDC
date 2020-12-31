using System;
using System.Runtime.Serialization;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Magazine.
	/// </summary>
	/// 
	[Serializable]
	public class CatalogSection
	{
		private int catalogSectionID = 0;
		private CatalogSectionType type = CatalogSectionType.None;
		private string typeDescription = "";
		private string name = "";
		private int fsProgramID = 0;

		public CatalogSection() { }

		public CatalogSection(int catalogSectionID, CatalogSectionType type, string typeDescription, string name, int fsProgramID)
		{
			this.catalogSectionID = catalogSectionID;
			this.type = type;
			this.typeDescription = typeDescription;
			this.name = name;
			this.fsProgramID = fsProgramID;
		}

		public int CatalogSectionID 
		{
			get 
			{
				return catalogSectionID;
			}
			set 
			{
				catalogSectionID = value;
			}
		}

		public CatalogSectionType Type 
		{
			get 
			{
				return type;
			}
			set 
			{
				type = value;
			}
		}

		public string TypeDescription 
		{
			get 
			{
				return typeDescription;
			}
			set 
			{
				typeDescription = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set 
			{
				name = value;
			}
		}

		public int FSProgramID 
		{
			get 
			{
				return fsProgramID;
			}
			set 
			{
				fsProgramID = value;
			}
		}
	}
}
