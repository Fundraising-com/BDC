using System;
using System.Web.UI;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persistence of the "display" style of an element.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public class ClientDisplayStyle : ClientPersistentStyle
	{
		private const string CLIENT_STYLE = "display";

		/// <summary>
		/// Manages the persistence of the "display" style of an element.
		/// </summary>
		/// <param name=container>The element for which the property has to be persisted.</param>
		public ClientDisplayStyle(System.Web.UI.WebControls.WebControl container) : base(container) { }

		/// <summary>
		/// Client style name used when rendering the control.
		/// </summary>
		public override string ClientPropertyName
		{
			get
			{
				return CLIENT_STYLE;
			}
		}

		/// <summary>
		/// Indicates whether the control is being displayed on the client.
		/// </summary>
		public bool IsDisplayed 
		{
			get 
			{
				return (this.Value.ToLower() != "none");
			}
		}
	}
}