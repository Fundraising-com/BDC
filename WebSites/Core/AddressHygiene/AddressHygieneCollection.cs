using System;

namespace GA.BDC.Core.AddressHygiene
{
	/// <summary>
	/// Summary description for AddressHygieneCollection.
	/// </summary>
	
	using System.Xml;

	public class AddressHygieneCollection : GA.BDC.Core.Collections.BusinessCollectionBase
	{
		string webServicePath = "descendant::Record";
		public AddressHygieneCollection()
		{
		}

		public void LoadXmlFromWebService(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlNodeList nodeList = doc.SelectNodes(webServicePath);
			foreach (XmlNode node in nodeList)
			{
				AddressHygiene addr = new AddressHygiene();
				addr.LoadXml("<Record>" + node.InnerXml + "</Record>");
				this.Add(addr);
			}
		}
		
		/// <summary>Get or set the object at specified index.</summary>
		/// 
		public AddressHygiene this[int index]
		{
			get { return (AddressHygiene) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>Index of the newly added object.</returns>
		/// 
		public int Add(AddressHygiene obj)
		{
			return List.Add(obj);
		}

		/// <summary>Add collection to collection of objects.</summary>
		/// <param name="obj">LeadCollection object.</param>
		/// 
		public void Add(AddressHygieneCollection obj)
		{
			if (obj != null)
			{
				for (int i = 0; i < obj.Count; i++)
				{
					List.Add(obj[i]);
				}
			}
		}

		/// <summary>Remove object from collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// 
		public void Remove(AddressHygiene obj)
		{
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">Lead object</param>
		/// <returns>True if object is in collection, else false.</returns>
		/// 
		public bool Contains(AddressHygiene obj)
		{
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>The index of the object.</returns>
		/// 
		public int IndexOf(AddressHygiene obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">Lead object.</param>
		/// 
		public void Insert(int index, AddressHygiene obj) 
		{
			List.Insert(index, obj);
		}

		
		public string ToXmlAddressValidation()
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			for (int i=0; i< Math.Min(this.Count, 3); i++)
			{
				result.Append("<Record>" + this[i].ToXmlAddressValidation() + "</Record>");
			}
			return "<DataSet>" + result.ToString() + "</DataSet>";
		}
		
		public string ToHumanReadableString()
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			for (int i=0; i< this.Count; i++)
			{
				result.Append(this[i].ToHumanReadableString());
				result.Append("\r\n**********\r\n");
			}
			return result.ToString();
		}

		public AddressHygiene Find(int addressID)
		{
			for (int i=0; i < this.Count; i++)
			{
				if (this[i].AddressId == addressID)
					return this[i];
			}
			return null;
		}

		public void SortByType(AddressComparable srtBy)
		{
			for (int i=0; i < this.Count; i++)
			{
				AddressHygiene obj = this[i] as AddressHygiene;
				if (obj != null)
					obj.SortBy = srtBy;
			}
			Sort();
			for (int i=0; i < this.Count; i++)
			{
				AddressHygiene obj = this[i] as AddressHygiene;
				if (obj != null)
					obj.SortBy = AddressComparable.ToHumanReadableString;
			}
		}
	}
}
