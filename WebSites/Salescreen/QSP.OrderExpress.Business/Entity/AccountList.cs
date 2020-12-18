using System;

namespace QSP.OrderExpress.Business.Entity {
    /// <summary>
    /// Represents an account and all necessary information to display in a grid.
    /// </summary>
    public class AccountList {

        #region Declarations / Constructor

        private int accountId;
        private string accountName;
        private string fmId;
        private int? fulfAccountId;
        private int? accountStatusId;
        private int? statusCategoryId;
        private string colorCode;
        private string shortDescription;
        private string firstName;
        private string lastName;
        private int? fiscalYear;
        private int programTypeId;
        private string programTypeName;
        private string city;
        private string zip;
        private string subdivisionCode;
        private DateTime createDate;
        private string creatorFirstName;
        private string creatorLastName;

        public AccountList() {
        }

        public AccountList(int accountId, string accountName, string fmId, int? fulfAccountId, int? accountStatusId, int? statusCategoryId, string colorCode, string shortDescription, string firstName, string lastName, int? fiscalYear, int programTypeId, string programTypeName, string city, string zip, string subdivisionCode, DateTime createDate, string creatorFirstName, string creatorLastName) {
            this.accountId = accountId;
            this.accountName = accountName;
            this.fmId = fmId;
            this.fulfAccountId = fulfAccountId;
            this.accountStatusId = accountStatusId;
            this.statusCategoryId = statusCategoryId;
            this.colorCode = colorCode;
            this.shortDescription = shortDescription;
            this.firstName = firstName;
            this.lastName = lastName;
            this.fiscalYear = fiscalYear;
            this.programTypeId = programTypeId;
            this.programTypeName = programTypeName;
            this.city = city;
            this.zip = zip;
            this.subdivisionCode = subdivisionCode;
            this.createDate = createDate;
            this.creatorFirstName = creatorFirstName;
            this.creatorLastName = creatorLastName;
        }

        #endregion

        #region Field Properties

        public int AccountId {
            get { return accountId; }
            set { accountId = value; }
        }

        public string AccountName {
            get { return accountName; }
            set { accountName = value; }
        }

        public string FmId {
            get { return fmId; }
            set { fmId = value; }
        }

        public int? FulfAccountId {
            get { return fulfAccountId; }
            set { fulfAccountId = value; }
        }

        public int? AccountStatusId {
            get { return accountStatusId; }
            set { accountStatusId = value; }
        }

        public int? StatusCategoryId {
            get { return statusCategoryId; }
            set { statusCategoryId = value; }
        }

        public string ColorCode {
            get { return colorCode; }
            set { colorCode = value; }
        }

        public string ShortDescription {
            get { return shortDescription; }
            set { shortDescription = value; }
        }

        public string FirstName {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName {
            get { return lastName; }
            set { lastName = value; }
        }

        public int? FiscalYear {
            get { return fiscalYear; }
            set { fiscalYear = value; }
        }

        public int ProgramTypeId {
            get { return programTypeId; }
            set { programTypeId = value; }
        }

        public string ProgramTypeName {
            get { return programTypeName; }
            set { programTypeName = value; }
        }

        public string City {
            get { return city; }
            set { city = value; }
        }

        public string Zip {
            get { return zip; }
            set { zip = value; }
        }

        public string SubdivisionCode {
            get { return subdivisionCode; }
            set { subdivisionCode = value; }
        }

        public DateTime CreateDate {
            get { return createDate; }
            set { createDate = value; }
        }

        public string CreatorFirstName {
            get { return creatorFirstName; }
            set { creatorFirstName = value; }
        }

        public string CreatorLastName {
            get { return creatorLastName; }
            set { creatorLastName = value; }
        }

        #endregion

        #region Display Properties

        public string DisplayColorCode {
            get {
                if (string.IsNullOrEmpty(colorCode))
                    return "White";
                else
                    return colorCode;
            }
        }

        public string DisplayFsmName {
            get {
                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                    return "";
                else
                    return string.Format("{0}, {1}", lastName, firstName);
            }
        }

        public string DisplaySubdivisionCode {
            get {
                if (subdivisionCode != null)
                    return subdivisionCode.Replace("US-", "");
                else
                    return "";
            }
        }

        public string DisplayCreatorName {
            get {
                if (string.IsNullOrEmpty(creatorFirstName) && string.IsNullOrEmpty(creatorLastName))
                    return "System";
                else
                    return string.Format("{0} {1}", creatorLastName, creatorFirstName);
            }
        }

        #endregion

    }
}