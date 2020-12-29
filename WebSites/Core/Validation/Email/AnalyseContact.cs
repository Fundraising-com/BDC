using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.Validation.Email
{
    public class contact
    {
        public string m_email;

        public string email 
        {
            get { return m_email; }

            set
            {
                m_email = value.Trim();
                isValide = EmailValidator.ValidateEmailStrict(m_email);
            }
        }

        public string firstName { get; set; }
        public string lastname { get; set; }
        public string error { get; set; }
        public bool isValide { get; set; }
        public string webformat()
        {            
            return firstName + " " + lastname + "*" + email;
        }
    }

    public class AnalyseContact
    {
        public string ErrorString { get; set; }

        public List<contact> lst { get; set; }           
            
        public AnalyseContact()
        {
            lst = new List<contact>();
        }

        private contact setName(string[] name, contact co)
        {
            co.firstName = name[0];
            co.lastname = "";

            if (name.Length > 1) 
            {
                for (int i = 1; i < name.Length; ++i)
                {
                    co.lastname += " " +name[i];
                }
            }
            co.lastname.Trim();

            return co;
        }

        private contact anaylyseFinalPart(string txt)
        {
            contact co = new contact();
            int posGreater = txt.IndexOf('<');

            //HANDLE => "bbbbbb , aaaaaaa" <email@email.com>;
            if (posGreater > -1)
            {
                co.email = txt.Substring(posGreater, txt.Length - posGreater - 1).Replace(">", "").Replace("<","");

                string[] name = txt.Substring(0, posGreater).Replace("\"", "").Trim().Split(' ');
                
                co = setName(name,co);
            }
            else //HANDLE => "bbbbbb , aaaaaaa" email@email.com; ( this one is need to be support because of <> are consider as html injection
            {
                int revPos = txt.LastIndexOf('"');
                if (revPos == -1)
                {
                   revPos= txt.LastIndexOf('\'');
                }

                if (revPos == -1)
                {
                    co.email = txt.Replace("'","").Replace("\"","").Trim();
                }
                else
                {
                    co.email = txt.Substring(revPos+1, txt.Length - revPos - 1).Replace("'","").Replace("\"","").Trim();
                    string[] name = txt.Substring(0, revPos ).Replace("\"", "").Replace("'","").Split(' ');

                    co = setName(name, co);
                }                
            }

            return co;
        }

        private string analyse(string[] arr)
        {
            string resultValide="";
            string resultNotValide = "";

            foreach (string part in arr)
            {
                contact con = anaylyseFinalPart(part);
                lst.Add(con);

                if (con.isValide)
                {
                    if (resultValide.Length > 0) { resultValide += ";" + con.webformat(); }
                    else { resultValide = con.webformat(); }
                }
                else
                {
                    if (resultNotValide.Length > 0) { resultNotValide += ";" + con.webformat(); }
                    else { resultNotValide = con.webformat(); }
                }
            }

            resultNotValide= resultNotValide.Replace("*", "").Trim();

            return resultValide + "|" + resultNotValide.Replace("\r","").Replace("\n","") + "|";
        }

        public string analyseEmail(string txt)
        {
            string result = "";

            char semicoma = ';';
            char coma = ',';

            txt = replaceComeFix(txt);

            string[] arrSemiComa = txt.Split(semicoma);
            string[] arrComa = txt.Split(coma);

            if (arrComa.Length > arrSemiComa.Length)
            {
                result = analyse(arrComa);
            }
            else
            {
                result =analyse(arrSemiComa);
            }

            return result;
        }

        private string replaceComeFix(string txt)
        {
            txt = txt.ToLower();
            txt = txt.Replace("  ", " ");
            txt = txt.Replace(" ,", ",").Replace(", ",",");

            string pattern = "\"(?<t1>[a-z]+),(?<t2>[a-z]+)\"";

            Match mc = Regex.Match(txt, pattern);

            while (mc.Success)
            {
                string lastName = mc.Groups["t1"].Value;
                string firstName = mc.Groups["t2"].Value;
                int pos = mc.Index;

                txt = txt.Substring(0, pos) + "\"" + firstName + " " + lastName + "\"" + txt.Substring(pos + mc.Value.Length, txt.Length - mc.Value.Length - pos);

                mc = Regex.Match(txt, pattern);
            }

            return txt;
        }
    }
}
