using System;
using System.Web.UI;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persistence of an element's attribute.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public abstract class ClientPersistentAttribute : ClientPersistentProperty
	{
		/// <summary>
		/// Manages the persistence of an element's attribute.
		/// </summary>
		/// <param name=container>The element for which the property has to be persisted.</param>
		public ClientPersistentAttribute(System.Web.UI.WebControls.WebControl container) : base(container) { }

		/// <summary>
		/// Client attribute name used when rendering the control.
		/// </summary>
		public override abstract string ClientPropertyName 
		{
			get;
		}

		/// <summary>
		/// Manages the property's initialization during the Load stage.
		/// </summary>
		/// <param name=e></param>
		protected override void OnLoad(EventArgs e)
		{
			if(this.IsFirstInitialization) 
			{
				this.EnsureChildControls();

				this.Value = this.Container.Attributes[this.ClientPropertyName];
			}

			base.OnLoad (e);
		}

		/// <summary>
		/// Applies the client changes to the control's server properties.
		/// </summary>
		protected override void ApplyClientChanges()
		{
			this.Container.Attributes[this.ClientPropertyName] = this.Value;
		}

		/// <summary>
		/// Registers the client script responsible of copying the property's value
		/// to the hidden field before submitting the page.
		/// </summary>
		protected override void AssignUpdateScript()
		{
			string script;
			string updateFunctionName = "Update" + this.Container.ClientID + "_" + this.ClientPropertyName;

			script =  "<script language=\"javascript\">\n";
			script += "  try {\n";
			script += "    document.forms[0].attachEvent(\"onsubmit\", " + updateFunctionName + ");\n";
			script += "  } catch(e) { }\n";
			script += "  try {\n";
			script += "    document.forms[0].addEventListener(\"submit\", " + updateFunctionName + ", false);\n";
			script += "  } catch(e) { }\n";
			script += "  function " + updateFunctionName + "() {\n";
			script += "    document.getElementById(\"" + this.HiddenClientID + "\").value = document.getElementById(\"" + this.Container.ClientID + "\")." + this.ClientPropertyName + ";\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterStartupScript(updateFunctionName, script);
		}
	}
}