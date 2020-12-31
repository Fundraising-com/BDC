using System;
using System.Web.UI;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persistence of the "readOnly" attribute of an element.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public class ClientReadOnlyAttribute : ClientPersistentAttribute
	{
		private const string CLIENT_READONLY_ATTRIBUTE = "readOnly";

		/// <summary>
		/// Manages the persistence of the "readOnly" attribute of an element.
		/// </summary>
		/// <param name=container>The element for which the property has to be persisted.</param>
		public ClientReadOnlyAttribute(System.Web.UI.WebControls.WebControl container) : base(container) { }

		/// <summary>
		/// Client style name used when rendering the control.
		/// </summary>
		public override string ClientPropertyName
		{
			get
			{
				return CLIENT_READONLY_ATTRIBUTE;
			}
		}

		/// <summary>
		/// Indicates whether the control is read only on the client.
		/// </summary>
		public bool IsReadOnly
		{
			get 
			{
				bool bIsReadOnly = false;

				try 
				{
					bIsReadOnly = Convert.ToBoolean(this.Value);
				} 
				catch { }

				return bIsReadOnly;
			}
		}

		/// <summary>
		/// Applies the client changes to the control's server properties.
		/// </summary>
		protected override void ApplyClientChanges()
		{
			if(IsReadOnly) 
			{
				base.ApplyClientChanges();
			} 
			else 
			{
				this.Container.Attributes.Remove(this.ClientPropertyName);
			}
		}
	}
}