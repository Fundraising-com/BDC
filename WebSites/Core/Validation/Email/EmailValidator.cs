using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.Xml;

namespace GA.BDC.Core.Validation.Email {
	/// <summary>
	/// Summary description for EmailValidator.
	/// </summary>
	public class EmailValidator {
        private static Regex regEx = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", RegexOptions.Compiled);
        private static Regex domainRegEx = new Regex(@"^[a-z|A-Z|0-9|-|_]{1,63}(\.[a-z|A-Z|0-9|-|_]{1,63})+$", RegexOptions.Compiled);

        public enum EmailGridItemStatus
        {
            OK = 0,
            NO_NAME = 1,
            NO_EMAIL = 2,
            WRONG_EMAIL_FORMAT = 3,
            INCORRECT_DOMAIN_NAME = 4,
            ALREADY_INSERTED = 5,
            SPAM_FILTER = 6,
            ERROR = 7
        }

		private EmailValidator() {
			//
			// TODO: Add constructor logic here
			//
		}

        public static bool ValidateEmail(string emailAddress)
        {
            return (IsEmailFormatValid(emailAddress));
        }

		public static bool ValidateEmailStrict(string emailAddress) {

            emailAddress = emailAddress.Trim();

            /* 1) check the input parameter */
            if (emailAddress == null || emailAddress == "")
                return false;                

            /* 2) check the sanity of email format against known Reg Exp */
            if (!IsEmailFormatValid(emailAddress))
                return false;

            /* 3) check the domain name of email address */
            //if (!IsDomainNameValid(emailAddress))
            //    return false;

            /* If it reaches this point then all validation passed */
            return true; 
		}

        public static bool IsEmailFormatValid(string emailAddress)
        {
            // check the format of the email against the latest email regular expression obtained from:
            // http://www.regular-expressions.info/email.html
            // OLD: Regex regEx = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z0-9]{1,6}$");            
            return (regEx.IsMatch(emailAddress));
        }

        public static bool IsDomainNameValid(string emailAddress)
        {
            /* checking the domain name */
            string domainname = emailAddress.Substring(emailAddress.IndexOf('@') + 1);
            // do a sanity check on the domain name to make sure its legal
            if (domainname.Length == 0 || domainname.Length > 255 ||
                !domainRegEx.IsMatch(domainname)) //, @"^[a-z|A-Z|0-9|-|_]{1,63}(\.[a-z|A-Z|0-9|-|_]{1,63})+$"))
            {
                // domain names can't be bigger than 255 chars, and individal labels can't be bigger than 63 chars
                return false;
            }

            // do a DNS lookup of the domain name            
            try
            {
                IPHostEntry GetIPHost = Dns.GetHostEntry(domainname);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                // invalid domain name
                switch (ex.ErrorCode)
                {
                    /* Error codes listed on http://msdn.microsoft.com/library/de...or_codes_2.asp */
                    case 11001: //Host not found
                        return false;
                    default:
                        return false;
                }
            }
            /* If it reaches this point then domain name validation passed */
            return true;
        }

        public static bool IsEmailValidAgainstKnownSpamList(string emailAddress, string[] spamlist)
        {
            /* 1) check the input parameter */
            if (emailAddress == null || emailAddress == "")
                return false;  

            /* 2 check if email address contains any text deemed unacceptable (SPAM) */
            foreach (string word in spamlist)
            {
                if (emailAddress.ToLower().StartsWith(word.ToLower()) || emailAddress.ToLower().EndsWith(word.ToLower()))
                    // invalid email
                    return false;
            }
            /* If it reaches here, then it passed spam check */
            return true;
        }

        public static EmailGridItemStatus ValidateEmailAll(string emailAddress, string[] spamlist)
        {
            /* 1) check the input parameter */
            if (string.IsNullOrEmpty(emailAddress))
                return EmailGridItemStatus.NO_EMAIL;

            emailAddress = emailAddress.Trim();

            /* 2) check the sanity of email format against known Reg Exp */
            if (!IsEmailFormatValid(emailAddress))
                return EmailGridItemStatus.WRONG_EMAIL_FORMAT;

            /* 3) check the domain name of email address */
            //if (!IsDomainNameValid(emailAddress))
            //    return EmailGridItemStatus.INCORRECT_DOMAIN_NAME;

            /* 4) check the email is not used for spamming*/
            if (!IsEmailValidAgainstKnownSpamList(emailAddress, spamlist))
                return EmailGridItemStatus.SPAM_FILTER;

            /* If it reaches this point then all validation passed */
            return EmailGridItemStatus.OK;
        }

        /// <summary>
        ///     ** IMPORTANT ** This method contains an embedded XML file: "EmailValidatorStatus.xml". 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="culture_code"></param>
        /// <returns></returns>
        public static string GetErrorMessageByStatus(int status, string language_code)
        {
            EmailGridItemStatus egStatus = (EmailGridItemStatus)status;
            return (GetErrorMessageByStatus(egStatus, language_code));
        }

        /// <summary>
        ///     ** IMPORTANT ** This method contains an embedded XML file: "EmailValidatorStatus.xml". 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="culture_code"></param>
        /// <returns></returns>
        public static string GetErrorMessageByStatus(EmailGridItemStatus status, string language_code)
        {
            XmlTextReader reader = new XmlTextReader((System.IO.Stream)(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()[0])));
            XmlDocument xd = new XmlDocument();
            xd.Load(reader);
            XmlNode xnodDE = xd.DocumentElement;
            string StringErrorCode = (Convert.ToInt32(status)).ToString();
            return (GetErrorMessageFromXML(xnodDE, StringErrorCode, language_code));
        }

        private static string GetErrorMessageFromXML(XmlNode xnode, string status, string language_code)
        {
            string RetVal = "";

            XmlNode xnodWorking;

            if (xnode.NodeType == XmlNodeType.Element && xnode.Name.ToLower() == "data".ToString())
            {
                XmlNamedNodeMap mapAttributes = xnode.Attributes;
                if (mapAttributes.Item(0).Name.ToLower() == "name".ToString() &&
                    mapAttributes.Item(0).Value.ToLower() == status)

                    return (mapAttributes.Item(1).Value);
            }

            if (xnode.HasChildNodes)
            {
                xnodWorking = xnode.FirstChild;
                while (xnodWorking != null && RetVal == "")
                {
                    if (xnodWorking.Name.ToLower() == "cultures".ToString() || xnodWorking.Name.ToLower() == "data".ToString())
                        RetVal = GetErrorMessageFromXML(xnodWorking, status, language_code);
                    else if (xnodWorking.Name.ToLower() == "culture".ToString() &&
                             xnodWorking.Attributes.Item(0).Value.ToLower() == language_code.ToLower())
                        RetVal = GetErrorMessageFromXML(xnodWorking, status, language_code);
                    xnodWorking = xnodWorking.NextSibling;
                }
            }

            return RetVal;
        }

    }
}
