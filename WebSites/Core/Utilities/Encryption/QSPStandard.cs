using System;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace GA.BDC.Core.Utilities.Encryption.QspEncryption
{
    public class QspStandard
    {
        string gstrKey;

        public QspStandard()
        {
            gstrKey = GetKey();
        }

        /// <summary>
        /// Tries to encrypt according to settings. Before encrypting, it will remove any "?" from the beginning. 
        /// It will append "?" to the beginning if there isn't any
        /// </summary>
        /// <param name="valueOne"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string TryEncryptQueryString(string valueOne)
        {
            string strEncrypted = string.Empty;
            string strEncryptionSetting = ConfigurationSettings.AppSettings["EnableQSPEncryption"];
            bool encryptEnabled = true;

            if (strEncryptionSetting != null)
            {
                if (strEncryptionSetting.Trim().ToLower(System.Globalization.CultureInfo.CurrentCulture) == "false")
                {
                    if (!valueOne.StartsWith("?", StringComparison.Ordinal))
                    {
                        strEncrypted = "?" + valueOne;
                    }
                    else
                    {
                        strEncrypted = valueOne;
                    }
                    encryptEnabled = false;
                }
            }

            if (encryptEnabled == true)
            {
                if (valueOne.StartsWith("?", StringComparison.Ordinal))
                {
                    valueOne = valueOne.Substring(1);
                }
                strEncrypted = Encrypt(valueOne);
                strEncrypted = "?LocalID=" + Utils.LocalEncode( strEncrypted );
            }

            return strEncrypted;
        }

        public string Encrypt(string valueOne)
        {
            Symmetric sym = new Symmetric(Provider.Rijndael);
            QspData ecdKey = new QspData(gstrKey);
            QspData ecdEncrypted;

            ecdEncrypted = sym.Encrypt(new QspData(valueOne), ecdKey);
            string strEncrypted = ecdEncrypted.ToBase64();

            return strEncrypted;
        }

        public string Encrypt(string valueOne, string valueSeed)
        {
            Symmetric sym = new Symmetric(Provider.Rijndael);
            QspData ecdKey = new QspData(gstrKey);
            QspData ecdEncrypted;

            ecdEncrypted = sym.Encrypt(new QspData(valueSeed + valueOne), ecdKey);
            string strEncrypted = ecdEncrypted.ToBase64();

            return strEncrypted;
        }

        public string TryDecryptQueryString(string valueOne)
        {
            string strDecrypted;
            string strEncryptionSetting = ConfigurationSettings.AppSettings["EnableQSPEncryption"];

            if (strEncryptionSetting != null)
            {
                if (strEncryptionSetting.Trim().ToLower(System.Globalization.CultureInfo.CurrentCulture) == "false")
                {
                    strDecrypted = valueOne;
                }
                else
                {
                    strDecrypted = Decrypt(Utils.LocalDecode(valueOne), "");
                }
            }
            else
            {
                return Decrypt(Utils.LocalDecode(valueOne), "");
            }

            return strDecrypted;
        }

        public string Decrypt(string valueOne)
        {
            return Decrypt(valueOne, "");
        }

        public string Decrypt(string valueOne, string valueSeed)
        {
            Symmetric sym = new Symmetric(Provider.Rijndael);
            QspData ecdKey = new QspData(gstrKey);
            QspData ecdEncrypted = new QspData();
            QspData ecdDecrypted;

            ecdEncrypted.Base64 = valueOne;
            ecdDecrypted = sym.Decrypt(ecdEncrypted, ecdKey);
            string strValue = RebuildString(ecdDecrypted.ToString());

            if (string.IsNullOrEmpty(valueSeed)) return strValue;
            else if(!strValue.StartsWith(valueSeed,StringComparison.Ordinal)) throw new System.FormatException("Seed value is incorrect");
            else return strValue.Substring(valueSeed.Length);
        }

        private static string RebuildString(string strInput)
        {
            int i;
            var strChar = strInput.ToCharArray();
            var sbdReturn = new System.Text.StringBuilder();

            for (i = 0; i < strInput.Length; i++)
            {
                if ((int) strChar[i] != 0)
                {
                    sbdReturn.Append(strChar[i]);
                }
            }
            return sbdReturn.ToString();
        }

        private static string GetKey()
        {
            string strReturn;
            //Dim rky As Microsoft.Win32.RegistryKey

            //rky = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Reimanpub\\")
            //If IsDBNull(rky) Then
            //    'pop error
            //Else
            //    strReturn = rky.GetValue("PazzKei").ToString()
            //End If
            strReturn = "sx/OMFTLiOZWkwlGljrJRHvmuppX+rxzTED1gzhndpc=";

            return strReturn;
        }

        /// <summary>
        /// Encrypt data using TripleDes Encryption
        /// </summary>
        /// <param name="encryptData">Data to be encrypted</param>
        /// <param name="keyData">Encryption Key</param>
        /// <returns>Encrypted Data</returns>
        /// <remarks>By default, the CipherMode is ECB (Electronic Codebook)</remarks>
        public static string EncryptTripleDes(string encryptData, string keyData)
        {
            return EncryptTripleDes(encryptData, keyData, CipherMode.ECB);
        }


        /// <summary>
        /// Encrypt data using TripleDes Encryption
        /// </summary>
        /// <param name="encryptData">Data to be encrypted</param>
        /// <param name="keyData">Encryption key</param>
        /// <param name="cipherMode">Cipher Mode</param>
        /// <returns>Data Encrypted</returns>
        public static string EncryptTripleDes(string encryptData, string keyData, CipherMode cipherMode)
        {
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
            //exactly the same when the same pxxxword is used.
            trippleDES.Mode = cipherMode;
            //CBC, CFB
            byte[] encryptedData = ASCIIEncoding.ASCII.GetBytes(encryptData);

            //encrypt the byte buffer representation of the original string
            //and base64 encode the encrypted string. the reason the encrypted
            //bytes are being base64 encoded as a string is the encryption will
            //have created some weird characters in there. Base64 encoding
            //provides a platform independent view of the encrypted string 
            //and can be sent as a plain text string to wherever.
            string encrypted = Convert.ToBase64String(trippleDES.CreateEncryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length));

            return encrypted;

        }

        /// <summary>
        /// Decrypt data using TripleDes Encryption
        /// </summary>
        /// <param name="encryptedData">Data encrypted</param>
        /// <param name="keyData">Encryption Key</param>
        /// <returns>Data decrypted</returns>
        /// <remarks>By default, the CipherMode is ECB (Electronic Codebook)</remarks>
        public static string DecryptTripleDes(string encryptedData, string keyData)
        {
            return DecryptTripleDes(encryptedData, keyData, CipherMode.ECB);
        }

        /// <summary>
        /// Decrypt data using TripleDes Encryption
        /// </summary>
        /// <param name="encryptedData">Data encrypted</param>
        /// <param name="keyData">Encryption Key</param>
        /// <param name="cipherMode">Cipher Mode</param>
        /// <returns>Data decrypted</returns>
        public static string DecryptTripleDes(string encryptedData, string keyData, CipherMode cipherMode)
        {
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
            //exactly the same when the same pxxxword is used.
            trippleDES.Mode = cipherMode;
            //CBC, CFB
            byte[] encrypted = Convert.FromBase64String(encryptedData);

            //encrypt the byte buffer representation of the original string
            //and base64 encode the encrypted string. the reason the encrypted
            //bytes are being base64 encoded as a string is the encryption will
            //have created some weird characters in there. Base64 encoding
            //provides a platform independent view of the encrypted string 
            //and can be sent as a plain text string to wherever.
            string decrypted = ASCIIEncoding.ASCII.GetString(trippleDES.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length));

            return decrypted;
        }
    }
}