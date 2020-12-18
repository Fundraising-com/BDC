using System;

namespace QSP.OrderExpress.Business.Entity {
    /// <summary>
    /// Represents an organization and all necessary information to display in a grid.
    /// </summary>
    public class OrganizationList {

        #region Declarations / Constructor

        private int organizationId;
        private int organizationTypeId;
        private string organizationName;
        private string organizationTypeName;
        private string fmId;
        private string firstName;
        private string lastName;
        private string address1;
        private string city;
        private string subdivisionCode;
        private string zip;

        public OrganizationList() {
        }

        public OrganizationList(int organizationId, int organizationTypeId, string organizationName, string organizationTypeName, string fmId, string firstName, string lastName, string address1, string city, string subdivisionCode, string zip) {
            this.organizationId = organizationId;
            this.organizationTypeId = organizationTypeId;
            this.organizationName = organizationName;
            this.organizationTypeName = organizationTypeName;
            this.fmId = fmId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address1 = address1;
            this.city = city;
            this.subdivisionCode = subdivisionCode;
            this.zip = zip;
        }

        #endregion

        #region Field Properties

        public int OrganizationId {
            get { return organizationId; }
            set { organizationId = value; }
        }

        public int OrganizationTypeId {
            get { return organizationTypeId; }
            set { organizationTypeId = value; }
        }

        public string OrganizationName {
            get { return organizationName; }
            set { organizationName = value; }
        }

        public string OrganizationTypeName {
            get { return organizationTypeName; }
            set { organizationTypeName = value; }
        }

        public string FmId {
            get { return fmId; }
            set { fmId = value; }
        }

        public string FirstName {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Address1 {
            get { return address1; }
            set { address1 = value; }
        }

        public string City {
            get { return city; }
            set { city = value; }
        }

        public string SubdivisionCode {
            get { return subdivisionCode; }
            set { subdivisionCode = value; }
        }

        public string Zip {
            get { return zip; }
            set { zip = value; }
        }

        #endregion

        #region Display Properties

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

        #endregion
        
    }
}