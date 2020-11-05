using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace GA.BDC.WebApi.Reports.Helpers.Formatters
{
   public class DataTableCsvFormatter : BufferedMediaTypeFormatter
   {
      public DataTableCsvFormatter()
      {
         SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
      }
      public override bool CanReadType(Type type)
      {
         return false;
      }

      public override bool CanWriteType(Type type)
      {
         return type == typeof(DataTable);
      }

      public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
      {
         using (var writer = new StreamWriter(writeStream))
         {
            var dataTable = value as DataTable;
            var columnStringBuilder = new StringBuilder();
            var rowStringBuilder = new StringBuilder();
            if (dataTable != null)
            {
               foreach (DataColumn column in dataTable.Columns)
               {
                  columnStringBuilder.Append($"{column.ColumnName},");
               }
               columnStringBuilder.AppendLine("");
               writer.Write(columnStringBuilder.ToString());

               foreach (DataRow row in dataTable.Rows)
               {
                  foreach (var cell in row.ItemArray)
                  {
                     rowStringBuilder.Append($"{cell},");
                  }
                  rowStringBuilder.AppendLine("");
                  writer.Write(rowStringBuilder.ToString());
                  rowStringBuilder = new StringBuilder();
               }
            }
         }
      }
   }
}