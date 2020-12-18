using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;


namespace QSPForm.Common.DataDef
{
    /// <summary>
    ///     A custom serializable dataset containing user information.
    ///     <remarks>
    ///         This class is used to define the shape of UserData.
    ///     </remarks>
    ///     <remarks>
    ///         The serializale constructor allows objects of type UserData to be remoted.
    ///     </remarks>
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    [SerializableAttribute]
    public class Promo_ImageTable : DataTable, System.Collections.IEnumerable
    {

        public const string TBL_PROMO = "QSPForm_promo_image";
        public const string FLD_PKID = "ID";
        public const string FLD_PROMOTION_ID = "Promotion_ID";
        public const string FLD_FORMAT_ID = "Format_ID";
        public const string FLD_FILE_EXTENSION = "File_Extension";
        public const string FLD_DELETED = "Deleted";
        public const string FLD_CREATE_DATE = "Create_Date";
        public const string FLD_CREATE_USER_ID = "Create_User_ID";
        public const string FLD_UPDATE_DATE = "Update_Date";
        public const string FLD_UPDATE_USER_ID = "Update_User_ID";
        public const string FLD_NAME = "Name";
        public const string FLD_DESCRIPTION = "Description";
        public const string FLD_CATEGORY = "Category";

        public Promo_ImageTable()
            :
            base(TBL_PROMO)
        {
            this.InitClass();
        }

        public Promo_ImageTable(DataTable table)
            :
            base(table.TableName)
        {
            if ((table.CaseSensitive != table.DataSet.CaseSensitive))
            {
                this.CaseSensitive = table.CaseSensitive;
            }
            if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
            {
                this.Locale = table.Locale;
            }
            if ((table.Namespace != table.DataSet.Namespace))
            {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
            this.DisplayExpression = table.DisplayExpression;
        }

        /// <summary>
        ///     Constructor to support serialization.
        ///     <remarks>Constructor that supports serialization.</remarks> 
        ///     <param name="info">The SerializationInfo object to read from.</param>
        ///     <param name="context">Information on who is calling this method.</param>
        /// </summary>
        public Promo_ImageTable(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        public override DataTable Clone()
        {
            Promo_ImageTable cln = ((Promo_ImageTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }

        protected override DataTable CreateInstance()
        {
            return new Promo_ImageTable();
        }

        internal void InitVars()
        {

        }

        private void InitClass()
        {
            //
            // Create the Users table
            //
            this.TableName = TBL_PROMO;
            DataColumnCollection columns = this.Columns;

            DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));

            Column.AllowDBNull = false;
            //For the system, when PKID = 0, that means that is a new record
            //When we will update the row we will be able to know what kind of operation
            //we will have to do....
            Column.AutoIncrement = true;
            Column.AutoIncrementSeed = 0;
            Column.AutoIncrementStep = -1;

            //columns.Add(FLD_PKID,typeof(System.Int32));
            columns.Add(FLD_PROMOTION_ID, typeof(System.Int32));
            columns.Add(FLD_FORMAT_ID, typeof(System.Int32));
            columns.Add(FLD_FILE_EXTENSION, typeof(System.String));
            columns.Add(FLD_DELETED, typeof(System.Boolean));
            columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
            columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));
            columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));

            columns.Add(FLD_NAME, typeof(System.String));
            columns.Add(FLD_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_CATEGORY, typeof(System.Int32));

        }


    }
}
