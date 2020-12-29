using System;
using System.Collections;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Xml;

namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	/// Summary description for SeeThat.
	/// </summary>
	public class SeeThat
	{
		private string title = "";
		private string description = "";
		private ArrayList xmlToHtmlList = new ArrayList(); //contains XmlToHtml objects
		private string footerNote = "";
		private Table seeThatTable = new Table();

		public SeeThat() {}

		public SeeThat(string title, string desc) {
			this.title = title;
			this.description = desc;
		}

		public SeeThat(string title, string desc, string footer) {
			this.title = title;
			this.description = desc;
			this.footerNote = footer;
		}

		public SeeThat(string title, string desc, ArrayList xmlTables) {
			this.title = title;
			this.description = desc;
			
			try {
				foreach(XmlDocument xd in xmlTables){
					XmlToHtml xTH = new XmlToHtml(xd);
					xmlToHtmlList.Add(xTH);
				}
			} catch(Exception) {
				throw new Exception("ArrayList xmlTable contains other objects then XmlDocuments!");
			}			
		}

		public SeeThat(string title, string desc, string footer, ArrayList xmlTables) {
			this.title = title;
			this.description = desc;
			this.footerNote = footer;
			
			try {
				foreach(XmlDocument xd in xmlTables){
					XmlToHtml xTH = new XmlToHtml(xd);
					xmlToHtmlList.Add(xTH);
				}
			} catch(Exception) {
				throw new Exception("ArrayList xmlTable contains other objects then XmlDocuments!");
			}	
		}

		public void AddTable(XmlDocument table) {
			XmlToHtml xTH = new XmlToHtml(table);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(string table) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(table);
			XmlToHtml xTH = new XmlToHtml(xdoc);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(XmlDocument table, bool theme) {
			XmlToHtml xTH = new XmlToHtml(table, theme);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(string table, bool theme) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(table);
			XmlToHtml xTH = new XmlToHtml(xdoc, theme);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(XmlDocument table, bool theme, Color themeColor) {
			XmlToHtml xTH = new XmlToHtml(table, theme, themeColor);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(string table, bool theme, Color themeColor) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(table);
			XmlToHtml xTH = new XmlToHtml(xdoc, theme, themeColor);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(XmlDocument table, bool theme, Color themeColor, string header, bool headerBold, string footer, bool footerBold) {
			XmlToHtml xTH = new XmlToHtml(table, theme, themeColor);
			xTH.AddHeader(header, headerBold);
			xTH.AddFooter(footer, footerBold);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(string table, bool theme, Color themeColor, string header, bool headerBold, string footer, bool footerBold) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(table);
			XmlToHtml xTH = new XmlToHtml(xdoc, theme, themeColor);
			xTH.AddHeader(header, headerBold);
			xTH.AddFooter(footer, footerBold);
			xmlToHtmlList.Add(xTH);
		}

        public void AddTable(XmlDocument table, bool theme, Color themeColor, string tabChar) {
			XmlToHtml xTH = new XmlToHtml(table, theme, themeColor, tabChar);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(string table, bool theme, Color themeColor, string tabChar) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(table);
			XmlToHtml xTH = new XmlToHtml(xdoc, theme, themeColor, tabChar);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(XmlDocument table, bool theme, Color themeColor, string tabChar, string header, bool headerBold, string footer, bool footerBold) {
			XmlToHtml xTH = new XmlToHtml(table, theme, themeColor, tabChar);
			xTH.AddHeader(header, headerBold);
			xTH.AddFooter(footer, footerBold);
			xmlToHtmlList.Add(xTH);
		}

		public void AddTable(string table, bool theme, Color themeColor, string tabChar, string header, bool headerBold, string footer, bool footerBold) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(table);
			XmlToHtml xTH = new XmlToHtml(xdoc, theme, themeColor, tabChar);
			xTH.AddHeader(header, headerBold);
			xTH.AddFooter(footer, footerBold);
			xmlToHtmlList.Add(xTH);
		}

		private void AddSpace(){
			TableCell tc = new TableCell();
			TableRow tr = new TableRow();
			tr.Height = Unit.Pixel(10);
			tr.Cells.Add(tc);
			seeThatTable.Rows.Add(tr);
		}

		public void CssClass(string cssClass) {
			seeThatTable.CssClass = cssClass;
			foreach(XmlToHtml t in xmlToHtmlList){
				t.CssClass(cssClass);
			}
		}

		public Table HtmlTable(){
            TableCell tc;
			TableRow tr;
			//Gets color of the first table, to be used for header,desc, and footer
			Color c = new Color();
            if(xmlToHtmlList.Count > 0){
				c = ((XmlToHtml)xmlToHtmlList[0]).Color;
			}
			//Add title if not null and apply color
			if (title != ""){
				tc = new TableCell();
				tr = new TableRow();
				tc.Text = title; 
				tc.Font.Bold = true;
				tr.Cells.Add(tc);
				tr.BackColor = c;
				seeThatTable.Rows.Add(tr);
				AddSpace();
			}
			//Add description if not null and apply color
			if(description != ""){
				tc = new TableCell();
				tr = new TableRow();
				tc.Text = description; 
				tc.Font.Bold = true;
				tr.Cells.Add(tc);
				tr.BackColor = c;
				seeThatTable.Rows.Add(tr);
				AddSpace();
			}
			//Add all the tables from XmlToHtmlList. Color included in XmlToHtml
			if(xmlToHtmlList.Count > 0){
				foreach(XmlToHtml t in xmlToHtmlList){
					tc = new TableCell();
					tr = new TableRow();
					tc.Controls.Add(t.HtmlTable);
					tr.Cells.Add(tc);
					seeThatTable.Rows.Add(tr);
					AddSpace();
				}
			}
			//Add footer if not null and apply color
			if(footerNote != ""){
				tc = new TableCell();
				tr = new TableRow();
				tc.Text = footerNote; 
				tc.Font.Bold = true;
				tr.Cells.Add(tc);
				tr.BackColor = c;
				seeThatTable.Rows.Add(tr);
			}
            return seeThatTable;
		}

		public string ToString(string tabChar){
			string s = "";
			s += title + System.Environment.NewLine + System.Environment.NewLine + description + System.Environment.NewLine + System.Environment.NewLine;
			foreach(XmlToHtml t in xmlToHtmlList){
				s = s + t.ToString(tabChar);
			}
			s += System.Environment.NewLine + System.Environment.NewLine + footerNote;
			return s;
		}

		public string ToHtmlString(){
			Color c = Color.FromArgb(255,255,255);
			if(xmlToHtmlList.Count > 0){
				c = ((XmlToHtml)xmlToHtmlList[0]).Color;
			}
			string s = "<table cellspacing=1 cellpadding=0 border=0>" + System.Environment.NewLine;
			if(title != ""){
				s += "<tr style=\"background-color:#" + c.R.ToString("x") + c.G.ToString("x") + c.B.ToString("x") + "\">" + System.Environment.NewLine;
				s += "<td>" + title + "</td>" + System.Environment.NewLine + "</tr>" + System.Environment.NewLine;
			}
			if(description != ""){
				s += "<tr style=\"background-color:#" + c.R.ToString("x") + c.G.ToString("x") + c.B.ToString("x") + "\">" + System.Environment.NewLine;
				s += "<td>" + description + "</td>" + System.Environment.NewLine + "</tr>" + System.Environment.NewLine;
			}
			foreach(XmlToHtml t in xmlToHtmlList){
				s += "<tr>" + System.Environment.NewLine + "<td>" + System.Environment.NewLine + t.ToHtmlString() + "</td>" + System.Environment.NewLine + "</tr>" + System.Environment.NewLine;
			}
			if(footerNote != ""){
				s += "<tr style=\"background-color:#" + c.R.ToString("x") + c.G.ToString("x") + c.B.ToString("x") + "\">" + System.Environment.NewLine;
				s += "<td>" + footerNote + "</td>" + System.Environment.NewLine + "</tr>" + System.Environment.NewLine;
			}
			return s += "</table>";
		}

		
		#region Attributes
		public string Title {
			set {title = value;}
			get {return title;}
		}

		public string Description {
			set {description = value;}
			get {return description;}
		}

		public string FooterNote {
			set {footerNote = value;}
			get {return footerNote;}
		}


		#endregion


	}
}
