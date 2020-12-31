// StoredProcGenerators.cs: Shawn Wildermuth [swildermuth@adoguy.com]

using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using EnvDTE;
using System.Runtime.InteropServices;

namespace QSP.CommonObjects
{
  [ComVisible(false)]
  public class CSStoredProcGenerator : StoredProcGenerator
  {
    public CSStoredProcGenerator(ProjectItems items, OleDbCommand cmd) : base(items, cmd)
    {
      CSharpCodeProvider prov = new CSharpCodeProvider();
      _generator = prov.CreateGenerator();
      _prov = prov;
    }
  }

  [ComVisible(false)]
  public class VBStoredProcGenerator : StoredProcGenerator
  {
    public VBStoredProcGenerator(ProjectItems items, OleDbCommand cmd) : base(items, cmd)
    {
      VBCodeProvider prov = new VBCodeProvider();
      _generator = prov.CreateGenerator();
      _prov = prov;
    }
  }

//  [ComVisible(false)]
//  public class JSStoredProcGenerator : StoredProcGenerator
//  {
//    public JSStoredProcGenerator(ProjectItems items, OleDbCommand cmd) : base(items, cmd)
//    {
//      JScriptCodeProvider prov = new JScriptCodeProvider();
//      _generator = prov.CreateGenerator();
//      _prov = prov;
//    }
//  }
}
