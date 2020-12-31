using System;

namespace Common
{
	/// <summary>Data Representation for a US based phone #</summary>
	public class PhoneNumber
	{
		#region Constructors
		///<summary>default constructor</summary>
		public PhoneNumber()
		{
			_IDM			= -1;
			_type			= UNDEF_TYPE;
			_PhoneListIDM	= -1;
			_number			= "";
		}
		
		///<summary>constructor</summary>
		public PhoneNumber(string number)
		{
			_IDM			= -1;
			_type			= UNDEF_TYPE;
			_PhoneListIDM	= -1;
			_number			= number;
		}

		///<summary>Constructor for items being populated from the database</summary>
		///<param name="number">string: phone number</param>
		///<param name="id"></param>
		///<param name="type"></param>
		///<param name="list"></param>
		public PhoneNumber(string number, int id, int type, int PhoneListID)
		{
			_IDM			= Phone_ID;
			_type			= type;
			_PhoneListIDM	= PhoneListID;
			_number			= number;

		}

		///<summary>Constructor for items being populated from the database</summary>
		///<param name="number">string: phone number</param>
		///<param name="id"></param>
		///<param name="type"></param>
		///<param name="list"></param>
		public PhoneNumber(string number, int Phone_ID, int type, int PhoneListID, string BestTimeToCall)
		{
			_IDM			= Phone_ID;
			_type			= type;
			_number			= number;
			_PhoneListIDM	= PhoneListID;
			_BestTimeToCall	= BestTimeToCall;
		}

		/// <summary>Constructor for new entries</summary>
		/// <param name="Type"></param>
		public PhoneNumber(int Type)
		{
			_type			= Type;
		}
		#endregion Constructors

		#region Private Class Members
		private string	_number;
		private int		_type;
		private int		_PhoneListIDM = -1;
		private int		_IDM = -1;
		private string	_BestTimeToCall = "";
		private int		UNDEF_TYPE = 30500;
		#endregion Private Class Members

		#region Public Methods 
		public string GetNumberStripped
		{
			get
			{
				if ((_number != null)&&(_number.Trim() != ""))
				{
					string strOUT = _number.Replace(" ", "").Replace("-", "");
					strOUT = strOUT.Replace("(", "").Replace(")", "").Replace(".", "");
					return strOUT;
				}
				else
				{
					return "";
				}
			}
			set{}
		}

		public string GetNumberRaw
		{
			get
			{
				return _number;
			}
			set{}
		}

		public string GetNumberFormatted
		{
			get
			{
				if((GetNumberRaw != null)&&(GetNumberRaw.Length == 10))
					return GetNumberRaw.Substring(0, 3) + "-" + GetNumberRaw.Substring(3, 3) + "-" + GetNumberRaw.Substring(6, 4);
				else
					return GetNumberRaw;
			}
			set{}
		}

		public string GetNumber
		{
			get
			{
				return GetNumberStripped;
			}
			set{}
		}

		public string Number
		{
			get
			{
				return GetNumber;
			}
			set
			{
				_number = value;
			}
		}

		public string XMLline
		{

			get
			{
				string retval = "&lt;number id=\"" + _IDM.ToString() + "\" ";
				retval += "type=\"" + _type.ToString() + "\" ";
				retval += "number=\"" + GetNumber  + "\" ";
				retval += "PhoneListID=\"" + _PhoneListIDM.ToString() + "\" ";
				retval += "BestTimeToCall=\"" + _BestTimeToCall + "\" ";
				retval += "/&gt;";
				return retval;
			}
			set{}
		}

		public int Type
		{
			get { return this._type; }
			set { this._type = value; }
		}

		public int Phone_ID
		{
			get { return this._IDM; }
			set { this._IDM = value; }
		}

		public int PhoneListID
		{
			get { return this._PhoneListIDM; }
			set { this._PhoneListIDM = value; }
		}

		public string BestTimeToCall
		{
			get { return this._BestTimeToCall; }
			set { this._BestTimeToCall = value; }
		}

		#endregion
	}
}
