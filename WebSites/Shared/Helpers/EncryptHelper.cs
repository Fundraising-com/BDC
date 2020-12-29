using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GA.BDC.Shared.Helpers
{
   public static class EncryptHelper
   {
      private const string Key = ":Egdb:dV7ebDq`Z6";
      // This constant is used to determine the keysize of the encryption algorithm in bits.
      // We divide this by 8 within the code below to get the equivalent number of bytes.
      private const int Keysize = 256;

      // This constant determines the number of iterations for the password bytes generation function.
      private const int DerivationIterations = 1000;

      public static string Encrypt(string plainText)
      {
         // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
         // so that the same Salt and IV values can be used when decrypting.  
         var saltStringBytes = Generate256BitsOfRandomEntropy();
         var ivStringBytes = Generate256BitsOfRandomEntropy();
         var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
         using (var password = new Rfc2898DeriveBytes(Key, saltStringBytes, DerivationIterations))
         {
            var keyBytes = password.GetBytes(Keysize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
               symmetricKey.BlockSize = 256;
               symmetricKey.Mode = CipherMode.CBC;
               symmetricKey.Padding = PaddingMode.PKCS7;
               using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
               {
                  using (var memoryStream = new MemoryStream())
                  {
                     using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                     {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                        var cipherTextBytes = saltStringBytes;
                        cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                        cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                        memoryStream.Close();
                        cryptoStream.Close();
                        return Convert.ToBase64String(cipherTextBytes);
                     }
                  }
               }
            }
         }
      }

      public static string Decrypt(string cipherText)
      {
         // Get the complete stream of bytes that represent:
         // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
         var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
         // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
         var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
         // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
         var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
         // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
         var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

         using (var password = new Rfc2898DeriveBytes(Key, saltStringBytes, DerivationIterations))
         {
            var keyBytes = password.GetBytes(Keysize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
               symmetricKey.BlockSize = 256;
               symmetricKey.Mode = CipherMode.CBC;
               symmetricKey.Padding = PaddingMode.PKCS7;
               using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
               {
                  using (var memoryStream = new MemoryStream(cipherTextBytes))
                  {
                     using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                     {
                        var plainTextBytes = new byte[cipherTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        memoryStream.Close();
                        cryptoStream.Close();
                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                     }
                  }
               }
            }
         }
      }

      private static byte[] Generate256BitsOfRandomEntropy()
      {
         var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
         using (var rngCsp = new RNGCryptoServiceProvider())
         {
            // Fill the array with cryptographically secure random bytes.
            rngCsp.GetBytes(randomBytes);
         }
         return randomBytes;
      }

      public static string EncryptTripleDES(string value, string key)
      {
         var tripleDesCryptoServiceProvider = new TripleDESCryptoServiceProvider();

         // hash md5
         var md5 = new MD5CryptoServiceProvider();
         byte[] encryptedPasswordBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(key));

         // set the key
         tripleDesCryptoServiceProvider.Key = encryptedPasswordBytes;

         //the mode is the block cipher mode which is basically the
         //details of how the encryption will work. There are several
         //kinds of ciphers available in DES3 and they all have benefits
         //and drawbacks. Here the Electronic Codebook cipher is used
         //which means that a given bit of text is always encrypted
         //exactly the same when the same password is used.
         tripleDesCryptoServiceProvider.Mode = CipherMode.ECB; //CBC, CFB

         var encryptedData = Encoding.ASCII.GetBytes(value);

         //encrypt the byte buffer representation of the original string
         //and base64 encode the encrypted string. the reason the encrypted
         //bytes are being base64 encoded as a string is the encryption will
         //have created some weird characters in there. Base64 encoding
         //provides a platform independent view of the encrypted string 
         //and can be sent as a plain text string to wherever.
         var encrypted = Convert.ToBase64String(tripleDesCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length));

         return encrypted;
      }

      public static string DecryptTripleDES(string value, string key)
      {
         // create the encryption object
         var tripleDesCryptoServiceProvider = new TripleDESCryptoServiceProvider();

         // hash md5
         var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
         var encryptedPasswordBytes = md5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(key));

         // set the key
         tripleDesCryptoServiceProvider.Key = encryptedPasswordBytes;

         //the mode is the block cipher mode which is basically the
         //details of how the encryption will work. There are several
         //kinds of ciphers available in DES3 and they all have benefits
         //and drawbacks. Here the Electronic Codebook cipher is used
         //which means that a given bit of text is always encrypted
         //exactly the same when the same password is used.
         tripleDesCryptoServiceProvider.Mode = CipherMode.ECB; //CBC, CFB

         var encrypted = Convert.FromBase64String(value);

         //encrypt the byte buffer representation of the original string
         //and base64 encode the encrypted string. the reason the encrypted
         //bytes are being base64 encoded as a string is the encryption will
         //have created some weird characters in there. Base64 encoding
         //provides a platform independent view of the encrypted string 
         //and can be sent as a plain text string to wherever.
         var decrypted = Encoding.ASCII.GetString(
               tripleDesCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length));

         return decrypted;
      }
   }
}
