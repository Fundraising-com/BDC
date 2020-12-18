using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace GA.BDC.Web.MGP.Helpers
{
    /// <summary>
    /// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
    /// decrypt data. As long as encryption and decryption routines use the same
    /// parameters to generate the keys, the keys are guaranteed to be the same.
    /// </summary>
    public class AESHelper
    {
        #region Private members
        // If key size is not specified, use the longest 256-bit key.
        private const int DEFAULT_KEY_SIZE = 256;

        // If hashing algorithm is not specified, use SHA-1.
        private static string DEFAULT_HASH_ALGORITHM = "SHA1";

        // These members will be used to perform encryption and decryption.
        private ICryptoTransform encryptor = null;
        private ICryptoTransform decryptor = null;
        #endregion

        #region Constructors
        /// <param name="secretKey">
        /// Secret key from which ASCII byte encoding of the key will be derived.
        /// Secret key can be any string, but the length in bytes (character 
        /// length * 8) must conform to the KeySize parameter of the RijndaelManaged 
        /// object.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. 
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hashing algorithm: "MD5" or "SHA1". SHA1 is recommended.
        /// </param>
        public AESHelper(string secretKey) : this(secretKey, null) { }
        public AESHelper(string secretKey, string initVector) : this(secretKey, initVector, -1) { }
        public AESHelper(string secretKey, string initVector, int keySize) : this(secretKey, initVector, keySize, null) { }
        public AESHelper(string secretKey, string initVector, int keySize, string hashAlgorithm)
        {
            // Validate the secretKey parameter
            if (secretKey == null || secretKey.Trim() == "")
                throw new Exception("AES SecretKey parameter cannot be null or empty string");

            // Set the size of cryptographic key.
            if ((keySize != 128 && keySize != 192 && keySize != 256))
                keySize = DEFAULT_KEY_SIZE;

            // Set the name of algorithm. Make sure it is in UPPER CASE and does
            // not use dashes, e.g. change "sha-1" to "SHA1".
            if (hashAlgorithm == null || hashAlgorithm.Trim() == "")
                hashAlgorithm = DEFAULT_HASH_ALGORITHM;
            else
                hashAlgorithm = hashAlgorithm.ToUpper().Replace("-", "");
            if (hashAlgorithm.IndexOf("SHA1") < 0 && hashAlgorithm.IndexOf("MD5") < 0)
                throw new Exception("Invalid AES hashAlgorithm: " + hashAlgorithm);

            // Initialization vector converted to a byte array.
            byte[] initVectorBytes = null;

            // Get bytes of initialization vector.
            if (initVector == null || initVector.Trim() == "")
                initVectorBytes = new byte[0];
            else
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            // Convert key to a byte array
            byte[] keyBytes = Encoding.ASCII.GetBytes(secretKey);

            // Initialize Rijndael key object, and set it's properties
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Padding = PaddingMode.PKCS7;
            symmetricKey.KeySize = keySize;

            // If we do not have initialization vector, we cannot use the CBC mode.
            // The only alternative is the ECB mode (which is not as good).
            if (initVectorBytes.Length == 0)
                symmetricKey.Mode = CipherMode.ECB;
            else
                symmetricKey.Mode = CipherMode.CBC;

            // Create encryptor and decryptor, which we will use for cryptographic
            // operations.
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }
        #endregion

        #region Encryption routines
        /// <summary>
        /// Encrypts a string value generating a base64-encoded string.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(string plainText)
        {
            if (plainText == null)
                throw new Exception("AES Encrypt method requires a non-null plainText parameter");
            return Encrypt(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a base64-encoded string.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(byte[] plainTextBytes)
        {
            if (plainTextBytes == null || plainTextBytes.Length == 0)
                throw new Exception("AES Encrypt method requires a non-null plainTextBytes parameter");
            return Convert.ToBase64String(EncryptToBytes(plainTextBytes));
        }

        /// <summary>
        /// Encrypts a string value generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(string plainText)
        {
            if (plainText == null)
                throw new Exception("AES EncryptToBytes method requires a non-null plainText parameter");
            try
            {
                return EncryptToBytes(Encoding.UTF8.GetBytes(plainText));
            }
            catch (Exception ex)
            {
                throw new Exception("AES EncryptToBytes method was not able to encrypt plainText=" + plainText, ex);
            }
        }

        /// <summary>
        /// Encrypts a byte array generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(byte[] plainTextBytes)
        {
            using (MemoryStream msEncrypt = new MemoryStream())
            // To perform encryption, we must use the Write mode.
            using (CryptoStream encStream = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                // Start encrypting data.
                encStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                // Finish the encryption operation.
                encStream.FlushFinalBlock();
                // Return encrypted data.
                return msEncrypt.ToArray();
            }
        }
        #endregion

        #region Decryption routines
        /// <summary>
        /// Decrypts a base64-encoded cipher text value generating a string result.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(string cipherText)
        {
            if (cipherText == null)
                throw new Exception("AES Decrypt method requires a non-null cipherText parameter");
            try
            {
                return Decrypt(Convert.FromBase64String(cipherText.Replace(" ", "+")));
            }
            catch (Exception ex)
            {

                throw new Exception("AES Decrypt method was not able to decrypt cipherText=" + cipherText, ex);
            }
        }

        /// <summary>
        /// Decrypts a byte array containing cipher text value and generates a
        /// string result.
        /// </summary>
        /// <param name="cipherTextBytes">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(byte[] cipherTextBytes)
        {
            if (cipherTextBytes == null || cipherTextBytes.Length == 0)
                throw new Exception("AES Decrypt method requires a non-null cipherTextBytes parameter");
            return Encoding.UTF8.GetString(DecryptToBytes(cipherTextBytes));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(string cipherText)
        {
            if (cipherText == null)
                throw new Exception("AES DecryptToBytes method requires a non-null cipherText parameter");
            try
            {
                return DecryptToBytes(Convert.FromBase64String(cipherText));
            }
            catch (Exception ex)
            {
                throw new Exception("AES DecryptToBytes method was not able to decrypt cipherText=" + cipherText, ex);
            }
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="cipherTextBytes">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(byte[] cipherTextBytes)
        {
            using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
            // To perform decryption, we must use the Read mode.
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                // Decrypted bytes will always be less then encrypted bytes, so len of encrypted data will be big enoupgh for buffer.
                byte[] fromEncrypt = new byte[cipherTextBytes.Length];
                // Read as many bytes as possible.
                int read = csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                if (read < fromEncrypt.Length)
                {
                    // Return a byte array of proper size.
                    byte[] clearBytes = new byte[read];
                    System.Buffer.BlockCopy(fromEncrypt, 0, clearBytes, 0, read);
                    return clearBytes;
                }
                return fromEncrypt;
            }
        }
        #endregion
    }
}