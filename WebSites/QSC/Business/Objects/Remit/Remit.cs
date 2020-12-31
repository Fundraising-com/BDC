using System;
using System.Collections;
using System.Data;
using System.IO;
using Business.Objects.RemitTests;
using DAL;
using dataAccessRef = DAL.RemitData;
using FileStore;
using QSPFulfillment.DataAccess.Common;
using QSP.WebControl;
using System.Collections.Generic;
using System.Net;

namespace Business.Objects
{
	/// <summary>
	/// global Remit object
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class Remit
    {
        #region Constants
        private const int REPORT_TIMEOUT = 320000;
        private const string REMIT_GENERATE_FILES_KEY = "RemitGenerateFiles";

		private const string SUMMARY_MESSAGE_FROM = "RemitAutomation_Email_From";
		private const string SUMMARY_MESSAGE_TO = "RemitAutomationValidationEmail";
		private const string SUMMARY_MESSAGE_SUBJECT = "Remit Results";

		private const string GIFTCARDS_MESSAGE_FROM = "RemitAutomation_Email_From";
		private const string GIFTCARDS_MESSAGE_TO = "GiftCardsEmail";
		private const string GIFTCARDS_MESSAGE_SUBJECT = "QSP Gift Cards";

        private const string REMITREPORT_MESSAGE_FROM = "RemitAutomation_Email_From";
        private const string REMITREPORT_MESSAGE_TO = "RemitReportEmail";
        private const string REMITREPORT_MESSAGE_SUBJECT = "QSP Remit Report";
        private const string REMITREPORT_REPORTNAME = "RemitProcessingReport";
        private const string REMITREPORT_FILENAME = "RemitReport";
        private const string REMITREPORT_FILEEXT = "pdf";
        private const string REMITREPORT_FORMAT = "PDF";

        private const string MAGAZINESUBSCRIBERINFOREPORT_MESSAGE_FROM = "RemitAutomation_Email_From";
        private const string MAGAZINESUBSCRIBERINFOREPORT_MESSAGE_TO = "MagazineSubscriberInfoReportEmail";
        private const string MAGAZINESUBSCRIBERINFOREPORT_MESSAGE_SUBJECT = "QSP Magazine Subscriber Info Report";
        private const string MAGAZINESUBSCRIBERINFOREPORT_REPORTNAME = "MagazineSubscriberInformationReport";
        private const string MAGAZINESUBSCRIBERINFOREPORT_FILENAME = "MagazineSubscriberInfoReport";
        private const string MAGAZINESUBSCRIBERINFOREPORT_FILEEXT = "pdf";
        private const string MAGAZINESUBSCRIBERINFOREPORT_FORMAT = "PDF";

        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_MESSAGE_FROM = "RemitAutomation_Email_From";
        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_MESSAGE_TO = "FullMagazineSubscriberInfoReportEmail";
        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_MESSAGE_SUBJECT = "QSP Full Magazine Subscriber Info Report";
        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_REPORTNAME = "MagazineSubscriberInformationReport";
        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_FILENAME = "FullMagazineSubscriberInfoReport";
        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_FILEEXT = "pdf";
        private const string FULLMAGAZINESUBSCRIBERINFOREPORT_FORMAT = "PDF";

        private const string REMITINFOREPORT_MESSAGE_FROM = "RemitAutomation_Email_From";
        private const string REMITINFOREPORT_MESSAGE_TO = "RemitInfoReportEmail";
        private const string REMITINFOREPORT_MESSAGE_SUBJECT = "QSP Remit Info Report";
        private const string REMITINFOREPORT_REPORTNAME = "RemitInfo";
        private const string REMITINFOREPORT_FILENAME = "RemitInfoReport";
        private const string REMITINFOREPORT_FILEEXT = "xls";
        private const string REMITINFOREPORT_FORMAT = "EXCEL";
        #endregion

        #region Fields
        protected dataAccessRef dataAccess;
		protected int _runID;
		protected RemitSummary remitSummary;
		protected RemitValidation _rValidation;
		protected Store fileStore;
        protected Store giftCardsStore;
        protected Store remitReportStore;
        protected Store magazineSubscriberInfoReportStore;
        protected Store fullMagazineSubscriberInfoReportStore;
        protected Store remitInfoReportStore;
        protected RemitMailer rMailer;
		protected RemitMailer rMailerGiftCards;
        protected RemitMailer rMailerRemitReport;
        protected RemitMailer rMailerMagazineSubscriberInfoReport;
        protected RemitMailer rMailerFullMagazineSubscriberInfoReport;
        protected RemitMailer rMailerRemitInfoReport; 
        protected bool reProcess;
        private bool? generateFiles = null;
        #endregion

        #region Properties
        public bool? GenerateFiles
        {
            get
            {
                if (generateFiles == null)
                    generateFiles = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings[REMIT_GENERATE_FILES_KEY]);
                return generateFiles;
            }
        }
        #endregion

        #region Constructors
		public Remit(int runID)
		{
			dataAccess = new dataAccessRef();
			fileStore = new Store();
            System.Threading.Thread.Sleep(100);
            giftCardsStore = new Store();
            System.Threading.Thread.Sleep(100);
            remitReportStore = new Store();
            System.Threading.Thread.Sleep(100);
            magazineSubscriberInfoReportStore = new Store();
            System.Threading.Thread.Sleep(100);
            fullMagazineSubscriberInfoReportStore = new Store();
            System.Threading.Thread.Sleep(100);
            remitInfoReportStore = new Store();
            
			string messageFrom = System.Configuration.ConfigurationManager.AppSettings[SUMMARY_MESSAGE_FROM];
			string messageTo = System.Configuration.ConfigurationManager.AppSettings[SUMMARY_MESSAGE_TO];

			if(runID != 0)
			{
				reProcess = true;
				_runID = runID;
                rMailer = new RemitMailer(messageFrom, messageTo, SUMMARY_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));
				rMailer.BodyBuilder.Append("<h3 style=\"font-family:arial;\">Automated Remit Re-Processing for Run ID " + _runID.ToString() + "</h3>");
			}
			else
			{
				reProcess = false;
                _runID = CreateNewRemit();
                rMailer = new RemitMailer(messageFrom, messageTo, SUMMARY_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));
                rMailer.BodyBuilder.Append("<h3 style=\"font-family:arial;\">Automated Remit Processing for Run ID " + _runID.ToString() + "</h3>");
			}

			remitSummary = new RemitSummary(_runID);
			
			rMailer.BodyBuilder.Append(remitSummary.htmlTable);
			
			//get all remit validation tests
			_rValidation = new RemitValidation(true);
        }
        #endregion

        #region Methods
        /// <summary>
		/// create new remit and get current RunID
		/// </summary>
		/// <returns>current RunID</returns>
		private int CreateNewRemit()
		{
			int runID = 0;

			try
			{
				runID = dataAccess.CreateNewRemit();
			}
			catch(Exception ex)
			{
				ApplicationError.ManageError(ex);
				throw ex;
			}

			return runID;
		}

		/// <summary>
		/// run remit tests
		/// </summary>
		/// <returns>true, if valid. false otherwise</returns>
		public bool ValidateRemitBatch()
		{
			bool isValid = true;

			try 
			{
				isValid = _rValidation.Validate(_runID, fileStore, rMailer);		
			} 
			catch(RemitException remitException) 
			{
				rMailer.BodyBuilder.Append(remitException.Message);
				isValid = false;
			}

			return isValid;
		}

		/// <summary>
		/// Generate files
		/// </summary>
		/// <returns>true, if OK. false otherwise</returns>
		public bool ProcessRemitBatch()
		{
			bool isValid = true;

			try
			{
                if (GenerateFiles == true)
                {
                    if (!this.reProcess)
                    {
                        isValid = dataAccess.ProcessRemit(this._runID);
                    }
                    else
                    {
                        isValid = dataAccess.ReProcessRemit(this._runID);
                    }
                }
			}
			catch(RemitException remitException) 
			{
				rMailer.BodyBuilder.Append(remitException.Message);
				isValid = false;
			}

            if (GenerateFiles == false)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>remit batch processing skipped FH file generation</b></p>");
            }
            else if (!isValid)
			{
				rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>remit batch processing failed on FH file generation</b></p>");
			}
			else
			{
				rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully processed remit batch FH file generation</b></p>");
			}

			return isValid;
		}

		/// <summary>
		/// Calculate taxes
		/// </summary>
		/// <returns>true, if OK. false otherwise</returns>
		public bool CalculateTaxesForRemit()
		{
			bool isValid = true;

			try
			{
				isValid = dataAccess.CalculateTaxes(this._runID);
			}
			catch(RemitException remitException) 
			{
				rMailer.BodyBuilder.Append(remitException.Message);
				isValid = false;
			}

			if(!isValid)
			{
				rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>remit batch processing failed during Tax Calculation</b></p>");
			}
			else
			{
				rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully processed remit batch Tax Calculation</b></p>");
			}

			return isValid;
		}

        public bool SendAP(bool forceGeneration)
        {
            bool isValid = true;

            try
            {
                if (!reProcess || forceGeneration)
                {
                    isValid = dataAccess.SendAP(this._runID);
                }
            }
            catch (RemitException remitException)
            {
                rMailer.BodyBuilder.Append(remitException.Message);
                isValid = false;
            }

            if (reProcess && !forceGeneration)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>remit batch processing skipped AP sending</b></p><br>");
            }
            else if (!isValid)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>remit batch processing failed during AP sending</b></p><br>");
            }
            else
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully sent remit batch AP</b></p><br>");
            }

            return isValid;
        }

		/// <summary>
		/// Generate Gift Cards
		/// </summary>
		/// <returns>true, if OK. false otherwise</returns>
		public bool GenerateGiftCards(bool forceGeneration)
		{
			bool isValid = true;
			string fileName = null;

            string messageFrom = System.Configuration.ConfigurationManager.AppSettings[GIFTCARDS_MESSAGE_FROM];
			string messageTo = System.Configuration.ConfigurationManager.AppSettings[GIFTCARDS_MESSAGE_TO];
			rMailerGiftCards = new RemitMailer(messageFrom, messageTo, GIFTCARDS_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));

			try
			{
				if (!reProcess || forceGeneration) //only generate gift cards on new remit batch or when manually run
				{
					fileName = "Gift_Cards_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute;
					isValid = dataAccess.GenerateGiftCards(fileName);
				}
			}
			catch(RemitException remitException) 
			{
				rMailer.BodyBuilder.Append(remitException.Message);
				isValid = false;
			}

            if (reProcess && !forceGeneration)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Remit batch processing skipped Gift Card Generation</b></p><br>");
            }
            else if (!isValid)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Remit batch processing failed during Gift Card Generation</b></p><br>");
            }
            else
            {
                if (!reProcess || forceGeneration) //only generate gift cards on new remit batch or when manually run
                {
                    rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully generated Gift Cards</b></p><br>");

                    //Get Gift Cards
                    string filePath = System.Configuration.ConfigurationManager.AppSettings["GiftCardsURL"];
                    string fullFileName = filePath + fileName;
                    string fileNameRegular = fileName + "_Regular.txt";
                    string fileNameHoliday = fileName + "_Holiday.txt";
                    string fullFileNameRegular = filePath + fileNameRegular;
                    string fullFileNameHoliday = filePath + fileNameHoliday;

                    WebClient webClient = new WebClient();

                    try
                    {
                        int LineCountRegular = 0;

                        try
                        {
                            giftCardsStore.Add(fileNameRegular, webClient.DownloadData(fullFileNameRegular));
                        }
                        catch (System.Net.WebException)
                        {
                            //File wasn't created, so continue on
                        }
                            
                        if (giftCardsStore.StoreDirectory.GetFiles(fileNameRegular).Length == 1)
                        {
                            StreamReader srRegular = new StreamReader(giftCardsStore.StoreDirectory.FullName + @"\" + fileNameRegular);
                            while (srRegular.ReadLine() != null)
                            {
                                LineCountRegular++;
                            }
                            srRegular.Close();
                        }

                        if (LineCountRegular > 0)
                            rMailerGiftCards.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Number of Regular Gift Cards = " + LineCountRegular + "</b></p><br>");

                        int LineCountHoliday = 0;

                        try
                        {
                            giftCardsStore.Add(fileNameHoliday, webClient.DownloadData(fullFileNameHoliday));
                        }
                        catch (System.Net.WebException)
                        {
                            //File wasn't created, so continue on
                        }

                        if (giftCardsStore.StoreDirectory.GetFiles(fileNameHoliday).Length == 1)
                        {
                            StreamReader srHoliday = new StreamReader(giftCardsStore.StoreDirectory.FullName + @"\" + fileNameHoliday);
                            while (srHoliday.ReadLine() != null)
                            {
                                LineCountHoliday++;
                            }
                            srHoliday.Close();
                        }

                        if (LineCountHoliday > 0)
                            rMailerGiftCards.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Number of Holiday Gift Cards = " + LineCountHoliday + "</b></p><br>");

                        if (LineCountRegular > 0 || LineCountHoliday > 0)
                        {
                            Zip zip = new Zip();
                            List<string> fileNames = new List<string>();
                            if (LineCountRegular > 0)
                                fileNames.Add(giftCardsStore.StoreDirectory.FullName + @"\" + fileNameRegular);

                            if (LineCountHoliday > 0)
                                fileNames.Add(giftCardsStore.StoreDirectory.FullName + @"\" + fileNameHoliday);

                            zip.ZipFile(fileNames, giftCardsStore.StoreDirectory.FullName + @"\" + fileName + ".zip");

                            rMailerGiftCards.AddAttachment(giftCardsStore.StoreDirectory.FullName + @"\" + fileName + ".zip");
                        }
                    }
                    catch (RemitException remitException)
                    {
                        rMailer.BodyBuilder.Append(remitException.Message);
                        isValid = false;
                    }
                }
            }
            
            return isValid;
		}

		/// <summary>
		/// Send Gift Card e-mail
		/// </summary>
		/// <returns>true, if OK. false otherwise</returns>
        public bool SendGiftCards(bool forceGeneration)
		{
			try
			{
                if (!reProcess || forceGeneration)
                {
                    rMailerGiftCards.Send();
                }
			}
			catch(Exception ex)
			{
				ApplicationError.ManageError(ex);
				throw ex;
			}
            finally
            {
                giftCardsStore.Close();
            }

			return true;
		}

        public bool GenerateRemitReport()
        {
            bool isValid = true;

            string messageFrom = System.Configuration.ConfigurationManager.AppSettings[REMITREPORT_MESSAGE_FROM];
            string messageTo = System.Configuration.ConfigurationManager.AppSettings[REMITREPORT_MESSAGE_TO];
            rMailerRemitReport = new RemitMailer(messageFrom, messageTo, REMITREPORT_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));

            try
            {
                RSClient rsClient = new RSClient();
                Business.ReportExecution.ParameterValue[] parameterValues = new Business.ReportExecution.ParameterValue[2];
                parameterValues[0] = new Business.ReportExecution.ParameterValue();
                parameterValues[0].Name = "From_Batch_ID";
                parameterValues[0].Value = Convert.ToString(_runID);
                parameterValues[1] = new Business.ReportExecution.ParameterValue();
                parameterValues[1].Name = "To_Batch_ID";
                parameterValues[1].Value = Convert.ToString(_runID);

                string remitReportFullFilename = REMITREPORT_FILENAME + "." + REMITREPORT_FILEEXT;
                remitReportStore.Add(remitReportFullFilename, rsClient.GenerateReportStream(REMITREPORT_REPORTNAME, REMITREPORT_FORMAT, parameterValues, REPORT_TIMEOUT));
                
                Zip zip = new Zip();
                List<string> fileNames = new List<string>();
                fileNames.Add(this.remitReportStore.StoreDirectory.FullName + "\\" + remitReportFullFilename);
                zip.ZipFile(fileNames, this.remitReportStore.StoreDirectory.FullName + "\\" + REMITREPORT_FILENAME + ".zip");
                
                rMailerRemitReport.AddAttachment(remitReportStore.StoreDirectory.FullName + "\\" + REMITREPORT_FILENAME + ".zip");
            }
            catch (RemitException remitException)
            {
                rMailer.BodyBuilder.Append(remitException.Message);
                isValid = false;
            }

            if (!isValid)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Remit batch processing failed during Remit Report Processing</b></p><br>");
            }
            else
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully generated Remit Report</b></p><br>");

            }

            return isValid;
        }

        /// <summary>
        /// Send Remit Report e-mail
        /// </summary>
        /// <returns>true, if OK. false otherwise</returns>
        public bool SendRemitReport()
        {
            try
            {
                rMailerRemitReport.Send();
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
                throw ex;
            }
            finally
            {
                remitReportStore.Close();
            }

            return true;
        }

        public bool GenerateMagazineSubscriberInfoReport()
        {
            bool isValid = true;

            string messageFrom = System.Configuration.ConfigurationManager.AppSettings[MAGAZINESUBSCRIBERINFOREPORT_MESSAGE_FROM];
            string messageTo = System.Configuration.ConfigurationManager.AppSettings[MAGAZINESUBSCRIBERINFOREPORT_MESSAGE_TO];
            rMailerMagazineSubscriberInfoReport = new RemitMailer(messageFrom, messageTo, MAGAZINESUBSCRIBERINFOREPORT_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));

            try
            {
                RSClient rsClient = new RSClient();
                Business.ReportExecution.ParameterValue[] parameterValues = new Business.ReportExecution.ParameterValue[3];
                parameterValues[0] = new Business.ReportExecution.ParameterValue();
                parameterValues[0].Name = "From_Batch_ID";
                parameterValues[0].Value = Convert.ToString(_runID);
                parameterValues[1] = new Business.ReportExecution.ParameterValue();
                parameterValues[1].Name = "To_Batch_ID";
                parameterValues[1].Value = Convert.ToString(_runID);
                parameterValues[2] = new Business.ReportExecution.ParameterValue();
                parameterValues[2].Name = "hardCopy";
                parameterValues[2].Value = Convert.ToString(true);

                string magazineSubscriberInfoReportFullFilename = MAGAZINESUBSCRIBERINFOREPORT_FILENAME + "." + MAGAZINESUBSCRIBERINFOREPORT_FILEEXT;
                magazineSubscriberInfoReportStore.Add(magazineSubscriberInfoReportFullFilename, rsClient.GenerateReportStream(MAGAZINESUBSCRIBERINFOREPORT_REPORTNAME, MAGAZINESUBSCRIBERINFOREPORT_FORMAT, parameterValues, REPORT_TIMEOUT));
                
                Zip zip = new Zip();
                List<string> fileNames = new List<string>();
                fileNames.Add(this.magazineSubscriberInfoReportStore.StoreDirectory.FullName + "\\" + magazineSubscriberInfoReportFullFilename);
                zip.ZipFile(fileNames, this.magazineSubscriberInfoReportStore.StoreDirectory.FullName + "\\" + MAGAZINESUBSCRIBERINFOREPORT_FILENAME + ".zip");

                rMailerMagazineSubscriberInfoReport.AddAttachment(magazineSubscriberInfoReportStore.StoreDirectory.FullName + "\\" + MAGAZINESUBSCRIBERINFOREPORT_FILENAME + ".zip");
            }
            catch (RemitException remitException)
            {
                rMailer.BodyBuilder.Append(remitException.Message);
                isValid = false;
            }

            if (!isValid)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Remit batch processing failed during Magazine Subscriber Info Report Processing</b></p><br>");
            }
            else
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully generated Magazine Subscriber Info Report Report</b></p><br>");
            }

            return isValid;
        }

        public bool SendMagazineSubscriberInfoReport()
        {
            try
            {
                rMailerMagazineSubscriberInfoReport.Send();
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
                throw ex;
            }
            finally
            {
                magazineSubscriberInfoReportStore.Close();
            }

            return true;
        }

        public bool GenerateFullMagazineSubscriberInfoReport()
        {
            bool isValid = true;

            string messageFrom = System.Configuration.ConfigurationManager.AppSettings[FULLMAGAZINESUBSCRIBERINFOREPORT_MESSAGE_FROM];
            string messageTo = System.Configuration.ConfigurationManager.AppSettings[FULLMAGAZINESUBSCRIBERINFOREPORT_MESSAGE_TO];
            rMailerFullMagazineSubscriberInfoReport = new RemitMailer(messageFrom, messageTo, FULLMAGAZINESUBSCRIBERINFOREPORT_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));

            try
            {
                RSClient rsClient = new RSClient();
                Business.ReportExecution.ParameterValue[] parameterValues = new Business.ReportExecution.ParameterValue[2];
                parameterValues[0] = new Business.ReportExecution.ParameterValue();
                parameterValues[0].Name = "From_Batch_ID";
                parameterValues[0].Value = Convert.ToString(_runID);
                parameterValues[1] = new Business.ReportExecution.ParameterValue();
                parameterValues[1].Name = "To_Batch_ID";
                parameterValues[1].Value = Convert.ToString(_runID);

                string fullMagazineSubscriberInfoReportFullFilename = FULLMAGAZINESUBSCRIBERINFOREPORT_FILENAME + "." + FULLMAGAZINESUBSCRIBERINFOREPORT_FILEEXT;
                fullMagazineSubscriberInfoReportStore.Add(fullMagazineSubscriberInfoReportFullFilename, rsClient.GenerateReportStream(FULLMAGAZINESUBSCRIBERINFOREPORT_REPORTNAME, FULLMAGAZINESUBSCRIBERINFOREPORT_FORMAT, parameterValues, REPORT_TIMEOUT));

                Zip zip = new Zip();
                List<string> fileNames = new List<string>();
                fileNames.Add(this.fullMagazineSubscriberInfoReportStore.StoreDirectory.FullName + "\\" + fullMagazineSubscriberInfoReportFullFilename);
                zip.ZipFile(fileNames, this.fullMagazineSubscriberInfoReportStore.StoreDirectory.FullName + "\\" + FULLMAGAZINESUBSCRIBERINFOREPORT_FILENAME + ".zip");

                rMailerFullMagazineSubscriberInfoReport.AddAttachment(fullMagazineSubscriberInfoReportStore.StoreDirectory.FullName + "\\" + FULLMAGAZINESUBSCRIBERINFOREPORT_FILENAME + ".zip");
            }
            catch (RemitException remitException)
            {
                rMailer.BodyBuilder.Append(remitException.Message);
                isValid = false;
            }

            if (!isValid)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Remit batch processing failed during Full Magazine Subscriber Info Report Processing</b></p><br>");
            }
            else
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully generated Full Magazine Subscriber Info Report Report</b></p><br>");
            }

            return isValid;
        }

        public bool SendFullMagazineSubscriberInfoReport()
        {
            try
            {
                rMailerFullMagazineSubscriberInfoReport.Send();
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
                throw ex;
            }
            finally
            {
                fullMagazineSubscriberInfoReportStore.Close();
            }

            return true;
        }

        public bool GenerateRemitInfoReport()
        {
            bool isValid = true;

            string messageFrom = System.Configuration.ConfigurationManager.AppSettings[REMITINFOREPORT_MESSAGE_FROM];
            string messageTo = System.Configuration.ConfigurationManager.AppSettings[REMITINFOREPORT_MESSAGE_TO];
            rMailerRemitInfoReport = new RemitMailer(messageFrom, messageTo, REMITINFOREPORT_MESSAGE_SUBJECT + " - " + Convert.ToString(_runID));

            try
            {
                RSClient rsClient = new RSClient();
                Business.ReportExecution.ParameterValue[] parameterValues = new Business.ReportExecution.ParameterValue[1];
                parameterValues[0] = new Business.ReportExecution.ParameterValue();
                parameterValues[0].Name = "RemitBatchID";
                parameterValues[0].Value = Convert.ToString(_runID);

                string remitInfoReportFullFilename = REMITINFOREPORT_FILENAME + "." + REMITINFOREPORT_FILEEXT;
                remitInfoReportStore.Add(remitInfoReportFullFilename, rsClient.GenerateReportStream(REMITINFOREPORT_REPORTNAME, REMITINFOREPORT_FORMAT, parameterValues, REPORT_TIMEOUT));

                Zip zip = new Zip();
                List<string> fileNames = new List<string>();
                fileNames.Add(this.remitInfoReportStore.StoreDirectory.FullName + "\\" + remitInfoReportFullFilename);
                zip.ZipFile(fileNames, this.remitInfoReportStore.StoreDirectory.FullName + "\\" + REMITINFOREPORT_FILENAME + ".zip");

                rMailerRemitInfoReport.AddAttachment(remitInfoReportStore.StoreDirectory.FullName + "\\" + REMITINFOREPORT_FILENAME + ".zip");
            }
            catch (RemitException remitException)
            {
                rMailer.BodyBuilder.Append(remitException.Message);
                isValid = false;
            }

            if (!isValid)
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Remit batch processing failed during Remit Info Report Processing</b></p><br>");
            }
            else
            {
                rMailer.BodyBuilder.Append("<p style=\"font-family:arial;\"><b>Successfully generated Remit Info Report</b></p><br>");

            }

            return isValid;
        }

        /// <summary>
        /// Send Remit Info Report e-mail
        /// </summary>
        /// <returns>true, if OK. false otherwise</returns>
        public bool SendRemitInfoReport()
        {
            try
            {
                rMailerRemitInfoReport.Send();
            }
            catch (Exception ex)
            {
                ApplicationError.ManageError(ex);
                throw ex;
            }
            finally
            {
                remitInfoReportStore.Close();
            }

            return true;
        }

		/// <summary>
		/// Send e-mail to IT and users
		/// </summary>
		/// <returns>true, if OK. false otherwise</returns>
		public bool NotifyUsers()
		{
			try
			{
				rMailer.Send();
			}
			catch(Exception ex)
			{
				ApplicationError.ManageError(ex);
				throw ex;
			}
			finally
			{
				fileStore.Close();
            }

			return true;
        }
        #endregion
    }
}
