using System;
using System.Web.UI;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persistence of the "visibility" style of an element.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public class ClientVisibilityStyle : ClientPersistentStyle
	{
		private const string CLIENT_VISIBILITY_STYLE = "visibility";

		/// <summary>
		/// Manages the persistence of the "visibility" style of an element.
		/// </summary>
		/// <param name=container>The element for which the property has to be persisted.</param>
		public ClientVisibilityStyle(System.Web.UI.WebControls.WebControl container) : base(container) { }

		/// <summary>
		/// Client style name used when rendering the control.
		/// </summary>
		public override string ClientPropertyName
		{
			get
			{
				return CLIENT_VISIBILITY_STYLE;
			}
		}

		/// <summary>
		/// Indicates whether the control is visible on the client.
		/// </summary>
		public bool IsVisible
		{
			get 
			{
				bool bIsVisible = true;

				try 
				{
					if(this.Value == "hidden")
					{
						bIsVisible = false;
					}
				} 
				catch { }

				return bIsVisible;
			}
		}

		/// <summary>
		/// Applies the client changes to the control's server properties.
		/// </summary>
		protected override void ApplyClientChanges()
		{
			if(!IsVisible) 
			{
				base.ApplyClientChanges();
			} 
			else 
			{
				this.Container.Style.Remove(this.ClientPropertyName);
			}
		}
	}
}