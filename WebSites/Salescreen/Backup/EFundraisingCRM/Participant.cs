using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Participant: EFundraisingCRMDataObject {

		private int participantId;
		private string firstName;
		private string lastName;
		private DateTime createDate;
		private System.Collections.ArrayList salesItems = new System.Collections.ArrayList();


		public Participant() : this(int.MinValue) { }
		public Participant(int participantId) : this(participantId, null) { }
		public Participant(int participantId, string firstName) : this(participantId, firstName, null) { }
		public Participant(int participantId, string firstName, string lastName) : this(participantId, firstName, lastName, DateTime.MinValue) { }
		public Participant(int participantId, string firstName, string lastName, DateTime createDate) {
			this.participantId = participantId;
			this.firstName = firstName;
			this.lastName = lastName;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Participant>\r\n" +
				"	<ParticipantId>" + participantId + "</ParticipantId>\r\n" +
				"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
				"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</Participant>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("participantId")) {
					SetXmlValue(ref participantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("createDate")) {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Participant[] GetParticipants() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetParticipants();
		}

		public static Participant GetParticipantByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetParticipantByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertParticipant(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateParticipant(this);
		}

		public static System.Collections.Hashtable GetParticipantBySalesId(int salesId)
		{			
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetParticipantBySalesId(salesId);
		}
		#endregion

		#region Properties
		public int ParticipantId {
			set { participantId = value; }
			get { return participantId; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		public System.Collections.ArrayList SalesItems
		{
			get
			{
				return salesItems;
			}
		}


		#endregion

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			// TODO:  Add Participant.CompareTo implementation
			Participant p = obj as Participant;
			if (p != null)
				return string.Compare(p.FirstName + p.LastName, this.FirstName + this.LastName);
			return 0;
		}

		#endregion
	}



	public class ParticipantCollection : EFundraisingCRMCollectionBase 
	{
		
		public ParticipantCollection()
		{}

		#region Comparable Methods
		
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() 
		{
			// sort the collection
			SortProcess();
		}

		/*// sort the collection list using the specified sort argument
		public void Sort(SaleComparable sortBy) {
			// set the sort by option
			SetSortBy(sortBy);

			// sort the collection
			SortProcess();
		}*/

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) 
		{
			SaleCollection copy =
				(SaleCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		#endregion
       
		#region Operators
		public static ParticipantCollection operator +(ParticipantCollection collection1, ParticipantCollection collection2) 
		{
			return (ParticipantCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static ParticipantCollection operator +(ParticipantCollection collection, ScratchBook item) 
		{
			return (ParticipantCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static ParticipantCollection operator -(ParticipantCollection collection1, ParticipantCollection collection2) 
		{
			return (ParticipantCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static ParticipantCollection operator -(ParticipantCollection collection, ScratchBook item) 
		{
			return (ParticipantCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

		#region Properties
		
		#endregion

	}
}
