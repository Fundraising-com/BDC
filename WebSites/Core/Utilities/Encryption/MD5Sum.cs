//
// 2006-06-17 - Stephen Lim - New class.
//

using System;
using System.Security.Cryptography;
using System.Text;

namespace GA.BDC.Core.Utilities.Encryption
{
	/// <summary>
	/// MD5Sum class.
	/// </summary>
	public class MD5Sum
	{
		public MD5Sum()
		{

		}

		/// <summary>
		/// Compute the MD5 sum.
		/// </summary>
		/// <param name="data">The data to sum.</param>
		/// <returns>The MD5 sum in lowercase hex string representation.</returns>
		public string ComputeSum(byte[] data) 
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] hash = md5.ComputeHash(data);
			string sum = BitConverter.ToString(hash).Replace("-", "").ToLower();
			return sum;
		}

		/// <summary>
		/// Compute the MD5 sum.
		/// </summary>
		/// <param name="data">The data to sum.</param>
		/// <returns>The MD5 sum in lowercase hex string representation.</returns>
		public string ComputeSum(string data) 
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(data));
			string sum = BitConverter.ToString(hash).Replace("-", "").ToLower();
			return sum;
		}
	}
}
