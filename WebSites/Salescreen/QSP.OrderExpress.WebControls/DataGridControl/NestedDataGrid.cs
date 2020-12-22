namespace QSP.WebControl.DataGridControl
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Data;
	using System.Drawing;
	using System.Collections;
	public delegate void OnSelectDGClick(object sender,DataGridCommandEventArgs e);
	
	/// <summary>
	///		Summary description for NestedDataGrid.
	/// </summary>
	public class NestedDataGrid : DataGrid
	{

		public Unit HostColumnWidth;
		private DataGrid detailsGrid;
		private bool bColSelect;
		public event OnSelectDGClick SelectDGClick;
		private bool bRefreshOnClickSelect;
		private bool bAllowPagingDetailGrid;
	

		// **********************************************************
		// Gets or sets the item to render expanded
		public int ExpandedItem
		{
			get {return Convert.ToInt32(ViewState["ExpandedItem"]);}
			set {ViewState["ExpandedItem"] = value;}
		}
		// **********************************************************

		// **********************************************************
		// Gets or sets whether the child grid is scrollable or pageable
		public bool ScrollChildren
		{
			get {return Convert.ToBoolean(ViewState["ScrollChildren"]);}
			set {ViewState["ScrollChildren"] = value;}
		}
		// **********************************************************

		// **********************************************************
		// Gets or sets the name of the DataSet's relation to use to fill the subgrid
		public string RelationName
		{
			get {return Convert.ToString(ViewState["RelationName"]);}
			set {ViewState["RelationName"] = value;}
		}
		// **********************************************************



		// **********************************************************
		// Fire the UpdateView event to the page for binding
		public event EventHandler UpdateView;
		private void OnUpdateView()
		{
			if (UpdateView != null)
				UpdateView(this, EventArgs.Empty); 
		}
		// **********************************************************

		// **********************************************************
		// Public ctor
		public NestedDataGrid() : base()
		{
			ExpandedItem = -1;
			HostColumnWidth = Unit.Pixel(150);
			ScrollChildren = true;

			AllowPaging = true;
			PageIndexChanged += new DataGridPageChangedEventHandler(NestedDataGrid_PageIndexChanged);
			ItemCommand += new DataGridCommandEventHandler(NestedDataGrid_ItemCommand);
			ItemDataBound += new DataGridItemEventHandler(NestedDataGrid_ItemDataBound);
		}
		// **********************************************************

		// **********************************************************
		// Page change handler
		private void NestedDataGrid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
		{
			CurrentPageIndex = e.NewPageIndex;
			SelectedIndex = -1;
			EditItemIndex = -1;
			ExpandedItem = -1;

			OnUpdateView();
		}
		// **********************************************************

		// **********************************************************
		// Command handler
		private void NestedDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.CommandName != "Expand" && e.CommandName != "Collapsed")
				return;

			ExpandItem(e.Item);
		}
		// **********************************************************

		// **********************************************************
		// Adjust the index of the expanded item
		protected void ExpandItem(DataGridItem item)
		{
			if (item.ItemIndex == (ExpandedItem % this.PageSize))
				SetExpandedItem(item, false);
			else
				SetExpandedItem(item, true);

			OnUpdateView();
		}
		// **********************************************************

		// **********************************************************
		// Adjust the index of the expanded item
		private void SetExpandedItem(DataGridItem item, bool expand)
		{
			if (expand)
				ExpandedItem = (this.PageSize*this.CurrentPageIndex+item.ItemIndex); 
			else
				ExpandedItem = -1;
		}
		// **********************************************************

		// **********************************************************
		// Opens the subtree and shows the related records
		protected override void InitializeItem(DataGridItem item, DataGridColumn[] columns)
		{
			for (int i=0; i<columns.Length; i++)
			{
				TableCell cell = new TableCell();
				if (columns[i] is ExpandCommandColumn)
					((ExpandCommandColumn)columns[i]).InitializeCell(cell, i, item.ItemType, (item.ItemIndex==(ExpandedItem % this.PageSize)));
				else
					columns[i].InitializeCell(cell, i, item.ItemType);
				item.Cells.Add(cell);
			}
		}
		// **********************************************************

		// **********************************************************
		// Modify the layout of the cell being expanded
		private void NestedDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			// Process only items and alternating items
			if (e.Item.ItemType != ListItemType.Item &&
				e.Item.ItemType != ListItemType.AlternatingItem)
				return;

			// Default if the item doesn't have to be expanded
			if (e.Item.ItemIndex != (ExpandedItem % this.PageSize))
			{
				// Instead of itemstyle-width set declaratively
				e.Item.Cells[1].Width = HostColumnWidth;

				return;
			}

			// Build the subtree
			BuildChildLayout(e.Item);
		}
		// **********************************************************

		// **********************************************************
		// Modify the layout of the cell being expanded
		public virtual void BuildChildLayout(DataGridItem item)
		{
			DataGridItem row = item;

			// Assumes the Expand column is the first

			// Remove all cells	but one
			int cellsToSpanOver = row.Cells.Count-1;
			ArrayList listOfText = new ArrayList();
			ArrayList listOfWidth = new ArrayList();
			for (int i=row.Cells.Count-1; i>0; i--)
			{
				listOfText.Add(row.Cells[i].Text);
				if (i==1) // Add the width of the column whose width is not declared
					listOfWidth.Add(HostColumnWidth);
				else
					listOfWidth.Add(this.Columns[i].ItemStyle.Width);
					row.Cells.RemoveAt(i);
			}

			// Add the new cell that will host the child grid
			TableCell newCell = new TableCell();
			newCell.ColumnSpan = cellsToSpanOver;
			newCell.BackColor = Color.SkyBlue;

			// MUST BE empty. If you set a fixed width declaratively that value
			// will override this one. For this reason, we set the width of the 
			// first column after the EXPAND column dynamically. We also assume
			// that the first column after the EXPAND column is the host cell, where
			// the child grid is inserted.
			newCell.Width = Unit.Empty;
			row.Cells.Add(newCell);
			
			// The child layout is made of a 2-row table: header (same as the 
			// previous unexpanded row) and the subgrid
			Table t = new Table();
			t.Font.Name = this.Font.Name;
			t.Font.Size = this.Font.Size;
			t.CellSpacing = this.CellSpacing;
			t.CellPadding = this.CellSpacing;
			t.BorderWidth = this.BorderWidth;

			TableRow rowHeader = new TableRow();
			t.Rows.Add(rowHeader);
			TableRow rowSubGrid = new TableRow();
			t.Rows.Add(rowSubGrid);
			newCell.Controls.Add(t);

			// Fill the header row
			for (int i=listOfText.Count-1; i>=0; i--)
			{
				TableCell c = new TableCell();
				c.Text = listOfText[i].ToString();
				c.Width = (Unit) listOfWidth[i];
				rowHeader.Cells.Add(c);
			}
		
			
			// Fill the second row
			Panel outerPanel = null;
			if (ScrollChildren)
			{
				outerPanel = new Panel();
				outerPanel.Height = Unit.Pixel(100);
				outerPanel.Style["overflow"] = "auto"; 
			}

			TableCell cellSubGrid = new TableCell();
			cellSubGrid.ColumnSpan = cellsToSpanOver;
			cellSubGrid.BackColor = Color.LightCyan;
			rowSubGrid.Cells.Add(cellSubGrid);

			detailsGrid = new DataGrid();
			detailsGrid.ID = "detailsGrid";
			detailsGrid.BackColor = Color.LightCyan;
			detailsGrid.Font.Name = this.Font.Name;
			detailsGrid.Font.Size = this.Font.Size;
			detailsGrid.HeaderStyle.Font.Bold = true; 
			detailsGrid.Width = Unit.Percentage(100);

			if (!ScrollChildren)
			{
				detailsGrid.AllowPaging = bAllowPagingDetailGrid;
				if(bAllowPagingDetailGrid)
				{
					detailsGrid.PageSize = 5;
					detailsGrid.PagerStyle.Mode = PagerMode.NumericPages;
					detailsGrid.PageIndexChanged += new DataGridPageChangedEventHandler(detailsGrid_PageIndexChanged);
				}
				this.detailsGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.detailsGrid_ItemDataBound);
				
			}
			BindDetails(detailsGrid);

			if (ScrollChildren)
			{
				outerPanel.Controls.Add(detailsGrid);
				cellSubGrid.Controls.Add(outerPanel);
			}
			else
			{
				cellSubGrid.Controls.Add(detailsGrid);
			}
		}		
		// **********************************************************

		// **********************************************************
		// Bind the child view to the subgrid
		private void BindDetails(DataGrid detailsGrid)
		{
			DataSet ds = (DataSource as DataSet);
			if (ds == null)
				return;

			DataTable dt = ds.Tables[this.DataMember];
			DataView theView = new DataView(dt);
			DataRowView drv = theView[ExpandedItem];	 
			DataView detailsView = drv.CreateChildView(this.RelationName);
			
			
			
			detailsGrid.DataSource = detailsView;
			
			if(bColSelect)
			{
				ButtonColumn ss = new ButtonColumn();
				ss.ButtonType = ButtonColumnType.LinkButton;
				ss.Text = "Select";
				ss.CommandName ="SelectDG";
				
				this.detailsGrid.Columns.Add(ss);
				this.detailsGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.detailsGrid_ItemCommand);
			}
			
			detailsGrid.DataBind();
		}
		
		private void detailsGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)&& bRefreshOnClickSelect) 
			{
				((LinkButton)(e.Item.Controls[0].Controls[0])).Attributes.Add("onClick","javascript:window.opener.location.reload(true);window.opener.focus();");
			}
		}
		// **********************************************************

		// **********************************************************
		// Takes care of paging the child grid
		private void detailsGrid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
		{
			this.detailsGrid = (DataGrid) sender;
			detailsGrid.CurrentPageIndex = e.NewPageIndex;
			AsSelectedColumn = false;
			//Page.Trace.Warn("Child grid page: " + detGrid.CurrentPageIndex.ToString());
			BindDetails(detailsGrid);

			
		}
		// **********************************************************

		public bool AsSelectedColumn
		{
			get{return bColSelect;}
			set{bColSelect = value;}
		}

		private void detailsGrid_ItemCommand(object sender,System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//todo:dont work completly when you are in other page then the first page
			if(e.CommandName == "SelectDG")
			{
				FireEventSelectClicked(sender,e);
				
			}
			
		}

		private void FireEventSelectClicked(object sender,System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(SelectDGClick!=null)
			{
				
				SelectDGClick(sender,e);
			}
				
		}

		public bool RefreshOnClickSelect
		{
			get{return bRefreshOnClickSelect;}
			set{bRefreshOnClickSelect=value;}

		}

		public bool AllowPagingDetailGrid
		{
			get{return bAllowPagingDetailGrid;}
			set{bAllowPagingDetailGrid = value;}
		}

	}
}

