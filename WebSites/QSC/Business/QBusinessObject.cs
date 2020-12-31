using System;
using System.Data;
using System.Data.SqlClient;
using DAL;
using System.Reflection;
using QCommon;
using System.ComponentModel;


namespace Business
{
	/// <summary>
	/// QBusinessObject.
	/// </summary>
	public abstract class QBusinessObject : IQError
	{
		public QBusinessObject()
		{
		}
		#region Data Members
//		protected string userIDCreatedM;
//		[DAL.DataColumn("UserIDCreated")]
//		public string UserIDCreated
//		{
//			get{ return this.userIDCreatedM; }
//			set{ this.userIDCreatedM = value; }
//		}
//		protected DateTime dateCreatedM;
//		[DAL.DataColumn("DateCreated")]
//		public DateTime DateCreated
//		{
//			get{ return this.dateCreatedM; }
//			set{ this.dateCreatedM = value; }
//		}
		protected DateTime dateChangedM;
		[DAL.DataColumn("DateChanged")]
		public DateTime DateChanged
		{
			get{ return this.dateChangedM; }
			set{ this.dateChangedM = value; }
		}
		protected string userIDChangedM;
		[DAL.DataColumn("UserIDChanged")]
		public string UserIDChanged
		{
			get{ return this.userIDChangedM; }
			set{ this.userIDChangedM = value; }
		}
		protected int UserIDModifiedM;
		[DAL.DataColumn("UserIDModified")]
		public int UserIDModified
		{
			get{ return this.UserIDModifiedM; }
			set{ this.UserIDModifiedM = value; }
		}
		#endregion

        abstract public bool ValidateAndSave();

        protected int nErrorCode;
        protected string zErrorCode;
        

        public int GetCode( ){return nErrorCode;}
        public void SetCode(int nCodeP) { nErrorCode = nCodeP;}

        /// <summary>
        /// Map the data row columns to the DataColumn properties
        /// </summary>
        protected void Fill(DataRow dr, Type aType )
        {
			if(aType!= Type.GetType("Business.QBusinessObject"))
				Fill(dr, aType.BaseType);
			
			foreach(PropertyInfo prop in aType.GetProperties(BindingFlags.Public 
                | BindingFlags.Instance
                | BindingFlags.Static 
                | BindingFlags.NonPublic
                | BindingFlags.Public
                | BindingFlags.DeclaredOnly
				| BindingFlags.FlattenHierarchy))
            {
								
                int n = prop.GetCustomAttributes(typeof(DAL.DataColumnAttribute),true).Length;

                if(n != 0)
                {

                    string nn =prop.Name;
                   // int i = (int)dr[nn];
                    PropertyDescriptor property = TypeDescriptor.GetProperties(this)[ nn ];
                  
					
					if(	property.PropertyType == typeof(System.Int32) ||
						property.PropertyType == typeof(System.Double)||
						property.PropertyType == typeof(System.DateTime))
					{
						try
						{
							property.SetValue(this,dr[nn]);
						}
						catch(System.ArgumentException e)
						{
							bool blIgnore = false;
							System.Text.RegularExpressions.Regex myRegExp = 
								new System.Text.RegularExpressions.Regex("UserIDModified");
							if( myRegExp.IsMatch( e.Message ) ) { blIgnore = true; }
							myRegExp = new System.Text.RegularExpressions.Regex("UserIDChanged");
							if( myRegExp.IsMatch( e.Message ) ) { blIgnore = true; }
							
							
//							if	(	(e.Message != "Column 'UserIDModified' does not belong to table Table.")&&
//									(e.Message != "Column 'UserIDChanged' does not belong to table Table.")										
//								)//ignore these two errors while we transition itemsfrom UserIDChanged (string) to UserIDModified (int)
//							{
//								throw e;
//							}

							if ( blIgnore == false )
							{
								throw e;
							}
			
						}

					}							
					else if(property.PropertyType == typeof(System.String) )
					{
						property.SetValue(this,dr[nn].ToString());
					}
					
                }
            }
        }




	}
}
