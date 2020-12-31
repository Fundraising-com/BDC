// QSPDataSetGenerator.cs: Benoit Nadon

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using VSInterfaces;

/*
 * TODO:
 *       Add ability to add commenting
 */

namespace QSP.CommonObjects
{
  public abstract class QSPDataSetGenerator
  {
    public string GenerateCode(string file, string input, string fileNamespace) 
    {
      // Create the DataSet
      DataSet dataSet = new DataSet();
      
      // Set the XSD into the DataSet
      StringReader rdr = new StringReader(input);
      dataSet.ReadXmlSchema(rdr);

      // Create the Writer to get the output
      StringWriter writer = new StringWriter();

      // Get ICodeGenerator
      ICodeGenerator gen = _Provider.CreateGenerator(writer);

      // Create a Namespace
      CodeNamespace ns = new CodeNamespace(fileNamespace);

      // Run the Generator
      PowerDataSetGenerator generator = new PowerDataSetGenerator();
      generator.Generate(dataSet, ns, gen);
      
      // Create the CompileUnit
      CodeCompileUnit unit = new CodeCompileUnit();
      unit.Namespaces.Add(ns);
      unit.ReferencedAssemblies.Add("System.dll");
      unit.ReferencedAssemblies.Add("System.Data.dll");

      // Take compile Unit and make the code
      gen.GenerateCodeFromCompileUnit(unit, writer, null);

      // Fix the generated Code
      return writer.ToString();

    }

    protected CodeDomProvider _Provider = null;
  }

  public class CSharpQSPDataSetGenerator : QSPDataSetGenerator
  {
    public CSharpQSPDataSetGenerator()
    {
      _Provider = new CSharpCodeProvider();
    }

  }

  public class VBNETQSPDataSetGenerator : QSPDataSetGenerator
  {
    public VBNETQSPDataSetGenerator()
    {
      _Provider = new VBCodeProvider();
    }
  }
        
  [Guid("3C13044D-394D-45cd-89FF-51C885BFBCDF")]
  public class VsCSharpQSPDataSetGenerator : VsGenerator
  {   
    protected override string GetExtension() { return ".cs"; }

    protected override byte[] GenerateCode(string fileName, string fileContents)
    {
      string code = "";

      try
      {
        XmlDocument xsd = new XmlDocument();
        xsd.LoadXml(fileContents);
        XmlNode elementNode = xsd.DocumentElement["xs:element"];
        code = (new CSharpQSPDataSetGenerator()).GenerateCode(fileName, fileContents, this.codeFileNameSpace);
      }
      catch( Exception e )
      {
        code = "***ERROR***\n" + e.Message;
        this.codeGeneratorProgress.GeneratorError(false, 1, e.Message, 0, 0);
      }

      return System.Text.Encoding.ASCII.GetBytes(code);
    }
  }

  [Guid("87BF1B1C-E1DF-4f24-A07E-2BD9B8CAD316")]
  public class VsVBNETQSPDataSetGenerator : VsGenerator
  {
    protected override string GetExtension() { return ".vb"; }

    protected override byte[] GenerateCode(string fileName, string fileContents)
    {
      string code = "";

      try
      {
        code = (new VBNETQSPDataSetGenerator()).GenerateCode(fileName, fileContents, this.codeFileNameSpace);
      }
      catch( Exception e )
      {
        code = "***ERROR***\n" + e.Message;
        this.codeGeneratorProgress.GeneratorError(false, 1, e.Message, 0, 0);
      }

      return System.Text.Encoding.ASCII.GetBytes(code);
    }
  }

}
