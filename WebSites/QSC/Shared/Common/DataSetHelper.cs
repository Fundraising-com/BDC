using System;
using System.Data;
using System.Collections;

namespace Common
{
	/// <summary>
	/// Summary description for DataSetHelper.
	/// </summary>
	public class DataSetHelper
	{
		public static void CreateDataSetExtendedProperties(DataSet ds) 
		{
			DataSet dsExtended = new DataSet();
			string dataSetSchemaPath = System.Configuration.ConfigurationSettings.AppSettings["DataSetSchemaPath"];
			
			try 
			{
				dsExtended.ReadXmlSchema(dataSetSchemaPath + ds.GetType().Name + ".xsd");
				foreach(DataTable table in dsExtended.Tables)
				{
					if(ds.Tables.Contains(table.TableName)) 
					{
						foreach(DataColumn column in table.Columns) 
						{
							if(ds.Tables[table.TableName].Columns.Contains(column.ColumnName)) 
							{
								foreach(DictionaryEntry de in column.ExtendedProperties) 
								{
									ds.Tables[table.TableName].Columns[column.ColumnName].ExtendedProperties.Add(de.Key, de.Value);
								}
							}
						}
					}
				}
				ds.DataSetName = dsExtended.DataSetName;
				ds.Prefix = dsExtended.Prefix;
				ds.Namespace = dsExtended.Namespace;
				ds.Locale = dsExtended.Locale;
				ds.CaseSensitive = dsExtended.CaseSensitive;
				ds.EnforceConstraints = dsExtended.EnforceConstraints;
				ds.Merge(dsExtended, false, System.Data.MissingSchemaAction.Add);
			}
			catch (Exception) 
			{
			}
		}
	}
}
