// ImprovedDataSetGenerator.cs: Shawn Wildermuth [swildermuth@adoguy.com]

using System;
using System.Data;
using System.Data.Common;
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
using System.Globalization;
using System.Collections;
using System.Reflection;
using System.ComponentModel;

namespace QSP.CommonObjects
{
	/// <summary>
	/// An in-place replacement for System.Data.TypedDataSetGenerator.  It is used 
	/// </summary>
	public class ImprovedTypedDataSetGenerator : GeneratorBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ImprovedTypedDataSetGenerator() : base()
		{
		}

		#region Fields
		/// <summary>
		/// 
		/// </summary>
		protected Hashtable lookupIdentifiers = null;
		/// <summary>
		/// 
		/// </summary>
		protected ICodeGenerator codeGen = null;
		/// <summary>
		/// 
		/// </summary>
		protected ArrayList errorList = new ArrayList();

		/// <summary>
		/// 
		/// </summary>
		protected DataSet _dataSet;
		#endregion

		#region Generate DataSet
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataSet"></param>
		/// <param name="ns"></param>
		/// <param name="gen"></param>
		public virtual void Generate(DataSet dataSet, CodeNamespace ns, ICodeGenerator gen)
		{
			// bind CodeGenerator
			codeGen = gen;

			// Bind the DataSet
			_dataSet = dataSet;
 
			// Add Using/Imports
			ns.Imports.Add(Import("System"));
			ns.Imports.Add(Import("System.Data"));
			ns.Imports.Add(Import("System.Xml"));
			ns.Imports.Add(Import("System.Runtime.Serialization"));

			// Add the DataSet to the Namespace
			ns.Types.Add(GenerateDataSet());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeTypeDeclaration GenerateDataSet()
		{
			// Create the new Typed DataSet
			CodeTypeDeclaration typedDataSet = ClassDecl(FixIdName(_dataSet.DataSetName), "DataSet");

			// Add class Attributes
			typedDataSet.CustomAttributes.Add(AttributeDecl("Serializable"));
			typedDataSet.CustomAttributes.Add(AttributeDecl("System.ComponentModel.DesignerCategoryAttribute", Primitive("code")));
			typedDataSet.CustomAttributes.Add(AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			typedDataSet.CustomAttributes.Add(AttributeDecl("System.ComponentModel.ToolboxItem", Primitive(true)));
      
			// Create Constructors
			typedDataSet.Members.Add(GenerateDataSetEmptyConstructor());
			typedDataSet.Members.Add(GenerateDataSetSerializationConstructor());

			// Create Clone Method
			typedDataSet.Members.Add(GenerateDataSetClone());

			// Create XML Serialization methods
			typedDataSet.Members.Add(GenerateDataSetSerializeTables());
			typedDataSet.Members.Add(GenerateDataSetSerializeRelations());
			typedDataSet.Members.Add(GenerateDataSetReadXmlSerializable());
			typedDataSet.Members.Add(GenerateGetSchemaSerializable());

			// Create Initializing Methods
			typedDataSet.Members.Add(GenerateDataSetInitVars());
			typedDataSet.Members.AddRange(GenerateDataSetInitClass());

			// Add the RowEventHandlers
			foreach (DataTable tbl in _dataSet.Tables)
			{
				typedDataSet.Members.Add(CreateTypedRowEventHandler(tbl));
			}

			// Create Tables Classes
			foreach (DataTable tbl in _dataSet.Tables)
			{
				// Create the DataTable class
				CodeTypeDeclaration tblClass = GenerateTable(tbl);
				typedDataSet.Members.Add(tblClass);

				// Create the Field and Property
				CodeMemberField tblField = GenerateTableField(tbl, tblClass);
				typedDataSet.Members.Add(tblField);
				typedDataSet.Members.Add(GenerateTableProperty(tbl, tblClass, tblField));

				// Create the Typed Row Classes
				typedDataSet.Members.Add(CreateTypedRow(tbl));
				typedDataSet.Members.Add(CreateTypedRowEvent(tbl));      
			}

			// Create Relation Objects
			foreach (DataRelation rel in _dataSet.Relations)
			{
				GenerateDataRelation(rel, typedDataSet);
			}

			return typedDataSet;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeConstructor GenerateDataSetEmptyConstructor()
		{
			// Create empty Constructors
			CodeConstructor xtor = ConstructorDecl();
			xtor.Attributes = PublicVisibility;
			foreach(DataTable table in _dataSet.Tables) 
			{
				CodeExpression exp = Field(This(), TableFieldName(table));
				xtor.Statements.Add(Assign(exp, GenerateDataSetDataTableCreation(table)));
				xtor.Statements.Add(MethodCall(Property(This(), "Tables"), "Add", exp));
			}
			xtor.Statements.Add(MethodCall(TypeExpr("DataSetHelper"), "CreateDataSetExtendedProperties", This()));
			
			/*foreach (DataTable theTbl in _dataSet.Tables)
			{
				foreach (Constraint constraint in theTbl.Constraints)
				{
					CodeExpression theExp = null;

					// If not a ForeignKeyConstraint, just skip
					if (constraint as ForeignKeyConstraint == null) continue;

					// Get the Constraint
					ForeignKeyConstraint fkConstraint = (ForeignKeyConstraint) constraint;
          
					// Create an array of Source Columns
					CodeArrayCreateExpression sourceCols = new CodeArrayCreateExpression("DataColumn", 0);
					foreach (DataColumn col in fkConstraint.RelatedColumns)
					{
						sourceCols.Initializers.Add(Property(Field(This(), this.TableFieldName(col.Table)), TableColumnPropertyName(col)));
					}

					// Create an array of Destination Columns
					CodeArrayCreateExpression destCols = new CodeArrayCreateExpression("DataColumn", 0);
					foreach (DataColumn col in fkConstraint.Columns)
					{
						destCols.Initializers.Add(Property(Field(This(), TableFieldName(col.Table)), this.TableColumnPropertyName(col)));
					}

					// Create the foreign key constraint Variable if not created already
					if (theExp == null) 
					{
						xtor.Statements.Add(VariableDecl("ForeignKeyConstraint", "fkc"));
						theExp = Variable("fkc");
					}
          
					// Create the ForeignKey 
					CodeExpression[] xtorParams = new CodeExpression[3];
					xtorParams[0] = Str(fkConstraint.ConstraintName);
					xtorParams[1] = sourceCols;
					xtorParams[2] = destCols;
					xtor.Statements.Add(Assign(theExp, New("ForeignKeyConstraint", xtorParams)));
					xtor.Statements.Add(MethodCall(Property(Field(This(), this.TableFieldName(theTbl)), "Constraints"), "Add", theExp));
					xtor.Statements.Add(Assign(Property(theExp, "AcceptRejectRule"), Field(TypeExpr(fkConstraint.AcceptRejectRule.GetType()), fkConstraint.AcceptRejectRule.ToString())));
					xtor.Statements.Add(Assign(Property(theExp, "DeleteRule"), Field(TypeExpr(fkConstraint.DeleteRule.GetType()), fkConstraint.DeleteRule.ToString())));
					xtor.Statements.Add(Assign(Property(theExp, "UpdateRule"), Field(TypeExpr(fkConstraint.UpdateRule.GetType()), fkConstraint.UpdateRule.ToString())));
				}
			}

			foreach (DataRelation relation in _dataSet.Relations)
			{
				// Create each child Column array
				CodeArrayCreateExpression parentCols = new CodeArrayCreateExpression("DataColumn", 0);
				foreach (DataColumn col in relation.ParentColumns)
				{
					parentCols.Initializers.Add(Property(Field(This(), TableFieldName(col.Table)), TableColumnPropertyName(col)));
				}

				// Create each child Column array
				CodeArrayCreateExpression childCols = new CodeArrayCreateExpression("DataColumn", 0);
				foreach (DataColumn col in relation.ChildColumns)
				{
					childCols.Initializers.Add(Property(Field(This(), TableFieldName(col.Table)), TableColumnPropertyName(col)));
				}

				// Create the Relation assignment
				CodeExpression[] relationParams = new CodeExpression[4];
				relationParams[0] = Str(relation.RelationName);
				relationParams[1] = parentCols;
				relationParams[2] = childCols;
				relationParams[3] = Primitive(false);
				xtor.Statements.Add(Assign(Field(This(), RelationFieldName(relation)), New("DataRelation", relationParams)));

				// If its nested, mark it as such
				if (relation.Nested)
				{
					xtor.Statements.Add(Assign(Property(Field(This(), RelationFieldName(relation)), "Nested"), Primitive(true)));
				}

				// Add the relation to the method
				xtor.Statements.Add(MethodCall(Property(This(), "Relations"), "Add", Field(This(), RelationFieldName(relation))));
			}*/

			// If expressions exist, call InitExpressions
			if (CreateDataSetInitExpressions() != null) 
			{
				xtor.Statements.Add(MethodCall(This(), "InitExpressions"));
			}
			
			xtor.Statements.Add(Stm(MethodCall(This(), "InitVars")));
			xtor.Statements.Add(VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler", new CodeDelegateCreateExpression(Type(typeof(CollectionChangeEventHandler)), This(), "SchemaChanged")));
			xtor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(Property(This(), "Tables"), "CollectionChanged"), Variable("schemaChangedHandler")));
			xtor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(Property(This(), "Relations"), "CollectionChanged"), Variable("schemaChangedHandler")));
			return xtor;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeConstructor GenerateDataSetSerializationConstructor()
		{
			// Create XML Serialization Constructor
			CodeConstructor xtor = ConstructorDecl();
			xtor.Attributes = FamilyVisibility;
			xtor.Parameters.Add(ParameterDecl("SerializationInfo", "info"));
			xtor.Parameters.Add(ParameterDecl("StreamingContext", "context"));
			CodeExpression[] strSchemaParams = new CodeExpression[2];
			strSchemaParams[0] = Str("XmlSchema");
			strSchemaParams[1] = TypeOf("System.String");
			xtor.Statements.Add(VariableDecl("System.String", "strSchema", Cast("System.String", MethodCall(Argument("info"), "GetValue", strSchemaParams))));

			ArrayList items = new ArrayList();
			CodeExpression[] paramExpressions = new CodeExpression[0];
			items.Add(VariableDecl("DataSet", "ds", New("DataSet", paramExpressions)));
			paramExpressions = new CodeExpression[1];
			CodeExpression[] paramExpressions2 = new CodeExpression[1];
			CodeExpression[] paramExpressions3 = new CodeExpression[1];
			paramExpressions3[0] = Variable("strSchema");
			paramExpressions2[0] = New("System.IO.StringReader", paramExpressions3);
			paramExpressions[0] = New("XmlTextReader", paramExpressions2);
			items.Add(Stm(MethodCall(Variable("ds"), "ReadXmlSchema", paramExpressions)));
			foreach (DataTable table in _dataSet.Tables) 
			{
				paramExpressions = new CodeExpression[1];
				paramExpressions[0] = Indexer(Property(Variable("ds"), "Tables"), Str(table.TableName));
				items.Add(If(IdNotEQ(Indexer(Property(Variable("ds"), "Tables"), Str(table.TableName)), Primitive(null)), Stm(MethodCall(Property(This(), "Tables"), "Add", GenerateDataSetDataTableCreationWithDataTable(table, paramExpressions)))));
			}
			items.Add(Assign(Property(This(), "DataSetName"), Property(Variable("ds"), "DataSetName")));
			items.Add(Assign(Property(This(), "Prefix"), Property(Variable("ds"), "Prefix")));
			items.Add(Assign(Property(This(), "Namespace"), Property(Variable("ds"), "Namespace")));
			items.Add(Assign(Property(This(), "Locale"), Property(Variable("ds"), "Locale")));
			items.Add(Assign(Property(This(), "CaseSensitive"), Property(Variable("ds"), "CaseSensitive")));
			items.Add(Assign(Property(This(), "EnforceConstraints"), Property(Variable("ds"), "EnforceConstraints")));
			paramExpressions = new CodeExpression[3];
			paramExpressions[0] = Variable("ds");
			paramExpressions[1] = Primitive(false);
			paramExpressions[2] = Field(TypeExpr(typeof(MissingSchemaAction)), "Add");
			items.Add(Stm(MethodCall(This(), "Merge", paramExpressions)));
			items.Add(Stm(MethodCall(This(), "InitVars")));
			CodeStatement[] statements = new CodeStatement[checked((uint) items.Count)];
			items.CopyTo(statements);
			CodeStatement[] initStatements = new CodeStatement[1];
			initStatements[0] = Stm(MethodCall(This(), "InitClass"));
			xtor.Statements.Add(If(IdNotEQ(Variable("strSchema"), Primitive(null)), statements, initStatements));
			paramExpressions = new CodeExpression[2];
			paramExpressions[0] = Argument("info");
			paramExpressions[1] = Argument("context");
			xtor.Statements.Add(MethodCall(This(), "GetSerializationData", paramExpressions));
			xtor.Statements.Add(VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler", new CodeDelegateCreateExpression(Type(typeof(CollectionChangeEventHandler)), This(), "SchemaChanged")));
			xtor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(Property(This(), "Tables"), "CollectionChanged"), Variable("schemaChangedHandler")));
			xtor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(Property(This(), "Relations"), "CollectionChanged"), Variable("schemaChangedHandler")));
      
			return xtor;    
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateDataSetClone()
		{
			// Create the Clone Method
			CodeMemberMethod clone = MethodDecl("DataSet", "Clone", PublicOverrideVisibility);
			clone.Statements.Add(VariableDecl(FixIdName(_dataSet.DataSetName), "cln", Cast(DataSetName, MethodCall(Base(), "Clone", new CodeExpression[0]))));
			clone.Statements.Add(MethodCall(Variable("cln"), "InitVars", new CodeExpression[0]));
			clone.Statements.Add(Return(Variable("cln")));

			return clone;
		}
    
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateDataSetSerializeTables()
		{
			// Create the ShouldSerializeTables
			CodeMemberMethod serializeTables = MethodDecl("System.Boolean", "ShouldSerializeTables", FamilyOverrideVisibility);
			serializeTables.Statements.Add(Return(Primitive(false)));
      
			return serializeTables;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateDataSetSerializeRelations()
		{
			// Create the ShouldSerializeRelations
			CodeMemberMethod serializeRelations = MethodDecl("System.Boolean", "ShouldSerializeRelations", FamilyOverrideVisibility);
			serializeRelations.Statements.Add(Return(Primitive(false)));

			return serializeRelations;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateDataSetReadXmlSerializable()
		{
			// Create the ReadXmlSerializable attribute
			CodeMemberMethod readXml = MethodDecl("System.Void", "ReadXmlSerializable", FamilyOverrideVisibility);
			readXml.Parameters.Add(ParameterDecl("XmlReader", "reader"));
			readXml.Statements.Add(MethodCall(This(), "Reset", new CodeExpression[0]));
			readXml.Statements.Add(VariableDecl("DataSet", "ds", New("DataSet", new CodeExpression[0])));
			CodeExpression[] readerArg = new CodeExpression[1];
			readerArg[0] = Argument("reader");
			readXml.Statements.Add(MethodCall(Variable("ds"), "ReadXml", readerArg));
			foreach (DataTable table in _dataSet.Tables)
			{
				CodeExpression[] tableExpression = new CodeExpression[1];
				tableExpression[0] = Indexer(Property(Variable("ds"), "Tables"), Str(table.TableName));
				readXml.Statements.Add(If(IdNotEQ(Indexer(Property(Variable("ds"), "Tables"), Str(table.TableName)), Primitive(null)), Stm(MethodCall(Property(This(), "Tables"), "Add", GenerateDataSetDataTableCreationWithDataTable(table, tableExpression) ))));
			}
			readXml.Statements.Add(Assign(Property(This(), "DataSetName"),        Property(Variable("ds"), "DataSetName")));
			readXml.Statements.Add(Assign(Property(This(), "Prefix"),             Property(Variable("ds"), "Prefix")));
			readXml.Statements.Add(Assign(Property(This(), "Namespace"),          Property(Variable("ds"), "Namespace")));
			readXml.Statements.Add(Assign(Property(This(), "Locale"),             Property(Variable("ds"), "Locale")));
			readXml.Statements.Add(Assign(Property(This(), "CaseSensitive"),      Property(Variable("ds"), "CaseSensitive")));
			readXml.Statements.Add(Assign(Property(This(), "EnforceConstraints"), Property(Variable("ds"), "EnforceConstraints")));
			CodeExpression[] missingSchemaExpression = new CodeExpression[3];
			missingSchemaExpression[0] = Variable("ds");
			missingSchemaExpression[1] = Primitive(false);
			missingSchemaExpression[2] = Field(TypeExpr(typeof(MissingSchemaAction)), "Add");
			readXml.Statements.Add(MethodCall(This(), "Merge", missingSchemaExpression));
			readXml.Statements.Add(MethodCall(This(), "InitVars"));
			return readXml;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeExpression GenerateDataSetDataTableCreationWithDataTable(DataTable table, CodeExpression[] tableExpressions)
		{
			return New(TableClassName(table), tableExpressions );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateGetSchemaSerializable()
		{
			// Create the GetSchemaSerializable
			CodeMemberMethod getSchema = MethodDecl("System.Xml.Schema.XmlSchema", "GetSchemaSerializable", FamilyOverrideVisibility);

			// Add the Code to create the Stream
			getSchema.Statements.Add(VariableDecl("System.IO.MemoryStream", "stream", New("System.IO.MemoryStream", new CodeExpression[0])));

			// Add Writing schema to the Writer
			CodeExpression[] streamExpression = new CodeExpression[2];
			streamExpression[0] = Argument("stream");
			streamExpression[1] = Primitive(null);
			getSchema.Statements.Add(MethodCall(This(), "WriteXmlSchema", New("XmlTextWriter", streamExpression)));
			getSchema.Statements.Add(Assign(Property(Argument("stream"), "Position"), Primitive(0)));
      
			// Add the Read 
			CodeExpression[] xmlReaderExpression = new CodeExpression[2];
			CodeExpression[] xmlStreamExpression = new CodeExpression[1];
			xmlStreamExpression[0] = Argument("stream");
			xmlReaderExpression[0] = New("XmlTextReader", xmlStreamExpression);
			xmlReaderExpression[1] = Primitive(null);
			getSchema.Statements.Add(Return(MethodCall(TypeExpr("System.Xml.Schema.XmlSchema"), "Read", xmlReaderExpression)));

			// Return the method
			return getSchema;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateDataSetInitVars()
		{
			// Create the Method 
			CodeMemberMethod initVars = MethodDecl("System.Void", "InitVars", AssemblyVisibility);
      
			// Go Through all the Tables
			foreach (DataTable theTbl in _dataSet.Tables)
			{
				// Add the Table Assigment
				CodeExpression exp = Field(This(), TableFieldName(theTbl));
				initVars.Statements.Add(Assign(exp, Cast(this.TableClassName(theTbl), Indexer(Property(This(), "Tables"), Str(theTbl.TableName)))));
        
				// Call the Table's InitVars
				initVars.Statements.Add(If(IdNotEQ(exp, Primitive(null)), Stm(MethodCall(exp, "InitVars"))));
			}

			// Add the relations to the InitVars
			foreach (DataRelation relation in _dataSet.Relations)
			{
				initVars.Statements.Add(Assign(Field(This(), RelationFieldName(relation)), Indexer(Property(This(), "Relations"), Str(relation.RelationName))));
			}
      
			// return the method
			return initVars;
		}
    
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeTypeMemberCollection GenerateDataSetInitClass()
		{
			// Create the Method
			CodeMemberMethod initClass = MethodDecl("System.Void", "InitClass", PrivateVisibility);

			// Add Assignments
			initClass.Statements.Add(Assign(Property(This(), "DataSetName"), Str(_dataSet.DataSetName)));
			initClass.Statements.Add(Assign(Property(This(), "Prefix"), Str(_dataSet.Prefix)));
			initClass.Statements.Add(Assign(Property(This(), "Namespace"), Str(_dataSet.Namespace)));
			initClass.Statements.Add(Assign(Property(This(), "Locale"), New("System.Globalization.CultureInfo", new CodeExpression[] { Str(_dataSet.Locale.ToString()) })));
			initClass.Statements.Add(Assign(Property(This(), "CaseSensitive"), Primitive(_dataSet.CaseSensitive)));
			initClass.Statements.Add(Assign(Property(This(), "EnforceConstraints"), Primitive(_dataSet.EnforceConstraints)));

			CodeExpression theExp = null;

			// Go through each table
			foreach (DataTable theTbl in _dataSet.Tables)
			{
				// Add the Assignment
				CodeExpression exp = Field(This(), TableFieldName(theTbl));
				initClass.Statements.Add(Assign(exp, GenerateDataSetDataTableCreation(theTbl)));
				initClass.Statements.Add(MethodCall(Property(This(), "Tables"), "Add", exp));
			}

			foreach (DataTable theTbl in _dataSet.Tables)
			{
				foreach (Constraint constraint in theTbl.Constraints)
				{
					// If not a ForeignKeyConstraint, just skip
					if (constraint as ForeignKeyConstraint == null) continue;

					// Get the Constraint
					ForeignKeyConstraint fkConstraint = (ForeignKeyConstraint) constraint;
          
					// Create an array of Source Columns
					CodeArrayCreateExpression sourceCols = new CodeArrayCreateExpression("DataColumn", 0);
					foreach (DataColumn col in fkConstraint.RelatedColumns)
					{
						sourceCols.Initializers.Add(Property(Field(This(), this.TableFieldName(col.Table)), TableColumnPropertyName(col)));
					}

					// Create an array of Destination Columns
					CodeArrayCreateExpression destCols = new CodeArrayCreateExpression("DataColumn", 0);
					foreach (DataColumn col in fkConstraint.Columns)
					{
						destCols.Initializers.Add(Property(Field(This(), TableFieldName(col.Table)), this.TableColumnPropertyName(col)));
					}

					// Create the foreign key constraint Variable if not created already
					if (theExp == null) 
					{
						initClass.Statements.Add(VariableDecl("ForeignKeyConstraint", "fkc"));
						theExp = Variable("fkc");
					}
          
					// Create the ForeignKey 
					CodeExpression[] xtorParams = new CodeExpression[3];
					xtorParams[0] = Str(fkConstraint.ConstraintName);
					xtorParams[1] = sourceCols;
					xtorParams[2] = destCols;
					initClass.Statements.Add(Assign(theExp, New("ForeignKeyConstraint", xtorParams)));
					initClass.Statements.Add(MethodCall(Property(Field(This(), this.TableFieldName(theTbl)), "Constraints"), "Add", theExp));
					initClass.Statements.Add(Assign(Property(theExp, "AcceptRejectRule"), Field(TypeExpr(fkConstraint.AcceptRejectRule.GetType()), fkConstraint.AcceptRejectRule.ToString())));
					initClass.Statements.Add(Assign(Property(theExp, "DeleteRule"), Field(TypeExpr(fkConstraint.DeleteRule.GetType()), fkConstraint.DeleteRule.ToString())));
					initClass.Statements.Add(Assign(Property(theExp, "UpdateRule"), Field(TypeExpr(fkConstraint.UpdateRule.GetType()), fkConstraint.UpdateRule.ToString())));
				}
			}

			foreach (DataRelation relation in _dataSet.Relations)
			{
				// Create each child Column array
				CodeArrayCreateExpression parentCols = new CodeArrayCreateExpression("DataColumn", 0);
				foreach (DataColumn col in relation.ParentColumns)
				{
					parentCols.Initializers.Add(Property(Field(This(), TableFieldName(col.Table)), TableColumnPropertyName(col)));
				}

				// Create each child Column array
				CodeArrayCreateExpression childCols = new CodeArrayCreateExpression("DataColumn", 0);
				foreach (DataColumn col in relation.ChildColumns)
				{
					childCols.Initializers.Add(Property(Field(This(), TableFieldName(col.Table)), TableColumnPropertyName(col)));
				}

				// Create the Relation assignment
				CodeExpression[] relationParams = new CodeExpression[4];
				relationParams[0] = Str(relation.RelationName);
				relationParams[1] = parentCols;
				relationParams[2] = childCols;
				relationParams[3] = Primitive(false);
				initClass.Statements.Add(Assign(Field(This(), RelationFieldName(relation)), New("DataRelation", relationParams)));

				// If its nested, mark it as such
				if (relation.Nested)
				{
					initClass.Statements.Add(Assign(Property(Field(This(), RelationFieldName(relation)), "Nested"), Primitive(true)));
				}

				// Add the relation to the method
				initClass.Statements.Add(MethodCall(Property(This(), "Relations"), "Add", Field(This(), RelationFieldName(relation))));
			}

			// Create the Method Collection
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();

			// Grab the initExpressions method
			CodeMemberMethod initExpressions = CreateDataSetInitExpressions();
      
			// Add the initClass to the collection
			collection.Add(initClass);

			// Create ShouldSerialize(XXX) Methods
			foreach (DataTable table in _dataSet.Tables)
			{
				collection.Add(GenerateShouldSerializeTableName(table));
			}

			// Create the Schema Changed Method
			collection.Add(GenerateSchemaChanged());

			// If it exists, add it to the Init class and to the collection we're returning
			if (initExpressions != null) 
			{
				initClass.Statements.Add(MethodCall(This(), "InitExpressions"));
				collection.Add(initExpressions);
			}

			// Return the method
			return collection;
		}
    
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeExpression GenerateDataSetDataTableCreation(DataTable table)
		{
			return New(TableClassName(table), new CodeExpression[0]);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual CodeMemberMethod CreateDataSetInitExpressions()
		{
			// Initialize Expressions
			bool anyExpressions = false;
			CodeMemberMethod initExpressions = MethodDecl("System.Void", "InitExpressions", PrivateVisibility);

			// Go through each table
			foreach (DataTable theTbl in _dataSet.Tables)
			{
				// Go through each column
				foreach (DataColumn col in theTbl.Columns)
				{
					// If the column has an expression
					if (col.Expression.Length > 0) 
					{
						// Add the expression to the Method
						CodeExpression expressionProp = Property(Field(This(), TableFieldName(col.Table)), TableColumnPropertyName(col));
						anyExpressions = true;
						initExpressions.Statements.Add(Assign(Property(expressionProp, "Expression"), Str(col.Expression)));
					}
				}
			}

			// If there are any expressions, add them to the 
			if (anyExpressions) 
			{
				return initExpressions;
			}

			// If we don't need it, don't return it
			return null;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rel"></param>
		/// <param name="type"></param>
		protected virtual void GenerateDataRelation(DataRelation rel, CodeTypeDeclaration type)
		{
			// Create the Field
			CodeMemberField relField = FieldDecl("DataRelation", "relation" + FixIdName(rel.RelationName));
			relField.Attributes = PrivateVisibility;
			type.Members.Add(relField);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateShouldSerializeTableName(DataTable table)
		{
			CodeMemberMethod shouldMethod = MethodDecl("System.Boolean", "ShouldSerialize" + TablePropertyName(table), PrivateVisibility);
			shouldMethod.Statements.Add(Return(Primitive(false)));

			return shouldMethod;
		}

		protected virtual CodeMemberMethod GenerateSchemaChanged()
		{
			CodeMemberMethod changed = MethodDecl("System.Void", "SchemaChanged", PrivateVisibility);
			changed.Parameters.Add(ParameterDecl(typeof(Object), "sender"));
			changed.Parameters.Add(ParameterDecl(typeof(CollectionChangeEventArgs), "e"));
			changed.Statements.Add(If(EQ(Property(Argument("e"), "Action"), Field(TypeExpr(typeof(CollectionChangeAction)), "Remove")), Stm(MethodCall(This(), "InitVars"))));
      
			return changed;

		}
		#endregion

		#region DataTable Generation
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <param name="type"></param>
		protected virtual CodeTypeDeclaration GenerateTable(DataTable table)
		{
			// Create the Table Class
			CodeTypeDeclaration tblClass = AddComments(new CodeTypeDeclaration(TableClassName(table)));
			tblClass.BaseTypes.Add("DataTable");
			tblClass.BaseTypes.Add("System.Collections.IEnumerable");
			tblClass.CustomAttributes.Add(AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
    
			// Create Constructors
			tblClass.Members.Add(GenerateTableEmptyConstructor(table));
			tblClass.Members.Add(GenerateTableCreationConstructor(table));

			// Create Count Property
			tblClass.Members.Add(GenerateTableCount(table));

			// Add the typesafe members
			foreach (DataColumn column in table.Columns)
			{
				GenerateDataColumn(column, tblClass);
			}

			// Create the Indexer
			tblClass.Members.Add(GenerateTableIndexer(table));

			// Add Methods  
			tblClass.Members.Add(GenerateTableAdd(table));
			tblClass.Members.Add(GenerateTableAddFullRow(table));
			tblClass.Members.AddRange(GenerateTableFindBy(table));
			tblClass.Members.Add(GenerateGetEnumerator());
			tblClass.Members.Add(GenerateTableClone(table));
			tblClass.Members.Add(GenerateTableCreateInstance(table));
			tblClass.Members.Add(GenerateTableInitVars(table));
			tblClass.Members.Add(GenerateTableInitClass(table));
			tblClass.Members.Add(GenerateTableAddRow(table));
			tblClass.Members.Add(GenerateTableNewFromBuilder(table));
			tblClass.Members.Add(GenerateTableGetRowType(table));

			// Add Events
			tblClass.Members.AddRange(GenerateTableEventHandlers(table));

			// Add Remove Row Method
			tblClass.Members.Add(GenerateTableRemoveRow(table));

			return tblClass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeConstructor GenerateTableEmptyConstructor(DataTable table)
		{
			// Add Empty Constructor
			CodeConstructor xtor = ConstructorDecl();
			xtor.Attributes = PublicVisibility;
			xtor.BaseConstructorArgs.Add(Str(table.TableName));
			xtor.Statements.Add(MethodCall(This(), "InitClass"));
     
			// Return the constructor
			return xtor;
		}

		/// <summary>
		/// Creates the Constructor for the Typed DataSet that takes 
		/// a DataTable as a parameter.
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeConstructor GenerateTableCreationConstructor(DataTable table)
		{
			// Add DataTable Constructor
			CodeConstructor xtor = ConstructorDecl();
			xtor.Attributes = PublicVisibility;
			xtor.Parameters.Add(ParameterDecl("DataTable", "table"));
			xtor.BaseConstructorArgs.Add(Property(Argument("table"), "TableName"));
			xtor.Statements.Add(If(IdNotEQ(Property(Argument("table"), "CaseSensitive"), Property(Property(Argument("table"), "DataSet"), "CaseSensitive")), Assign(Property(This(), "CaseSensitive"), Property(Argument("table"), "CaseSensitive"))));
			xtor.Statements.Add(If(IdNotEQ(MethodCall(Property(Argument("table"), "Locale"), "ToString"), MethodCall(Property(Property(Argument("table"), "DataSet"), "Locale"), "ToString")), Assign(Property(This(), "Locale"), Property(Argument("table"), "Locale"))));
			xtor.Statements.Add(If(IdNotEQ(Property(Argument("table"), "Namespace"), Property(Property(Argument("table"), "DataSet"), "Namespace")), Assign(Property(This(), "Namespace"), Property(Argument("table"), "Namespace"))));
			xtor.Statements.Add(Assign(Property(This(), "Prefix"), Property(Argument("table"), "Prefix")));
			xtor.Statements.Add(Assign(Property(This(), "MinimumCapacity"), Property(Argument("table"), "MinimumCapacity")));
			xtor.Statements.Add(Assign(Property(This(), "DisplayExpression"), Property(Argument("table"), "DisplayExpression")));

			// Return the constructor
			return xtor;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberProperty GenerateTableCount(DataTable table)
		{
			// Count Property
			CodeMemberProperty count = PropertyDecl("System.Int32", "Count", PublicVisibility);
			count.CustomAttributes.Add(AttributeDecl("System.ComponentModel.Browsable", Primitive(false)));
			count.GetStatements.Add(Return(Property(Property(This(), "Rows"), "Count")));
      
			return count;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberProperty GenerateTableIndexer(DataTable table)
		{
			// Item Property
			CodeMemberProperty itemProp = PropertyDecl(RowConcreteClassName(table), "Item", PublicVisibility);
			itemProp.Parameters.Add(ParameterDecl("System.Int32", "index"));
			itemProp.GetStatements.Add(Return(Cast(RowConcreteClassName(table), Indexer(Property(This(), "Rows"), Argument("index")))));
      
			return itemProp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableAdd(DataTable table)
		{
			// Add Method
			CodeMemberMethod addMethod = MethodDecl("System.Void", "Add" + RowClassName(table), PublicVisibility);
			addMethod.Parameters.Add(ParameterDecl(RowConcreteClassName(table), "row"));
			addMethod.Statements.Add(MethodCall(Property(This(), "Rows"), "Add", Argument("row")));
      
			return addMethod;
		}

		/// <summary>
		/// Adds a row with every column specified
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableAddFullRow(DataTable table)
		{
			// Full Add Method
			CodeMemberMethod addAllMethod = MethodDecl(RowConcreteClassName(table), "Add" + RowClassName(table), PublicVisibility);
			DataColumn[] addColumns = GetTableColumnSelection(table);

			// Go through all the selected Columns
			foreach (DataColumn column in addColumns)
			{
				// Add the Relations
				Type dataType = column.DataType;

				// Call the Hidden Method
				MethodInfo info = GetInaccessibleMethod(column, "FindParentRelation");
				object obj = info.Invoke(column, null);
				DataRelation parentRelation = obj as DataRelation;

				// Determine if Relation is Followable
				if (ChildRelationFollowable(parentRelation)) 
				{
					string rowClassRawName = RowClassName(parentRelation.ParentTable);
					string typedRowClassName = FixIdName("parent" + rowClassRawName + "By" + parentRelation.RelationName);
					addAllMethod.Parameters.Add(ParameterDecl(rowClassRawName, typedRowClassName));
				}
				else
				{
					addAllMethod.Parameters.Add(ParameterDecl(GetTypeName(dataType), RowColumnPropertyName(column)));
				}
			}

			// Add Row Methods
			addAllMethod.Statements.Add(VariableDecl(RowConcreteClassName(table), "row" + RowClassName(table), Cast(RowConcreteClassName(table), MethodCall(This(), "NewRow"))));
			CodeExpression rowVar = Variable("row" + RowClassName(table));
			CodeAssignStatement itemArrayVar = new CodeAssignStatement();
			itemArrayVar.Left = Property(rowVar, "ItemArray");
			CodeArrayCreateExpression colArrayParams = new CodeArrayCreateExpression();
			colArrayParams.CreateType = Type(typeof(Object));
			DataColumn[] columnArray = new DataColumn[checked((uint) table.Columns.Count)];
			table.Columns.CopyTo(columnArray, 0);
			foreach (DataColumn column in columnArray)
			{
				if (column.AutoIncrement)
				{
					colArrayParams.Initializers.Add(Primitive(null));
				}
				else 
				{

					// Get the Inaccessible Method 
					MethodInfo info = GetInaccessibleMethod(column, "FindParentRelation");
					object obj = info.Invoke(column, null);
					DataRelation parentRelation = obj as DataRelation;

					// Determine if the relation is followable
					if (ChildRelationFollowable(parentRelation)) 
					{
						string rowClassRawName = RowClassName(parentRelation.ParentTable);
						string rowClassFixedName = this.FixIdName("parent" + rowClassRawName + "By" + parentRelation.RelationName);
						colArrayParams.Initializers.Add(Indexer(Argument(rowClassFixedName), Primitive(parentRelation.ParentColumns[0].Ordinal)));
					}
					else
					{
						colArrayParams.Initializers.Add(Argument(RowColumnPropertyName(column)));
					}
				}
			}
			itemArrayVar.Right = colArrayParams;

			// Add the Variables
			addAllMethod.Statements.Add(itemArrayVar);
			addAllMethod.Statements.Add(MethodCall(Property(This(), "Rows"), "Add", rowVar));
			addAllMethod.Statements.Add(Return(rowVar));
      
			// Return the Method
			return addAllMethod;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual DataColumn[] GetTableColumnSelection(DataTable table)
		{
			// Gather all non-auto incremented columns
			ArrayList colArray = new ArrayList();
			foreach (DataColumn column in table.Columns)
			{
				// Add it to the list of columns 
				// long as it's not AutoIncrement 
				if (!column.AutoIncrement)
				{
					colArray.Add(column);
				}
			}

			// Create a data column array
			DataColumn[] addColumns = new DataColumn[checked((uint) colArray.Count)];
			colArray.CopyTo(addColumns, 0);

			// Return the Data Columns
			return addColumns;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual DataColumn[] GetTableNonHiddenColumns(DataTable table)
		{
			// Gather all non-auto incremented columns
			ArrayList colArray = new ArrayList();
			foreach (DataColumn column in table.Columns)
			{
				// Add it to the list of columns 
				// long as it's not AutoIncrement 
				if (column.ColumnMapping != MappingType.Hidden)
				{
					colArray.Add(column);
				}
			}

			// Create a data column array
			DataColumn[] addColumns = new DataColumn[checked((uint) colArray.Count)];
			colArray.CopyTo(addColumns, 0);

			// Return the Data Columns
			return addColumns;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeMemberCollection GenerateTableFindBy(DataTable table)
		{
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();

			// Add the Constraints
			foreach (Constraint constraint in table.Constraints)
			{
				if (constraint is UniqueConstraint && ((UniqueConstraint)constraint).IsPrimaryKey) 
				{
					CodeMemberMethod method = GenerateTableFindBy(table, constraint as UniqueConstraint);
					if (method != null) collection.Add(method);
				}
			}

			// Return the collection
			return collection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <param name="constraint"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableFindBy(DataTable table, UniqueConstraint constraint)
		{
			// Go through each column to find hidden columns
			DataColumn[] cols = constraint.Columns;
			string methodName = "FindBy";
			bool noHidden = true;
			foreach (DataColumn column in cols)
			{
				methodName = methodName + RowColumnPropertyName(column);
				if (column.ColumnMapping != MappingType.Hidden)
				{
					noHidden = false;
				}
			}

			// If no Hidden columns, add the method
			if (!noHidden) 
			{
				// Create a FindBy
				CodeMemberMethod findBy = MethodDecl(RowClassName(table), FixIdName(methodName), PublicVisibility);
            
				// Add all the parameters
				foreach (DataColumn column in constraint.Columns)
				{
					findBy.Parameters.Add(ParameterDecl(GetTypeName(column.DataType), RowColumnPropertyName(column)));
				}

				// Add Columns to Code
				CodeArrayCreateExpression array = new CodeArrayCreateExpression(typeof(Object), (int) cols.Length);
				foreach (DataColumn column in cols)
				{
					array.Initializers.Add(Argument(RowColumnPropertyName(column)));
				}
				findBy.Statements.Add(Return(Cast(RowClassName(table), MethodCall(Property(This(), "Rows"), "Find", array))));
				return findBy;
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateGetEnumerator()
		{
			// Add IEnumerator Code
			CodeMemberMethod getEnum = MethodDecl("System.Collections.IEnumerator", "GetEnumerator", PublicVisibility);
			getEnum.ImplementationTypes.Add(Type("System.Collections.IEnumerable"));
			getEnum.Statements.Add(Return(MethodCall(Property(This(), "Rows"), "GetEnumerator")));
      
			return getEnum;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableClone(DataTable table)
		{
			// Add Clone
			CodeMemberMethod clone = MethodDecl("DataTable", "Clone", PublicOverrideVisibility);
			clone.Statements.Add(VariableDecl(TableClassName(table), "cln", Cast(TableClassName(table), MethodCall(Base(), "Clone", new CodeExpression[0]))));
			clone.Statements.Add(MethodCall(Variable("cln"), "InitVars", new CodeExpression[0]));
			clone.Statements.Add(Return(Variable("cln")));
      
			return clone;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableCreateInstance(DataTable table)
		{
			// Create Instance
			CodeMemberMethod createInstance = MethodDecl("DataTable", "CreateInstance", FamilyOverrideVisibility);
			createInstance.Statements.Add(Return(New(TableClassName(table), new CodeExpression[0])));
      
			return createInstance;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableInitClass(DataTable table)
		{
			// Create the Methods
			CodeMemberMethod initClass = MethodDecl("System.Void", "InitClass", PrivateVisibility);

			// Add access to each Column in the InitClass
			foreach (DataColumn column in table.Columns)
			{
				string tableColumnField = this.TableColumnFieldName(column);

				// The mapping Paramaters
				CodeExpression[] mappingParams = new CodeExpression[4];
				mappingParams[0] = Str(column.ColumnName);
				mappingParams[1] = TypeOf(this.GetTypeName(column.DataType));
				mappingParams[2] = Primitive(null);
				switch (column.ColumnMapping)
				{
					case MappingType.SimpleContent:
					{
						mappingParams[3] = Field(TypeExpr("System.Data.MappingType"), "SimpleContent");
						break;
					}
					case MappingType.Attribute:
					{
						mappingParams[3] = Field(TypeExpr("System.Data.MappingType"), "Attribute");
						break;
					}
					case MappingType.Hidden:
					{
						mappingParams[3] = Field(TypeExpr("System.Data.MappingType"), "Hidden");
						break;
					}
					default:
					{
						mappingParams[3] = Field(TypeExpr("System.Data.MappingType"), "Element");
						break;
					}
				}
				initClass.Statements.Add(Assign(Field(This(), tableColumnField), New("DataColumnExtended", mappingParams)));
				initClass.Statements.Add(MethodCall(Property(This(), "Columns"), "Add", Field(This(), tableColumnField)));
			}

			// Add Constraints       
			foreach (Constraint constraint in table.Constraints)
			{
				if (constraint is UniqueConstraint) 
				{
					UniqueConstraint unique = constraint as UniqueConstraint;
					CodeExpression[] theParams = new CodeExpression[checked((uint) (int) unique.Columns.Length)];
					for (int x = 0; x < unique.Columns.Length; ++x)
					{
						theParams[x] = Field(This(), TableColumnFieldName(unique.Columns[x]));
					}
					CodeExpression[] addParams = new CodeExpression[3];
					addParams[0] = Str(unique.ConstraintName);
					addParams[1] = new CodeArrayCreateExpression("DataColumn", theParams);
					addParams[2] = Primitive(unique.IsPrimaryKey);
					initClass.Statements.Add(MethodCall(Property(This(), "Constraints"), "Add", New("UniqueConstraint", addParams)));
				}
			}

			// Initialize each column as necessary
			foreach (DataColumn column in table.Columns)
			{
				string colName = this.TableColumnFieldName(column);
				CodeExpression colField = Field(This(), colName);
				if (column.AutoIncrement) 
				{
					initClass.Statements.Add(Assign(Property(colField, "AutoIncrement"), Primitive(true)));
				}
				if (column.AutoIncrementSeed != (long) 0)
				{
					initClass.Statements.Add(Assign(Property(colField, "AutoIncrementSeed"), Primitive(column.AutoIncrementSeed)));
				}
				if (column.AutoIncrementStep != (long) 1)
				{
					initClass.Statements.Add(Assign(Property(colField, "AutoIncrementStep"), Primitive(column.AutoIncrementStep)));
				}
				if (!(column.AllowDBNull))
				{
					initClass.Statements.Add(Assign(Property(colField, "AllowDBNull"), Primitive(false)));
				}
				if (column.ReadOnly)
				{
					initClass.Statements.Add(Assign(Property(colField, "ReadOnly"), Primitive(true)));
				}
				if (column.Unique)
				{
					initClass.Statements.Add(Assign(Property(colField, "Unique"), Primitive(true)));
				}
				if (column.Prefix.Length > 0)
				{
					initClass.Statements.Add(Assign(Property(colField, "Prefix"), Str(column.Prefix)));
				}

				// Get the inaccessible _columnUri property 
				if (GetInaccessibleFieldValue(column, "_columnUri") != null)
				{
					initClass.Statements.Add(Assign(Property(colField, "Namespace"), Str(column.Namespace)));
				}
				if (column.Caption != column.ColumnName)
				{
					initClass.Statements.Add(Assign(Property(colField, "Caption"), Str(column.Caption)));
				}
				if (column.DefaultValue != DBNull.Value)
				{
					initClass.Statements.Add(Assign(Property(colField, "DefaultValue"), Primitive(column.DefaultValue)));
				}
				if (column.MaxLength != -1)
				{
					initClass.Statements.Add(Assign(Property(colField, "MaxLength"), Primitive(column.MaxLength)));
				}
			}

			// Set Table Init Values
			if (((bool)GetInaccessibleFieldValue(table, "caseSensitiveAmbient")) == false)
			{
				initClass.Statements.Add(Assign(Property(This(), "CaseSensitive"), Primitive(table.CaseSensitive)));
			}
			if (GetInaccessibleFieldValue(table, "culture") != null) 
			{
				initClass.Statements.Add(Assign(Property(This(), "Locale"), New("System.Globalization.CultureInfo", new CodeExpression[] {Str(table.Locale.ToString())})));
			}
			if (table.Prefix != "")
			{
				initClass.Statements.Add(Assign(Property(This(), "Prefix"), Str(table.Prefix)));
			}
			if (GetInaccessibleFieldValue(table, "tableNamespace") != null)
			{
				initClass.Statements.Add(Assign(Property(This(), "Namespace"), Str(table.Namespace)));
			}
			if (table.MinimumCapacity != 50)
			{
				initClass.Statements.Add(Assign(Property(This(), "MinimumCapacity"), Primitive(table.MinimumCapacity)));
			}
			if (GetInaccessibleFieldValue(table, "displayExpression") != null)
			{
				initClass.Statements.Add(Assign(Property(This(), "DisplayExpression"), Str(table.DisplayExpression)));
			}

			// Add the Inits
			return initClass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableInitVars(DataTable table)
		{
			// Create the Methods
			CodeMemberMethod initVars = MethodDecl("System.Void", "InitVars", AssemblyVisibility);

			// Initialize each column as necessary
			foreach (DataColumn column in table.Columns)
			{
				CodeExpression colField = Field(This(), TableColumnFieldName(column));
				initVars.Statements.Add(Assign(colField, Indexer(Property(This(), "Columns"), Str(column.ColumnName))));
			}

			// Add the Inits
			return initVars;
		}
    
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableAddRow(DataTable table)
		{
			// Add New Row Method
			CodeMemberMethod newRowMethod = MethodDecl(RowConcreteClassName(table), "New" + RowClassName(table), PublicVisibility);
			newRowMethod.Statements.Add(Return(Cast(RowConcreteClassName(table), MethodCall(This(), "NewRow"))));

			return newRowMethod;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableNewFromBuilder(DataTable table)
		{
			// Add New from Builder Method
			CodeMemberMethod newFromBuilderMethod = MethodDecl("DataRow", "NewRowFromBuilder", FamilyOverrideVisibility);
			newFromBuilderMethod.Parameters.Add(ParameterDecl("DataRowBuilder", "builder"));
			newFromBuilderMethod.Statements.Add(Return(New(RowConcreteClassName(table), new CodeExpression[] {Argument("builder")})));
      
			return newFromBuilderMethod;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableGetRowType(DataTable table)
		{
			// Add GetRowType
			CodeMemberMethod getRowTypeMethod = MethodDecl("System.Type", "GetRowType", FamilyOverrideVisibility);
			getRowTypeMethod.Statements.Add(Return(TypeOf(RowConcreteClassName(table))));

			return getRowTypeMethod;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeMemberCollection GenerateTableEventHandlers(DataTable table)
		{
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();

			// Add Events
			collection.Add(CreateOnRowEventMethod("Changed", RowClassName(table)));
			collection.Add(CreateOnRowEventMethod("Changing", RowClassName(table)));
			collection.Add(CreateOnRowEventMethod("Deleted", RowClassName(table)));
			collection.Add(CreateOnRowEventMethod("Deleting", RowClassName(table)));

			// Add Events
			collection.Add(EventDecl(RowClassName(table)+ "ChangeEventHandler", RowClassName(table) + "Changed"));
			collection.Add(EventDecl(RowClassName(table)+ "ChangeEventHandler", RowClassName(table) + "Changing"));
			collection.Add(EventDecl(RowClassName(table)+ "ChangeEventHandler", RowClassName(table) + "Deleted"));
			collection.Add(EventDecl(RowClassName(table)+ "ChangeEventHandler", RowClassName(table) + "Deleting"));

			return collection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="rowClassName"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod CreateOnRowEventMethod(string eventName, string rowClassName) 
		{
			// Add the OnRow event
			CodeMemberMethod method = MethodDecl("System.Void", "OnRow" + eventName, FamilyOverrideVisibility);
			method.Parameters.Add(ParameterDecl("DataRowChangeEventArgs", "e"));
			method.Statements.Add(MethodCall(Base(), "OnRow" + eventName, Argument("e")));
			CodeExpression[] expression = new CodeExpression[2];
			expression[0] = Cast(rowClassName, Property(Argument("e"), "Row"));
			expression[1] = Property(Argument("e"), "Action");
			method.Statements.Add(If(IdNotEQ(Event(rowClassName + eventName), Primitive(null)), Stm(DelegateCall(Event(rowClassName + eventName), New(rowClassName + "ChangeEvent", expression)))));
			return method;
		}
    
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateTableRemoveRow(DataTable table)
		{
			// Add Remove Rows
			CodeMemberMethod removeRowMethod = MethodDecl("System.Void", "Remove" + RowConcreteClassName(table), PublicVisibility);
			removeRowMethod.Parameters.Add(ParameterDecl(RowConcreteClassName(table), "row"));
			removeRowMethod.Statements.Add(MethodCall(Property(This(), "Rows"), "Remove", Argument("row")));

			return removeRowMethod;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <param name="tblClass"></param>
		/// <returns></returns>
		protected virtual CodeMemberField GenerateTableField(DataTable table, CodeTypeDeclaration tblClass)
		{
			// Add the Field to the Typed DataSet
			CodeMemberField tblField = FieldDecl(tblClass.Name, "table" + FixIdName(table.TableName));
			tblField.Attributes = PrivateVisibility;
      
			// Return the field
			return tblField;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <param name="tblClass"></param>
		/// <returns></returns>
		protected virtual CodeMemberProperty GenerateTableProperty(DataTable table, CodeTypeDeclaration tblClass, CodeMemberField tblField)
		{
			// Create the Table Property to the TDS
			CodeMemberProperty tblProp = PropertyDecl(tblClass.Name,  FixIdName(table.TableName), PublicVisibility);
			tblProp.HasGet = true;
			tblProp.HasSet = false;
			tblProp.GetStatements.Add(Return(Field(This(), tblField.Name)));
			tblProp.CustomAttributes.Add(AttributeDecl("System.ComponentModel.Browsable", Primitive(false)));
			tblProp.CustomAttributes.Add(AttributeDecl("System.ComponentModel.DesignerSerializationVisibilityAttribute", Field(TypeExpr(typeof(System.ComponentModel.DesignerSerializationVisibility)), "Content")));
     
			// Return the Property
			return tblProp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeDelegate CreateTypedRowEventHandler(DataTable table) 
		{
			// Create the delegate
			CodeTypeDelegate handler = DelegateDecl(RowClassName(table) + "ChangeEventHandler");
			handler.TypeAttributes = handler.TypeAttributes | TypeAttributes.Public;
			handler.Parameters.Add(ParameterDecl(typeof(Object), "sender"));
			handler.Parameters.Add(ParameterDecl(RowClassName(table) + "ChangeEvent", "e"));

			// Return the Handler
			return handler;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <param name="table"></param>
		protected virtual void GenerateDataColumn(DataColumn column, CodeTypeDeclaration table)
		{
			// Create the Column Field
			CodeMemberField colField = FieldDecl("DataColumn", TableColumnFieldName(column));
			colField.Attributes = PrivateVisibility;
			table.Members.Add(colField);

			// Create the Column Property
			CodeMemberProperty colProp = PropertyDecl("DataColumnExtended",  TableColumnPropertyName(column), PublicVisibility);
			colProp.HasGet = true;
			colProp.HasSet = false;
			colProp.GetStatements.Add(Return(Cast(Type("DataColumnExtended"), Field(This(), colField.Name))));
			table.Members.Add(colProp);
		}

		#endregion

		#region DataRow Generation
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeDeclaration CreateTypedRow(DataTable table) 
		{
			// Create the Class Declaration
			CodeTypeDeclaration rowClass = AddComments(new CodeTypeDeclaration());
			rowClass.Name = RowClassName(table);
			rowClass.BaseTypes.Add(RowBaseClassName(table));
			rowClass.CustomAttributes.Add(AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
      
			// Add Table Field
			rowClass.Members.Add(GenerateRowTableField(table));

			// Constructor
			rowClass.Members.Add(CreateRowConstructor(table));

			// Add Columns
			rowClass.Members.AddRange(GenerateRowColumnProperties(table));

			return rowClass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeMemberField GenerateRowTableField(DataTable table)
		{
			return FieldDecl(TableClassName(table), TableFieldName(table));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeConstructor CreateRowConstructor(DataTable table)
		{
			// Create the Constructor
			CodeConstructor xtor = this.ConstructorDecl();
			xtor.Attributes = AssemblyVisibility;
			xtor.Parameters.Add(ParameterDecl("DataRowBuilder", "rb"));
			xtor.BaseConstructorArgs.Add(Argument("rb"));
			xtor.Statements.Add(Assign(Field(This(), TableFieldName(table)), Cast(TableClassName(table), Property(This(), "Table"))));

			return xtor;
		}

		protected virtual CodeTypeMemberCollection GenerateRowColumnProperties(DataTable table)
		{
			DataColumn[] columns = GetTableNonHiddenColumns(table);
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();

			// Iterate through all the columns
			foreach (DataColumn column in columns)
			{
				CodeTypeMemberCollection rowAccessorMethods = GenerateRowColumnAccessor(column);
				if (rowAccessorMethods != null)
				{
					collection.AddRange(rowAccessorMethods);
					if (column.AllowDBNull) 
					{
						collection.Add(GenerateRowColumnIsNull(column));
						collection.Add(GenerateRowColumnSetNull(column));
					}
				}
			}

			// Add Children/Parent members
			collection.AddRange(GenerateRowGetChildrenMethods(table));
			collection.AddRange(GenerateRowGetParentProperties(table));

			return collection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		protected virtual CodeTypeMemberCollection GenerateRowColumnAccessor(DataColumn column)
		{
			bool hasNewRow = false;
			CodeExpression nullValueExpression = null;
			Object[] errorObjs;
			CodeExpression nullInitExpression = null;

			// Create the Collection
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();

			// Create the Column Accessors
			Type colType = column.DataType;
			CodeMemberProperty colProp = PropertyDecl(this.GetTypeName(colType), RowColumnPropertyName(column), PublicVisibility);
			CodeStatement returnClause = Return(Cast(this.GetTypeName(colType), Indexer(This(), Property(Field(This(), TableFieldName(column.Table)), TableColumnPropertyName(column)))));
			CodeStatement setClause;

			// If DBNull is allowed, figure out how to handle it
			if (column.AllowDBNull) 
			{
				// Get the ms:codegen property if any
				string nullValue = (String) column.ExtendedProperties["nullValue"];
				if (nullValue == null || nullValue == "_throw")
				{
					returnClause = Try(returnClause, Catch("InvalidCastException", "e", Throw("StrongTypingException", "Cannot get value because it is DBNull.", "e")));
				}
				else 
				{
					nullInitExpression = null;
					if (nullValue == "_null") 
					{
						if (column.DataType.IsSubclassOf(typeof(ValueType))) 
						{
							errorList.Add(string.Format("Specified Type cannot be cast to null since it is a Value type: ColumnName: {0} Type: {1}", column.ColumnName, column.DataType));
							return null;
						}
						nullValueExpression = Primitive(null);
					}
					else 
					{
						if (nullValue == "_empty") 
						{
							if (column.DataType == typeof(String)) 
							{
								nullValueExpression = Property(TypeExpr(column.DataType), "Empty");
							}
							else
							{
								nullValueExpression = Field(TypeExpr(RowClassName(column.Table)), RowColumnPropertyName(column) + "_nullValue");
								ConstructorInfo xtorInfo = column.DataType.GetConstructor(new Type[] {typeof(String)});
								if (xtorInfo == null) 
								{
									errorList.Add(string.Format("No acceptable type of constructor to convert null value:  ColumnName: {0} Type: {1}", column.ColumnName, column.DataType.Name));
									return null;
								}
								xtorInfo.Invoke(new Object[0]);
								nullValueExpression = New(column.DataType.FullName, new CodeExpression[0]);
							}
						}
						else 
						{
							if (!(hasNewRow)) 
							{
								column.Table.NewRow();
								hasNewRow = true;
							}
							object columnAsXml = GetInaccessibleMethod(column, "ConvertXmlToObject").Invoke(column, new object[] {nullValue});
							// If it is a well known data type
							if (column.DataType == typeof(Char) || column.DataType == typeof(String) || column.DataType == typeof(Decimal) || column.DataType == typeof(Boolean) || column.DataType == typeof(Single) || column.DataType == typeof(Double) || column.DataType == typeof(SByte) || column.DataType == typeof(Byte) || column.DataType == typeof(Int16) || column.DataType == typeof(UInt16) || column.DataType == typeof(Int32) || column.DataType == typeof(UInt32) || column.DataType == typeof(Int64) || column.DataType == typeof(UInt64))
							{
								nullValueExpression = Primitive(columnAsXml);
							}
							else 
							{
								nullValueExpression = Field(TypeExpr(RowClassName(column.Table)), RowColumnPropertyName(column) + "_nullValue");
								if (column.DataType == typeof(byte[]))
								{
									nullInitExpression = MethodCall(TypeExpr(typeof(Convert)), "FromBase64String", Primitive(nullValue));
								}
								else 
								{
									if (column.DataType == typeof(DateTime) || column.DataType == typeof(TimeSpan))
									{
										nullInitExpression = MethodCall(TypeExpr(column.DataType), "Parse", Primitive(columnAsXml.ToString()));
									}
									else 
									{
										ConstructorInfo xtorInfo = column.DataType.GetConstructor(new Type[] { typeof(String) });
										if (xtorInfo == null) 
										{
											errorList.Add(string.Format("No acceptable type of constructor to convert null value:  ColumnName: {0} Type: {1}", column.ColumnName, column.DataType.Name));
											return null;
										}
										errorObjs = new Object[1];
										errorObjs[0] = nullValue;
										xtorInfo.Invoke(errorObjs);
										nullInitExpression = New(column.DataType.FullName, new CodeExpression[] {Primitive(nullValue)});
									}
								}
							}
						}
					}
					returnClause = If(MethodCall(This(), "Is" + RowColumnPropertyName(column) + "Null"), new CodeStatement[] {Return(nullValueExpression)}, new CodeStatement[] {returnClause});
					if (nullInitExpression != null) 
					{
						// Add the Init Field if needed
						collection.Add(GenerateRowColumnNullInitField(column, nullInitExpression));
					}
				}
			}

			// Generate the set method
			nullValueExpression = null;
			if(column.AllowDBNull && Convert.ToBoolean(column.ExtendedProperties["isDBNullOnNullValue"])) 
			{
				// Get the ms:codegen property if any
				string nullValue = (String) column.ExtendedProperties["nullValue"];
				
				if (nullValue == "_null") 
				{
					if (column.DataType.IsSubclassOf(typeof(ValueType))) 
					{
						errorList.Add(string.Format("Specified Type cannot be cast to null since it is a Value type: ColumnName: {0} Type: {1}", column.ColumnName, column.DataType));
						return null;
					}
					nullValueExpression = Primitive(null);
				}
				else 
				{
					if (nullValue == "_empty") 
					{
						if (column.DataType == typeof(String)) 
						{
							nullValueExpression = Property(TypeExpr(column.DataType), "Empty");
						}
						else
						{
							nullValueExpression = Field(TypeExpr(RowClassName(column.Table)), RowColumnPropertyName(column) + "_nullValue");
							ConstructorInfo xtorInfo = column.DataType.GetConstructor(new Type[] {typeof(String)});
							if (xtorInfo == null) 
							{
								errorList.Add(string.Format("No acceptable type of constructor to convert null value:  ColumnName: {0} Type: {1}", column.ColumnName, column.DataType.Name));
								return null;
							}
							xtorInfo.Invoke(new Object[0]);
							nullValueExpression = New(column.DataType.FullName, new CodeExpression[0]);
						}
					}
					else 
					{
						if (!(hasNewRow)) 
						{
							column.Table.NewRow();
							hasNewRow = true;
						}
						object columnAsXml = GetInaccessibleMethod(column, "ConvertXmlToObject").Invoke(column, new object[] {nullValue});
						// If it is a well known data type
						if (column.DataType == typeof(Char) || column.DataType == typeof(String) || column.DataType == typeof(Decimal) || column.DataType == typeof(Boolean) || column.DataType == typeof(Single) || column.DataType == typeof(Double) || column.DataType == typeof(SByte) || column.DataType == typeof(Byte) || column.DataType == typeof(Int16) || column.DataType == typeof(UInt16) || column.DataType == typeof(Int32) || column.DataType == typeof(UInt32) || column.DataType == typeof(Int64) || column.DataType == typeof(UInt64))
						{
							nullValueExpression = Primitive(columnAsXml);
						}
						else 
						{
							nullValueExpression = Field(TypeExpr(RowClassName(column.Table)), RowColumnPropertyName(column) + "_nullValue");
							if (column.DataType == typeof(byte[]))
							{
								nullInitExpression = MethodCall(TypeExpr(typeof(Convert)), "FromBase64String", Primitive(nullValue));
							}
							else 
							{
								if (column.DataType == typeof(DateTime) || column.DataType == typeof(TimeSpan))
								{
									nullInitExpression = MethodCall(TypeExpr(column.DataType), "Parse", Primitive(columnAsXml.ToString()));
								}
								else 
								{
									ConstructorInfo xtorInfo = column.DataType.GetConstructor(new Type[] { typeof(String) });
									if (xtorInfo == null) 
									{
										errorList.Add(string.Format("No acceptable type of constructor to convert null value:  ColumnName: {0} Type: {1}", column.ColumnName, column.DataType.Name));
										return null;
									}
									errorObjs = new Object[1];
									errorObjs[0] = nullValue;
									xtorInfo.Invoke(errorObjs);
									nullInitExpression = New(column.DataType.FullName, new CodeExpression[] {Primitive(nullValue)});
								}
							}
						}
					}
					
				}
			}

			if(nullValueExpression != null) 
			{
				setClause = If(EQ(Value(), nullValueExpression), new CodeStatement[] {new CodeExpressionStatement(MethodCall(null, "Set" + column.ColumnName + "Null"))}, new CodeStatement[] {Assign(Indexer(This(), Property(Field(This(), TableFieldName(column.Table)), TableColumnPropertyName(column))), Value())});
			} 
			else 
			{
				setClause = Assign(Indexer(This(), Property(Field(This(), TableFieldName(column.Table)), TableColumnPropertyName(column))), Value());
			}
      
			// Add the Get and Set Statements
			colProp.GetStatements.Add(returnClause);
			colProp.SetStatements.Add(setClause);

			// Add the Column Property to the Collection
			collection.Add(colProp);
      
			return collection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <param name="nullInitExpression"></param>
		/// <returns></returns>
		protected virtual CodeMemberField GenerateRowColumnNullInitField(DataColumn column, CodeExpression nullInitExpression)
		{
			CodeMemberField colFieldDecl = FieldDecl(column.DataType.FullName, RowColumnPropertyName(column) + "_nullValue");
			colFieldDecl.Attributes = PublicVisibility | MemberAttributes.Static;
			colFieldDecl.InitExpression = nullInitExpression;
      
			return colFieldDecl;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateRowColumnIsNull(DataColumn column)
		{
			CodeMemberMethod isMethod = MethodDecl("System.Boolean", "Is" + RowColumnPropertyName(column) + "Null", PublicVisibility);
			isMethod.Statements.Add(Return(MethodCall(This(), "IsNull", Property(Field(This(), TableFieldName(column.Table)), TableColumnPropertyName(column)))));
        
			return isMethod;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateRowColumnSetNull(DataColumn column)
		{
			CodeMemberMethod setMethod = MethodDecl("System.Void", "Set" + RowColumnPropertyName(column) + "Null", PublicVisibility);
			setMethod.Statements.Add(Assign(Indexer(This(), Property(Field(This(), TableFieldName(column.Table)), TableColumnPropertyName(column))), Field(TypeExpr(typeof(Convert)), "DBNull")));
      
			return setMethod;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeMemberCollection GenerateRowGetChildrenMethods(DataTable table)
		{
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();
      
			// Add Child Relation Methods
			foreach (DataRelation rel in table.ChildRelations)
			{
				collection.Add(GenerateRowGetChildren(rel));
			}

			return collection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rel"></param>
		/// <returns></returns>
		protected virtual CodeMemberMethod GenerateRowGetChildren(DataRelation rel)
		{
			CodeMemberMethod childRelMethod = Method(Type(RowConcreteClassName(rel.ChildTable), 1), ChildPropertyName(rel), PublicVisibility);
			childRelMethod.Statements.Add(Return(Cast(Type(RowConcreteClassName(rel.ChildTable), 1), MethodCall(This(), "GetChildRows", Indexer(Property(Property(This(), "Table"), "ChildRelations"), Str(rel.RelationName))))));
        
			return childRelMethod;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeMemberCollection GenerateRowGetParentProperties(DataTable table)
		{
			CodeTypeMemberCollection collection = new CodeTypeMemberCollection();

			// Add Parent Relations Accessors
			foreach (DataRelation relation in table.ParentRelations)
			{
				collection.Add(GenerateRowGetParent(relation));
			}
			return collection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rel"></param>
		/// <returns></returns>
		protected virtual CodeMemberProperty GenerateRowGetParent(DataRelation relation)
		{
			// Create the Property
			CodeMemberProperty parentRowProperty = PropertyDecl(RowClassName(relation.ParentTable), this.ParentPropertyName(relation), PublicVisibility);

			// Generate get and set methods
			parentRowProperty.GetStatements.Add(Return(Cast(RowClassName(relation.ParentTable), MethodCall(This(), "GetParentRow", Indexer(Property(Property(This(), "Table"), "ParentRelations"), Str(relation.RelationName))))));
			CodeExpression[] parentParams = new CodeExpression[2];
			parentParams[0] = Value();
			parentParams[1] = Indexer(Property(Property(This(), "Table"), "ParentRelations"), Str(relation.RelationName));
			parentRowProperty.SetStatements.Add(MethodCall(This(), "SetParentRow", parentParams));
      
			return parentRowProperty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		protected virtual CodeTypeDeclaration CreateTypedRowEvent(DataTable table) 
		{
			CodeTypeDeclaration eventType = AddComments(new CodeTypeDeclaration());
			eventType.Name = RowClassName(table) + "ChangeEvent";
			eventType.BaseTypes.Add("EventArgs");
			eventType.CustomAttributes.Add(AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			eventType.Members.Add(FieldDecl(RowConcreteClassName(table), "eventRow"));
			eventType.Members.Add(FieldDecl("DataRowAction", "eventAction"));
			CodeConstructor xtor = ConstructorDecl();
			xtor.Attributes = PublicVisibility;
			xtor.Parameters.Add(ParameterDecl(RowConcreteClassName(table), "row"));
			xtor.Parameters.Add(ParameterDecl("DataRowAction", "action"));
			xtor.Statements.Add(Assign(Field(This(), "eventRow"), Argument("row")));
			xtor.Statements.Add(Assign(Field(This(), "eventAction"), Argument("action")));
			eventType.Members.Add(xtor);
			CodeMemberProperty prop = PropertyDecl(RowConcreteClassName(table), "Row", PublicVisibility);
			prop.GetStatements.Add(Return(Field(This(), "eventRow")));
			eventType.Members.Add(prop);
			prop = PropertyDecl("DataRowAction", "Action", PublicVisibility);
			prop.GetStatements.Add(Return(Field(This(), "eventAction")));
			eventType.Members.Add(prop);
			return eventType;
		}

		#endregion

		#region Visibility Methods
		/// <summary>
		/// 
		/// </summary>
		protected virtual MemberAttributes PublicVisibility
		{
			get { return MemberAttributes.Public | MemberAttributes.Final; }
		}
		/// <summary>
		/// 
		/// </summary>
		protected virtual MemberAttributes PublicOverrideVisibility
		{
			get { return MemberAttributes.Public | MemberAttributes.Override; }
		}
		/// <summary>
		/// 
		/// </summary>
		protected virtual MemberAttributes FamilyVisibility
		{
			get { return MemberAttributes.Family | MemberAttributes.Final; }
		}
		/// <summary>
		/// 
		/// </summary>
		protected virtual MemberAttributes FamilyOverrideVisibility
		{
			get { return MemberAttributes.Family | MemberAttributes.Override; }
		}
		/// <summary>
		/// 
		/// </summary>
		protected virtual MemberAttributes AssemblyVisibility
		{
			get { return MemberAttributes.Assembly | MemberAttributes.Final; }
		}
		/// <summary>
		/// 
		/// </summary>
		protected virtual MemberAttributes PrivateVisibility
		{
			get { return MemberAttributes.Private; }
		}

		#endregion

		#region Miscellaneous Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="methodName"></param>
		/// <returns></returns>
		protected virtual MethodInfo GetInaccessibleMethod(object source, string methodName)
		{
			return source.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="fieldName"></param>
		/// <returns></returns>
		protected virtual object GetInaccessibleFieldValue(object source, string fieldName)
		{
			Type sourceType = source.GetType();
			FieldInfo field = sourceType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			object value = field.GetValue(source);
			return value;
		}
    
		#endregion

		#region Naming Functions
		protected virtual string TableClassName(DataTable table) 
		{
			string result = (String) table.ExtendedProperties["typedPlural"];
			if (IsEmpty(result)) 
			{
				result = (String) table.ExtendedProperties["typedName"];
				if (IsEmpty(result))
					result = this.FixIdName(table.TableName);
			}
			return result + "DataTable";
		}

		protected virtual string TableColumnFieldName(DataColumn column) 
		{
			return "column" + this.RowColumnPropertyName(column);
		}

		protected virtual string TableColumnPropertyName(DataColumn column) 
		{
			return this.RowColumnPropertyName(column) + "Column";
		}

		protected virtual string TableFieldName(DataTable table) 
		{
			return "table" + this.TablePropertyName(table);
		}

		protected virtual string TablePropertyName(DataTable table) 
		{
			string result;

			result = (String) table.ExtendedProperties["typedPlural"];
			if (IsEmpty(result)) 
			{
				result = (String) table.ExtendedProperties["typedName"];
				if (IsEmpty(result))
				{
					result = FixIdName(table.TableName);
				}
				else
				{
					result = result + "Table";
				}
			}
			return result;
		}


		protected virtual string RowBaseClassName(DataTable table) 
		{
			return "DataRow";
		}
    
		protected virtual string RowClassName(DataTable table) 
		{
			string result;

			result = (String) table.ExtendedProperties["typedName"];
			if (IsEmpty(result))
			{
				result = FixIdName(table.TableName) + "Row";
			}
			return result;
		}

		protected virtual string RowColumnPropertyName(DataColumn column) 
		{
			string result = (String) column.ExtendedProperties["typedName"];
			if (IsEmpty(result))
			{
				result = FixIdName(column.ColumnName);
			}
			return result;
		}

		protected virtual string RowConcreteClassName(DataTable table) 
		{
			return RowClassName(table);
		}

		protected virtual string ChildPropertyName(DataRelation relation) 
		{
			string result  = (String) relation.ExtendedProperties["typedChildren"];
			string temp;

			if (IsEmpty(result)) 
			{
				temp = (String) relation.ChildTable.ExtendedProperties["typedPlural"];
				if (IsEmpty(temp)) 
				{
					temp = (String) relation.ChildTable.ExtendedProperties["typedName"];
					if (IsEmpty(temp)) 
					{
						result = "Get" + relation.ChildTable.TableName + "Rows";
						if (1 < TablesConnectedness(relation.ParentTable, relation.ChildTable))
						{
							result = result + "By" + relation.RelationName;
						}
						return FixIdName(result);
					}
					temp = temp + "Rows";
				}
				result = "Get" + temp;
			}
			return result;
		}

		protected virtual bool ChildRelationFollowable(DataRelation relation) 
		{
			if (relation != null) 
			{
				if (relation.ChildTable == relation.ParentTable && relation.ChildTable.Columns.Count == 1)
					return false;
				return true;
			}
			return false;
		}

		protected virtual string ParentPropertyName(DataRelation relation) 
		{
			string result = (String) relation.ExtendedProperties["typedParent"];
			if (IsEmpty(result)) 
			{
				result = RowClassName(relation.ParentTable);
				if (relation.ChildTable == relation.ParentTable || (int) relation.ChildColumns.Length != 1)
				{
					result = result + "Parent";
				}

				if (1 < TablesConnectedness(relation.ParentTable, relation.ChildTable))
				{
					result = result + "By" + FixIdName(relation.RelationName);
				}
			}
			return result;
		}
    
		protected virtual int TablesConnectedness(DataTable parentTable, DataTable childTable) 
		{
			int result = 0;
			DataRelationCollection relations  = childTable.ParentRelations;
			int counter = 0;

			while (counter < relations.Count) 
			{
				if (relations[counter].ParentTable == parentTable) result++;
				counter++;
			}
			return result;
		}

		protected virtual bool IsEmpty(string s) 
		{
			if (s != null)
				return s == String.Empty;
			return true;
		}

		protected virtual string FixIdName(string inVarName) 
		{
			string result;

			if (lookupIdentifiers == null) InitLookupIdentifiers();

			result = (String) lookupIdentifiers[inVarName];
      
			if (result == null) 
			{
				result = GenerateIdName(inVarName);
        
				while (lookupIdentifiers.ContainsValue(result)) result = "_" + result;
        
				lookupIdentifiers[inVarName] = result;
        
				if (!(codeGen.IsValidIdentifier(result))) 
				{
					errorList.Add(string.Format("Error: Illegal Name: {0}", result));
				}
			}
			return result;
		}
    
		public string GenerateIdName(string name) 
		{
			string result;
			UnicodeCategory category;
			int ordinal = 0;

			if (codeGen.IsValidIdentifier(name)) return name;

			result = name.Replace(' ', '_');
			if (!(codeGen.IsValidIdentifier(result))) 
			{
				result = "_" + result;
				ordinal = 1;
				while (ordinal < result.Length) 
				{
					category = Char.GetUnicodeCategory(result[ordinal]);
					if (category != UnicodeCategory.UppercaseLetter && 
						UnicodeCategory.LowercaseLetter != category && 
						UnicodeCategory.TitlecaseLetter != category && 
						UnicodeCategory.ModifierLetter != category && 
						UnicodeCategory.OtherLetter != category && 
						UnicodeCategory.LetterNumber != category && 
						UnicodeCategory.NonSpacingMark != category && 
						UnicodeCategory.SpacingCombiningMark != category && 
						UnicodeCategory.DecimalDigitNumber != category && 
						UnicodeCategory.ConnectorPunctuation != category)
					{
						result = result.Replace(result[ordinal], '_');
					}
					ordinal++;
				}
			}
			return result;
		}

		protected virtual void InitLookupIdentifiers() 
		{
			lookupIdentifiers = new Hashtable();
			foreach (PropertyInfo propInfo in typeof(DataRow).GetProperties())
			{
				lookupIdentifiers[propInfo.Name] = "_" + propInfo.Name;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="relation"></param>
		/// <returns></returns>
		protected virtual string RelationFieldName(DataRelation relation) 
		{
			return FixIdName("relation" + relation.RelationName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		protected virtual string DataSetName
		{
			get 
			{
				return FixIdName(_dataSet.DataSetName);
			}
		}

		#endregion
	}
}