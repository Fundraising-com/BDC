// GeneratorBase.cs: ShawAn Wildermuth [swildermuth@adoguy.com]

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data.OleDb;
using System.Reflection;

namespace QSP.CommonObjects
{
	/// <summary>
	/// Base class for CodeDom Generators that has utility functions to make it easier to build
	/// code.
	/// </summary>
  public abstract class GeneratorBase
  {
    /// <summary>
    /// 
    /// </summary>
    public GeneratorBase()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="addComments"></param>
    public GeneratorBase(bool bAddComments) 
    {
      addComments = bAddComments;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="addComments"></param>
    /// <param name="commentText"></param>
    public GeneratorBase(bool bAddComments, string comments)
    {
      addComments = bAddComments;
      commentText = comments;
    }

    /// <summary>
    /// 
    /// </summary>
    protected bool addComments = false;
    
    /// <summary>
    /// 
    /// </summary>
    protected string commentText = "Empty Comments to avoid no Comment Messages when building XML Documentation";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    protected virtual CodeMemberMethod AddComments(CodeMemberMethod method)
    {
      if (addComments) method.Comments.AddRange(GetComments(string.Format("Method: {0}",method.Name)));
      return method;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prop"></param>
    protected virtual CodeMemberProperty  AddComments(CodeMemberProperty prop)
    {
      if (addComments) prop.Comments.AddRange(GetComments(string.Format("Property: {0}",prop.Name)));
      return prop;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="xtor"></param>
    /// <returns></returns>
    protected virtual CodeConstructor AddComments(CodeConstructor xtor)
    {
      if (addComments) xtor.Comments.AddRange(GetComments());
      return xtor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="field"></param>
    protected virtual CodeMemberField AddComments(CodeMemberField field)
    {
      if (addComments) field.Comments.AddRange(GetComments(string.Format("Field: {0}",field.Name)));
      return field;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="field"></param>
    protected virtual CodeTypeDelegate AddComments(CodeTypeDelegate del)
    {
      if (addComments) del.Comments.AddRange(GetComments(string.Format("Delegate: {0}", del.Name)));
      return del;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    protected virtual CodeTypeDeclaration AddComments(CodeTypeDeclaration type)
    {
      if (addComments) type.Comments.AddRange(GetComments(string.Format("Class: {0}",type.Name)));
      return type;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="?"></param>
    /// <returns></returns>
    protected virtual CodeMemberEvent AddComments(CodeMemberEvent eventDecl)
    {
      if (addComments) eventDecl.Comments.AddRange(GetComments(string.Format("Event: {0}",eventDecl.Name)));
      return eventDecl;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="comments"></param>
    protected virtual CodeCommentStatementCollection GetComments()
    {
      return GetComments("");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual CodeCommentStatementCollection GetComments(string additionalText)
    {
      CodeCommentStatementCollection coll = new CodeCommentStatementCollection();
      coll.Add(new CodeCommentStatement("<summary>", true));
      coll.Add(new CodeCommentStatement(commentText, true));
      if (additionalText.Length > 0) coll.Add(new CodeCommentStatement(additionalText, true));
      coll.Add(new CodeCommentStatement("</summary>", true));
      return coll;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    ///
    protected virtual CodeBinaryOperatorExpression Add(CodeExpression left, CodeExpression right) 
    {
      return BinOperator(left, 0, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeAttributeDeclaration AttributeDecl(string name) 
    {
      return new CodeAttributeDeclaration(name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual CodeConstructor ConstructorDecl()
    {
      return AddComments(new CodeConstructor());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="expr"></param>
    /// <returns></returns>
    protected virtual CodeExpression Cast(CodeTypeReference type, CodeExpression expr) 
    {
      return new CodeCastExpression(type, expr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="op"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    protected virtual CodeBinaryOperatorExpression BinOperator(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right) 
    {
      return new CodeBinaryOperatorExpression(left, op, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="catchStmnt"></param>
    /// <returns></returns>
    protected virtual CodeCatchClause Catch(string type, string name, CodeStatement catchStmnt) 
    {
      CodeCatchClause local0;

      local0 = new CodeCatchClause();
      local0.CatchExceptionType = Type(type);
      local0.LocalName = name;
      local0.Statements.Add(catchStmnt);
      return local0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    protected virtual CodeBinaryOperatorExpression EQ(CodeExpression left, CodeExpression right) 
    {
      return BinOperator(left, CodeBinaryOperatorType.ValueEquality, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    protected virtual CodeExpression Event(string eventName) 
    {
      return new CodeEventReferenceExpression(This(), eventName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeMemberEvent EventDecl(string type, string name) 
    {
      CodeMemberEvent local0;

      local0 = new CodeMemberEvent();
      local0.Name = name;
      local0.Type = Type(type);
      local0.Attributes = MemberAttributes.Public | MemberAttributes.Final;
      return AddComments(local0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetObject"></param>
    /// <param name="par"></param>
    /// <returns></returns>
    protected virtual CodeExpression DelegateCall(CodeExpression targetObject, CodeExpression par) 
    {
      System.CodeDom.CodeExpression[] local0;

      local0 = new CodeExpression[2];
      local0[0] = This();
      local0[1] = par;
      return new CodeDelegateInvokeExpression(targetObject, local0);
    }

    protected virtual CodeTypeDelegate DelegateDecl(string name)
    {
      return AddComments(new CodeTypeDelegate(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual CodeExpression Base() 
    {
      return new CodeBaseReferenceExpression();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual CodeAttributeDeclaration AttributeDecl(string name, CodeExpression value) 
    {
      System.CodeDom.CodeAttributeArgument[] local0;

      local0 = new CodeAttributeArgument[1];
      local0[0] = new CodeAttributeArgument(value);
      return new CodeAttributeDeclaration(name, local0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual CodeExpression This()
    {
      return new CodeThisReferenceExpression();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    protected virtual CodeExpression Argument(string argument) 
    {
      return new CodeArgumentReferenceExpression(argument);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    protected virtual CodeStatement Assign(CodeExpression left, CodeExpression right) 
    {
      return new CodeAssignStatement(left, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="expr"></param>
    /// <returns></returns>
    protected virtual CodeExpression Cast(string type, CodeExpression expr) 
    {
      return new CodeCastExpression(type, expr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    protected virtual CodeStatement Comment(string comment) 
    {
      return new CodeCommentStatement(comment);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exp"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    protected virtual CodeExpression Field(CodeExpression exp, string field) 
    {
      return new CodeFieldReferenceExpression(exp, field);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cond"></param>
    /// <param name="trueStms"></param>
    /// <param name="falseStms"></param>
    /// <returns></returns>
    protected virtual CodeStatement If(CodeExpression cond, System.CodeDom.CodeStatement[] trueStms, System.CodeDom.CodeStatement[] falseStms) 
    {
      return new CodeConditionStatement(cond, trueStms, falseStms);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cond"></param>
    /// <param name="trueStms"></param>
    /// <returns></returns>
    protected virtual CodeStatement If(CodeExpression cond, System.CodeDom.CodeStatement[] trueStms) 
    {
      return new CodeConditionStatement(cond, trueStms);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cond"></param>
    /// <param name="trueStm"></param>
    /// <returns></returns>
    protected virtual CodeStatement If(CodeExpression cond, CodeStatement trueStm) 
    {
      System.CodeDom.CodeStatement[] local0;

      local0 = new CodeStatement[1];
      local0[0] = trueStm;
      return If(cond, local0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cond"></param>
    /// <param name="falseStms"></param>
    /// <returns></returns>
    protected virtual CodeStatement IfNot(CodeExpression cond, System.CodeDom.CodeStatement[] falseStms) 
    {
      System.CodeDom.CodeStatement[] local0;

      local0 = new CodeStatement[0];
      return new CodeConditionStatement(cond, local0, falseStms);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cond"></param>
    /// <param name="falseStm"></param>
    /// <returns></returns>
    protected virtual CodeStatement IfNot(CodeExpression cond, CodeStatement falseStm) 
    {
      System.CodeDom.CodeStatement[] local0;

      local0 = new CodeStatement[1];
      local0[0] = falseStm;
      return IfNot(cond, local0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeNamespaceImport Import(string name)
    {
      return new CodeNamespaceImport(name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeMemberField FieldDecl(string type, string name) 
    {
      return AddComments(new CodeMemberField(type, name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetObject"></param>
    /// <param name="indices"></param>
    /// <returns></returns>
    protected virtual CodeExpression Indexer(CodeExpression targetObject, CodeExpression indices) 
    {
      System.CodeDom.CodeExpression[] local0;

      local0 = new CodeExpression[1];
      local0[0] = indices;
      return new CodeIndexerExpression(targetObject, local0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    protected virtual CodeBinaryOperatorExpression Or(CodeExpression left, CodeExpression right) 
    {
      return BinOperator(left, CodeBinaryOperatorType.BooleanOr, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    protected virtual CodeBinaryOperatorExpression Less(CodeExpression left, CodeExpression right) 
    {
      return BinOperator(left, CodeBinaryOperatorType.LessThan, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    protected virtual CodeMemberMethod Method(CodeTypeReference type, string name, MemberAttributes attributes) 
    {
      CodeMemberMethod local0;

      local0 = new CodeMemberMethod();
      local0.ReturnType = type;
      local0.Name = name;
      local0.Attributes = attributes;
      return AddComments(local0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetObject"></param>
    /// <param name="methodName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    protected virtual CodeExpression MethodCall(CodeExpression targetObject, string methodName, System.CodeDom.CodeExpression[] parameters) 
    {
      return new CodeMethodInvokeExpression(targetObject, methodName, parameters);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetObject"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    protected virtual CodeExpression MethodCall(CodeExpression targetObject, string methodName) 
    {
      return new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[0]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetObject"></param>
    /// <param name="methodName"></param>
    /// <param name="par"></param>
    /// <returns></returns>
    protected virtual CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression par) 
    {
      System.CodeDom.CodeExpression[] local0;

      local0 = new CodeExpression[1];
      local0[0] = par;
      return new CodeMethodInvokeExpression(targetObject, methodName, local0);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    protected virtual CodeMemberMethod MethodDecl(string type, string name, MemberAttributes attributes) 
    {
      return AddComments(Method(Type(type), name, attributes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeParameterDeclarationExpression ParameterDecl(string type, string name) 
    {
      return new CodeParameterDeclarationExpression(type, name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeParameterDeclarationExpression ParameterDecl(Type type, string name) 
    {
      return new CodeParameterDeclarationExpression(type, name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    protected virtual CodeExpression Variable(string variable) 
    {
      return new CodeVariableReferenceExpression(variable);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual CodeStatement VariableDecl(string type, string name) 
    {
      return new CodeVariableDeclarationStatement(type, name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="initExpr"></param>
    /// <returns></returns>
    protected virtual CodeStatement VariableDecl(string type, string name, CodeExpression initExpr) 
    {
      return new CodeVariableDeclarationStatement(type, name, initExpr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="initExpr"></param>
    /// <returns></returns>
    protected virtual CodeStatement VariableDecl(Type type, string name, CodeExpression initExpr) 
    {
      return new CodeVariableDeclarationStatement(type, name, initExpr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typename"></param>
    /// <param name="baseType"></param>
    /// <returns></returns>
    protected virtual CodeTypeDeclaration ClassDecl(string typename, System.Type baseType)
    {
      return ClassDecl(typename, new Type[] { baseType } );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typename"></param>
    /// <returns></returns>
    protected virtual CodeTypeDeclaration ClassDecl(string typename)
    {
      return ClassDecl(typename, "System.Object" );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typename"></param>
    /// <param name="baseType"></param>
    /// <returns></returns>
    protected virtual CodeTypeDeclaration ClassDecl(string typename, string baseType)
    {
      CodeTypeDeclaration decl = new CodeTypeDeclaration(typename);
      decl.BaseTypes.Add(baseType);
      decl.IsClass = true;
      return AddComments(decl);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typename"></param>
    /// <param name="baseTypes"></param>
    /// <returns></returns>
    protected virtual CodeTypeDeclaration ClassDecl(string typename, System.Type[] baseTypes)
    {
      CodeTypeDeclaration decl = new CodeTypeDeclaration(typename);
      foreach (Type baseType in baseTypes)
      {
        decl.BaseTypes.Add(baseType);
      }
      decl.IsClass = true;
      return AddComments(decl);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual CodeTypeReference Type(string type) 
    {
      return new CodeTypeReference(type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual CodeTypeReference Type(System.Type type) 
    {
      return new CodeTypeReference(type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    protected virtual CodeTypeReference Type(string type, int rank) 
    {
      return new CodeTypeReference(type, rank);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual CodeTypeReferenceExpression TypeExpr(Type type) 
    {
      return new CodeTypeReferenceExpression(type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual CodeTypeReferenceExpression TypeExpr(string type) 
    {
      return new CodeTypeReferenceExpression(type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual CodeExpression TypeOf(string type) 
    {
      return new CodeTypeOfExpression(type);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual CodeExpression Value() 
    {
      return new CodePropertySetValueReferenceExpression();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exp"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    protected virtual CodeExpression Property(CodeExpression exp, string property) 
    {
      return new CodePropertyReferenceExpression(exp, property);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    protected virtual CodeMemberProperty PropertyDecl(string type, string name, MemberAttributes attributes) 
    {
      CodeMemberProperty local0;

      local0 = new CodeMemberProperty();
      local0.Type = Type(type);
      local0.Name = name;
      local0.Attributes = attributes;
      return AddComments(local0);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    protected virtual CodeStatement Stm(CodeExpression expr) 
    {
      return new CodeExpressionStatement(expr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    protected virtual CodeStatement Return(CodeExpression expr) 
    {
      return new CodeMethodReturnStatement(expr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="primitive"></param>
    /// <returns></returns>
    protected virtual CodeExpression Primitive(object primitive) 
    {
      return new CodePrimitiveExpression(primitive);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected virtual CodeExpression Str(string str) 
    {
      return Primitive(str);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    protected virtual CodeExpression New(string type, System.CodeDom.CodeExpression[] parameters) 
    {
      return new CodeObjectCreateExpression(type, parameters);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="arg"></param>
    /// <returns></returns>
    protected virtual CodeStatement Throw(string exception, string arg) 
    {
      System.CodeDom.CodeExpression[] local0;

      local0 = new CodeExpression[1];
      local0[0] = Str(arg);
      return new CodeThrowExceptionStatement(New(exception, local0));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="arg"></param>
    /// <param name="inner"></param>
    /// <returns></returns>
    protected virtual CodeStatement Throw(string exception, string arg, string inner) 
    {
      System.CodeDom.CodeExpression[] local0;

      local0 = new CodeExpression[2];
      local0[0] = Str(Res.GetString(arg));
      local0[1] = Variable(inner);
      return new CodeThrowExceptionStatement(New(exception, local0));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tryStmnt"></param>
    /// <param name="catchClause"></param>
    /// <returns></returns>
    protected virtual CodeStatement Try(CodeStatement tryStmnt, CodeCatchClause catchClause) 
    {
      System.CodeDom.CodeStatement[] local0;
      System.CodeDom.CodeCatchClause[] local1;

      local0 = new CodeStatement[1];
      local0[0] = tryStmnt;
      local1 = new CodeCatchClause[1];
      local1[0] = catchClause;
      return new CodeTryCatchFinallyStatement(local0, local1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    protected virtual CodeBinaryOperatorExpression IdNotEQ(CodeExpression left, CodeExpression right) 
    {
      return BinOperator(left, CodeBinaryOperatorType.IdentityInequality, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    protected virtual string GetTypeName(Type t) 
    {
      return t.FullName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual Type GetTypeFromDb(OleDbType type)
    {
      switch(type)
      {
        case OleDbType.BigInt:
        {
          return typeof(Int64);
        }
        case OleDbType.Binary:
        {
          return typeof(byte[]); //A stream of binary data (DBTYPE_BYTES). This maps to an Array of type Byte. ;
        }
        case OleDbType.Boolean:
        {
          return typeof(bool); //A Boolean value (DBTYPE_BOOL). This maps to Boolean. ;
        }
        case OleDbType.BSTR:
        {
          return typeof(string); //A null-terminated character string of Unicode characters (DBTYPE_BSTR). This maps to String. ;
        }
        case OleDbType.Char:
        {
          return typeof(string); //A character string (DBTYPE_STR). This maps to String. ;
        }
        case OleDbType.Currency:
        {
          return typeof(Decimal); //A currency value ranging from -263 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit (DBTYPE_CY). This maps to Decimal. ;
        }
        case OleDbType.Date:
        {
          return typeof(DateTime); //Date data, stored as a double (DBTYPE_DATE). The whole portion is the number of days since December 30, 1899, while the fractional portion is a fraction of a day. This maps to DateTime. ;
        }
        case OleDbType.DBDate:
        {
          return typeof(DateTime); //Date data in the format yyyymmdd (DBTYPE_DBDATE). This maps to DateTime. ;
        }
        case OleDbType.DBTime:
        {
          return typeof(DateTime); //Time data in the format hhmmss (DBTYPE_DBTIME). This maps to TimeSpan. ;
        }
        case OleDbType.DBTimeStamp:
        {
          return typeof(DateTime); //Data and time data in the format yyyymmddhhmmss (DBTYPE_DBTIMESTAMP). This maps to DateTime. ;
        }
        case OleDbType.Decimal:
        {
          return typeof(Decimal); //A fixed precision and scale numeric value between -1038 -1 and 10 38 -1 (DBTYPE_DECIMAL). This maps to Decimal. ;
        }
        case OleDbType.Double:
        {
          return typeof(Double); //A floating point number within the range of -1.79E +308 through 1.79E +308 (DBTYPE_R8). This maps to Double. ;
        }
        case OleDbType.Empty:
        {
          return typeof(object); //No value (DBTYPE_EMPTY). This maps to Empty. ;
        }
        case OleDbType.Error:
        {
          return typeof(Exception); //A 32-bit error code (DBTYPE_ERROR). This maps to Exception. ;
        }
        case OleDbType.Filetime:
        {
          return typeof(DateTime); //A 64-bit unsigned integer representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME). This maps to DateTime. ;
        }
        case OleDbType.Guid:
        {
          return typeof(Guid); //A globally unique identifier (or GUID) (DBTYPE_GUID). This maps to Guid. ;
        }
        case OleDbType.IDispatch:
        {
          return typeof(object); //A pointer to an IDispatch interface (DBTYPE_IDISPATCH). This maps to Object. ;
        }
        case OleDbType.Integer:
        {
          return typeof(int); //A 32-bit signed integer (DBTYPE_I4). This maps to Int32. ;
        }
        case OleDbType.IUnknown:
        {
          return typeof(object); //A pointer to an IUnknown interface (DBTYPE_UNKNOWN). This maps to Object. ;
        }
        case OleDbType.LongVarBinary:
        {
          return typeof(byte[]); //A long binary value (OleDbParameter only). This maps to an Array of type Byte. ;
        }
        case OleDbType.LongVarChar:
        {
          return typeof(string); //A long string value (OleDbParameter only). This maps to String. ;
        }
        case OleDbType.LongVarWChar:
        {
          return typeof(string); //A long null-terminated Unicode string value (OleDbParameter only). This maps to String. ;
        }
        case OleDbType.Numeric:
        {
          return typeof(Decimal); //An exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC). This maps to Decimal. 
        }
        case OleDbType.PropVariant:
        {
          return typeof(object); //An automation PROPVARIANT (DBTYPE_PROP_VARIANT). This maps to Object. ;
        }
        case OleDbType.Single:
        {
          return typeof(Single); //A floating point number within the range of -3.40E +38 through 3.40E +38 (DBTYPE_R4). This maps to Single. ;
        }
        case OleDbType.SmallInt:
        {
          return typeof(Int16); //A 16-bit signed integer (DBTYPE_I2). This maps to Int16. ;
        }
        case OleDbType.TinyInt:
        {
          return typeof(SByte); //A 8-bit signed integer (DBTYPE_I1). This maps to SByte. ;
        }
        case OleDbType.UnsignedBigInt:
        {
          return typeof(UInt64); //A 64-bit unsigned integer (DBTYPE_UI8). This maps to UInt64. ;
        }
        case OleDbType.UnsignedInt:
        {
          return typeof(UInt32); //A 32-bit unsigned integer (DBTYPE_UI4). This maps to UInt32. ;
        }
        case OleDbType.UnsignedSmallInt:
        {
          return typeof(UInt16); //A 16-bit unsigned integer (DBTYPE_UI2). This maps to UInt16. ;
        }
        case OleDbType.UnsignedTinyInt:
        {
          return typeof(Byte); //A 8-bit unsigned integer (DBTYPE_UI1). This maps to Byte. ;
        }
        case OleDbType.VarBinary:
        {
          return typeof(byte[]); //A variable-length stream of binary data (OleDbParameter only). This maps to an Array of type Byte. ;
        }
        case OleDbType.VarChar:
        {
          return typeof(string); //A variable-length stream of non-Unicode characters (OleDbParameter only). This maps to String. ;
        }
        case OleDbType.Variant:
        {
          return typeof(object); //A special data type that can contain numeric, string, binary, or date data, as well as the special values Empty and Null (DBTYPE_VARIANT). This type is assumed if no other is specified. This maps to Object. ;
        }
        case OleDbType.VarNumeric:
        {
          return typeof(Decimal); //A variable-length numeric value (OleDbParameter only). This maps to Decimal. ;
        }
        case OleDbType.VarWChar:
        {
          return typeof(string); //A variable-length, null-terminated stream of Unicode characters (OleDbParameter only). This maps to String. ;
        }
        case OleDbType.WChar:
        {
          return typeof(string); //A null-terminated stream of Unicode characters (DBTYPE_WSTR). This maps to String. ;
        }
        default:
        {
          return typeof(object); //A null-terminated stream of Unicode characters (DBTYPE_WSTR). This maps to String. ;
        }
      }
    }

    internal class Res 
    {
      static public string GetString(string res)
      {
        return res;
      }

      static public string GetString(string res, object output)
      {
        return string.Format("{0}: {1}", res, output);
      }
    }
  }

}
