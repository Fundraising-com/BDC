using System;
using System.Collections;
using System.Data;
using Business.Objects;
using Business.ReportService;
using Common;
using Common.TableDef;
using FileStore;
using QSP.BarcodeWriter;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for CampaignConfirmationAgreementReport.
	/// </summary>
	public class SummaryFormReport : Report
	{
		private const int BARCODE_WIDTH = 400;
		private const int BARCODE_FONT_SIZE = 10;

		private SummaryReportsCollection selectedSummaryReports = new SummaryReportsCollection();
		private IEnumerator selectedReportEnumerator;

		public SummaryFormReport() : base() { }

		protected override string ReportName
		{
			get
			{
				string reportName = String.Empty;

				if(selectedReportEnumerator != null && selectedReportEnumerator.Current != null) 
				{
					reportName = selectedReportEnumerator.Current.ToString();
				}

				return reportName;
			}
		}

		public ParameterFieldReference ICampaignIDParameter 
		{
			get 
			{
				return ReportParameters[0];
			}
		}

		public ParameterFieldReference sBarcodeURLParameter 
		{
			get 
			{
				return ReportParameters[1];
			}
		}

		private string BarcodeTemporaryImagesPath
		{
			get 
			{
				string barcodeTemporaryImagesPath = String.Empty;

				try 
				{
					barcodeTemporaryImagesPath = System.Configuration.ConfigurationSettings.AppSettings["BarcodeTemporaryImagesPath"];
				} 
				catch { }

				return barcodeTemporaryImagesPath;
			}
		}

		private string BarcodeTemporaryImagesURL
		{
			get 
			{
                /*string uri = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                int indexOfSlash = uri.IndexOf('/', 10);
                indexOfSlash = (indexOfSlash == -1 ? uri.Length : indexOfSlash);
                string uriBase = uri.Substring(0, indexOfSlash);*/

                string uriPath = System.Configuration.ConfigurationSettings.AppSettings["BarcodeTemporaryImagesURL"];

                //string barcodeTemporaryImagesURL = uriBase + uriPath;
                string barcodeTemporaryImagesURL = uriPath; 

                return barcodeTemporaryImagesURL;
			}
		}

		protected override void InitializeReportParameters()
		{
			ReportParameters = new ParameterFieldReference[2];

			ReportParameters[0] = new ParameterFieldReference();
			ReportParameters[0].ParameterName = "ICampaignID";

			ReportParameters[1] = new ParameterFieldReference();
			ReportParameters[1].ParameterName = "sBarcodeURL";
		}

		public override byte[] Generate(System.Data.DataRow row)
		{
			CampaignDataSet.CampaignRow rowCampaign;
			Business.Objects.CampaignProgram campaignProgram;
			SummaryReports selectedReport;
			BarcodeWriter barcodeWriter = new BarcodeWriter();
			string barcodeText = String.Empty;

			byte[] pdfReport = null;
			byte[] mergedCopies = null;
			string mergedCopiesFileName;
			PDFStore pdfStore = null;

			try 
			{
				rowCampaign = (CampaignDataSet.CampaignRow) row;
				campaignProgram = new Business.Objects.CampaignProgram(rowCampaign.ID);
				selectedSummaryReports = SummaryFormsReportFactory.Instance.GetSummaryReports(campaignProgram);

				pdfStore = new PDFStore();

				if(selectedSummaryReports.Count > 0) 
				{
					barcodeText = DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + rowCampaign.ID.ToString().PadLeft(6, '0');
					barcodeWriter.Text = barcodeText;
					barcodeWriter.Width = BARCODE_WIDTH;
					barcodeWriter.FontSize = BARCODE_FONT_SIZE;
					barcodeWriter.SaveBarcodeImageToDisk(BarcodeTemporaryImagesPath + barcodeText + ".jpg", true);
					AddBarcodeURLColumn((CampaignDataSet) row.Table.DataSet, barcodeText + ".jpg");

					selectedReportEnumerator = selectedSummaryReports.GetEnumerator();

					while(selectedReportEnumerator.MoveNext()) 
					{
						selectedReport = (SummaryReports) selectedReportEnumerator.Current;

						if(selectedReport != SummaryReports.None) 
						{
							pdfReport = base.Generate(row);

							pdfStore.Add(selectedReport.ToString() + "1.pdf", pdfReport);

                     if (campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
   							pdfStore.Add(selectedReport.ToString() + "2.pdf", pdfReport);
						}
					}

					barcodeWriter.DeleteBarcodeImageFromDisk(BarcodeTemporaryImagesPath + barcodeText + ".jpg");
					mergedCopiesFileName = pdfStore.Merge();

					if(mergedCopiesFileName != String.Empty) 
					{
						mergedCopies = pdfStore.Get(mergedCopiesFileName);
					} 
					else 
					{
						mergedCopies = new byte[0];
					}

					pdfStore.Close();
				}
			} 
			catch(Exception ex) 
			{
				if(pdfStore != null) 
				{
					pdfStore.Close();
				}

				throw ex;
			}

			return mergedCopies;
		}

		private void AddBarcodeURLColumn(CampaignDataSet campaignDataSet, string barcodeFileName) 
		{
			if(!campaignDataSet.Campaign.Columns.Contains("sBarcodeURL")) 
			{
				campaignDataSet.Campaign.Columns.Add("sBarcodeURL", typeof(string));
			}

			foreach(DataRow row in campaignDataSet.Campaign) 
			{
				row["sBarcodeURL"] = BarcodeTemporaryImagesURL + barcodeFileName;
			}

			sBarcodeURLParameter.FieldAlias = "sBarcodeURL";
		}
	}
}
