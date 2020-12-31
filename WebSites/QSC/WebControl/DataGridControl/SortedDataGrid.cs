namespace QSP.WebControl.DataGridControl
{
	using System;
	using System.Web.UI.WebControls;
	using System.Web.UI;
	using System.ComponentModel;
	using System.Drawing;
	/// <summary>
	/// Summary description for SortedDataGrid.
	/// </summary>
	
	public class SortedDataGrid:DataGrid
	{
		private const string FILTER_EXPRESSION = "FilterExpression";
		private const string SORT_EXPRESSION = "DataSortExpression";
		private const string SEARCH_MODE = "SearchMode";
		private const string SEARCH_CRITERIA = "SearchCriteria";

		public SortedDataGrid():base()
		{
			this.SearchMode = 0;
			this.FilterExpression = "";
			this.SortExpression = "";
		}
		public virtual void SetDefaultSort(string SortExpression,string FilterExpression,int SearchMode,string Criteria)
		{
			if (ViewState[SORT_EXPRESSION] == null || (this.SortExpression == "" && this.SearchMode==0)) 
			{
				this.SortExpression = SortExpression;
				this.FilterExpression = FilterExpression;
				this.SearchMode = SearchMode;
				this.Criteria = Criteria;
				this.Criteria = "";
				
			}
		}
		public virtual void SetDefaultSort(string SortExpression,string FilterExpression,int SearchMode)
		{
			if (ViewState[SORT_EXPRESSION] == null || (this.SortExpression == "" && this.SearchMode==0)) 
			{
				this.SortExpression = SortExpression;
				this.FilterExpression = FilterExpression;
				this.SearchMode = SearchMode;
				this.Criteria = "";
				
			}
		}
		public virtual void SetFilterText(Label WebControl)
		{
					
			if (SearchMode > 0)
			{
					if (FilterExpression.Length == 0)
					{
						WebControl.Text = "No Filter";
					}
					else
					{
						WebControl.Text = "Filter " + Criteria + " containing \"" + this.FilterExpression+"\"";
					}
				
				
			}
			else
			{
				if (FilterExpression == "%" || FilterExpression == String.Empty)
				{
					WebControl.Text = "No Filter";
				}
				else if(FilterExpression == "#")
				{
					WebControl.Text = "Filter " + Criteria + " beginning with special characters or numbers";
				}
				else
				{
					WebControl.Text = "Filter " + Criteria + " beginning with " + "\""+this.FilterExpression+"\"";
				}
			}
		}

		public virtual void SetSortText(Label WebControl)
		{
			String sSortExp = this.SortExpression;
			if (sSortExp.Length > 0)
			{
				foreach(DataGridColumn dc in Columns)
				{
					if(dc.SortExpression !="")
						if (sSortExp.IndexOf(dc.SortExpression) >=0)
						{
						 
							WebControl.Text = "Sort by " + dc.HeaderText;
							break;
						}
				}
					
			}
			else
			{
				WebControl.Text = "No Sort";
			}

		}
		public virtual void SetPageIndexText(Label WebControl)
		{
			if (Items.Count == 0)
			{
				WebControl.Text = "No results have been found";				
			}
			else
			{
				WebControl.Text = "Page " + (CurrentPageIndex + 1) + " of " + PageCount;
			}
		}
		public virtual void CreateSortHeader(DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				//String sSort = ;				
					
				if (SortExpression != null)
				{
					int iIndex = 0;
					bool IsFound = false;
					string sSort="";
					string Order = GetOrder(SortExpression);
					string tempExpression = CleanExpression(this.SortExpression);
					string[] sSorts = tempExpression.Split(',');
					foreach(DataGridColumn dgCol in Columns)
					{
						if (dgCol.SortExpression.Length > 0)
						{
							foreach(string Sort in sSorts)
							{	sSort = Sort.TrimEnd(' ');
								sSort = Sort.TrimStart(' ');
								if (sSort.Equals(dgCol.SortExpression) || tempExpression.Replace(" ", "").Equals(dgCol.SortExpression.Replace(" ","")))
								{
									IsFound = true;
									break;						
								}
							}
						}
						if (IsFound)
						{
							Label arrow = new Label();
							arrow.Font.Name = "webdings";
							if ((Order.IndexOf("ASC") >= 0))
							{
								arrow.Text = "6";
								IsFound = false;
							}
							else
							{
								arrow.Text = "5";
								IsFound = false;
							}
						
							e.Item.Cells[iIndex].Controls.Add(arrow);
							

						}

						iIndex = iIndex +1;
					}
						
						
				}

			}
		}
		private String GetOrder(string SortExpression)
		{
			if(SortExpression != "")
			{
				string tempSort = SortExpression.TrimEnd(' ');
				string Order= tempSort.Substring(tempSort.Length-3,3);
				if(Order =="ASC")
					return Order;
			
				else
					return "DESC";
			}
			return "DESC";
			
		}
		private string CleanExpression(string SortExpression)
		{
			string tempSort = SortExpression.Replace("ASC","");
			tempSort = tempSort.Replace("DESC","");
			tempSort = tempSort.TrimEnd(' ');
			return tempSort;
		}
		//[Bindable(false), Category("Sort"), DefaultValue("")]
		public string FilterExpression
		{
			get{return ViewState[FILTER_EXPRESSION].ToString();}
			set{ViewState[FILTER_EXPRESSION] = value;}
		}
		//[Bindable(false), Category("Sort"), DefaultValue("")]
		public string SortExpression
		{
			get{return ViewState[SORT_EXPRESSION].ToString();}
			set{ViewState[SORT_EXPRESSION] = value;}
		}
		//[Bindable(false), Category("Sort"), DefaultValue(0)]
		public int SearchMode
		{
			get{return Convert.ToInt32(ViewState[SEARCH_MODE]);}
			set{ViewState[SEARCH_MODE] = value;}
		}
		public string Criteria
		{
			get
			{
				try
				{
					return ViewState[SEARCH_CRITERIA].ToString();
				}
				catch
				{
					return "";
				}
			}
			set{ViewState[SEARCH_CRITERIA] = value;}
		}
		public void EnsureVisibility(int Index)
		{

			if(Index >=0)
			{
				
				CurrentPageIndex = (Index /PageSize);
			}
		}
		
		
		 
	}
}
