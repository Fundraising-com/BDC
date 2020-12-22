using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using efundraising.eReport;
using efundraising.eReportWeb.Components.Server;

namespace efundraising.eReportWeb
{
	/// <summary>
	/// Summary description for ReportSummary.
	/// </summary>
	public partial class ReportSummary : eReportWebBasePage
	{
		#region Fields
		// LabelLogin should be removed when created by VS IDE
		#endregion

		#region Const

		private const string consultantNtLogin = "@consultant_nt_login";
		private const string toolTipconsultantNtLogin = "consultant name";

		#endregion

		#region Page Methods
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Validation required on fields
			GenerateButton.CausesValidation = true;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);

			// Call function to initialize page and its controls
			InitializePage();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Private Methods
		private void InitializePage()
		{
			// Initialization function in InitializePage rather than Page_Load or problems occur

			// Get reportID from url or redirect to page
			if(Request["rid"] == null)
				Response.Redirect("ReportSelection.aspx");

			Report report = null;

			try
			{
				int reportID = int.Parse(Request["rid"]);

				// Load report corresponding to reportID with his parameters
				report = Report.LoadReportByID(reportID);

				// Load parameters that match ReportID
				report.ParameterCollection = ReportParameter.LoadReportParameters(reportID);
			}
			catch
			{
				Response.Redirect("ReportSelection.aspx");
			}

			// We now have a complete report with parameters except for
			// parameter.val, an object that the user will provide by the controls
			// ex : choosing start date of event on calendar

			// Fill labels
			LabelReport.Text = report.Label;
			LabelReportDescription.Text = report.Desc;

			// Populate PlaceHolder with the report parameters
			FillControls(report);

			// Save report in session
			report.Save(Session);
		}

		private void FillControls(Report report)
		{
			// Int to specify which table number we are building
			int tableNb = 0;
			// Int to verify if we can automatically redirect to confirmation
			// if no controls are to be filled
			int nbControls = 0;
			foreach(ReportParameter parameter in report.ParameterCollection)
			{
				Table table = new Table();

				TableRow tableRow = new TableRow();

				TableCell tableCell1 = new TableCell();
				tableCell1.Width = 300;
				tableCell1.VerticalAlign = VerticalAlign.Top;

				TableCell tableCell2 = new TableCell();
				tableCell2.Width = 300;
				tableCell2.HorizontalAlign = HorizontalAlign.Right;

				TableCell tableCell3 = new TableCell();

				Label label = new Label();
				label.Text = parameter.Label;
				label.CssClass = "NormalTextBold Normal";
				label.Width = Unit.Percentage(100);

				tableCell1.Controls.Add(label);

				// If application is external we must find partnerID and fill
				// it in the appropriate form if it is named partner_id
				if(Config.IsExternal && parameter.Name == "@partner_id")
				{
					Label labelPartnerName = new Label();
					if (thePartner != null)
					{
						labelPartnerName.Text = thePartner.PartnerName;
						labelPartnerName.ToolTip = "partner id";
					}
					else
						labelPartnerName.Text = userLogin.UserName;
					tableCell2.Controls.Add(labelPartnerName);
				}
				// For internal reports
				else if(!Config.IsExternal && parameter.Name == consultantNtLogin) 
				{
					Label labelConsultantName = new Label();
					labelConsultantName.Text = userLogin.UserName;
					labelConsultantName.ToolTip = toolTipconsultantNtLogin;
					tableCell2.Controls.Add(labelConsultantName);
				}
				else
				{
					if(parameter.ControlName == "TextBox")
					{
						// Create tableCell with textBox
						TextBox textBox = new TextBox();
						// Set default TextBox value if one set
						if(parameter.DefaultValue != null && parameter.DefaultValue != "")
							textBox.Text = parameter.DefaultValue;
						textBox.ID = "TextBox" + tableNb;
						textBox.Width = Unit.Percentage(100);

						tableCell2.Controls.Add(textBox);

						// Validator section
						if(parameter.Nullable == false)
						{
							System.Web.UI.WebControls.RequiredFieldValidator rfv = new RequiredFieldValidator();
							rfv.Text = "Required Field";
							rfv.CssClass = "SmallText ErrorMessage";
							rfv.ControlToValidate = "TextBox" + tableNb;
							rfv.Display = ValidatorDisplay.Dynamic;

							tableCell3.Controls.Add(rfv);
						}
						if(parameter.Type == "Int")
						{
							System.Web.UI.WebControls.RangeValidator rv = new RangeValidator();
							rv.Text = "Integer required";
							rv.CssClass = "SmallText ErrorMessage";
							rv.ControlToValidate = "TextBox" + tableNb;
							rv.Display = ValidatorDisplay.Dynamic;
							rv.Type = ValidationDataType.Integer;
							rv.MinimumValue = "0";
							rv.MaximumValue = "2147483647";	// Maximum value of unsigned integer (2^31-1)

							tableCell3.Controls.Add(rv);
						}
					}

					else if(parameter.ControlName == "Calendar")
					{
						// Create tableCell with calendar
						System.Threading.Thread.CurrentThread.CurrentCulture = 
							System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
						Calendar calendar =	new Calendar();
						if(parameter.DefaultValue != null && parameter.DefaultValue != "")
							calendar.SelectedDate = System.DateTime.Parse(parameter.DefaultValue);
						else
							calendar.SelectedDate = System.DateTime.Today;

						tableCell2.Controls.Add(calendar);
					}

					else if(parameter.ControlName == "DropDownList")
					{
						// Create tableCells with dropDownList
						DropDownList dropDownList =	new DropDownList();
						dropDownList.Width = Unit.Percentage(100);

						// Generate dropDownList content
						ArrayList listItems = parameter.GenerateDropDownList(report);
						foreach(ListItem listItem in listItems)
						{
							dropDownList.Items.Add(listItem);
						}

						
						// Select Consultant ID.
						if (userLogin != null)
						{
							int selectedConsultantID = -1;
							try
							{
								selectedConsultantID = Convert.ToInt32(report.GetConsultantByNtLogin(userLogin.UserName, parameter));
							}
							catch (Exception)
							{
								selectedConsultantID = -1;
							}
							if (selectedConsultantID > 0)
							{
								dropDownList.SelectedValue = selectedConsultantID.ToString();
							}
							else
							{
								if(parameter.DefaultValue != null && parameter.DefaultValue != "")
								{
									dropDownList.SelectedValue = Convert.ToInt32(parameter.DefaultValue).ToString();
								}
								else
								{
									//throw new Exception(string.Format("There is no consultantID for this user {0}", userLogin.UserName));
									RegisterClientScriptBlock("FillControl", 
string.Format("<script>alert('There is no consultantID for this user {0}');document.location.replace('ReportSelection.aspx');</script>", userLogin.UserName)
										);
								}
							}

						}

						dropDownList.Enabled = false;
						tableCell2.Controls.Add(dropDownList);
					}
					nbControls++;
				}

				// Adds tableCells in tableRow
				tableRow.Controls.Add(tableCell1);
				tableRow.Controls.Add(tableCell2);
				tableRow.Controls.Add(tableCell3);

				table.Controls.Add(tableRow);

				// Adds space between different controls
				TableCell tableCellSpace = new TableCell();
				tableCellSpace.ColumnSpan = 2;
				tableCellSpace.Height = 5;

				TableRow tableRowSpace = new TableRow();

				tableRowSpace.Cells.Add(tableCellSpace);
				table.Rows.Add(tableRowSpace);

				ParametersPlaceHolder.Controls.Add(table);

				// Increment table number
				tableNb++;
			}
			// In the case a control has no parameters to set
			if(nbControls == 0)
			{
				// Save report in session
				report.Save(Session);

				ViewReport();
			}
		}

		private void ViewReport()
		{
			Report report = Report.Create(Session);

			// Get all selection controls in an ArrayList
			ArrayList controls = new ArrayList();
			foreach(Table table in ParametersPlaceHolder.Controls)
			{
				Control control = table.Rows[0].Cells[1].Controls[0];
				controls.Add(control);
			}

			// Remember information as an objet in object parameter.val
			// that is a String, Int or DateTime
			int i = 0;
			foreach(ReportParameter parameter in report.ParameterCollection)
			{
				// If it is a label than it is a partner_id field
				if(controls[i] is Label)
				{
					if(Config.IsExternal)
					{
						parameter.Val = userLogin.ExternalId;//partner.PartnerID;
						if (parameter.Name == "@partner_id")
							parameter.PassingValueDesc = thePartner.PartnerName;
						else
							parameter.PassingValueDesc = Convert.ToString(parameter.Val);
					}
					else if ((controls[i] as Label).ToolTip.ToLower() == toolTipconsultantNtLogin )
					{
						parameter.Val = userLogin.UserName;
						parameter.PassingValueDesc = report.Desc;
					}
				}
				else if(controls[i] is TextBox)
				{
					parameter.Val = (controls[i] as TextBox).Text;
					parameter.PassingValueDesc = Convert.ToString(parameter.Val);
				}
				else if(controls[i] is DropDownList)
				{
					parameter.Val = (controls[i] as DropDownList).SelectedValue;
					parameter.PassingValueDesc =  parameter.Val + string.Format(" - ({0})", (controls[i] as DropDownList).SelectedItem.Text);
				}
				else if(controls[i] is Calendar)
				{
					parameter.Val = (controls[i] as Calendar).SelectedDate;
					parameter.PassingValueDesc = (controls[i] as Calendar).SelectedDate.ToString("MMMM, dd yyyy");
				}
				i++;
			}
			// Save report in Session
			report.Save(Session);

			Response.Redirect("ReportConfirmation.aspx?rid=" + report.ReportID.ToString());
		}
		#endregion

		#region Events
		protected void GenerateButton_Click(object sender, System.EventArgs e)
		{
			// IsValid necessary for validators check in browsers other than IE
			if(IsValid)
                ViewReport();
		}
		#endregion
	}
}