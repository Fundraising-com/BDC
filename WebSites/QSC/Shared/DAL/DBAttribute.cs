using System;

namespace DAL
{
	/// <summary>
	/// DataTableAttribute - used on QDataTable derivations to provide table name etc
	/// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DataTableAttribute : Attribute
    {
        string tableName;

        public DataTableAttribute(string tableName)
        {
            this.tableName = tableName;
        }


        public string TableName
        {
            get { return tableName;  }
            set { tableName = value; }
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataColumnAttribute : Attribute
    {
        string columnName;

        public DataColumnAttribute(string columnName)
        {
            this.columnName = columnName;
        }

        public string ColumnName
        {
            get { return columnName;  }
            set { columnName = value; }
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataIdentityColumnAttribute : Attribute
    {
        string columnName;

        public DataIdentityColumnAttribute(string columnName)
        {
            this.columnName = columnName;
        }

        public string ColumnName
        {
            get { return columnName;  }
            set { columnName = value; }
        }
    }

}
