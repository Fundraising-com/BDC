//
// 2005-08-02 - Stephen Lim - New class.
//
using System;

namespace GA.BDC.Core.BusinessBase
{
	/// <summary>
	/// DBValue.
	/// </summary>
	public class DBValue
	{

		#region Constructors

		private DBValue()
		{

		}
		#endregion

		#region Boolean
		public static bool NullBoolean
		{
			get {return false;}
		}

		public static bool IsNull(bool o)
		{
			return o;
		}

		public static bool ToBoolean(object o) 
		{
			if(o == DBNull.Value) 
				return false;
			else 
			{
				switch (o.ToString().ToLower())
				{
					case "true":
					case "t":
					case "1":
					case "y":
					case "yes":
						return true;
				}
			}
			return false;
		}

		public static object ToDBBoolean(bool o) 
		{
			return o;
		}
		#endregion		

		#region Byte
		public static byte NullByte
		{
			get {return byte.MinValue;}
		}

		public static bool IsNull(byte o)
		{
			return o == byte.MinValue;		
		}

		public static byte ToByte(object o)
		{
			if (o == DBNull.Value)
				return byte.MinValue;
			else
				return (byte) o;
		}

		public static object ToDBByte(byte o)
		{
			if (o == byte.MinValue)
				return DBNull.Value;
			else
				return o;
		}
		#endregion

		#region Int16
		public static short NullInt16
		{
			get {return short.MinValue;}
		}

		public static bool IsNull(short o)
		{
			return o == short.MinValue;		
		}

		public static short ToInt16(object o) 
		{
			if(o == DBNull.Value)
				return short.MinValue;
			else
				return Convert.ToInt16(o);
		}

		public static object ToDBInt16(int o) 
		{
			if(o == short.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		#endregion

		#region Int32
		public static int NullInt32
		{
			get {return int.MinValue;}
		}

		public static bool IsNull(int o)
		{
			return o == int.MinValue;		
		}

		public static int ToInt32(object o) 
		{
			if(o == DBNull.Value)
				return int.MinValue;
			else
				return Convert.ToInt32(o);
		}

		public static object ToDBInt32(int o) 
		{
			if(o == int.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		#endregion

		#region Int64
		public static long NullInt64
		{
			get {return long.MinValue;}
		}

		public static bool IsNull(long o)
		{
			return o == long.MinValue;		
		}

		public static long ToInt64(object o) 
		{
			if(o == DBNull.Value)
				return int.MinValue;
			else
				return Convert.ToInt64(o);
		}

		public static object ToDBInt64(long o) 
		{
			if(o == int.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		#endregion

		#region Char
		public static string NullChar
		{
			get {return null;}
		}

		public static bool IsNull(char o)
		{
			return o == char.MinValue;		
		}

		public static char ToChar(object o) 
		{
			if(o == DBNull.Value) 
				return char.MinValue;
			else 
				return Convert.ToChar(o);
		}

		public static object ToDBChar(char o) 
		{
			if(o == char.MinValue) 
				return DBNull.Value;
			else 
				return o.ToString();
		}
		#endregion

		#region String
		public static string NullString
		{
			get {return null;}
		}

		public static bool IsNull(string o)
		{
			return o == null;		
		}

		public static bool IsNullOrEmpty(string o)
		{
			return (o == null || o == "");		
		}

		public static bool IsEmpty(string o)
		{
			return o == "";
		}

		public static string ToString(object o) 
		{
			if(o == DBNull.Value) 
				return null;
			else 
				return o.ToString();
		}

		public static object ToDBString(string o) 
		{
			if(o == null) 
				return DBNull.Value;
			else 
				return o;
		}
		#endregion

		#region DateTime
		public static DateTime NullDateTime
		{
			get {return DateTime.MinValue;}
		}

		public static bool IsNull(DateTime o)
		{
			return o == DateTime.MinValue;		
		}

		public static DateTime ToDateTime(object o) 
		{
			if(o == DBNull.Value) 
				return DateTime.MinValue;
			else 
				return (DateTime)o;
		}

		public static object ToDBDateTime(DateTime o) 
		{
			if(o == DateTime.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		
		public static object ToDBDateTimeRemoveHour(DateTime o) 
		{
			if(o == DateTime.MinValue) 
				return DBNull.Value;
			else
			{
				DateTime dt = new DateTime(o.Year, o.Month, o.Day, 0, 0, 0, 0);
				return dt;
			}
		}
		#endregion

		#region Decimal
		public static decimal NullMoney
		{
			get {return decimal.MinValue;}
		}

		public static bool IsNull(decimal o)
		{
			return o == decimal.MinValue;		
		}

		public static decimal ToDecimal(object o) 
		{
			if(o == DBNull.Value) 
				return decimal.MinValue;
			else 
				return (decimal) o;
		}

		public static object ToDBDecimal(decimal o) 
		{
			if(o == decimal.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		
		public static object ToDBDecimal(float o) 
		{
			if(o == float.MinValue) 
				return DBNull.Value;
			else
			{ 
				Convert.ToDecimal(o);
				return o;
			}
		}
		
		public static object ToDBDecimal(int o) 
		{
			if(o == int.MinValue) 
				return DBNull.Value;
			else
			{ 
				Convert.ToDecimal(o);
				return o;
			}
		}
		
		#endregion

		#region Float
		public static float NullFloat
		{
			get {return float.MinValue;}
		}

		public static bool IsNull(float o)
		{
			return o == float.MinValue;		
		}

		public static float ToFloat(object o) 
		{
			if(o == DBNull.Value) 
				return float.MinValue;
			else 
				return (float) o;
		}

		public static object ToDBFloat(float o) 
		{
			if(o == float.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		#endregion

		#region Double
		public static double NullDecimal
		{
			get {return double.MinValue;}
		}

		public static bool IsNull(double o)
		{
			return o == double.MinValue;		
		}

		public static double ToDouble(object o)
		{
			if (o == DBNull.Value)
				return double.MinValue;
			else
				return Convert.ToDouble(o);
		}

		public static object ToDBDouble(double o) 
		{
			if(o == double.MinValue) 
				return DBNull.Value;
			else 
				return o;
		}
		#endregion

		#region GUID
		public static Guid NullGuid
		{
			get {return Guid.Empty;}
		}

		public static bool IsNull(Guid o)
		{
			if (o == Guid.Empty)
				return true;
			else
				return false;
		}

		public static Guid ToGuid(object o)
		{
			if (o == DBNull.Value)
				return Guid.Empty;
			else
				return (Guid) o;
		}


		public static object ToDBGuid(Guid o)
		{
			if (o == Guid.Empty)
				return DBNull.Value;
			else
				return o;
		}
		#endregion



	}
}
