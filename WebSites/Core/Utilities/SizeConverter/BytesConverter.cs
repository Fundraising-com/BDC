using System;

namespace GA.BDC.Core.Utilities.SizeConverter
{

	#region public enum

	/// <summary>
	/// DataType of Byte Definition
	/// </summary>
	[Serializable()]
	public enum ByteDefinition : byte {
		Byte = 0,
		KB = 1,
		MB = 2,
		GB = 3,
		TB = 4
	}

	/// <summary>
	/// DataType of Comparing mode
	/// </summary>
	public enum CompareResult : byte {
		LessThan = 0,
		EqualTo = 1,
		GreatherThan = 2
	}

	#endregion

	#region public struct

	/// <summary>
	/// class struct containing the size value
	/// </summary>
	[Serializable()]
	public struct SizeValue {
		
		/// <summary>
		/// File size based on the ByteDefinition specified format
		/// </summary>
		public short Size;

		/// <summary>
		/// Type of format of byte definition
		/// </summary>
		public ByteDefinition SizeType;
	}

	#endregion

	/// <summary>
	/// class contained some static function for byte conversion
	/// </summary>
	/// <remarks>This class cannot be inherit from others class</remarks>
	public sealed class BytesConverter {

		#region private variables

		private const short __ConvertingFactor = 1024;
	
		#region to be deleted

		private const short __KG_BYTES = 1024;
		private const int __MB_BYTES = 1048576;
		private const long __GB_BYTES = 1073741824;
		private const long __TB_BYTES = 1099511627776;
 
		#endregion

		#endregion

		#region public static functions

		/// <summary>
		/// static function where the specified file size is compare
		/// with another base
		/// </summary>
		/// <param name="pByteLength"></param>
		/// <param name="pToCompareLimit"></param>
		/// <returns></returns>
		public static CompareResult Compare(long pByteLength, SizeValue pToCompareLimit) {
			CompareResult oCpRsl;
			short oCrtVal = ConvertByteTo(pByteLength,pToCompareLimit.SizeType);
			if(oCrtVal > pToCompareLimit.Size)
				oCpRsl = CompareResult.GreatherThan;
			else if(oCrtVal < pToCompareLimit.Size)
				oCpRsl = CompareResult.LessThan;
			else
				oCpRsl = CompareResult.EqualTo;
			return oCpRsl;
		}

		#endregion

		#region private static functions

		/// <summary>
		/// static function where the byte is convert to another base Byte Definition
		/// </summary>
		/// <param name="pByteToConvert"></param>
		/// <param name="pToConvert"></param>
		/// <returns></returns>
		private static short ConvertByteTo(long pByteToConvert, ByteDefinition pToConvert) {
			float oDbl = pByteToConvert;
			for(byte i=0;i<(byte)pToConvert;i++) 
				oDbl /= __ConvertingFactor;
			return (short)oDbl;
		}

		#endregion
	}
}
