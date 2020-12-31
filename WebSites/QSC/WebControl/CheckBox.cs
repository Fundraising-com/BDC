using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QSP.WebControl.ClientPersistentProperties;

namespace QSP.WebControl
{
	/// <summary>
	/// That checkbox enable the persistant attribute
	/// </summary>
	public class CheckBox : System.Web.UI.WebControls.CheckBox, IClientPersistentPropertyContainer
	{
		private ClientPersistentPropertiesManagerControl clientPersistentPropertiesManagerControl = new ClientPersistentPropertiesManagerControl();

		public ClientPersistentPropertiesManagerControl ClientPersistentProperties 
		{
			get 
			{
				return clientPersistentPropertiesManagerControl;
			}
		}

		protected override void Render(HtmlTextWriter output)
		{	
			base.Render(output);
			ClientPersistentProperties.RenderControl(output);
		}
		protected override void OnInit(EventArgs e) 
		{
			ClientPersistentProperties.ID = this.ID + "_ClientPersistentProperties";
			Controls.Add(ClientPersistentProperties);
		}
	}
}
