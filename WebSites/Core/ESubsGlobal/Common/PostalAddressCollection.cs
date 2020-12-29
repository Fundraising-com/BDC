/* Title:	PostalAddressCollection
 * Author:	Jean-Francois Buist
 * Summary:	Handle all postal addresses for one member.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Common {
	/// <summary>
	/// Summary description for PostalAddressCollection.
	/// </summary>
    [Serializable]
	public class PostalAddressCollection : EnvironmentCollectionBase {

		#region Constructors
		/// <summary>
		/// Create a new instance of PostalAddressCollection.
		/// </summary>
		public PostalAddressCollection()
		{

		}
		#endregion

		#region Properties
		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public PostalAddress this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (PostalAddress) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}
		#endregion

		#region Methods

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">PostalAddress object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(PostalAddress obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">PostalAddress object.</param>
		public void Remove(PostalAddress obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">PostalAddress object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(PostalAddress obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">PostalAddress object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(PostalAddress obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">PostalAddress object.</param>
		public void Insert(int index, PostalAddress obj) 
		{
			List.Insert(index, obj);
		}

        public void InsertToMember(ESubsGlobal.Users.eSubsGlobalUser member)
        {
            foreach(PostalAddress postalAddress in this.List) {
                postalAddress.Insert();

                DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
                dbo.InsertPostalAddressToMember(member, postalAddress);
            }
        }

        public static PostalAddressCollection GetPostalAddressCollectionForMember(ESubsGlobal.Users.eSubsGlobalUser member)
        {
            PostalAddressCollection pac = new PostalAddressCollection();
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            dbo.GetMemberPostalAddressCollection(member.ID, pac);
            return pac;
        }

		#endregion

	}
}
