using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;


namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	///		Summary description for DataSetToHtmlReport.
	/// </summary>
	public class DataSetToHtmlReport //: System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Table ReportTable;
		
		//Default table formating
		private int borderWidth = 0;
		private string borderColor = "#FFFFFF";
		private int cellSpacing = 0;
		private int cellPadding = 0;
		private string cssClass = "NormalText";
		private string headerColor = "#B0B0B0";
		private bool headerFontBold = true;
		private string bodyColorLight = "#F0F0F0";
		private string bodyColorDark = "#D8D8D8";
		private string footerColor = "#D0D0D0";
		private bool footerFontBold = true;
		private double grandTotal;
		private string culture = "en-US";
		

		public DataSetToHtmlReport()
		{
			culture = "en-US";
		}

		//Check of DataSet schema has a valid format
		private void Validate(DataSet dataset)
		{
			if ( dataset.Tables.Count != 2 ) {
				throw new Exception("DataSet does not contain 2 tables!");
			}
		}


		//Set css etc
		private void PreTransform(int BorderWidth, string BorderColor, int CellSpacing, int CellPadding, string CssClass,
			string HeaderColor, bool HeaderFontBold, string BodyColorLight, string BodyColorDark, string FooterColor, bool FooterFontBold)
		{
			borderWidth = BorderWidth;
			borderColor = BorderColor;
			cellSpacing = CellSpacing;
			cellPadding = CellPadding;
			cssClass = CssClass;
			headerColor = HeaderColor;
			headerFontBold = HeaderFontBold;
			bodyColorLight = BodyColorLight;
			bodyColorDark = BodyColorDark;
			footerColor = FooterColor;
			footerFontBold = FooterFontBold;
		}



		//Set HTML properties
		private void PostTransform(){}

		// Set Cell Style for HTML Table

		private void SetCellStyle(TableCell cell, string styleFormat)
		{
			// styleFormat must be in form width: 100; height: 20
			string[] style = styleFormat.Split(';');
			for (int i=0; i < style.Length;i++)
			{
				string[] detailStyle = style[i].Split(':');
				if (detailStyle.Length > 1)
				{
					string theStyle = detailStyle[0].ToUpper();
					switch (theStyle)
					{
						case "WIDTH":
							cell.Width = new Unit(detailStyle[1]);
							break;
						case "HEIGHT":
							cell.Height = new Unit(detailStyle[1]);
							break;
					}
				}
			}
		}


        //Parses DataSet object to a asp.Table object
		public Table Transform(DataSet ds)
		{
			Validate(ds); //Validate if DataSet schema has correct format
		
			ReportTable = new Table();
            
			//appends a table to DataSet for calculations of Totals (3rd table)
			ds.Tables.Add(new DataTable());
			for(int i=0; i < ds.Tables[0].Columns.Count; i++){
				ds.Tables[2].Columns.Add(new DataColumn());	
			}
			ds.Tables[2].Rows.Add(ds.Tables[2].NewRow());
			
			//initialize variables for ReportTable
            TableRow row = new TableRow();
			TableCell cell = new TableCell();
			int tableCount = 0;
			int rowCount = 0;
			int colCount = 0;

			ArrayList total = new ArrayList(ds.Tables[0].Columns.Count);
			ArrayList totalCalculationType = new ArrayList(ds.Tables[0].Columns.Count);
			ArrayList totalCalculationFunction = new ArrayList(ds.Tables[0].Columns.Count);
			grandTotal = new double();
			grandTotal = 0.0;

			// set ReportTable formating
			ReportTable.BorderWidth = borderWidth;
			ReportTable.BorderColor = System.Drawing.ColorTranslator.FromHtml(borderColor);
			ReportTable.CellSpacing = cellSpacing;
			ReportTable.CellPadding = cellPadding;
			ReportTable.CssClass = cssClass;
			ReportTable.Width = Unit.Percentage(100);
			ReportTable.Font.Name = "Arial";
			ReportTable.Font.Size = 8;
			
			System.Threading.Thread.CurrentThread.CurrentCulture = 
				System.Globalization.CultureInfo.CreateSpecificCulture(culture);
			IFormatProvider currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;

			foreach(DataTable dt in ds.Tables)
			{
				foreach(DataRow dr in dt.Rows)
				{
					foreach(DataColumn dc in dt.Columns)
					{
						cell.HorizontalAlign = HorizontalAlign.Center;
						// Format Columns
						string[] colStyle = dc.ColumnName.Split('|');
						if (colStyle.Length >1)
						{
							SetCellStyle(cell, colStyle[1]);
						}
						//for header row
						if(tableCount == 0) {
							//get type of calculations for total (footer)
							if(dr[dc].ToString().StartsWith("-") || dr[dc].ToString().StartsWith("+") || dr[dc].ToString().StartsWith("$") || dr[dc].ToString().StartsWith("%")){
								totalCalculationType.Add(dr[dc].ToString().Substring(0,1));
								
								// can add formulas like +{TOTAL=1/3} which means the total will be
								// calculated from the other totals

								if(dr[dc].ToString().IndexOf("{") > 0) {
									int start = dr[dc].ToString().IndexOf("{");
									int end = dr[dc].ToString().IndexOf("}");
									string function = dr[dc].ToString().Substring(start, end - start + 1);
									totalCalculationFunction.Add(function);
									dr[dc] = dr[dc].ToString().Replace(function, "");
								} else {
									totalCalculationFunction.Add("");
								}
								
								dr[dc] = dr[dc].ToString().Remove(0,1);
								if(totalCalculationType[colCount].ToString() == "-"){
									total.Add(new int());
								}
								else if(totalCalculationType[colCount].ToString() == "+"){
									total.Add(new int());
									total[colCount]= 0;
								}
								else if(totalCalculationType[colCount].ToString() == "$"){
									total.Add(new double());
									total[colCount]= 0.0;
								}
								else if(totalCalculationType[colCount].ToString() == "%"){
									total.Add(new int());
									total[colCount]= 0;
								}
							}
							else{
								totalCalculationType.Add("_");
								totalCalculationFunction.Add("");
								total.Add("");
							}
							cell.Text = dr[dc].ToString();
						}
						// for body rows
						else if(tableCount == 1){
							switch(totalCalculationType[colCount].ToString())
							{
								case "-":
									total[colCount] = rowCount;
									cell.Text = dr[dc].ToString();
									break;
								case "+":
									total[colCount] = (int)total[colCount] + int.Parse(dr[dc].ToString());
									cell.Text = dr[dc].ToString();
									break;
								case "$":
									total[colCount] = (double)total[colCount] + (dr[dc].ToString() == ""? 0 : double.Parse(dr[dc].ToString()));
									dr[dc] = (dr[dc].ToString() == ""? 0 : double.Parse(dr[dc].ToString()));
									//cell.Text = double.Parse(dr[dc].ToString()).ToString("$###,###,###,##0.00");
									cell.Text = Convert.ToDouble(dr[dc].ToString()).ToString("C",currentCulture);
									break;
								case "%":
//									if(double.Parse(dr[dc].ToString()) <= 1.0){
                                        dr[dc] = double.Parse(dr[dc].ToString());
										cell.Text = double.Parse(dr[dc].ToString()).ToString("###.00%");
//									}
//									else{
//										throw new Exception("Invalid % value in column " + colCount);
//									}
									break;
								default:
									cell.Text = dr[dc].ToString();
									break;
							}
						}
						//for footer row
						else if(tableCount == 2){
							bool showFooter = true;
							if(totalCalculationFunction[colCount].ToString().StartsWith("{TOTAL=")) {
								string function = totalCalculationFunction[colCount].ToString();
								int start = function.IndexOf("=");
								int end = function.IndexOf("}");
								string calculation = function.Substring(start + 1);
								calculation = calculation.Substring(0, calculation.Length - 1);
								
								if(calculation.IndexOf("+") > 0) {
									string[] vals = calculation.Split('+');
									int val1 = int.Parse(vals[0]);
									int val2 = int.Parse(vals[1]);
									total[colCount] = (Double.Parse(total[val1 - 1].ToString()) + 
										(Double.Parse(total[val2 - 1].ToString())));
								} else if(calculation.IndexOf("-") > 0) {
									string[] vals = calculation.Split('-');
									int val1 = int.Parse(vals[0]);
									int val2 = int.Parse(vals[1]);
									total[colCount] = (Double.Parse(total[val1 - 1].ToString()) - 
										(Double.Parse(total[val2 - 1].ToString())));
								} else if(calculation.IndexOf("*") > 0) {
									string[] vals = calculation.Split('*');
									int val1 = int.Parse(vals[0]);
									int val2 = int.Parse(vals[1]);
									total[colCount] = (Double.Parse(total[val1 - 1].ToString()) * 
										(Double.Parse(total[val2 - 1].ToString())));
								} else if(calculation.IndexOf("/") > 0) {
									string[] vals = calculation.Split('/');
									int val1 = int.Parse(vals[0]);
									int val2 = int.Parse(vals[1]);
									total[colCount] = (Double.Parse(total[val1 - 1].ToString()) / 
										(Double.Parse(total[val2 - 1].ToString())));
								}
							} else if(totalCalculationFunction[colCount].ToString().StartsWith("{TOTAL=")) {
								if(!totalCalculationFunction[colCount].ToString().ToUpper().EndsWith("TRUE}")) {
									showFooter = false;
								}
							}
							
							if(totalCalculationType[colCount].Equals("$")){
								//cell.Text = ((Double)total[colCount]).ToString("$###,###,###,##0.00");
								cell.Text = Convert.ToDouble(total[colCount].ToString()).ToString("C",currentCulture);
								//store total in grandTotal and assign ID to the last $ cell of footer row
								if(colCount == totalCalculationType.LastIndexOf("$")){
									grandTotal = Convert.ToDouble(total[colCount].ToString());
									//cell.ID = "Total";
								}
							}
							else if(totalCalculationType[colCount].Equals("%")){
								//cell.Text = "";	
								cell.Text = Double.Parse(total[colCount].ToString()).ToString("%###,###,###,##0.00");
							}
							else{
								cell.Text = total[colCount].ToString();	
							}

							if(!showFooter) {
								cell.Text = "";
							}
						}
						//for all rows
						row.Cells.Add(cell);	
						cell = new TableCell();
						colCount++;
					}
					colCount = 0;
					if(rowCount == 0) {
						row.BackColor = System.Drawing.ColorTranslator.FromHtml(headerColor);
						row.Font.Bold = headerFontBold;
					}
					else if(rowCount % 2 == 0 && tableCount == 1){
						row.BackColor = System.Drawing.ColorTranslator.FromHtml(bodyColorLight);
					}
					else if(tableCount == 2) {
						row.BackColor = System.Drawing.ColorTranslator.FromHtml(footerColor);
						row.Font.Bold = footerFontBold;
					}
					else{
						row.BackColor = System.Drawing.ColorTranslator.FromHtml(bodyColorDark);
					}

					ReportTable.Rows.Add(row);
					row = new TableRow();
					//do not count footer row
					if(tableCount != ds.Tables.Count){
						rowCount++;
					}

				}
				tableCount++;
			}
			ReportTable.DataBind();
			return ReportTable;
		}

		//Parses DataSet object to a asp.Table object with custizable parameters
		public Table Transform(DataSet ds, int BorderWidth, string BorderColor, int CellSpacing, int CellPadding, string CssClass,
			string HeaderColor, bool HeaderFontBold, string BodyColorLight, string BodyColorDark, string FooterColor, bool FooterFontBold)
		{
			PreTransform(BorderWidth, BorderColor, CellSpacing, CellPadding, CssClass, HeaderColor, HeaderFontBold, BodyColorLight, BodyColorDark, FooterColor, FooterFontBold);
			return Transform(ds);
		}

		public string TransformToHtmlString(DataSet ds, int BorderWidth, string BorderColor, int CellSpacing, int CellPadding, string CssClass,
			string HeaderColor, bool HeaderFontBold, string BodyColorLight, string BodyColorDark, string FooterColor, bool FooterFontBold)
        {
			PreTransform(BorderWidth, BorderColor, CellSpacing, CellPadding, CssClass, HeaderColor, HeaderFontBold, BodyColorLight, BodyColorDark, FooterColor, FooterFontBold);
			Table tbl = Transform(ds);

			System.IO.StringWriter sw = new System.IO.StringWriter();
			tbl.RenderControl(new System.Web.UI.HtmlTextWriter(sw));
			return sw.ToString();
		}

		#region Properties
		public double GrandTotal{
			//set {grandTotal = value;}
			get {return grandTotal;}
		}
		public string Culture
		{
			get
			{
				return culture;
			}
			set
			{
				culture = value;
			}
		}
		#endregion

	}
}
