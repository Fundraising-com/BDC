using System;
using System.Text;
using System.Security.Cryptography;

namespace GA.BDC.Core.Utilities.Encryption.DES {
	/*
	 * Created by:	Jean-Francois Buist.
	 * Date:		Novembre 2004.
	 * Version:		0.0
	 * 
	 */

	/// <summary>
	/// Class that encrypt and decrypt data.
	/// </summary>
	/// <example>
	/// <code>
	///		using GA.BDC.Core.Utilities.DES;
	///		
	///		TripleDES tripleDES = new TripleDES();
	///		string organizationIdEncrypted = tripleDES.Encrypt(organizationID.ToString(), "ThisIsMyKey");
	///		Response.Redirect("Logged.aspx?orgID=" + organizationIdEncrypted);
	///		
	///		//..........
	///		string organizationIdDecrypted = trupleDES.Decrypt(Request["orgID"], "ThisIsMyKey");
	///		
	///		
	/// </code>
	/// </example>
	/// <remarks>
	/// Might need special format in order to be used throw url. (eg. System.Utility.UrlEncode)
	/// </remarks>
	public class TripleDES {

		/// <summary>
		/// TribleDES constructor
		/// </summary>
		public TripleDES() {
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Encrypt data
		/// </summary>
		/// <param name="encryptData">Data to be encrypted</param>
		/// <param name="keyData">Encryption Key</param>
		/// <returns>Encrypted Data</returns>
		/// <remarks>By default, the CipherMode is ECB (Electronic Codebook)</remarks>
		public string Encrypt(string encryptData, string keyData) {
			return Encrypt(encryptData, keyData, CipherMode.ECB);
		}


		/// <summary>
		/// Encrypt data
		/// </summary>
		/// <param name="encryptData">Data to be encrypted</param>
		/// <param name="keyData">Encryption key</param>
		/// <param name="cipherMode">Cipher Mode</param>
		/// <returns>Data Encrypted</returns>
		public string Encrypt(string encryptData, string keyData, CipherMode cipherMode) {
			// create the encryption object
			TripleDESCryptoServiceProvider trippleDES = new TripleDESCryptoServiceProvider();

			// hash md5
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] encryptedPasswordBytes = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(keyData));

            // set the key
			trippleDES.Key = encryptedPasswordBytes;

			//the mode is the block cipher mode which is basically the
			//details of how the encryption will work. There are several
			//kinds of ciphers available in DES3 and they all have benefits
			//and drawbacks. Here the Electronic Codebook cipher is used
			//which means that a given bit of text is always encrypted
			//exactly the same when the same password is used.
			trippleDES.Mode = cipherMode; //CBC, CFB

			byte[] encryptedData = ASCIIEncoding.ASCII.GetBytes(encryptData);

			//encrypt the byte buffer representation of the original string
			//and base64 encode the encrypted string. the reason the encrypted
			//bytes are being base64 encoded as a string is the encryption will
			//have created some weird characters in there. Base64 encoding
			//provides a platform independent view of the encrypted string 
			//and can be sent as a plain text string to wherever.
			string encrypted = Convert.ToBase64String(
				trippleDES.CreateEncryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length));

			return encrypted;

		}

		/// <summary>
		/// Decrypt data
		/// </summary>
		/// <param name="encrypedData">Data encrypted</param>
		/// <param name="keyData">Encryption Key</param>
		/// <returns>Data decrypted</returns>
		/// <remarks>By default, the CipherMode is ECB (Electronic Codebook)</remarks>
		public string Decrypt(string encrypedData, string keyData) {
			return Decrypt(encrypedData, keyData, CipherMode.ECB);
		}

		/// <summary>
		/// Decrypt data
		/// </summary>
		/// <param name="encrypedData">Data encrypted</param>
		/// <param name="keyData">Encryption Key</param>
		/// <param name="cipherMode">Cipher Mode</param>
		/// <returns>Data decrypted</returns>
		public string Decrypt(string encrypedData, string keyData, CipherMode cipherMode) {
			// create the encryption object
			TripleDESCryptoServiceProvider trippleDES = new TripleDESCryptoServiceProvider();

			// hash md5
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] encryptedPasswordBytes = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(keyData));

			// set the key
			trippleDES.Key = encryptedPasswordBytes;

			//the mode is the block cipher mode which is basically the
			//details of how the encryption will work. There are several
			//kinds of ciphers available in DES3 and they all have benefits
			//and drawbacks. Here the Electronic Codebook cipher is used
			//which means that a given bit of text is always encrypted
			//exactly the same when the same password is used.
			trippleDES.Mode = cipherMode; //CBC, CFB

			byte[] encrypted = Convert.FromBase64String(encrypedData);

			//encrypt the byte buffer representation of the original string
			//and base64 encode the encrypted string. the reason the encrypted
			//bytes are being base64 encoded as a string is the encryption will
			//have created some weird characters in there. Base64 encoding
			//provides a platform independent view of the encrypted string 
			//and can be sent as a plain text string to wherever.
			string decrypted = ASCIIEncoding.ASCII.GetString(
					trippleDES.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length));

			return decrypted;
		}
	}
}
