using System;
using System.Web.UI;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persistence of the "disabled" attribute (Enabled state) of an element.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public class ClientEnabledAttribute : ClientPersistentAttribute
	{
		private const string CLIENT_ENABLED_ATTRIBUTE = "disabled";

		/// <summary>
		/// Manages the persistence of the "disabled" attribute (Enabled state) of an element.
		/// </summary>
		/// <param name=container>The element for which the property has to be persisted.</param>
		public ClientEnabledAttribute(System.Web.UI.WebControls.WebControl container) : base(container) { }

		/// <summary>
		/// Client style name used when rendering the control.
		/// </summary>
		public override string ClientPropertyName
		{
			get
			{
				return CLIENT_ENABLED_ATTRIBUTE;
			}
		}

		/// <summary>
		/// Indicates whether the control is enabled on the client.
		/// </summary>
		public bool IsEnabled 
		{
			get 
			{
				bool bIsEnabled = true;

				try 
				{
					bIsEnabled = !Convert.ToBoolean(this.Value);
				} 
				catch { }

				return bIsEnabled;
			}
		}

		/// <summary>
		/// Applies the client changes to the control's server properties.
		/// </summary>
		protected override void ApplyClientChanges()
		{
			if(!IsEnabled) 
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