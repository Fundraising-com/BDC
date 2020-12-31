using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing;

namespace skmMenu
{
	namespace Design 
	{
		/// <summary>
		/// MenuDesigner is the Designer class for the Menu control.  This class contains methods
		/// used by the VS.NET IDE to provide a rich experience in the VS.NET Designer.
		/// </summary>
		public class MenuDesigner : ControlDesigner
		{
			private Menu menuInstance;

			///<summary></summary>
			public MenuDesigner() : base()
			{				
			}

			///<summary></summary>
			public override void Initialize(System.ComponentModel.IComponent component)
			{
				this.menuInstance = (Menu) component;
				base.Initialize (component);
			}

			///<summary></summary>
			public override string GetDesignTimeHtml() 
			{
				StringWriter sw = new StringWriter();
				HtmlTextWriter writer = new HtmlTextWriter(sw);

				Table menu = new Table();				
				menu.CellSpacing = menuInstance.ItemSpacing;
				menu.CellPadding = menuInstance.ItemPadding;
				
				// Display the Menu based on its specified Layout
				if (menuInstance.Layout == MenuLayout.Vertical)
				{
					for (int i = 1; i <= 5; i++)
					{
						TableRow tr = new TableRow();
						TableCell td = new TableCell();
						td.ApplyStyle(menuInstance.UnselectedMenuItemStyle);
						// The style is overwritten by anything specifically set in menuitem
						if (menuInstance.BackColor != Color.Empty) 
							td.BackColor = menuInstance.BackColor;
						if (menuInstance.Font != null)
							td.Font.CopyFrom(menuInstance.Font);
						if (menuInstance.ForeColor != Color.Empty)
							td.ForeColor = menuInstance.ForeColor;
						if (menuInstance.Height != Unit.Empty)
							td.Height = menuInstance.Height;
						if (menuInstance.Width != Unit.Empty)
							td.Width = menuInstance.Width;
						if (menuInstance.CssClass != String.Empty)
							td.CssClass = menuInstance.CssClass;
						else if	(menuInstance.DefaultCssClass != String.Empty)
							td.CssClass = menuInstance.DefaultCssClass;
						if (menuInstance.BorderColor != Color.Empty)
							td.BorderColor = menuInstance.BorderColor;
						if (menuInstance.BorderStyle != BorderStyle.NotSet)
							td.BorderStyle = menuInstance.BorderStyle;
						if (menuInstance.BorderWidth != Unit.Empty)
							td.BorderWidth = menuInstance.BorderWidth;						
						td.Text = "Menu Item " + i.ToString();
						tr.Cells.Add(td);
						menu.Rows.Add(tr);
					}
				}
				else
				{
					TableRow tr = new TableRow();
					for (int i = 1; i <= 5; i++)
					{						
						TableCell td = new TableCell();
						td.ApplyStyle(menuInstance.UnselectedMenuItemStyle);
						// The style is overwritten by anything specifically set in menuitem
						if (menuInstance.BackColor != Color.Empty) 
							td.BackColor = menuInstance.BackColor;
						if (menuInstance.Font != null)
							td.Font.CopyFrom(menuInstance.Font);
						if (menuInstance.ForeColor != Color.Empty)
							td.ForeColor = menuInstance.ForeColor;
						if (menuInstance.Height != Unit.Empty)
							td.Height = menuInstance.Height;
						if (menuInstance.Width != Unit.Empty)
							td.Width = menuInstance.Width;
						if (menuInstance.CssClass != String.Empty)
							td.CssClass = menuInstance.CssClass;
						else if	(menuInstance.DefaultCssClass != String.Empty)
							td.CssClass = menuInstance.DefaultCssClass;
						if (menuInstance.BorderColor != Color.Empty)
							td.BorderColor = menuInstance.BorderColor;
						if (menuInstance.BorderStyle != BorderStyle.NotSet)
							td.BorderStyle = menuInstance.BorderStyle;
						if (menuInstance.BorderWidth != Unit.Empty)
							td.BorderWidth = menuInstance.BorderWidth;
						td.Text = "Menu Item " + i.ToString();
						tr.Cells.Add(td);						
					}
					menu.Rows.Add(tr);
				}

				menu.RenderControl(writer);
				return sw.ToString();
			}

		}
	}
}
