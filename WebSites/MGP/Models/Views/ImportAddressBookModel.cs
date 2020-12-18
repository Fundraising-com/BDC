using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using GA.BDC.Web.MGP.Properties;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers;
using LumenWorks.Framework.IO.Csv;

namespace GA.BDC.Web.MGP.Models.Views
{
    [Serializable]
    public class ImportAddressBookModel
    {
        [Flags]
        public enum ProviderType
        {
            None = 0,
            Google = 1,
            WindowsLive = 2,
            Yahoo = 4,
            Outlook = 8,
            CSV = 16
        }

        public struct Contact
        {
            public string FirstName, LastName, Nickname, EmailAddress;

            public Contact(string firstName, string lastName, string nickname, string emailAddress)
            {
                FirstName = firstName;
                LastName = lastName;
                Nickname = nickname;
                EmailAddress = emailAddress;
            }
        }

        public ImportAddressBookModel(ProviderType providerType)
        {
            SelectedProvider = providerType;
        }

        public ProviderType SelectedProvider { get; set; }
        public List<Contact> Contacts { get; set; }
        public bool ContactsLoaded
        {
            get { return (Contacts != null && Contacts.Any()); }
        }

        public void ProcessImport(string csvFilePath)
        {
            Contacts = LoadDataFromCSVFile(csvFilePath);
        }

        #region Private Methods

        private List<Contact> LoadDataFromCSVFile(string csvFilePath)
        {
            var contactList = new List<Contact>();
            int firstNameIndex = 0, lastNameIndex = 0, nickNameIndex = 0, emailAddressIndex = 0;

            // Apply default index in case the CSV file does not have the correct header names
            switch (SelectedProvider)
            {
                case ProviderType.WindowsLive:
                    firstNameIndex = 1;
                    lastNameIndex = 3;
                    emailAddressIndex = 46;
                    nickNameIndex = 78;
                    break;

                case ProviderType.Outlook:
                    break;

                case ProviderType.CSV:
                    firstNameIndex = 0;
                    lastNameIndex = 1;
                    nickNameIndex = 2;
                    emailAddressIndex = 3;
                    break;
            }

            // open the file csvFileName which is a CSV file with headers
            using (var csv = new CsvReader(new StreamReader(csvFilePath), true))
            {
                var headers = csv.GetFieldHeaders();

                for (var i = 0; i < headers.Length; i++)
                {
                    // Get the correct index for the header names
                    var header = Regex.Replace(headers[i], @"\s+", "").ToLower();

                    switch (SelectedProvider)
                    {
                        case ProviderType.WindowsLive:
                            if (Settings.Default.HotmailCSVColumnNameForFirstName.Split(',').Contains(header))
                            {
                                firstNameIndex = i;
                            }
                            else if (Settings.Default.HotmailCSVColumnNameForLastName.Split(',').Contains(header))
                            {
                                lastNameIndex = i;
                            }
                            else if (Settings.Default.HotmailCSVColumnNameForNickname.Split(',').Contains(header))
                            {
                                nickNameIndex = i;
                            }
                            else if (Settings.Default.HotmailCSVColumnNameForEmail.Split(',').Contains(header))
                            {
                                emailAddressIndex = i;
                            }
                            break;
                        case ProviderType.Outlook:
                            break;
                        case ProviderType.CSV:
                            if (Settings.Default.DefaultCSVColumnNameForFirstName.Split(',').Contains(header))
                            {
                                firstNameIndex = i;
                            }
                            else if (Settings.Default.DefaultCSVColumnNameForLastName.Split(',').Contains(header))
                            {
                                lastNameIndex = i;
                            }
                            else if (Settings.Default.DefaultCSVColumnNameForNickname.Split(',').Contains(header))
                            {
                                nickNameIndex = i;
                            }
                            else if (Settings.Default.DefaultCSVColumnNameForEmail.Split(',').Contains(header))
                            {
                                emailAddressIndex = i;
                            }
                            break;
                    }
                }

                while (csv.ReadNextRecord())
                {
                    var firstName = csv[firstNameIndex].IsNotEmpty()
                                        ? csv[firstNameIndex].Trim()
                                        : string.Empty;

                    var lastName = csv[lastNameIndex].IsNotEmpty()
                                       ? csv[lastNameIndex].Trim()
                                       : string.Empty;

                    var nickName = csv[nickNameIndex].IsNotEmpty()
                                       ? csv[nickNameIndex].Trim()
                                       : string.Empty;

                    var emailAddress = csv[emailAddressIndex].IsNotEmpty()
                                           ? csv[emailAddressIndex].Trim()
                                           : string.Empty;

                    if (!RegexHelper.IsValidEmail(emailAddress))
                    {
                        continue;
                    }

                    if (firstName == string.Empty && lastName == string.Empty)
                    {
                        firstName = lastName = "n/a";
                    }

                    if (contactList.Count(c => c.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower()) == 0)
                    {
                        contactList.Add(new Contact
                        {
                            FirstName = firstName.HtmlEncodeDecode(),
                            LastName = (lastName != string.Empty ? lastName.HtmlEncodeDecode() : string.Empty),
                            Nickname = (nickName != string.Empty ? nickName.HtmlEncodeDecode() : string.Empty),
                            EmailAddress = emailAddress.HtmlEncodeDecode()
                        });
                    }
                }
            }
            return contactList;
        }

        #endregion Private Methods
    }
}