using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GA.BDC.WEB.ScratchcardWeb.Components.Server.UIFactories
{
	public class PanelUtilities
	{
		internal static void SetPanelVisibility(string PanelToHide, string PanelToShow,
			System.Web.UI.Control ControlSource) 
		{
			System.Web.UI.Control oCtrl = ControlSource.FindControl(PanelToHide);
			oCtrl.Visible = false;
			oCtrl = ControlSource.FindControl(PanelToShow);
			oCtrl.Visible = true;
		}

		internal static void SetParentPanelVisibility(string PanelToHide, string PanelToShow,
			System.Web.UI.Control ControlSource) 
		{
			System.Web.UI.Control oCtrl = ControlSource;
			while(oCtrl.ID != PanelToHide)
				oCtrl = oCtrl.Parent;
			oCtrl.Visible = false;
			Panel oPnl = (System.Web.UI.WebControls.Panel)oCtrl.Parent.FindControl(PanelToShow);
			oPnl.Visible = true;
		}

		internal static string FindFullControlNameOf(string pCtrlName, Control containerControl) 
		{
			System.Web.UI.Control oCtrl = containerControl.FindControl(pCtrlName);
			if(oCtrl is GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl) 
			{
				GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl oBp = 
					(GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl)oCtrl;
				switch(oBp.ButtonType) 
				{
					case GA.BDC.Core.Web.UI.UIControls.BUTTON_TYPE.IMAGE:
						return FindFullImageControlNameOf(pCtrlName, containerControl);	
						break;
					case GA.BDC.Core.Web.UI.UIControls.BUTTON_TYPE.BUTTON:
					case GA.BDC.Core.Web.UI.UIControls.BUTTON_TYPE.LINK:
						break;
				}			
			} 
			else if(oCtrl is ImageButton)
				return FindFullImageControlNameOf(pCtrlName, containerControl);
			string oFullName = oCtrl.ID;
			while(oCtrl.Parent != null) 
			{
				if(oCtrl.Parent is System.Web.UI.UserControl)
					oFullName = oCtrl.Parent.ID + "_" + oFullName;
				oCtrl = oCtrl.Parent;
			}		
			return oFullName;
		}

		private static string FindFullImageControlNameOf(string pCtrlName, Control containerControl) 
		{
			System.Web.UI.Control oCtrl = containerControl.FindControl(pCtrlName);
			string oFullName = oCtrl.ID;			
			oFullName = oFullName + ":_ctl0";
			while(oCtrl.Parent != null) 
			{
				if(oCtrl.Parent is System.Web.UI.UserControl)
					oFullName = oCtrl.Parent.ID + ":" + oFullName;
				oCtrl = oCtrl.Parent;
			}
			return oFullName;
		}

		internal static string FindFullControlName(Control containerControl) 
		{
			System.Web.UI.Control container = containerControl;
			string fullName = container.ID;
			while(container.Parent != null) 
			{
				if(container.Parent is System.Web.UI.UserControl)
					fullName = container.Parent.ID + "_" + fullName;
				container = container.Parent;
			}
			return fullName;
		}
	}
}
