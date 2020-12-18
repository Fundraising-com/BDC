using System;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for TreeNodeAttributes.
	/// </summary>
	public class TreeViewAttributes: Microsoft.Web.UI.WebControls.TreeView
	{


		

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			string OpenTag = "";
			

			string renderedHTML;
			StringBuilder SB = new StringBuilder();
			StringWriter SW = new StringWriter(SB);
			HtmlTextWriter htmlTW = new HtmlTextWriter(SW);

			base.Render(htmlTW);

			renderedHTML = SB.ToString();

			int iIndex = renderedHTML.IndexOf("<tvns:treenode Selected=\"true\">");
			string header = renderedHTML.Substring(0,iIndex);
			string body = " ";

			foreach(TreeNode node in this.Nodes)
			{
				body += NodeHTML(node);
			}


			if(this.ChildStyle != String.Empty)
			{
				OpenTag = "<div "+this.ChildFunction+" style=\""+this.ChildStyle+"\">";
			}
			else
			{
				OpenTag = "<div "+this.ChildFunction+">";
			}
			body = body.Replace("<div>",OpenTag);
			
			writer.Write(header+body+"</tvns:treeview>");
		}

		private string NodeHTML(TreeNode node)
		{
			string html = "";
			html += "<tvns:treenode>";
			if(node.ChildNodes.Count == 0)
				html += "<div>";
			html += node.Text;
            foreach (TreeNode child in node.ChildNodes)
			{
				html+=NodeHTML(child);
			}
            if (node.ChildNodes.Count == 0)
				html += "</div>";
			html += "</tvns:treenode>";
			return html;
		}

		[Bindable(true)]
		public string ChildFunction
		{
			get
			{
				try
				{
					return this.ViewState["ChildFunction"].ToString();
				}
				catch
				{
					return String.Empty;
				}
			}
			set{ this.ViewState["ChildFunction"]=value;}
		}

		[Bindable(true)]
		public string ChildStyle
		{
			get
			{
				try
				{
					return this.ViewState["ChildStyle"].ToString();
				}
				catch
				{
					return String.Empty;
				}
			}
			set{ this.ViewState["ChildStyle"]=value;}
		}

/*
		protected override void Render(HtmlTextWriter output)
		{

			
			string strAttributes;
			int idx = 0;
			int index = 0;
			int endIndex;
			HtmlTextWriter liHtmlTW;
			StringWriter liSW;
			StringBuilder liSB = new StringBuilder();
			string myHTML;
			StringBuilder SB = new StringBuilder();
			StringWriter SW = new StringWriter(SB);
			HtmlTextWriter htmlTW = new HtmlTextWriter(SW);

			base.Render(htmlTW);

			myHTML = SB.ToString();

			liSW = new StringWriter(liSB);
			liHtmlTW = new HtmlTextWriter(liSW);

			while (idx < Items.Count) 
			{
				if (Items[idx].Attributes["Visible"] == "False") 
				{
					index = myHTML.IndexOf("<tr>", index);
					endIndex = (myHTML.IndexOf("</tr>", index) + "</tr>".Length);
					myHTML = myHTML.Remove(index, (endIndex - index));
				}
				else
				{
					index = (myHTML.IndexOf("<input ", index) + "<input ".Length);
					liSB.Remove(0, liSB.Length);
					Items[idx].Attributes.Render(liHtmlTW);
					strAttributes = (liSB.ToString() + " ");
					myHTML = myHTML.Insert(index, strAttributes);
				}
				idx++;
			}
			output.Write(myHTML);



		}
*/	}
}
