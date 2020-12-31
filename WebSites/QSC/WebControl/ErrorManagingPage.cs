using System;
using System.Web;
using System.Web.UI;
using System.Threading;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for ErrorManagingPage.
	/// </summary>
	public class ErrorManagingPage : System.Web.UI.Page
	{
		protected override void OnInit(EventArgs e)
		{
			try 
			{
				base.OnInit (e);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected override void OnDataBinding(EventArgs e)
		{
			try 
			{
				base.OnDataBinding (e);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			try 
			{
				base.OnLoad (e);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
		{
			try 
			{
				base.RaisePostBackEvent (sourceControl, eventArgument);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			try 
			{
				base.OnPreRender (e);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
		
		public virtual void ManageError(Exception ex) 
		{
			if(ex is ThreadAbortException) 
			{
				throw ex;
			}

			QSP.WebControl.DataAccess.Common.ApplicationError.ManageError(ex);
		}
	}
}
