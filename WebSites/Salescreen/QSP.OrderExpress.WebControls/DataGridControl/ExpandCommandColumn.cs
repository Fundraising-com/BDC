using System;
using System.Web;
using System.Web.UI.WebControls;

namespace QSP.WebControl.DataGridControl
{
	/// <summary>
	/// Summary description for ExpandCommandColumn.
	/// </summary>
	public class ExpandCommandColumn: DataGridColumn
	{
			public const string COMMANDNAMEEXPAND ="Expand";
			public const string COMMANDNAMECOLLAPSED = "Collapsed";
			public ExpandCommandColumn()
			{
				ExpandText = "+";
				CollapseText = "-";
			}


			/// <summary>
			/// Gets or sets the text for the item used to expand
			/// </summary>
 			public string ExpandText 
			{
				get {return Convert.ToString(ViewState["ExpandText"]);}
				set {ViewState["ExpandText"] = value;}
			}
			/// <summary>
			/// Gets or sets the text for the item used to collapse
			/// </summary>
			public string CollapseText 
			{
				get {return Convert.ToString(ViewState["CollapseText"]);}
				set {ViewState["CollapseText"] = value;}
			}
			/// <summary>
			///  Initializes the cells in the column
			/// </summary>
			/// <param name="cell"></param>
			/// <param name="columnIndex"></param>
			/// <param name="itemType"></param>
			public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType) 
			{
				base.InitializeCell(cell, columnIndex, itemType);
			}

			public void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType, bool collapsed) 
			{
				base.InitializeCell(cell, columnIndex, itemType);

				if (itemType == ListItemType.Item ||
					itemType == ListItemType.AlternatingItem) 
				{
					LinkButton link = new LinkButton();
					

					if (collapsed)
					{
						link.Text = this.CollapseText;
						link.CommandName = "Collapsed";
					}
					else
					{
						link.Text = this.ExpandText;
						link.CommandName = "Expand";
					}
					link.CausesValidation = false;
					cell.Controls.Add(link);
				}
			}
		
		}
	}