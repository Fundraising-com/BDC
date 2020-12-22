using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public class ProgramAgreementSearchItem
    {

        #region Properties

        public int? StatusId { get; set; }
        public int? StatusCategoryId { get; set; }
        public string StatusColorCode { get; set; }
        public string StatusShortDescription { get; set; }
        public int ProgramAgreementId { get; set; }
        public string EDSProgramAgreementId { get; set; }
        public int AccountId { get; set; }
        public int? EDSAccountId { get; set; }
        public string AccountName { get; set; }
        public string FmId { get; set; }
        public string FmFirstName { get; set; }
        public string FmLastName { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int ProgramTypeId { get; set; }
        public string ProgramTypeName { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string SubdivisionCode { get; set; }
        public string Zip { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public int? FiscalYear { get; set; }

        #endregion

        #region Display Properties

        public string DisplayColorCode
        {
            get
            {
                if (string.IsNullOrEmpty(StatusColorCode))
                {
                    return "White";
                }
                else
                {
                    return StatusColorCode;
                }
            }
        }
        public string DisplayFsmName
        {
            get
            {
                if (string.IsNullOrEmpty(FmFirstName) && string.IsNullOrEmpty(FmLastName))
                {
                    return "";
                }
                else
                {
                    return string.Format("{0}, {1}", FmLastName, FmFirstName);
                }
            }
        }
        public string DisplaySubdivisionCode
        {
            get
            {
                if (SubdivisionCode != null)
                {
                    return SubdivisionCode.Replace("US-", "");
                }
                else
                {
                    return "";
                }
            }
        }
        public string DisplayCreatorName
        {
            get
            {
                if (string.IsNullOrEmpty(CreatorFirstName) && string.IsNullOrEmpty(CreatorLastName))
                {
                    return "System";
                }
                else
                {
                    return string.Format("{0} {1}", CreatorLastName, CreatorFirstName);
                }
            }
        }
        public string DisplayCreateDate
        {
            get
            {
                return string.Format("{0} {1}", CreateDate.ToShortDateString(), CreateDate.ToShortTimeString());
            }
        }

        #endregion

    }

}
