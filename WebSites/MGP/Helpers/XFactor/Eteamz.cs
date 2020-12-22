using System;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Helpers.XFactor
{
    public class Eteamz
    {
        public Eteamz()
        {
            PersonList = new List<Person>();
        }

        public struct Person
        {
            public string ID { get; set; }
            public string TypeID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Person> PersonList { get; set; }
        #endregion

        #region Public Methos
        public static Eteamz CallActiveWebservice(string externalGroupID, string methodname)
        {
            Eteamz eteamuser = null;
            try
            {
                //declare web service instance
                com.eteamz.services.efundraising.EFundraisingService ws =
                    new com.eteamz.services.efundraising.EFundraisingService();

                // get url from config file
                ws.Url = ConfigurationManager.AppSettings["webServiceUrl"];

                ws.Timeout = 300000;

                // retreive the system username and password for web service authentication
                string wsUsername = ConfigurationManager.AppSettings["webServiceUserName"];
                string wsPassword = ConfigurationManager.AppSettings["webServicePassword"];

                // call getGroupInfo(username, password, grid) or getRosterByPersonType(username, password, grid)
                string response = "";
                if (string.Compare(methodname, "getgroupinfo", true) == 0)
                    response = ws.getGroupInfo(wsUsername, wsPassword, externalGroupID);
                else if (string.Compare(methodname, "getrosterbypersontype", true) == 0)
                    response = ws.getRosterByPersonType(wsUsername, wsPassword, externalGroupID, null);
                if (response != null && response != "")
                {
                    response = response.Replace("\n", "").Replace("\t", "").Trim();
                    eteamuser = new Eteamz();
                    eteamuser.LoadXML(response, methodname);
                }
                return eteamuser;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to call webservice getGroupInfo(XXX) ", ex);
            }
        }
        #endregion

        #region Private Methods
        private void LoadXML(string xmltext, string method)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmltext);
            foreach (XmlNode child in doc.ChildNodes)
            {
                if (method == "getgroupinfo")
                    this.GetGroupInfo(child);
                else if (method == "getrosterbypersontype")
                {
                    foreach (XmlNode personnode in child.ChildNodes)
                    {
                        this.GetRosterByPersonType(personnode);
                    }
                }
            }
        }
        private void GetGroupInfo(XmlNode node)
        {
            foreach (XmlNode child in node)
            {
                if (child.Name.ToLower() == "firstName".ToLower())
                {
                    FirstName = child.InnerText;
                }
                else if (child.Name.ToLower() == "lastName".ToLower())
                {
                    LastName = child.InnerText;
                }
                else if (child.Name.ToLower() == "email".ToLower())
                {
                    Email = child.InnerText.Trim();
                }
                else if (child.Name.ToLower() == "address1".ToLower())
                {
                    Address1 = child.InnerText;
                }
                else if (child.Name.ToLower() == "address2".ToLower())
                {
                    Address2 = child.InnerText;
                }
                else if (child.Name.ToLower() == "city".ToLower())
                {
                    City = child.InnerText;
                }
                else if (child.Name.ToLower() == "state".ToLower())
                {
                    State = child.InnerText;
                }
                else if (child.Name.ToLower() == "zip".ToLower())
                {
                    Zip = child.InnerText;
                }
                else if (child.Name.ToLower() == "country".ToLower())
                {
                    Country = child.InnerText;
                }
                else if (child.Name.ToLower() == "name".ToLower())
                {
                    Name = child.InnerText;
                }
                else if (child.Name.ToLower() == "url".ToLower())
                {
                    Url = child.InnerText;
                }
            }
        }
        private void GetRosterByPersonType(XmlNode node)
        {
            var person = new Person();
            foreach (XmlNode child in node)
            {
                if (child.Name.ToLower() == "id".ToLower())
                {
                    person.ID = child.InnerText;
                }
                else if (child.Name.ToLower() == "typeId".ToLower())
                {
                    person.TypeID = child.InnerText;
                }
                else if (child.Name.ToLower() == "name".ToLower())
                {
                    person.Name = child.InnerText;
                }
                else if (child.Name.ToLower() == "email".ToLower())
                {
                    person.Email = child.InnerText;
                }
            }
            PersonList.Add(person);
        }
        #endregion
    }
}