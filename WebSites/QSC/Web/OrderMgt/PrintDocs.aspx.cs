using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing; 
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration; //For Web.Config
using System.Net;
using Common;
using FileStore;

namespace QSPFulfillment.OrderMgt
{
	/// </summary>
	public class PrintDocs : OrderMgtPage
	{
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DropDownList ddDC;
		protected System.Web.UI.WebControls.Button Button1;
        protected System.Web.UI.WebControls.Button Button8;
        protected System.Web.UI.WebControls.Label HOrderId;
		protected System.Web.UI.WebControls.TextBox fOrderID;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox fAccountID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox fCampaignID;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Panel Panel2;
		protected System.Web.UI.WebControls.Panel Panel3;
		protected System.Web.UI.WebControls.Panel PanelQualifier;
        protected System.Web.UI.WebControls.Panel PanelHasShipment;
        protected QSPFulfillment.OrderMgt.UC.OrderQualifier ucOHOrderQualifier;
        protected QSPFulfillment.OrderMgt.UC.ShipmentGroup ucShipmentGroup;
        protected System.Web.UI.WebControls.Button Button3;
		protected QSPFulfillment.CommonWeb.PDFStoreMerger PDFStoreMergerPrintDocs;
		protected QSPFulfillment.CommonWeb.UC.DateEntry	fToDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry	fFromDate;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
        protected System.Web.UI.WebControls.DropDownList ddlHasShipment;

        private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			Server.ScriptTimeout = 180;

			if (!IsPostBack) 
			{
				try
				{
					populate_list_items();
					fFromDate.Date = Convert.ToDateTime("02/01/2007");
					fToDate.Date = DateTime.Now;
					ucOHOrderQualifier.SelectedValue = 0;
                    ucShipmentGroup.SelectedValue = 0;
					PopulateDC();
					PopulateDG();
				}
				catch(Exception ex) 
				{
					DataAccess.Common.ApplicationError.ManageError(ex);

					this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
				}
			}
		}

		private void populate_list_items()
		{
		
			this.ucOHOrderQualifier.Bind();//user control
            this.ucShipmentGroup.Bind();//user control
        }

        private void PopulateDC()
		{
			
			/*PrintDocument prtdoc = new PrintDocument();
		//	string strDefaultPrinter = ConfigurationSettings.AppSettings["DocsPrinter1"];

			String InstalledPrinters; 

		
//			for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++) 
//			{ 
//				InstalledPrinters = PrinterSettings.InstalledPrinters[i]; 


//				ddDC.Items.Add(InstalledPrinters); 
//			} 

			
			int PrintersCount = Convert.ToInt32(ConfigurationSettings.AppSettings["TotalPrinters"]);
			string PrinterName;

			for (int i = 1; i <= PrintersCount ; i++) 
			{ 
				PrinterName = "DocsPrinter"+Convert.ToString(i);
				
				InstalledPrinters = ConfigurationSettings.AppSettings[PrinterName];
	

				ddDC.Items.Add(InstalledPrinters); 
			} 

			//ddDC.SelectedValue = strDefaultPrinter;
            */
		}


		private void PopulateDG()
		{
			int vQualifier;
            int vShipmentGroup;
            int vHasShipment;
            bool? hasShipment;
			string vDate1;
			string vDate2;

			vQualifier = this.ucOHOrderQualifier.SelectedValue;
            vShipmentGroup = this.ucShipmentGroup.SelectedValue;

            vHasShipment = Convert.ToInt32(ddlHasShipment.SelectedItem.Value);
            if (vHasShipment == -1)
                hasShipment = null;
            else if (vHasShipment == 1)
                hasShipment = true;
            else
                hasShipment = false;

            vDate1 = fFromDate.Value;
			vDate2  = fToDate.Value;

			if (fOrderID.Text == "") 
			{
					fOrderID.Text= "0";
			}

			if (fCampaignID.Text == "") 
			{
				fCampaignID.Text= "0";
			}

			if (fAccountID.Text == "") 
			{
				fAccountID.Text= "0";
			}

				
			DAL.PrintDocsDataAccess oPrintDA = new DAL.PrintDocsDataAccess();

			DataGrid1.DataSource = oPrintDA.Get_OrdersTobePrinted(Convert.ToInt32(fOrderID.Text),
																	Convert.ToInt32(fCampaignID.Text),
																	Convert.ToInt32(fAccountID.Text),
																	vQualifier,
																	fFromDate.Value,
																	fToDate.Value,
                                                                    vShipmentGroup,
                                                                    hasShipment);
			DataGrid1.DataBind();
	
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			try 
			{
				ResetDG();
				PopulateDG();
			} 
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Button 1 (Print docs) click
		private void Button1_Click(object sender, System.EventArgs e)
		{
			PDFStore pdfStore = null;
			WebClient webClient = new WebClient();
			DAL.PrintDocsDataAccess printDA = new DAL.PrintDocsDataAccess();

			string pdfFilePath = ConfigurationSettings.AppSettings["BatchFilesURL"];

			string picklist;
			string packingSlip;
			string BHE;
			string participantListing;
			string homeRoom;
			string groupRoom;
			string magazineItems;
			string problemSolver;
			string teacherBox;
			string orderEntry;
			string priceDiscrepancy;
			int BHEWH;
			int prizesWH;
            string dataGridItemID;
            int dataGridItemCount = 0;

			CheckBox chkSelected;
			System.Web.UI.HtmlControls.HtmlInputHidden orderID;
            System.Web.UI.HtmlControls.HtmlInputHidden shipmentGroup;
            DataTable dataTable;
			bool printMergedFile = false;
			
			try 
			{
				pdfStore = new PDFStore();

				foreach (DataGridItem dgi in DataGrid1.Items)
				{
                    dataGridItemCount++;
                    dataGridItemID = dataGridItemCount.ToString().PadLeft(3, '0');

                    chkSelected = (CheckBox) dgi.FindControl("Checkbox1");
					if(chkSelected.Checked) 
					{
						orderID = (System.Web.UI.HtmlControls.HtmlInputHidden) dgi.FindControl("HOrderId");

                        shipmentGroup = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("HShipmentGroupID");
                        
                        int? shipmentGroupID;
                        if (shipmentGroup.Value == "")
                            shipmentGroupID = null;
                        else
                            shipmentGroupID = Convert.ToInt32(shipmentGroup.Value);

                        dataTable = printDA.GetFilesForPrint(Convert.ToInt32(orderID.Value), shipmentGroupID);

						picklist			= dataTable.Rows[0].ItemArray[0].ToString();
						packingSlip			= dataTable.Rows[0].ItemArray[1].ToString();
						BHE					= dataTable.Rows[0].ItemArray[2].ToString();
						participantListing	= dataTable.Rows[0].ItemArray[3].ToString();
						homeRoom			= dataTable.Rows[0].ItemArray[4].ToString();
						groupRoom			= dataTable.Rows[0].ItemArray[5].ToString();
						magazineItems		= dataTable.Rows[0].ItemArray[6].ToString();
						problemSolver		= dataTable.Rows[0].ItemArray[7].ToString();
						teacherBox			= dataTable.Rows[0].ItemArray[8].ToString();
						orderEntry			= dataTable.Rows[0].ItemArray[9].ToString();
						priceDiscrepancy	= dataTable.Rows[0].ItemArray[10].ToString();

						BHEWH				= Convert.ToInt32(dataTable.Rows[0].ItemArray[11]);
						prizesWH			= Convert.ToInt32(dataTable.Rows[0].ItemArray[12]);

                        bool packedForStudent = false; //Once report ready, update logic

                        if (packingSlip.Length > 0)
                        {
                            pdfStore.Add(dataGridItemID + packingSlip, webClient.DownloadData(pdfFilePath + "PackingSlip/" + packingSlip));
                            printMergedFile = true;
                        }

                        if (!packedForStudent)
                        {
                            if (picklist.Length > 0)
                            {
                                pdfStore.Add(dataGridItemID + picklist, webClient.DownloadData(pdfFilePath + "PickList/" + picklist));
                                printMergedFile = true;
                            }

                            if (participantListing.Length > 0)
                            {
                                pdfStore.Add(dataGridItemID + participantListing, webClient.DownloadData(pdfFilePath + "ParticipantListing/" + participantListing));
                                printMergedFile = true;
                            }

                            if (homeRoom.Length > 0)//home room first copy
                            {
                                pdfStore.Add(dataGridItemID + homeRoom, webClient.DownloadData(pdfFilePath + "HomeRoomSummary/" + homeRoom));
                                printMergedFile = true;
                            }
                        }
                        else
                        {
                            dataTable = printDA.GetParticipantListingParticipants(Convert.ToInt32(orderID.Value));

                            foreach(DataRow dr in dataTable.Rows)
                            {
                                pdfStore.Add(dataGridItemID + participantListing + dr["StudentInstance"].ToString(), webClient.DownloadData(pdfFilePath + "ParticipantListing/" + participantListing + dr["StudentInstance"].ToString()));
                            }
                        }
						
                        //	if (vHomeRoom.Length > 0) //home room second copy
						//	{
						//		sw.WriteLine(vAcrobatPath+ " /t "+vPDF_FilePath+"HomeRoomSummary\\"+vHomeRoom+" "+vDQ+ddDC.SelectedValue.ToString()+vDQ);
						//	}

						if(groupRoom.Length > 0) //group room  first copy
						{
							pdfStore.Add(dataGridItemID + groupRoom, webClient.DownloadData(pdfFilePath + "GroupSummary/" + groupRoom));
							printMergedFile = true;
						}

						//	if (vGroupRoom.Length > 0) //group room second copy
						//	{
						//		sw.WriteLine(vAcrobatPath+ " /t "+vPDF_FilePath+"GroupSummary\\"+vGroupRoom+" "+vDQ+ddDC.SelectedValue.ToString()+vDQ);
						//	}


						if(magazineItems.Length > 0)
						{
							pdfStore.Add(dataGridItemID + magazineItems, webClient.DownloadData(pdfFilePath + "MagazineItemsSummary/" + magazineItems));
							printMergedFile = true;
						}
		
						if(problemSolver.Length > 0)
						{
							pdfStore.Add(dataGridItemID + problemSolver, webClient.DownloadData(pdfFilePath + "ProblemSolver/" + problemSolver));
							printMergedFile = true;
						}

						if(orderEntry.Length > 0)
						{
							pdfStore.Add(dataGridItemID + orderEntry, webClient.DownloadData(pdfFilePath + "OrderEntryFollowup/" + orderEntry));
							printMergedFile = true;
						}
						
						if(priceDiscrepancy.Length > 0)
						{
							pdfStore.Add(dataGridItemID + priceDiscrepancy, webClient.DownloadData(pdfFilePath + "PriceDiscrepancy/" + priceDiscrepancy));
							printMergedFile = true;
						}

						//Labels 

						if(BHE.Length > 0 && BHEWH == 1) // print only if qsp warehouse
						{
							pdfStore.Add(dataGridItemID + BHE, webClient.DownloadData(pdfFilePath + "BHELabels/" + BHE));
							printMergedFile = true;
						}

						/*if(teacherBox.Length > 0 && prizesWH == 1) // print only if qsp warehouse
						{
							pdfStore.Add(teacherBox, webClient.DownloadData(pdfFilePath + "TeacherBoxLabels/" + teacherBox));
							printMergedFile = true;
						}*/

						//sw.Close();

					}// end if (chkSelected.Checked)

				}//end - foreach (DataGridItem dgItem in DataGrid1.Items)
			
				if (printMergedFile) //if atleast one order was selected for printing
				{
					PDFStoreMergerPrintDocs.Merge(pdfStore);
				} 
			}
			catch(Exception ex) 
			{
				if(pdfStore != null) 
				{
					pdfStore.Close();
				}

				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0));
			}
		}
		#endregion

        #region Button 8 (Print Labels) click
        private void Button8_Click(object sender, System.EventArgs e)
        {
            PDFStore pdfStore = null;
            WebClient webClient = new WebClient();
            DAL.PrintDocsDataAccess printDA = new DAL.PrintDocsDataAccess();

			string pdfFilePath = ConfigurationSettings.AppSettings["BatchFilesURL"];

			string picklist;
			string packingSlip;
			string BHE;
			string participantListing;
			string homeRoom;
			string groupRoom;
			string magazineItems;
			string problemSolver;
			string teacherBox;
			string orderEntry;
			string priceDiscrepancy;
			int BHEWH;
			int prizesWH;
            string dataGridItemID;
            int dataGridItemCount = 0;

            CheckBox chkSelected;
            System.Web.UI.HtmlControls.HtmlInputHidden orderID;
            System.Web.UI.HtmlControls.HtmlInputHidden shipmentGroupID;
            DataTable dataTable;
            bool printMergedFile = false;

            try
            {
                pdfStore = new PDFStore();

                foreach (DataGridItem dgi in DataGrid1.Items)
                {
                    dataGridItemCount++;
                    dataGridItemID = dataGridItemCount.ToString().PadLeft(3, '0');

                    chkSelected = (CheckBox)dgi.FindControl("Checkbox1");
                    if (chkSelected.Checked)
                    {
                        orderID = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("HOrderId");
                        shipmentGroupID = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("HShipmentGroupID");

                        dataTable = printDA.GetFilesForPrint(Convert.ToInt32(orderID.Value), Convert.ToInt32(shipmentGroupID.Value));

                        picklist = dataTable.Rows[0].ItemArray[0].ToString();
                        packingSlip = dataTable.Rows[0].ItemArray[1].ToString();
                        BHE = dataTable.Rows[0].ItemArray[2].ToString();
                        participantListing = dataTable.Rows[0].ItemArray[3].ToString();
                        homeRoom = dataTable.Rows[0].ItemArray[4].ToString();
                        groupRoom = dataTable.Rows[0].ItemArray[5].ToString();
                        magazineItems = dataTable.Rows[0].ItemArray[6].ToString();
                        problemSolver = dataTable.Rows[0].ItemArray[7].ToString();
                        teacherBox = dataTable.Rows[0].ItemArray[8].ToString();
                        orderEntry = dataTable.Rows[0].ItemArray[9].ToString();
                        priceDiscrepancy = dataTable.Rows[0].ItemArray[10].ToString();

                        BHEWH = Convert.ToInt32(dataTable.Rows[0].ItemArray[11]);
                        prizesWH = Convert.ToInt32(dataTable.Rows[0].ItemArray[12]);


                        /*if (picklist.Length > 0)
                        {
                            pdfStore.Add(picklist, webClient.DownloadData(pdfFilePath + "PickList/" + picklist));
                            printMergedFile = true;
                        }

                        if (packingSlip.Length > 0)
                        {
                            pdfStore.Add(packingSlip, webClient.DownloadData(pdfFilePath + "PackingSlip/" + packingSlip));
                            printMergedFile = true;
                        }

                        if (participantListing.Length > 0)
                        {
                            pdfStore.Add(participantListing, webClient.DownloadData(pdfFilePath + "ParticipantListing/" + participantListing));
                            printMergedFile = true;
                        }

                        if (homeRoom.Length > 0)//home room first copy
                        {
                            pdfStore.Add(homeRoom, webClient.DownloadData(pdfFilePath + "HomeRoomSummary/" + homeRoom));
                            printMergedFile = true;
                        }

                        //	if (vHomeRoom.Length > 0) //home room second copy
                        //	{
                        //		sw.WriteLine(vAcrobatPath+ " /t "+vPDF_FilePath+"HomeRoomSummary\\"+vHomeRoom+" "+vDQ+ddDC.SelectedValue.ToString()+vDQ);
                        //	}

                        if (groupRoom.Length > 0) //group room  first copy
                        {
                            pdfStore.Add(groupRoom, webClient.DownloadData(pdfFilePath + "GroupSummary/" + groupRoom));
                            printMergedFile = true;
                        }

                        //	if (vGroupRoom.Length > 0) //group room second copy
                        //	{
                        //		sw.WriteLine(vAcrobatPath+ " /t "+vPDF_FilePath+"GroupSummary\\"+vGroupRoom+" "+vDQ+ddDC.SelectedValue.ToString()+vDQ);
                        //	}


                        if (magazineItems.Length > 0)
                        {
                            pdfStore.Add(magazineItems, webClient.DownloadData(pdfFilePath + "MagazineItemsSummary/" + magazineItems));
                            printMergedFile = true;
                        }

                        if (problemSolver.Length > 0)
                        {
                            pdfStore.Add(problemSolver, webClient.DownloadData(pdfFilePath + "ProblemSolver/" + problemSolver));
                            printMergedFile = true;
                        }

                        if (orderEntry.Length > 0)
                        {
                            pdfStore.Add(orderEntry, webClient.DownloadData(pdfFilePath + "OrderEntryFollowup/" + orderEntry));
                            printMergedFile = true;
                        }

                        if (priceDiscrepancy.Length > 0)
                        {
                            pdfStore.Add(priceDiscrepancy, webClient.DownloadData(pdfFilePath + "PriceDiscrepancy/" + priceDiscrepancy));
                            printMergedFile = true;
                        }

                        //Labels 

                        if (BHE.Length > 0 && BHEWH == 1) // print only if qsp warehouse
                        {
                            pdfStore.Add(BHE, webClient.DownloadData(pdfFilePath + "BHELabels/" + BHE));
                            printMergedFile = true;
                        }*/

                        if (teacherBox.Length > 0 && prizesWH == 1) // print only if qsp warehouse
                        {
                            pdfStore.Add(dataGridItemID + teacherBox, webClient.DownloadData(pdfFilePath + "TeacherBoxLabels/" + teacherBox));
                            printMergedFile = true;
                        }

                        //sw.Close();

                    }// end if (chkSelected.Checked)

                }//end - foreach (DataGridItem dgItem in DataGrid1.Items)

                if (printMergedFile) //if atleast one order was selected for printing
                {
                    PDFStoreMergerPrintDocs.Merge(pdfStore);
                }
            }
            catch (Exception ex)
            {
                if (pdfStore != null)
                {
                    pdfStore.Close();
                }

                QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
                this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0));
            }
        }
        #endregion

        private void Button2_Click(object sender, System.EventArgs e)
		{
		   CheckBox chkSelected;
		   System.Web.UI.HtmlControls.HtmlInputHidden orderID;
            System.Web.UI.HtmlControls.HtmlInputHidden shipmentGroup;
            DAL.PrintDocsDataAccess oPrintDA = new DAL.PrintDocsDataAccess();

			foreach (DataGridItem dgItem in DataGrid1.Items)
			{
				chkSelected = (CheckBox)dgItem.FindControl("Checkbox1");
				if (chkSelected.Checked) 
				{
					orderID	= (System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("HOrderId");
                    shipmentGroup = (System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("HShipmentGroupID");

                    int? shipmentGroupID;
                    if (shipmentGroup.Value == "")
                        shipmentGroupID = null;
                    else
                        shipmentGroupID = Convert.ToInt32(shipmentGroup.Value);

                    oPrintDA.UpdateOrderPrintStatus(Convert.ToInt32(orderID.Value), shipmentGroupID);

	
				}// end if (chkSelected.Checked)

			}//end - foreach (DataGridItem dgItem in DataGrid1.Items)
	
			PopulateDG();
	
		}


		private void ResetDG()
		{
			DataGrid1.CurrentPageIndex = 0;
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
		  CheckDocumentToPrint(true);
		}
		private void CheckDocumentToPrint(bool pCheckUncheck)
		{
			DataGridItem dgItem;
			DataGrid1.SelectedIndex = 0;
			CheckBox chkBox ;


			//for (int i=0; i < DataGrid1.Items.Count; i++)
			for (int i=0; i < 50; i++)
			{
				dgItem = DataGrid1.Items[i];
				chkBox = (CheckBox)dgItem.FindControl("CheckBox1");
				if (chkBox.Checked != pCheckUncheck)
				{
					chkBox.Checked = pCheckUncheck;

				}
				else
				{
					chkBox.Checked = pCheckUncheck;

				}


			}
		}

		private void Button5_Click(object sender, System.EventArgs e)
		{
			CheckDocumentToPrint(false);
		}

	}
}
