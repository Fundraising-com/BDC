using System;

namespace GA.BDC.Core.Utilities.Encryption
{
	/// <summary>
	/// Summary description for Password.
	/// </summary>
	public class Password
	{
		private Password()
		{

		}

		public static string CreateRandomPassword(int passwordLength)
		{
			string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
			Byte[] randomBytes = new Byte[passwordLength];
			char[] chars = new char[passwordLength];
			int allowedCharCount = allowedChars.Length;
			Random rnd = new Random();

			for (int i = 0; i < passwordLength; i++)
			{
				chars[i] = allowedChars[rnd.Next(allowedCharCount)];
			}

			return new string(chars);
		}
	}
}
