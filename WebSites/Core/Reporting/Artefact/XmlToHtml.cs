using System;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace GA.BDC.Core.Reporting.Artefact {
	/// <summary>
	/// Summary description for XmlToHtml.
	/// </summary>
	public class XmlToHtml {
		private System.Web.UI.WebControls.Table htmlTable = new Table();
		private int indent = 0;
		private bool applyTheme = false;
		private Color color = Color.FromArgb(120,140,120);
		private string tab = "&nbsp;&nbsp;&nbsp;&nbsp;";
		private string header = "";
		private bool showHeader = false;
		private string footer = "";
		private bool showFooter = false;

		//public XmlToHtml() {}

		public XmlToHtml(XmlDocument xdoc) {
			Transform(xdoc);
		}

		public XmlToHtml(string xml) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(xml);
			Transform(xdoc);
		}

		public XmlToHtml(XmlDocument xdoc, bool theme) {
			applyTheme = theme;
			Transform(xdoc);
		}
		
		public XmlToHtml(string xml, bool theme) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(xml);
			applyTheme = theme;
			Transform(xdoc);
		}


		public XmlToHtml(XmlDocument xdoc, bool theme, Color color) {
			applyTheme = theme;
			this.color = color;
			Transform(xdoc);
		}

		public XmlToHtml(string xml, bool theme, Color color) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(xml);
			applyTheme = theme;
			this.color = color;
			Transform(xdoc);
		}

		public XmlToHtml(XmlDocument xdoc, bool theme, Color color, string tabChar) {
			applyTheme = theme;
			this.color = color;
			tab = tabChar;
			Transform(xdoc);
		}

		public XmlToHtml(string xml, bool theme, Color color, string tabChar) {
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(xml);
			applyTheme = theme;
			this.color = color;
			tab = tabChar;
			Transform(xdoc);
		}


		private void Transform(XmlDocument xdoc){
			XmlNodeList xnl = xdoc.ChildNodes;
			foreach(XmlNode xn in xnl) {
				if(xn.NodeType.ToString() == "Element"){
					AddRow(xn.Name,xn.Value,indent);
					if(xn.HasChildNodes){
						indent++;
						XmlNodeListToHtml(xn.ChildNodes);
						indent--;
					}
				}
			}
		}


		private void XmlNodeListToHtml(XmlNodeList xnl){
			foreach(XmlNode xn in xnl){
				if(xn.HasChildNodes && xn.FirstChild.NodeType.ToString() == "Text"){
					AddRow(xn.Name,xn.InnerText,indent);	
				}
				else if(xn.Name != "#text"){
					AddRow(xn.Name,xn.Value,indent);
				}
				if(xn.HasChildNodes){
					indent++;
					XmlNodeListToHtml(xn.ChildNodes);
					indent--;
				}
			}
		}

		
		private void AddRow(string name, string value, int indent) {
			TableCell tc1 = new TableCell();
			TableCell tc2 = new TableCell();
			TableRow tr = new TableRow();
			if(applyTheme){
				int r = color.R;
				int g = color.G;
				int b = color.B;
				if(r+(10*indent) <= 255 && g+(10*indent) <= 255 && b+(10*indent) <= 255) {
					tr.BackColor = Color.FromArgb(r+(10*indent),g+(10*indent),b+(10*indent));
				}
				else{
					tr.BackColor = Color.FromArgb(255,255,255);	
				}
				tc1.Font.Bold = true;
			}
			tc1.VerticalAlign = VerticalAlign.Top;
			string pad = "";
			for(int i=1;i<=indent;i++) {
				pad = pad + tab;
			}
			tc1.Text = pad + name;
			tc2.Text = value;
			tr.Cells.Add(tc1);
			tr.Cells.Add(tc2);
			htmlTable.Rows.Add(tr);
		}
		
		public void AddHeader(string text, bool bold){
			TableCell tc = new TableCell();
			TableRow tr = new TableRow();
			showHeader = true;
			header = text;
			tc.Text = header;
			tc.Attributes.Add("colspan", htmlTable.Rows[1].Cells.Count.ToString());
			tr.Cells.Add(tc);
			tr.BackColor = color;
			tr.Font.Bold = bold;
			htmlTable.Rows.AddAt(0,tr);
		}

		public void AddFooter(string text, bool bold){
			TableCell tc = new TableCell();
			TableRow tr = new TableRow();
			showFooter = true;
			footer = text;
			tc.Text = footer;
			tc.Attributes.Add("colspan", htmlTable.Rows[1].Cells.Count.ToString());
			tr.Cells.Add(tc);
			tr.BackColor = color;
			tr.Font.Bold = bold;
			htmlTable.Rows.AddAt(htmlTable.Rows.Count ,tr);
		}


		public void CssClass(string cssClass) {
			htmlTable.CssClass = cssClass;
		}


		public string ToHtmlString()
		{
			string s = "";
			s += "<table cellspacing=";
			s += (htmlTable.CellSpacing == -1? "1": htmlTable.CellSpacing.ToString());
			s += " cellpadding=";
			s += (htmlTable.CellPadding == -1? "0": htmlTable.CellPadding.ToString());
			s += " border=0";
			//s += " bgcolor=#" + color.R.ToString("x") + color.G.ToString("x") + color.B.ToString("x");
			s += ">";
			s += System.Environment.NewLine;
			
			foreach(TableRow tr in htmlTable.Rows){
				s += "<tr style=\"background-color:#" + tr.BackColor.R.ToString("x") + tr.BackColor.G.ToString("x") + tr.BackColor.B.ToString("x");
				s += (tr.Font.Bold == true? "; font-weight:bold;": "");
				s += "\">" + System.Environment.NewLine;
				foreach(TableCell tc in tr.Cells) {
					s += "<td";
					s += (tc.Font.Bold == true? " style=\"font-weight:bold;\"" : "");  
					s += ">" + tc.Text + "</td>" + System.Environment.NewLine;
				}
				s += "</tr>" + System.Environment.NewLine;
			}
			
			s += "</table>" + System.Environment.NewLine;
			
			return s;
		}


		public string ToString(string tabChar) {
			string s = "";
			bool firstCol = false;
			Table t = this.HtmlTable;
			foreach(TableRow tr in t.Rows){
				firstCol = true;
				foreach(TableCell tc in tr.Cells) {
					if(tr.Cells.Count > 1 && firstCol == true){
						s += tc.Text.Replace(tab,tabChar) + ": ";
						firstCol = false;
					}
					else{
                        s += tc.Text.Replace(tab,tabChar) + " ";
					}
				}
				s = s + System.Environment.NewLine;
			}
			return s;
		}


		#region Attributes
		public Table HtmlTable {
			//set {htmlTable = value;}
			get{return htmlTable;}
		}

		public string Header {
			//set {header = value;}
			get {return header;}
		}

		public string Footer {
			//set {footer = value;}
			get {return footer;}
		}

		public string Tab {
			set {tab = value;}
			get {return tab;}
		}

		public bool ApplyTheme {
			//set {applyTheme = value;}
			get {return applyTheme;}
		}

		public Color Color {
			//set {color = value;}
			get {return color;}
		}
		#endregion
	}
}
