//
// 2005-06-29 - Stephen Lim - New class.
//

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using GA.BDC.Core.Utilities.Encryption.DES;



namespace GA.BDC.Core.ESubsGlobal.Touch
{
	#region Enums
	[Serializable]
	public enum TouchProcessedStatus : byte
	{        
		New = 0,
		Queued = 1,
		Sent = 2,
		CommunicationError = 3,
		MailError = 4,
		DatabaseError = 5,
		UnknownError = 6,
		SpamError = 7,
        BlockedByRequest = 8,
        LateEmail = 9,
        Draft = 12, 
        DeleteByUser = 14, 
        SecondAttemp = 20,
        SecoudFail=22,
        ThirdAttemp  = 30,
        ThirdFail = 32,
        ToManyAttempBlock= 40,
        NoSpecified = 100,
	}   
  
	#endregion

	/// <summary>
	/// TouchEmail.
	/// </summary>
	[Serializable]
	public class TouchEmail : EnvironmentBase
	{
		#region Constants
		private const long RESENDSMTPERRORDELAY = 1000;
		private const string ENCRYPTIONKEY = "3fuNd84151NG";
        private const int defaultPercentage = 40;
        private readonly string removeChar = "&#160;".ToString();
		#endregion

		#region Fields
		private int _touchId = int.MinValue;
		private int _identification = int.MinValue;
		private int _partnerId = int.MinValue;
		private TouchProcessedStatus _processed = TouchProcessedStatus.New;
		private string _replyToName = null;
		private string _replyToEmail = null;
		private string _toName = null;
		private string _toEmail = null;
		private string _fromName = null;
		private string _fromEmail = null;
		private string _bounceEmail = null;
		private string _procedureCall = null;
		private string _cultureCode = null;
		private string _subject = null;
		private string _htmlBody = null;
		private string _textBody = null;
		private string _htmlFooter = null;
		private string _textFooter = null;
		private int eventID = int.MinValue;
		private int emailTemplateID = int.MinValue;
        private DateTime launchDate = DateTime.MinValue;
		private NameValueCollection _paramTags = new NameValueCollection();
		#endregion

		#region Constructors
		/// <summary>
		/// Create a new instance of the TouchEmail.
		/// </summary>
		public TouchEmail()
		{

		}
		#endregion

        #region Private Methods
        private void removeEnding(ref string input)
        {
            input = input.Trim();
            if (input.EndsWith(removeChar))
            {
                input = input.TrimEnd(removeChar.ToCharArray());
            }
        }

        private string overwriteMailPieceWithTag(string input, ESubsGlobal.Touch.Tag tag)
        {
            int startIndex = input.IndexOf(tag.StartTagName);
            int endIndex = input.IndexOf(tag.EndTagName);

            if (startIndex < 0 || endIndex < 0 || (endIndex - startIndex) < 0)
            {
                return input;
            }

            int length = endIndex - startIndex + tag.EndTagName.Length;
            return input.Replace(input.Substring(startIndex, length), tag.Description);
        }
        #endregion

        #region Static Methods

        static public string[] GetUserTouchIDs(int memberID)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            System.Data.DataTable dt = dbo.GetUserTouchIDs(memberID);
            string[] result = null;
            if (dt.Rows.Count > 0)
            {
                result = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (DBNull.Value.Equals(dt.Rows[i]["touch_id"]))
                        result[i] = int.MinValue.ToString();
                    else
                    {
                        try
                        {
                            int.Parse(dt.Rows[i]["touch_id"].ToString());
                            result[i] = dt.Rows[i]["touch_id"].ToString();
                        }
                        catch (Exception)
                        {
                            result[i] = int.MinValue.ToString();
                        }
                    }
                }
            }
            return result;
        }
    
        #endregion

		#region Methods
		public void UpdateProcessedStatus()
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.UpdateTouchProcessedStatus(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException("Unable to update touch processed status.", ex, this);
			}
		}

		public void LoadParamTags()
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				_paramTags = dbo.GetTouchParamTags(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException("Unable to load parameter tags.", ex, this);
			}
		}

		public string ReplaceTags(string input)
		{
			if (input != null)
			{
				foreach (string tag in _paramTags.Keys) 
				{
					string tagValue = _paramTags[tag];

					// Locate all param tags and store in match collection
					MatchCollection mc = Regex.Matches(input, @"\[\+{2}([\w\(]*?" + Regex.Escape(tag) + @"?[\)]*)\+{2}\]", RegexOptions.IgnoreCase);

					// Loop each match item in matchcollection and replace the param tag.
					foreach (Match m in mc) 
					{
						// Initialize string holder and error flag.
						string replacementValue = "";
						bool errorDetected = false;

						// The paramTagStr contains the string between the brackets [paramTagStr]
						// excluding the brackets.
						string paramTagStr = m.Groups[1].Captures[0].Value.ToLower();

						// Locate the tokens in paramTagStr. A param tag can contain multiple tokens
						// like "[token(token(token))]"
						string[] tokens = paramTagStr.ToLower().TrimEnd(')').Split('(');

						// Search for tokens in reverse because 
						// functions appear right-to-left.
						// eg. token(token(token(token)))
						for (int j = tokens.Length - 1; j >= 0; j--) 
						{
							// Apply replacement for tag and function
							if (tokens[j] == tag.ToLower()) 
								replacementValue = tagValue;

								// Encrypt
							else if (tokens[j] == "encrypt") 
								replacementValue = new TripleDES().Encrypt(replacementValue, ENCRYPTIONKEY);

								// Html encode
							else if (tokens[j] == "htmlencode") 
								replacementValue = HttpUtility.HtmlEncode(replacementValue);

								// Url encode
							else if (tokens[j] == "urlencode") 
								replacementValue = HttpUtility.UrlEncode(replacementValue);

								// Convert newlines into HTML breaks
							else if (tokens[j] == "htmlbreak")
								replacementValue = replacementValue.Replace("\n", "\n<br>");

								// Others
							else 
							{
								// Invalid function
								errorDetected = true;
								break;
							}
						}

						// Replace back function in string if no error detected.
						if (! errorDetected) 
						{
							input = Regex.Replace(input, @"\[\+{2}" + Regex.Escape(paramTagStr) + @"\+{2}\]", 
								replacementValue, RegexOptions.IgnoreCase);
						}
					}
				}
			}

			return input;
		}

		public void ParseEmail()
		{
			_toName = ReplaceTags(_toName);
			_toEmail = ReplaceTags(_toEmail);
			_replyToName = ReplaceTags(_replyToName);
			_replyToEmail = ReplaceTags(_replyToEmail);

            removeEnding(ref _replyToEmail);

			_fromName = ReplaceTags(_fromName);
			_fromEmail = ReplaceTags(_fromEmail);
			_bounceEmail = ReplaceTags(_bounceEmail);
			_subject = ReplaceTags(_subject);
			_htmlBody = ReplaceTags(_htmlBody);
			_textBody = ReplaceTags(_textBody);
			_htmlFooter = ReplaceTags(_htmlFooter);
			_textFooter = ReplaceTags(_textFooter);


            // Added by PT to implement that default percentage to get overwritten on the fly
            //Dictionary<int, int> profitPartner = PartnerProfitDictionary.GetPartnetProfit();
            /* ### */
            GA.BDC.Core.eFundraisingCommon.PartnerProfit pp = GA.BDC.Core.eFundraisingCommon.PartnerProfit.GetCurrentPartnerProfitByID(this._partnerId);
            if (pp != null)
            {
                List<GA.BDC.Core.eFundraisingCommon.Profit> profits = GA.BDC.Core.eFundraisingCommon.Profit.GetProfitByProfitGroupID(pp.ProfitGroupID);
                if (profits != null)
                {
                    GA.BDC.Core.eFundraisingCommon.Profit currentProfit = null;
                    foreach (GA.BDC.Core.eFundraisingCommon.Profit p in profits)
                    {
                        if (p.QspCatalogTypeID == int.MinValue)
                            currentProfit = p;
                    }
                    if (currentProfit != null)
                    {
                        _htmlBody = _htmlBody.Replace(defaultPercentage.ToString() + "%".ToString(), currentProfit.Description);
                        _htmlBody = _htmlBody.Replace("up to up to".ToString(), "up to".ToString());
                        _htmlBody = _htmlBody.Replace("Up to up to".ToString(), "Up to".ToString());
                        _htmlBody = _htmlBody.Replace("%*".ToString(), "%".ToString());
                        _htmlBody = _htmlBody.Replace("%**".ToString(), "%".ToString());

                        _textBody = _textBody.Replace(defaultPercentage.ToString() + "%".ToString(), currentProfit.Description);
                        _textBody = _textBody.Replace("up to up to".ToString(), "up to".ToString());
                        _textBody = _textBody.Replace("Up to up to".ToString(), "Up to".ToString());
                        _textBody = _textBody.Replace("%**".ToString(), "%".ToString());
                        _textBody = _textBody.Replace("%*".ToString(), "%".ToString());
                    }
                }
            }/* #### */
		}

        public void ReplaceProductTags()
        {
            List<ESubsGlobal.Touch.Tag> tags = ESubsGlobal.Touch.Tag.GetTags();
            if (tags != null)
            {
  
                ESubsGlobal.PartnerProductOffer ppo = ESubsGlobal.PartnerProductOffer.GetPartnerProductOfferByID(this._partnerId);
                if (ppo != null)
                {
                    List<EmailTemplateTag> etts = EmailTemplateTag.GetEmailTemplateTags(this.EmailTemplateID, ppo.ProductOfferID);
                        if(etts != null && etts.Count > 0)
                        {
                            foreach (EmailTemplateTag ett in etts)
                            {
                                ESubsGlobal.Touch.Tag tag = tags.FirstOrDefault(x => x.TagID == ett.TagID);
                                if (tag != null)
                                {
                                    _subject = overwriteMailPieceWithTag(_subject, tag);
                                    _htmlBody = overwriteMailPieceWithTag(_htmlBody, tag);
                                    _textBody = overwriteMailPieceWithTag(_textBody, tag);
                                }
                            }
                        }
                    }

                foreach (string s in tags.Select(x => x.StartTagName).Distinct())
                {
                 _subject =  _subject.Replace(s,  string.Empty);
                 _htmlBody = _htmlBody.Replace(s, string.Empty);
                 _textBody =  _textBody.Replace(s, string.Empty);
                }

                foreach (string s in tags.Select(x => x.EndTagName).Distinct())
                {
                    _subject = _subject.Replace(s, string.Empty);
                    _htmlBody = _htmlBody.Replace(s, string.Empty);
                    _textBody = _textBody.Replace(s, string.Empty);
                }
  
            }

        }
		#endregion

		#region Properties
		public TouchProcessedStatus Processed
		{
			get {return _processed;}
			set {_processed = value;}
		}

		/// <summary>
		/// Get flag whether object still contains Param Tags not yet replaced.
		/// </summary>
		public bool IsParamTagFree
		{
			get 
			{
				try
				{
					if (Regex.Match(_replyToName, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_replyToEmail, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_toName, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_toEmail, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_fromName, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_fromEmail, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_bounceEmail, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_subject, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_htmlBody, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_textBody, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_htmlFooter, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success ||
						Regex.Match(_textFooter, @"\[\+{2}[\w\-\.\(\)]+\+{2}\]", RegexOptions.Compiled).Success)
					{
						return false;
					}

					return true;
				}
				catch(Exception ex)
				{
					GA.BDC.Core.Diagnostics.Logger.LogError("Error parsing email.\r\n" + this.ToXmlString(), ex);
					return false;
				}
			}
		}

		public int TouchId
		{
			get { return _touchId; }
			set { _touchId = value; }
		}

		public int Identification
		{
			get { return _identification; }
			set { _identification = value; }
		}

		public int PartnerId
		{
			get { return _partnerId; }
			set { _partnerId = value; }
		}

		public string ReplyToName
		{
			get { return _replyToName; }
			set { _replyToName = value; }
		}

		public string ReplyToEmail
		{
			get { return _replyToEmail; }
			set { _replyToEmail = value; }
		}

		public string ToName
		{
			get { return _toName; }
			set { _toName = value; }
		}

		public string ToEmail
		{
			get { return _toEmail; }
			set { _toEmail = value; }
		}

		public string FromName
		{
			get { return _fromName; }
			set { _fromName = value; }
		}

		public string FromEmail
		{
			get { return _fromEmail; }
			set { _fromEmail = value; }
		}

		public string BounceEmail
		{
			get { return _bounceEmail; }
			set { _bounceEmail = value; }
		}

		public string ProcedureCall
		{
			get { return _procedureCall; }
			set { _procedureCall = value; }
		}

		public string CultureCode
		{
			get { return _cultureCode; }
			set { _cultureCode = value; }
		}

		public string Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public string HtmlBody
		{
			get { return _htmlBody; }
			set { _htmlBody = value; }
		}

		public string TextBody
		{
			get { return _textBody; }
			set { _textBody = value; }
		}

		public string HtmlFooter
		{
			get { return _htmlFooter; }
			set { _htmlFooter = value; }
		}

		public string TextFooter
		{
			get { return _textFooter; }
			set { _textFooter = value; }
		}

		public int EventID {
			get { return eventID; }
			set { eventID = value; }
		}

		public int EmailTemplateID {
			get { return emailTemplateID; }
			set { emailTemplateID = value; }
		}

        public DateTime LaunchDate
        {
            get { return launchDate; }
            set { launchDate = value; }
        }

		[XmlIgnore]
		public NameValueCollection ParamTags
		{
			get { return _paramTags; }
			set { _paramTags = value; }
		}
		#endregion
	}
}
