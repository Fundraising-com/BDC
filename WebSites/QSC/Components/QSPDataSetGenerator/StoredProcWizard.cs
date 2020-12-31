// StoredProcWizard.cs: Shawn Wildermuth [swildermuth@adoguy.com]
#region Copyright © 2002 Shawn Wildermuth
/* This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the
 * use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software in a
 *    product, an acknowledgment in the product documentation is requested, as
 *    shown here:
 * 
 *    Portions copyright © 2002 Shawn Wildermuth (http://www.adoguy.com/).
 * 
 * 2. No substantial portion of this source code may be redistributed without
 *    the express written permission of the copyright holders, where
 *    "substantial" is defined as enough code to be recognizably from this code.
 */
#endregion

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EnvDTE;

namespace QSP.CommonObjects
{
  /// <summary>
  /// Summary description for StoredProcWizard.
  /// </summary>
  public class StoredProcWizard : IAGWizard
  {

    public void Execute(DTE dte, ref object[] ContextParams, string projectType, ref EnvDTE.wizardResult retval)
    {
      SPWizardDialog dlg = new SPWizardDialog(((string)ContextParams[4]).Substring(0, ((string)ContextParams[4]).IndexOf(".")));
      
      // Show the Dialog
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        // Retreive the Stored Procedure
        OleDbCommand cmd = GetStoredProc(dlg);
      
        // Generate the Code
        CreateWrapper(ContextParams[2] as ProjectItems, cmd, projectType, dlg, string.Format("{0}\\{1}", ContextParams[3], ContextParams[4]));

        // Set the result
        retval = wizardResult.wizardResultSuccess;
      }    
    }

    internal OleDbCommand GetStoredProc(SPWizardDialog dlg)
    {
      OleDbConnection conn = new OleDbConnection(dlg.editConnString.Text);
      OleDbCommand cmd = conn.CreateCommand();
      cmd.CommandText = dlg.editStoredProc.Text;
      cmd.CommandType = CommandType.StoredProcedure;
      try
      {
        conn.Open();
        OleDbCommandBuilder.DeriveParameters(cmd);
      }
      finally
      {
        conn.Close();
      }

      return cmd;
    }

    internal void CreateWrapper(ProjectItems items, OleDbCommand cmd, string projectType, SPWizardDialog dlg, string fileName)
    {
      StoredProcGenerator gen = null;

      if (projectType == "CSPROJ")
      {
        gen = new CSStoredProcGenerator(items, cmd);
      }
      else if (projectType == "VBPROJ")
      {
        gen = new VBStoredProcGenerator(items, cmd);
      }
//      else if (projectType == "JSPROJ")
//      {
//        gen = new JSStoredProcGenerator(items, cmd);
//      }
      gen.dbType = dlg.Provider;
      gen.Generate(dlg.editNamespace.Text, dlg.editClassName.Text, fileName);

    }

  }
}
