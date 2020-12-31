// WizardRouter.cs: Shawn Wildermuth [swildermuth@adoguy.com]

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EnvDTE;

namespace QSP.CommonObjects
{

  [GuidAttribute("7D1D23DD-C2F0-4cac-B3FF-FF183E9F2158")]
  public class WizardRouter : IDTWizard
  {
    public void Execute(object Application, int hwndOwner, ref object[] ContextParams, ref object[] CustomParams, ref EnvDTE.wizardResult retval)
    {

      string wizardName = (string) CustomParams[0];
      IAGWizard wizard;

      // Route the Wizard
      if (wizardName.ToUpper() == "STOREDPROCEDUREWIZARD")
      {
        wizard = new StoredProcWizard() as IAGWizard;
      }
      else if (wizardName.ToUpper() == "CONNECTIONFACTORYWIZARD")
      {
        wizard = new ConnectionFactoryWizard() as IAGWizard;
      }
      else if (wizardName.ToUpper() == "TYPEDDATAREADERWIZARD")
      {
        wizard = new TypedDataReaderWizard() as IAGWizard;
      }
      else
      {
        throw new Exception("Unknown Wizard Name");
      }

      // Run the Wizard
      wizard.Execute((DTE) Application, ref ContextParams, ((string)CustomParams[1]), ref retval);
    }
  }

  public interface IAGWizard
  {
    void Execute(DTE dte, ref object[] ContextParams, string projectType, ref EnvDTE.wizardResult retval);
  }
}
