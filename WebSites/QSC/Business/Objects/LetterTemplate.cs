namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using Common.TableDef;
	using DAL;
	using dataSetRef = Common.TableDef.LetterTemplateDataSet;
	using dataAccessRef = DAL.LetterTemplateData;
	
	/// <summary>
	///     This class contains the business rules. 
	/// </summary>
	public class LetterTemplate : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		public LetterTemplate()
		{
			dtsDataSet = new dataSetRef();
			CreateRulesCollection();
		}

		public LetterTemplate(Transaction CurrentTransaction) : this()
		{
			this.CurrentTransaction = CurrentTransaction;
		}

		public dataSetRef dataSet
		{
			get 
			{
				return dtsDataSet;
			}
		}

		internal override DataSet baseDataSet
		{
			get 
			{
				return (DataSet) dtsDataSet;
			}
		}

		public override string DefaultTableName 
		{
			get 
			{
				return dataSet.LetterTemplate.TableName;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		public void GetAll()
		{
			try
			{
				dataAccess.SelectAll(dataSet, DefaultTableName);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
			}
		}

		public LetterTemplateItem GetOneLoadedByID(int id) 
		{
			return GetOneByID(dataSet, id);
		}

		public LetterTemplateItem GetOneByID(LetterTemplateDataSet letterTemplateDataSet, int id) 
		{
			return LetterTemplateItem.GetFromRow((LetterTemplateDataSet.LetterTemplateRow) letterTemplateDataSet.LetterTemplate.Select("ID = " + id.ToString())[0]);
		}
	}
}