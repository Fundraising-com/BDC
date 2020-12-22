using System;
using System.Data;
using System.IO;

namespace QSP.IO.Csv
{
    /// <summary>
    /// Summary description for CsvWriter.
    /// </summary>
    public class CsvWriter
    {

        public static string WriteToString(DataTable table, bool header, bool quoteall)
        {
            StringWriter writer = new StringWriter();
            WriteToStream(writer, table, header, quoteall);
            return writer.ToString();
        }

        public static string WriteToString(DataRow row, bool quoteall)
        {
            StringWriter stream = new StringWriter();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                WriteItem(stream, row[i], quoteall);
                if (i < row.Table.Columns.Count - 1)
                    stream.Write(",");
            }
            return stream.ToString();
        }

        public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(",");
                    else
                        stream.Write("\r\n");
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(",");
                    else
                        stream.Write("\r\n");
                }
            }
        }

        private static void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
                return;
            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            else
                stream.Write(s);
        }
    }

}
