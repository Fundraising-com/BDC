// ConnectionFactoryWizard.cs: Shawn Wildermuth [swildermuth@adoguy.com]

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EnvDTE;

namespace QSP.CommonObjects
{
	/// <summary>
	/// Summary description for ConnectionFactoryWizard.
	/// </summary>
	public class ConnectionFactoryWizard : IAGWizard
	{
    public void Execute(DTE dte, ref object[] ContextParams, string projectType, ref EnvDTE.wizardResult retval)
    {
      ConnFactoryWizardDialog dlg = new ConnFactoryWizardDialog(((string)ContextParams[4]).Substring(0, ((string)ContextParams[4]).IndexOf(".")));
      
      // Show the Dialog
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        // Set the result
        retval = wizardResult.wizardResultSuccess;
      }    
    }
	}
}
