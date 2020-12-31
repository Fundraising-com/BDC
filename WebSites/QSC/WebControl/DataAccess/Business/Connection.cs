using System;
using QSP.WebControl.DataAccess.Data;

namespace QSP.WebControl.DataAccess.Business
{
	/// <summary>
	/// Summary description for Connection.
	/// </summary>
	public abstract class Connection
	{
		
		public static string ConnnectionString
		{
			set{DBInteractionBase.ConnnectionString=value;}
		}
	}
}
