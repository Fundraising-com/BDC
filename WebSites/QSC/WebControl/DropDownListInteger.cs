using System;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for DropDownListInteger.
	/// </summary>
	public class DropDownListInteger : DropDownListReq
	{
		[Bindable(true),Category("SqlQuery"),DefaultValue(0)]
		public new int Value
		{
			get 
			{
				return Convert.ToInt32(SelectedValue);
			}
			set 
			{
				SelectedIndex = Items.IndexOf(Items.FindByValue(value.ToString()));
			}
		}
	}
}
