using System;
using System.Net; //for Cookie
using System.Configuration; //For Web.Config
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Protocols;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Interfaces;
using Business.Objects;
using Business.ReportExecution;
//using Microsoft.Samples.ReportingServices.CustomSecurity;



namespace QSPFulfillment.Finance
{
	#region WinUtils Class
	public class WinUtils
	{
		public const UInt32 PD_RETURNDC = 0x00000100;
		public const UInt32 PD_RETURNDEFAULT = 0x00000400;

		[StructLayout(LayoutKind.Explicit, CharSet=CharSet.Auto)] 
			public struct PRINTDLG
		{
			[FieldOffset(0)] public UInt32 lStructSize;
			[FieldOffset(4)] public IntPtr hwndOwner;
			[FieldOffset(8)] public IntPtr hDevMode;
			[FieldOffset(12)] public IntPtr hDevNames;
			[FieldOffset(16)] public IntPtr hDC;
			[FieldOffset(20)] public UInt32 Flags;
			[FieldOffset(24)] public UInt16 nFromPage;
			[FieldOffset(26)] public UInt16 nToPage;
			[FieldOffset(28)] public UInt16 nMinPage;
			[FieldOffset(30)] public UInt16 nMaxPage;
			[FieldOffset(32)] public UInt16 nCopies;
			[FieldOffset(34)] public IntPtr hInstance;
			[FieldOffset(38)] public IntPtr lCustData;
			[FieldOffset(42)] public IntPtr lpfnPrintHook;
			[FieldOffset(46)] public IntPtr lpfnSetupHook;
			[FieldOffset(50)] public IntPtr lpPrintTemplateName;
			[FieldOffset(54)] public IntPtr lpSetupTemplateName;
			[FieldOffset(58)] public IntPtr hPrintTemplate;
			[FieldOffset(62)] public IntPtr hSetupTemplate;
		}

		[DllImport("comdlg32")]
		public static extern int PrintDlg(ref PRINTDLG lppd);

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)] 
			public struct RECT
		{
			public Int32 left;
			public Int32 top;
			public Int32 right;
			public Int32 bottom;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)] 
			public struct DOCINFO
		{
			public int cbSize;
			public string lpszDocName;
			public string lpszOutput;
			public string lpszDatatype;
			public UInt32 fwType;
		}

		[DllImport("gdi32")]
		public static extern int StartDoc(IntPtr hdc, [In] ref DOCINFO lpdi);
		[DllImport("gdi32")]
		public static extern int StartPage(IntPtr hDC);
		[DllImport("gdi32")]
		public static extern int EndDoc(IntPtr hDC);
		[DllImport("gdi32")]
		public static extern int EndPage(IntPtr hDC);
		[DllImport("gdi32")]
		public static extern bool TextOut(IntPtr hDC, int nXStart, int nYStart,  string lpString, int cbString);
		[DllImport("gdi32")]
		public static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
		[DllImport("gdi32")]
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);
		[DllImport("gdi32")]
		public static extern bool DeleteDC(IntPtr hdc);

		// This plays EMF into specified rectangle.
		[DllImport("gdi32")]
		public static extern bool PlayEnhMetaFile(IntPtr hdc, IntPtr hemf, [In] ref RECT lpRect);

		// This plays EMF at original size.
		[DllImport("gdi32",EntryPoint="PlayEnhMetaFile")]
		public static extern bool PlayEnhMetaFileNULL(IntPtr hdc, IntPtr hemf, IntPtr lpRect);
		
		//Error information.
		[DllImport("kernel32.dll")]
		public static extern Int32 GetLastError();

		[DllImport("Kernel32")]
		public static extern IntPtr GlobalLock(IntPtr n); 

		[DllImport("Kernel32")]
		public static extern void GlobalUnlock(IntPtr n); 

		[DllImport("Winspool.drv",EntryPoint="AdvancedDocumentPropertiesW")]
		public static extern int AdvancedDocumentPropertiesW(IntPtr hWnd, 
			IntPtr hPrinter,
			String deviceName, 
			IntPtr output, 
			IntPtr input); 
			

		[DllImport("Winspool.drv",EntryPoint="OpenPrinter")]
		public static extern bool OpenPrinterW(String printerName, 
			out IntPtr printerHandle, //Containing an IntPtr
			long defaults);  //Pass Null

		[DllImport("Winspool.drv",EntryPoint="ClosePrinter")]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		// This copies the EMF to a file. 
		[DllImport("gdi32")]
		public static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, string lpszFile);

		// Deletes a Win32 EMF.
		[DllImport("gdi32")]
		public static extern bool DeleteEnhMetaFile(IntPtr hemf);

		//Rectangle
		public const int HORZRES = 0x00000008;
		public const int VERTRES = 0x0000000A;
		[DllImport("gdi32")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
	}
	

	#endregion

	#region PrintInvoice Class
		public partial class PrintInvoice : QSPFulfillment.CommonWeb.QSPPage
		{
			RSClient rs = new RSClient();
			byte[] m_renderedReport;
			Graphics.EnumerateMetafileProc m_delegate = null;
			MemoryStream m_currentPageStream;
			Metafile m_metafile = null;
			int	m_numberOfPages;
			int	m_currentPrintingPage;
			int	m_lastPrintingPage;
			public string m_reportName;
						
			#region PrintInvoice Constructor
			public PrintInvoice()
			{
			}
			#endregion

			#region Page_Load
				protected void Page_Load(object sender, System.EventArgs e)
				{}
			#endregion

			#region RenderReport
			public byte[] RenderReport(string reportPath, string strParamName, string strParamValue)
			{
				//set the params
				ParameterValue[] parameters = new ParameterValue[1];
				parameters[0] = new ParameterValue();
				parameters[0].Name  = strParamName.Trim(); 
				parameters[0].Value = strParamValue.Trim();

                return rs.GenerateReportStream(reportPath, "IMAGE", parameters, 99999);
			}
			#endregion

			#region PrintReport
				public bool PrintReport(string printerName, string reportName, string strParamName, string strParamValue)
				{
					this.RenderedReport = this.RenderReport(reportName, strParamName, strParamValue);

					try
					{
						// Wait for the report to completely render.
						if(m_numberOfPages < 1)
							return false;
						
						WinUtils.DOCINFO di;
						IntPtr hDC = WinUtils.CreateDC("winspool", printerName, IntPtr.Zero, IntPtr.Zero);
			
						//Rectangle
						WinUtils.RECT rect;
						rect.top = 0;
						rect.left = 0;
						rect.right = WinUtils.GetDeviceCaps(hDC, WinUtils.HORZRES);
						rect.bottom = WinUtils.GetDeviceCaps(hDC, WinUtils.VERTRES);

						// 
						if (hDC != IntPtr.Zero)
						{
							// Start the document
							di = new WinUtils.DOCINFO();
							di.cbSize = Marshal.SizeOf(typeof(WinUtils.DOCINFO));
							di.fwType = 0;
							di.lpszDocName = "X";
							WinUtils.StartDoc(hDC, ref di);

							m_currentPrintingPage	= 1;
							m_lastPrintingPage		= m_numberOfPages;

							bool b;
							PrintPageEventHandler ev = new PrintPageEventHandler(this.pd_PrintPage);
							Int32 dwErr;

							//Loop here	until no more pages from report						
							while (m_currentPrintingPage <= m_lastPrintingPage && MoveToPage(m_currentPrintingPage))
							{
								//We need to get a Win32 metafile handle corresponding to the metafile (m_metafile).  
								//To do this, we are given a simple method in the managed Metafile class called GetHenhmetafile.
								//This new variable (hEmf) represents the metafile in the Win32 world.  
								//We then can play it when we are printing from Win32. 
								IntPtr hEmf = m_metafile.GetHenhmetafile();
								
								WinUtils.StartPage(hDC);
																
								b = WinUtils.PlayEnhMetaFile(hDC, hEmf, ref rect);

								if (b == false)
								{
									dwErr = WinUtils.GetLastError();
									Context.Response.Write("Error = " + dwErr);
									return false;
								}
								
								WinUtils.EndPage(hDC);
								++m_currentPrintingPage;
								
							}//End Loop					

							// End the document
							WinUtils.EndDoc(hDC);
							WinUtils.DeleteDC(hDC);
						}

					}
					catch(Exception ex)
					{
						Context.Response.Write("<br> Message = " + ex.Message + "<br> Type = " + ex.GetType().ToString() + "<br>InnerException = " + ex.InnerException + "<br> Source = " + ex.Source + "<br>StackTrace= " + ex.StackTrace);
						return false;
					}
					return true;
				}
			#endregion PrintReport

			#region pd_PrintPage
			private void pd_PrintPage(object sender, PrintPageEventArgs ev)
			{
				ev.HasMorePages = false;
				if (m_currentPrintingPage <= m_lastPrintingPage && MoveToPage(m_currentPrintingPage))
				{
					// Draw the page
					ReportDrawPage(ev.Graphics);
					// If the next page is less than or equal to the last page, print another page.
					if (++m_currentPrintingPage <= m_lastPrintingPage)
						ev.HasMorePages = true;
				}
			}
			#endregion

			#region ReportDrawPage
			// Method to draw the current emf memory stream
			private void ReportDrawPage(Graphics g)
			{
				if(null == m_currentPageStream || 0 == m_currentPageStream.Length || null ==m_metafile)
					return;
				lock(this)
				{
					// Set the metafile delegate.
					int width = m_metafile.Width;
					int height= m_metafile.Height;
					m_delegate = new Graphics.EnumerateMetafileProc(MetafileCallback);
					// Draw in the rectangle
					Point[] points		= new Point[3];
					Point destPoint		= new Point(0, 0);
					Point destPoint1	= new Point(width, 0);
					Point destPoint2	= new Point(0, height);
					points[0]=destPoint;
					points[1]=destPoint1;
					points[2]=destPoint2;
					g.EnumerateMetafile(m_metafile,points, m_delegate);
					// Clean up
					m_delegate = null;
				}
			}
			#endregion

			#region MoveToPage
			private bool MoveToPage(Int32 page)
			{
				// Check to make sure that the current page exists in the array list
				if(null == this.RenderedReport[m_currentPrintingPage-1])
					return false;
				// Set current page stream equal to the rendered page
				m_currentPageStream = new MemoryStream(this.RenderedReport[m_currentPrintingPage-1]);
				// Set its postion to start.
				m_currentPageStream.Position = 0;
				// Initialize the metafile
				if(null != m_metafile)
				{
					m_metafile.Dispose();
					m_metafile = null;
				}
				// Load the metafile image for this page
				m_metafile =  new Metafile((Stream)m_currentPageStream);
				return true;
			}
			#endregion

			#region MetafileCallback
			private bool MetafileCallback(
				EmfPlusRecordType recordType,
				int flags,
				int dataSize,
				IntPtr data,
				PlayRecordCallback callbackData)
			{
				byte[] dataArray = null;
				if (data != IntPtr.Zero)
				{
					// Copy the unmanaged record to a managed byte buffer that can be used by PlayRecord.
					dataArray = new byte[dataSize];
					Marshal.Copy(data, dataArray, 0, dataSize);
				}
				m_metafile.PlayRecord(recordType, flags, dataSize, dataArray);

				return true;
			}
			#endregion

			#region RenderedReport
			public byte[] RenderedReport
			{
				get
				{return m_renderedReport;}
				set
				{m_renderedReport = value;}
			}
			#endregion

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

				}
			#endregion
		}
	#endregion
}

