// ImprovedDataSetGenerator.cs: Shawn Wildermuth [swildermuth@adoguy.com]

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

namespace QSP.CommonObjects
{
	[Guid("F4BC18E2-FB02-4379-A393-EFE554735333")]
	public class VsCSharpImprovedDataSetGenerator : VsGenerator
	{   
		protected override string GetExtension() { return ".cs"; }
#if DEBUG
    public string GenerateCode(string fileName)
    {
      FileStream strm = File.Open(fileName, FileMode.Open);
      byte[] buffer = new byte[strm.Length + 1];
      strm.Position = 0;
      strm.Read(buffer, 0, Convert.ToInt32(strm.Length + 1));
      String fileContents = System.Text.Encoding.ASCII.GetString(buffer);
      this.codeFileNameSpace = "TestDSGen";
      return System.Text.Encoding.ASCII.GetString(GenerateCode(fileName, fileContents));
    }
#endif
		protected override byte[] GenerateCode(string fileName, string fileContents)
		{
			string code = "";

			try
			{
				XmlDocument xsd = new XmlDocument();
				xsd.LoadXml(fileContents);
				XmlNode elementNode = xsd.DocumentElement["xs:element"];
        code = TypedDataSetFileGenerator.GenerateCode(new CSharpCodeProvider(), fileName, fileContents, this.codeFileNameSpace, elementNode);
			}
			catch( Exception e )
			{
				code = "***ERROR***\n" + e.Message;
				this.codeGeneratorProgress.GeneratorError(false, 1, e.Message, 0, 0);
			}

			return System.Text.Encoding.ASCII.GetBytes(code);
		}
	}

	[Guid("0E6D01A4-020D-4049-B382-59F465B020B3")]
  public class VsVBNETImprovedDataSetGenerator : VsGenerator
	{
		protected override string GetExtension() { return ".vb"; }

		protected override byte[] GenerateCode(string fileName, string fileContents)
		{
			string code = "";

			try
			{
				XmlDocument xsd = new XmlDocument();
				xsd.LoadXml(fileContents);
				XmlNode elementNode = xsd.DocumentElement["xs:element"];
        code = TypedDataSetFileGenerator.GenerateCode(new VBCodeProvider(), fileName, fileContents, this.codeFileNameSpace, elementNode);
			}
			catch( Exception e )
			{
				code = "***ERROR***\n" + e.Message;
				this.codeGeneratorProgress.GeneratorError(false, 1, e.Message, 0, 0);
			}

			return System.Text.Encoding.ASCII.GetBytes(code);
		}
	}

  internal sealed class TypedDataSetFileGenerator
  {
    private TypedDataSetFileGenerator() {}

    internal static string GenerateCode(CodeDomProvider provider, string file, string input, string fileNamespace, XmlNode elementNode) 
    {
      // Create the DataSet
      DataSet dataSet = new DataSet();
      
      // Set the XSD into the DataSet
      StringReader rdr = new StringReader(input);
      dataSet.ReadXmlSchema(rdr);

      // Create the Writer to get the output
      StringWriter writer = new StringWriter();
      
      // Get ICodeGenerator
      ICodeGenerator gen = provider.CreateGenerator(writer);

      // Set Namespace
      CodeNamespace ns = new CodeNamespace(fileNamespace);

      // Run the Generator
      ImprovedTypedDataSetGenerator typedDSGen = new PowerDataSetGenerator() as ImprovedTypedDataSetGenerator;
      typedDSGen.Generate(dataSet, ns, gen);
      
      // Create the CompileUnit
      CodeCompileUnit unit = new CodeCompileUnit();
      unit.Namespaces.Add(ns);
      unit.ReferencedAssemblies.Add("System.dll");
      unit.ReferencedAssemblies.Add("System.Data.dll");

      // Take compile Unit and make the code
      CodeGeneratorOptions options = new CodeGeneratorOptions();
      options.IndentString = "  ";
      gen.GenerateCodeFromCompileUnit(unit, writer, options);

      // Fix the generated Code
      return writer.ToString();
    }
  }
}
