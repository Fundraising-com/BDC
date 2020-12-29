using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GA.BDC.PAP.Data.GpfRpcObjects
{
    [Serializable]
    internal class GpfRpcFormRespond
    {
        public GpfRpcFormRespond(JArray input)
        {
            if (input != null && input.First != null && input.First.HasValues)
            {
                if (input.First is JArray)
                {
                    this.Columns = new List<string[]>();
                    foreach (JToken jt in input.First)
                    {
                        if (jt is JArray && jt.Count() > 0)
                        {
                            List<string> temp = new List<string>();
                            foreach (JValue jv in jt)
                            {
                                if (jv.Type == JTokenType.String)
                                {
                                    temp.Add(jv.Value.ToString());
                                }
                            }
                            this.Columns.Add(temp.ToArray());
                        }
                    }
                }
                else if (input.First is JObject)
                {
                    if (input.First.First != null && input.First.First.First != null)
                    {
                        this.Columns = new List<string[]>();
                        foreach (JToken jt in input.First.First.First)
                        {
                            if (jt is JArray && jt.Count() > 0)
                            {
                                List<string> temp = new List<string>();
                                foreach (JValue jv in jt)
                                {
                                    temp.Add(jv.Type == JTokenType.Null ? string.Empty : jv.Value.ToString()); // Fix by Javier Arellano, January 30, 2014.
                                }
                                this.Columns.Add(temp.ToArray());
                            }
                        }
                    }
                }
            }


        }

        public string GetFieldValue(string fieldName, string fieldType)
        {
            if (Columns == null || Columns.Count <= 1) return string.Empty;
            var pos = Array.IndexOf(Columns[0], fieldName);
            if (pos < 0) return string.Empty;
            try
            {
                foreach (var s in Columns.Where(s => s[pos] == fieldType))
                {
                    return s[0];
                }
            }
            catch
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public string GetRowValue(string fieldName, string fieldValue)
        {
            if (this.Columns != null && Columns.Count > 1)
            {
                int pos = Array.IndexOf(Columns[0], fieldName);
                foreach (string[] ss in Columns)
                {
                    foreach (string s in ss)
                    {
                        if (s == fieldValue)
                        {
                            return ss[pos];
                        }
                    }
                }
            }
            return string.Empty;
        }

        public string GetRowValue(string fieldValue)
        {
            if (this.Columns != null && Columns.Count > 1)
            {
                int pos = Array.IndexOf(Columns[0], "value".ToString());
                foreach (string[] ss in Columns)
                {
                    foreach (string s in ss)
                    {
                        if (s == fieldValue)
                        {
                            return ss[pos];
                        }
                    }
                }


            }

            return string.Empty;
        }

        public List<String[]> Columns { get; set; }
    }
}
