using System;

namespace GA.BDC.Core.EFundraisingCRM {

	/// <summary>
	/// Summary description for EmailTemplateCollection.
	/// </summary>
	public class EmailTemplateCollection : EFundraisingCRMCollectionBase {
		private bool sortAssending = true;

		public EmailTemplateCollection() {
			//
			// TODO: Add constructor logic here
			//
		}

		#region Sorting Methods
		public void Sort(EmailTemplateSort sort) {
			for(int i=0;i<List.Count;i++) {
				EmailTemplate emailTemplate = (EmailTemplate)List[i];
				emailTemplate.Sort = sort;
				emailTemplate.SortAssending = sortAssending;
			}
			Sort();
		}
		#endregion

		#region Methods

		public EmailTemplate GetEmailTemplateByID(int id) {
			for(int i=0;i<List.Count;i++) {
				EmailTemplate emailTemplate = (EmailTemplate)List[i];
				if(emailTemplate.EmailTemplateId == id) {
					return emailTemplate;
				}
			}
			return null;
		}

		public void LoadAllTemplates() {
			EmailTemplate[] emailTemplates = EmailTemplate.GetEmailTemplates();
			for(int i=0;i<emailTemplates.Length;i++) {
				List.Add(emailTemplates[i]);
			}
		}
		/*
		// get the grand total of sales
		public float GetSalesTotal() {
			float total = 0.0f;
			foreach(Sale sale in List) {
				total += sale.TotalAmount;
			}
			return total;
		}
		#endregion

		#region Comparable Methods
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() {
			// sort the collection
			SortProcess();
		}

		// sort the collection list using the specified sort argument
		public void Sort(SaleComparable sortBy) {
			// set the sort by option
			SetSortBy(sortBy);

			// sort the collection
			SortProcess();
		}

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) {
			SaleCollection copy =
				(SaleCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		// apply the sort by to a complete list in the collection
		public void SetSortBy(SaleComparable comparable) {
			foreach(Sale sale in List) {
				sale.SortBy = comparable;
			}
		}*/
		#endregion
       
		#region Operators
		public static EmailTemplateCollection operator +(EmailTemplateCollection collection1, EmailTemplateCollection collection2) {
			return (EmailTemplateCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static EmailTemplateCollection operator +(EmailTemplateCollection collection, EmailTemplate item) {
			return (EmailTemplateCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static EmailTemplateCollection operator -(EmailTemplateCollection collection1, EmailTemplateCollection collection2) {
			return (EmailTemplateCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static EmailTemplateCollection operator -(EmailTemplateCollection collection, EmailTemplate item) {
			return (EmailTemplateCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

		#region Properties
		public bool SortAssending {
			get { return sortAssending; }
			set { sortAssending = value; }
		}
		#endregion
	}
}

