using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persistence of an element's property.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public abstract class ClientPersistentProperty : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		private System.Web.UI.WebControls.WebControl container;
		private System.Web.UI.HtmlControls.HtmlInputHidden hidProperty;

		/// <summary>
		/// Manages the persistence of an element's property.
		/// </summary>
		/// <param name=container>The element for which the property has to be persisted.</param>
		/// 		
		public ClientPersistentProperty(System.Web.UI.WebControls.WebControl container)
		{
			this.container = container;
		}

		/// <summary>
		/// The element for which the property has to be persisted.
		/// </summary>
		protected System.Web.UI.WebControls.WebControl Container 
		{
			get 
			{
				return container;
			}
		}

		/// <summary>
		/// Client ID of the hidden field holding the property's value.
		/// </summary>
		protected string HiddenClientID
		{
			get 
			{
				return hidProperty.ClientID;
			}
		}

		/// <summary>
		/// Client property name used when rendering the control.
		/// </summary>
		public abstract string ClientPropertyName 
		{
			get;
		}

		/// <summary>
		/// Current value of the property held in the hidden field.
		/// </summary>
		public string Value 
		{
			get 
			{
				return hidProperty.Value;
			}
			set 
			{
				hidProperty.Value = value;
			}
		}

		/// <summary>
		/// Indicates through the whole page processing flow whether the control is
		/// initialized for the first time.
		/// </summary>
		/// <remarks>
		/// IsPostBack wouldn't have worked because of dynamic controls.
		/// </remarks>
		public bool IsFirstInitialization 
		{
			get 
			{
				if(this.ViewState["IsFirstInitialization"] == null)
					this.ViewState["IsFirstInitialization"] = true;

				return Convert.ToBoolean(this.ViewState["IsFirstInitialization"]);
			}
		}

		/// <summary>
		/// Manages the hidden field.
		/// </summary>
		protected override void CreateChildControls()
		{
			base.CreateChildControls ();

			hidProperty = new System.Web.UI.HtmlControls.HtmlInputHidden();
			hidProperty.ID = "hidProperty";

			Controls.Add(hidProperty);
		}


		/// <summary>
		/// Manages the hidden field.
		/// </summary>
		/// <param name=e></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			this.EnsureChildControls();

			ApplyClientChanges();
		}

		/// <summary>
		/// Manages the hidden field.
		/// </summary>
		/// <param name=e></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			AssignUpdateScript();

			SetFirstInitialization(false);
		}

		/// <summary>
		/// Set accessor for IsFirstInitialization.
		/// </summary>
		/// <param name=value>Value to be assigned.</param>
		private void SetFirstInitialization(bool value) 
		{
			this.ViewState["IsFirstInitialization"] = value;
		}

		/// <summary>
		/// Applies the client changes to the control's server properties.
		/// </summary>
		protected abstract void ApplyClientChanges();

		/// <summary>
		/// Registers the client script responsible of copying the property's value
		/// to the hidden field before submitting the page.
		/// </summary>
		protected abstract void AssignUpdateScript();
	}
}