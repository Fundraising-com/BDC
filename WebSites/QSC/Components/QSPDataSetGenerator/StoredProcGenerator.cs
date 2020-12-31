// StoredProcGenerator.cs: Shawn Wildermuth [swildermuth@adoguy.com]

using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace QSP.CommonObjects
{
  [ComVisible(false)]
  public abstract class StoredProcGenerator : GeneratorBase
  {
    protected EnvDTE.ProjectItems _items;
    protected OleDbCommand        _cmd;
    protected CodeDomProvider     _prov;
    protected ICodeGenerator      _generator;
    protected ManagedProviderType _dbLibType = ManagedProviderType.Invalid;

    public StoredProcGenerator(EnvDTE.ProjectItems items, OleDbCommand cmd)
    {
      _items = items;
      _cmd = cmd;
    }

    public ManagedProviderType dbType
    {
      set
      {
        _dbLibType = value;
      }
    }

    public void Generate(string ns, string classname, string fileName)
    {
      // Make sure we have a DB Type
      System.Diagnostics.Debug.Assert(_dbLibType != ManagedProviderType.Invalid, "DB Type has not been set yet!");

      // Create the new File
      TextWriter writer = new IndentedTextWriter(new StreamWriter(fileName), "  ");
      CodeGeneratorOptions  opts = new CodeGeneratorOptions();
      opts.BlankLinesBetweenMembers = true;
      opts.BracingStyle = "C";

      // Generate the comments at the beginning of the file
      string justFileName = fileName.Substring(fileName.LastIndexOf("\\"));
      string comments = string.Format(@"{0} - Stored Procedure Wrapper for {1}
 File Generated with the ADOGuy's Stored Procedure Wrapper Wizard
 http://adoguy.com
 For more information, contact Shawn Wildermuth (swildermuth@adoguy.com)", justFileName, _cmd.CommandText);
      _generator.GenerateCodeFromStatement(Comment(comments), writer, opts);
      writer.WriteLine("");

      // Generate the main namespace and class
      CodeNamespace codeNamespace = new System.CodeDom.CodeNamespace(ns);
      codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
      codeNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
      codeNamespace.Imports.Add(new CodeNamespaceImport(GetImportName()));
      codeNamespace.Imports.Add(new CodeNamespaceImport("System.Xml"));
      codeNamespace.Types.Add(GenerateClass(classname));
      
      // Write the Namespace
      _generator.GenerateCodeFromNamespace(codeNamespace, writer,  opts);
      
      // Close the text file
      writer.Flush();
      writer.Close();

      // Add it to the Project
      _items.AddFromFile(fileName);

      // Open it
      _items.DTE.ItemOperations.OpenFile(fileName, "");
    }


    private CodeTypeDeclaration GenerateClass(string classname)
    {
      CodeTypeDeclaration cls = new CodeTypeDeclaration(classname);
      cls.BaseTypes.Add("MarshalByRefObject");
      cls.BaseTypes.Add("IDisposable");
      AddCodeComments(cls.Comments, classname + " Stored Procedure Wrapper");
      cls.IsClass = true;
      
      // Create the First Constructor
      CodeConstructor ctor = new CodeConstructor();
      ctor.Attributes = MemberAttributes.Public;
      ctor.Parameters.Add(ParameterDecl(GetConnectionTypeName(), "conn"));
      ctor.Statements.Add(MethodCall(This(), "ConstructCommand", new CodeExpression[0]));
      ctor.Statements.Add(Assign(Property(Field(This(), "_cmd"), "Connection"), Variable("conn")));
      AddCodeComments(ctor.Comments, "Public Constructor");
      cls.Members.Add(ctor);

      // Add Command Property
      CodeMemberProperty command = PropertyDecl(GetCommandTypeName(), "Command", MemberAttributes.Public| MemberAttributes.Final);
      command.HasSet = false;
      command.HasGet = true;
      command.GetStatements.Add(Return(Field(This(), "_cmd")));
      AddCodeComments(command.Comments, "Command Property");
      cls.Members.Add(command);

      // Add IDispoable Interface
      CodeMemberMethod dispose = MethodDecl("System.Void", "Dispose", MemberAttributes.Public | MemberAttributes.Final);
      dispose.ImplementationTypes.Add("System.IDisposable");
      AddCodeComments(dispose.Comments, "Implementation of IDispose");
      dispose.Statements.Add(MethodCall(Field(This(), "_cmd"), "Dispose", new CodeExpression[0]));
      cls.Members.Add(dispose);

      // Add Parameters
      for (int x = 0; x < _cmd.Parameters.Count; ++x)
      {
        cls.Members.Add(MakeParamProp(_cmd.Parameters[x], x));
      }

      // Add Command Field
      CodeMemberField cmdfield = FieldDecl(GetCommandTypeName(), "_cmd");
      AddCodeComments(cmdfield.Comments, "Command Object");
      cls.Members.Add(cmdfield);

      // Add ConstructCommand Method
      CodeMemberMethod meth = MethodDecl("System.Void", "ConstructCommand", MemberAttributes.Public | MemberAttributes.Final);
      AddCodeComments(meth.Comments, "ConstructCommand Method");
      meth.Statements.Add(Assign(Field(This(), "_cmd"), New(GetCommandTypeName(), new CodeExpression[] { Str(_cmd.CommandText) })));
      meth.Statements.Add(Assign(Field(Field(This(), "_cmd"), "CommandType"), Field(TypeExpr("CommandType"), "StoredProcedure")));
      for (int x = 0; x < _cmd.Parameters.Count; ++x)
      {
        meth.Statements.AddRange(MakeParamCreate(_cmd.Parameters[x], x));
      }
      cls.Members.Add(meth);

      return cls;
    }

    private CodeStatementCollection MakeParamCreate(OleDbParameter p, int ordinal)
    {

      // Create the Collection
      CodeStatementCollection coll = new CodeStatementCollection();

      // Create Statements
      coll.Add(Comment(p.ParameterName + "Parameter"));
      coll.Add(VariableDecl(GetParameterTypeName(), "_" + p.ParameterName, New(GetParameterTypeName(), new CodeExpression[] {})));
      coll.Add(Assign(Field(Variable("_" + p.ParameterName), "ParameterName"), Str(p.ParameterName)));
      coll.Add(Assign(Field(Variable("_" + p.ParameterName), "DbType"), Field(TypeExpr("DbType"), p.DbType.ToString())));
      coll.Add(Assign(Field(Variable("_" + p.ParameterName), "Direction"), Field(TypeExpr("ParameterDirection"), p.Direction.ToString())));
      coll.Add(Assign(Field(Variable("_" + p.ParameterName), "SourceVersion"), Field(TypeExpr("DataRowVersion"), "Current")));
      coll.Add(MethodCall(Field(Field(This(),"_cmd"), "Parameters"), "Add", Variable("_" + p.ParameterName)));

      return coll;
    }

    private CodeTypeMember MakeParamProp(OleDbParameter p, int ordinal)
    {
      // Create Property
      CodeMemberProperty prop = PropertyDecl(GetTypeNameFromDb(p.OleDbType), p.ParameterName, MemberAttributes.Public | MemberAttributes.Final);
      
      // Add Comments
      AddCodeComments(prop.Comments, p.ParameterName + " Parameter Property");

      // Create Gets
      if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.ReturnValue)
      {
        prop.HasGet = true;
        prop.GetStatements.Add(Return(Cast(this.GetTypeNameFromDb(p.OleDbType), GetParameterValue(p, ordinal))));
      }

      // Create Sets
      if (p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput)
      {
        prop.HasSet = true;
        prop.SetStatements.Add(Assign(GetParameterValue(p, ordinal), Value()));
      }

      return prop;
    }

    private CodeExpression GetParameterValue(OleDbParameter p, int ordinal)
    {
      return Field(Indexer(Property(Property(This(), "_cmd"), "Parameters"), Primitive(ordinal)), "Value");
    }
    
    private string GetCommandTypeName()
    {
      switch (_dbLibType)
      {
        case ManagedProviderType.ODBC:
          return GetODBCTypeName("Command");
        case ManagedProviderType.OLEDB:
          return typeof(OleDbCommand).FullName;
        case ManagedProviderType.Oracle:
          return GetOracleTypeName("Command");
        case ManagedProviderType.SQLServer:
          return typeof(SqlCommand).FullName;
        default:
          return "BadType";
      }
      
    }

    private string GetConnectionTypeName()
    {
      switch (_dbLibType)
      {
        case ManagedProviderType.ODBC:
          return GetODBCTypeName("Connection");
        case ManagedProviderType.OLEDB:
          return typeof(OleDbConnection).FullName;
        case ManagedProviderType.Oracle:
          return GetOracleTypeName("Connection");
        case ManagedProviderType.SQLServer:
          return typeof(SqlConnection).FullName;
        default:
          return "BadType";
      }
    }

    private string GetParameterTypeName()
    {
      switch (_dbLibType)
      {
        case ManagedProviderType.ODBC:
          return GetODBCTypeName("Parameter");
        case ManagedProviderType.OLEDB:
          return typeof(OleDbParameter).FullName;
        case ManagedProviderType.Oracle:
          return GetOracleTypeName("Parameter");
        case ManagedProviderType.SQLServer:
          return typeof(SqlParameter).FullName;
        default:
          return "BadType";
      }
    }

    private void AddCodeComments(CodeCommentStatementCollection comments,string comment)
    {
      if (this is CSStoredProcGenerator)
      {
        comments.Add(new CodeCommentStatement("<summary>", true));
        comments.Add(new CodeCommentStatement(comment, true));
        comments.Add(new CodeCommentStatement("</summary>", true));
      }
      else
      {
        comments.Add(new CodeCommentStatement(comment, false));
      }
    }

    private string GetImportName()
    {
      switch (_dbLibType)
      {
        case ManagedProviderType.ODBC:
        {
          Assembly data = Assembly.LoadWithPartialName("System.Data");
//          if (data.ImageRuntimeVersion == "v1.1.4322")
//            return "System.Data.Odbc";
//          else
            return "Microsoft.Data.Odbc";
        }
        case ManagedProviderType.OLEDB:
          return "System.Data.OleDb";
        case ManagedProviderType.Oracle:
          return "System.Data.OracleClient";
        case ManagedProviderType.SQLServer:
          return "System.Data.SqlClient";
        default:
          return "";
      }
    }

    private string GetOracleTypeName(string cls)
    {
      return "System.Data.OracleClient.Oracle" + cls;
    }

    private string GetODBCTypeName(string cls)
    {
      return "Microsoft.Data.Odbc.Odbc" + cls;
    }

    private string GetTypeNameFromDb(OleDbType type)
    {
      return GetTypeFromDb(type).Name;
    }

  }


}
