using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSPForm.Business {
    /// <summary>
    /// Holds settings to search accounts, orders and other lists.
    /// </summary>
    public class SearchSettings {

        #region Declarations

        private string accountName = "";
        private string organizationName = "";
        private string city = "";
        private string accountId = "";
        private string edsAccountId = "";
        private string organizationId = "";
        private string zipCode = "";
        private string firstChar = "";
        private int? programTypeId;
        private int? organizationTypeId;
        private string subdivisionCode = "";
        private int? statusCategoryId;
        private string fsmId = "";
        private string fsmName = "";
        private DisplayMode displayMode;

        private string sort = "";
        private int pageIndex = 0;
        private int pageSize = 0;

        #endregion

        #region Properties

        /// <summary>
        /// The account name to search for.
        /// </summary>
        public string AccountName {
            get { return accountName; }
            set { accountName = Trim(value); }
        }

        /// <summary>
        /// The organization name to search for.
        /// </summary>
        public string OrganizationName {
            get { return organizationName; }
            set { organizationName = Trim(value); }
        }

        /// <summary>
        /// The city to search for.
        /// </summary>
        public string City {
            get { return city; }
            set { city = Trim(value); }
        }

        /// <summary>
        /// The QSP account ID to search for.
        /// </summary>
        public string AccountId {
            get { return accountId; }
            set { accountId = Trim(value); }
        }

        /// <summary>
        /// The EDS account ID to search for.
        /// </summary>
        public string EdsAccountId {
            get { return edsAccountId; }
            set { edsAccountId = Trim(value); }
        }

        /// <summary>
        /// The organization ID to search for.
        /// </summary>
        public string OrganizationId {
            get { return organizationId; }
            set { organizationId = Trim(value); }
        }

        /// <summary>
        /// The zip code to search for.
        /// </summary>
        public string ZipCode {
            get { return zipCode; }
            set { zipCode = Trim(value); }
        }

        /// <summary>
        /// The first character to filter on: A-Z to filter by letter, "" to get all or # to get special characters.
        /// </summary>
        public string FirstChar {
            get { return firstChar; }
            set {
                firstChar = Trim(value).ToUpper();
                if (firstChar.Length > 1)
                    firstChar = firstChar.Substring(0, 1);
            }
        }

        /// <summary>
        /// The QSP program to filter on.
        /// </summary>
        public int? ProgramTypeId {
            get { return programTypeId; }
            set { programTypeId = value; }
        }

        /// <summary>
        /// The organization type to filter on.
        /// </summary>
        public int? OrganizationTypeId {
            get { return organizationTypeId; }
            set { organizationTypeId = value; }
        }

        /// <summary>
        /// The subdivision code or state to filter on.
        /// </summary>
        public string SubdivisionCode {
            get { return subdivisionCode; }
            set { subdivisionCode = Trim(value); }
        }

        /// <summary>
        /// The status category to filter on.
        /// </summary>
        public int? StatusCategoryId {
            get { return statusCategoryId; }
            set { statusCategoryId = value; }
        }

        /// <summary>
        /// The field sales manager ID to filter on.
        /// </summary>
        public string FsmId {
            get { return fsmId; }
            set { fsmId = Trim(value); }
        }

        /// <summary>
        /// The field sales manager name to filter on.
        /// </summary>
        public string FsmName {
            get { return fsmName; }
            set { fsmName = Trim(value); }
        }

        /// <summary>
        /// The way results are filtered by field sales manager.
        /// </summary>
        public DisplayMode DisplayMode {
            get { return displayMode; }
            set { displayMode = value; }
        }

        /// <summary>
        /// The field to sort results with.
        /// </summary>
        public string Sort {
            get { return sort; }
            set { sort = Trim(value); }
        }

        /// <summary>
        /// When using paging, the page to display.
        /// </summary>
        public int PageIndex {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        /// <summary>
        /// When using paging, the size of each page.
        /// </summary>
        public int PageSize {
            get { return pageSize; }
            set { pageSize = value; }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Returns a string that is not null and trimmed.
        /// </summary>
        /// <param name="text">The text to trim.</param>
        /// <returns>A non-null trimmed string.</returns>
        private string Trim(string text) {
            if (text == null)
                text = string.Empty;
            return text.Trim();
        }

        #endregion

    }
}