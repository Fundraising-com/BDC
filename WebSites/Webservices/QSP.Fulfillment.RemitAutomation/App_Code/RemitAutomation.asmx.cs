using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using Business.Objects;
using QSPFulfillment.DataAccess.Common;

namespace RemitAutomation
{
	/// <summary>
	/// Remit Automation Web Service
	/// </summary>
	[WebService(Namespace="http://ws.qsp.com/qspcanada/")]
	public class RemitAutomation : System.Web.Services.WebService
	{
		public RemitAutomation()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		[WebMethod]
		public bool ProcessRemit()
		{
			return ReProcessRemit(0);
		}

		[WebMethod(MessageName="ReProcessRemit")]
		public bool ReProcessRemit(int remitBatch)
		{
			bool isValid = true;
			
			try 
			{
				InitializeConfiguration();

				Remit remit = new Remit(remitBatch);

				//Validate the Remit
				isValid = remit.ValidateRemitBatch();

				//Process remit Batch : create files
				if(isValid)
				{
					isValid = remit.ProcessRemitBatch();
				}

				//Calculate taxes
				if(isValid)
				{
					isValid = remit.CalculateTaxesForRemit();
				}

                //Generate Gift Cards
                if (isValid)
                {
                    remit.GenerateGiftCards(false);
                    remit.SendGiftCards(false);
                }

                //Send AP info
                if (isValid)
                {
                    isValid = remit.SendAP(false);
                }
                
                //Process Remit Report
                if (isValid)
                {
                    remit.GenerateRemitReport();
                    remit.SendRemitReport();
                }

                //Process Magazine Subscriber Information Report
                if (isValid)
                {
                    remit.GenerateMagazineSubscriberInfoReport();
                    remit.SendMagazineSubscriberInfoReport();
                }

                //Process Full Magazine Subscriber Information Report
                if (isValid)
                {
                    remit.GenerateFullMagazineSubscriberInfoReport();
                    remit.SendFullMagazineSubscriberInfoReport();
                }

                //Process Remit Info Report
                if (isValid)
                {
                    remit.GenerateRemitInfoReport();
                    remit.SendRemitInfoReport();
                }

				//Notify Users
				remit.NotifyUsers();

			} 
			catch(Exception ex) 
			{
				ApplicationError.ManageError(ex);
			}
			
			return isValid;
		}

        [WebMethod]
        public bool GenerateGiftCards()
        {
            bool isValid = true;

            try
            {
                InitializeConfiguration();
                Remit remit = new Remit(1);

                isValid = remit.GenerateGiftCards(true);
                if (isValid)
                {
                    remit.SendGiftCards(true);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
            }

            return isValid;
        }

        [WebMethod]
        public bool SendAP(int remitBatch)
        {
            bool isValid = true;

            try
            {
                InitializeConfiguration();
                Remit remit = new Remit(remitBatch);

                isValid = remit.SendAP(true);
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
            }

            return isValid;
        }

        [WebMethod]
        public bool GenerateRemitReport(int remitBatch)
        {
            bool isValid = true;

            try
            {
                InitializeConfiguration();
                Remit remit = new Remit(remitBatch);

                isValid = remit.GenerateRemitReport();
                if (isValid)
                {
                    remit.SendRemitReport();
                }
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
            }

            return isValid;
        }

        [WebMethod]
        public bool GenerateMagazineSubscriberInfoReport(int remitBatch)
        {
            bool isValid = true;

            try
            {
                InitializeConfiguration();
                Remit remit = new Remit(remitBatch);

                isValid = remit.GenerateMagazineSubscriberInfoReport();
                if (isValid)
                {
                    remit.SendMagazineSubscriberInfoReport();
                }
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
            }

            return isValid;
        }

        [WebMethod]
        public bool GenerateFullMagazineSubscriberInfoReport(int remitBatch)
        {
            bool isValid = true;

            try
            {
                InitializeConfiguration();
                Remit remit = new Remit(remitBatch);

                isValid = remit.GenerateFullMagazineSubscriberInfoReport();
                if (isValid)
                {
                    remit.SendFullMagazineSubscriberInfoReport();
                }
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
            }

            return isValid;
        }
        
        [WebMethod]
        public bool GenerateRemitInfoReport(int remitBatch)
		{
            bool isValid = true;

            try
            {
                InitializeConfiguration();
                Remit remit = new Remit(remitBatch);

                isValid = remit.GenerateRemitInfoReport();
                if (isValid)
                {
                    remit.SendRemitInfoReport();
                }
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
            }
			
			return isValid;
		}

		private static void InitializeConfiguration() 
		{
			object o = System.Configuration.ConfigurationManager.GetSection("ApplicationConfiguration");
			ApplicationConfiguration.OnApplicationStart(String.Empty);
		}
	}	
}
