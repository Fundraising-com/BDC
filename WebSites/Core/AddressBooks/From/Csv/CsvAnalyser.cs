//
//	November 30, 2004	-	Louis Turmel	Class Implementation Added
//	February 24, 2005	-	Louis Turmel	Code Comments
//

using System;
using System.IO;
using GA.BDC.Core.AddressBooks.Types;

namespace GA.BDC.Core.AddressBooks.From.Csv {

    /// <summary>
    /// This is class for parse csv contact list file
    /// </summary>
    /// <remarks>This class can be use only inside this namespace</remarks>
    internal class CsvAnalyser : GenericEmailImporter
    {

        #region private const variables

        /// <summary>
        /// constant who determine le maximum of row that we can check if 
        /// the header columns is present.
        /// </summary>
        private const int __MaxLineFieldFinder = 70;

        #endregion

        #region private variables


        /// <summary>
        /// string array containing somes specifics FirstName columns name
        /// </summary>
        /// <remarks>Should be assignable from configuration settings</remarks>
        private string[] _NameField = { "first name", "firstname", "Prénom", "Prnom", "nom", "members", "names", "fname", "name" };

        /// <summary>
        /// string array containing somes specifics Lastname columns name
        /// </summary>
        /// <remarks>Should be assignable from configuration settings</remarks>
        private string[] _LastNameField = { "last name", "lname", "nom", "lastname" };

        /// <summary>
        /// string array containing somes specifics Email columns name
        /// </summary>
        /// <remarks>Should be assignable from configuration settings</remarks>
        private string[] _EmailField = { "Email Address", "E-mail Address", "email", "courriel", "e-mail", "adresse électronique", "adresse lectronique", "adresse de messagerie" };

        #endregion

        #region public constructors

        public CsvAnalyser() { }

        /// <summary>
        /// Constructors with Stream parameter
        /// </summary>
        /// <param name="pStream">Stream containing the ContactList in CSV Format to analyse</param>
        public CsvAnalyser(Stream pStream)
        {
            this.Analyser(pStream);
        }

        #endregion

        #region private functions

        /// <summary>
        /// Function returning the index of Columns Index of ColumnType specified by the pReadLine parameter
        /// </summary>
        /// <param name="pReadLine">Line to check if contain ColumnName</param>
        /// <param name="pColumnType">ColumnType to get the Index Number</param>
        /// <returns>Index of the ColumnType Name</returns>
        private int GetColumnIndex(string pReadLine, ColumnType pColumnType)
        {

            string[] oColumnName = pReadLine.Split(',', ';');
            string[] oListHeaderName = null;

            #region switch on ColumnType

            // Conditionnal on ColumnType, to get the appropiate
            // const string array
            switch (pColumnType)
            {
                case ColumnType.FirstName:
                    oListHeaderName = _NameField;
                    break;
                case ColumnType.LastName:
                    oListHeaderName = _LastNameField;
                    break;
                case ColumnType.Email:
                    oListHeaderName = _EmailField;
                    break;
                default:
                    oListHeaderName = _EmailField;
                    break;
            }

            #endregion

            int oColumnNumber = -1;
            bool oFind = false;
            string oValid;
            // loop on the number of Column containing in the specified pReadLine string parameter
            for (int i = 0; i < oColumnName.Length; i++)
            {
                if (!oFind)
                {
                    for (int j = 0; j < oListHeaderName.Length; j++)
                    {
                        oValid = oColumnName[i].ToLower().Trim();
                        if (oValid.IndexOf('"') > -1)
                            oValid = oValid.Replace('"', ' ').Trim();

                        int idx = oValid.IndexOf(oListHeaderName[j].ToLower().ToString(), 0, oValid.Length);

                        if (idx == 0)
                        {
                            oColumnNumber = i;
                            // The Column Index Number have been found !
                            oFind = true;
                        }
                    }
                }
            }
            return oColumnNumber;
        }

        #endregion

        #region public override methods

        /// <summary>
        /// Method analysing a specified Stream containing csv contact list
        /// </summary>
        /// <param name="pStream">Stream of ContactList content</param>
        public override void Analyser(Stream pStream)
        {
            int i = 0;
            int idxColumnName = -1;
            int idxColumnLast = -1;
            int idxColumnEmail = -1;
            int oLineRead = 0;
            try
            {
                // Create an StreamReader from Stream parameter pStream
                using (System.IO.StreamReader usr = new StreamReader(pStream))
                {
                    try
                    {
                        string wLine;
                        while ((wLine = usr.ReadLine()) != null)
                        {
                            string[] oBlockLine = wLine.Replace('"', ' ').Split(';', ',');
                            // Get the Columns Index if the column wasn't found
                            if (oLineRead < __MaxLineFieldFinder && (idxColumnName == -1 || idxColumnEmail == -1))
                            {
                                idxColumnName = GetColumnIndex(wLine, ColumnType.FirstName);
                                idxColumnLast = GetColumnIndex(wLine, ColumnType.LastName);
                                idxColumnEmail = GetColumnIndex(wLine, ColumnType.Email);
                            }

                            if (oLineRead > 0)
                            {

                                if (idxColumnName == -1 && idxColumnEmail == -1)
                                {
                                    for (int j = 0; j < oBlockLine.Length; j++)
                                    {
                                        if (this.ContactManager.NotInTheList(oBlockLine[j]))
                                        {
                                            this.ContactManager.Add(new ContactInfo("", "", oBlockLine[j].Trim()));
                                            i++;
                                        }
                                    }
                                }
                                else if (idxColumnName != -1 && idxColumnEmail != -1 && oBlockLine.Length >= idxColumnName && oBlockLine.Length >= idxColumnEmail)
                                {
                                    if (this.IsValidEmail(oBlockLine[idxColumnEmail].Trim()) && this.ContactManager.NotInTheList(oBlockLine[idxColumnEmail].Trim()))
                                    {
                                        if (idxColumnLast > -1)
                                        {
                                            string oFullName = oBlockLine[idxColumnName].Trim() + " " + oBlockLine[idxColumnLast].Trim();
                                            if (oFullName.IndexOf('@') == -1)
                                                this.ContactManager.Add(new ContactInfo(oBlockLine[idxColumnName].Trim(), oBlockLine[idxColumnLast].Trim(), oBlockLine[idxColumnEmail].Trim()));
                                            else
                                                this.ContactManager.Add(new ContactInfo("", "", oBlockLine[idxColumnEmail].Trim()));
                                        }
                                        else
                                        {
                                            string oFullName = oBlockLine[idxColumnName].Trim();
                                            if (oFullName.IndexOf('@') == -1)
                                                this.ContactManager.Add(new ContactInfo(oBlockLine[idxColumnName].Trim(), "", oBlockLine[idxColumnEmail].Trim()));
                                            else
                                                this.ContactManager.Add(new ContactInfo("", "", oBlockLine[idxColumnEmail].Trim()));
                                        }
                                    }
                                }
                            }
                            oLineRead++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        usr.Close();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region public override functions

        public override string GetEmail(string pCrtLine)
        {
            return base.GetEmail(pCrtLine);
        }

        #endregion

        #region public override attributes

        public override int GetNumberContact(System.Collections.IEnumerator pIEnumList)
        {
            return base.GetNumberContact(pIEnumList);
        }

        public override ContactInfo[] ContactList
        {
            get { return base.ContactList; }
            set { base.ContactList = value; }
        }

        public override int ContactNumber
        {
            get { return base.ContactNumber; }
        }

        public override string FileName
        {
            get { return base.FileName; }
            set { base.FileName = value; }
        }

        public override FileType GetContactFileType
        {
            get { return base.GetContactFileType; }
        }

        #endregion
    }
}
