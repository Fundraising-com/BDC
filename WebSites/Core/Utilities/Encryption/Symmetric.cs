using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace GA.BDC.Core.Utilities.Encryption.QspEncryption
{
    /// <summary>
    /// Symmetric encryption uses a single key to encrypt and decrypt. 
    /// Both parties (encryptor and decryptor) must share the same secret key.
    /// </summary>
    public class Symmetric : IDisposable
    {

        private const string _DefaultIntializationVector = "%1Az=-@qT";
        private const int _BufferSize = 2048;

        private QspData _key;
        private QspData _iv;
        private SymmetricAlgorithm _crypto;

        private Symmetric()
        {
        }

        /// <summary>
        /// Instantiates a new symmetric encryption object using the specified provider.
        /// </summary>
        public Symmetric(Provider provider)
        {
            switch (provider)
            {
                case Provider.Des:
                    _crypto = new DESCryptoServiceProvider();
                    break;
                case Provider.RC2:
                    _crypto = new RC2CryptoServiceProvider();
                    break;
                case Provider.Rijndael:
                    _crypto = new RijndaelManaged();
                    break;
                case Provider.TripleDes:
                    _crypto = new TripleDESCryptoServiceProvider();
                    break;
            }

            //-- make sure key and IV are always set, no matter what
            this.Key = RandomKey();
            this.InitializationVector = new QspData(_DefaultIntializationVector);
        }

        /// <summary>
        /// Instantiates a new symmetric encryption object using the specified provider.
        /// </summary>
        public Symmetric(Provider provider, bool useDefaultInitializationVector)
        {
            switch (provider)
            {
                case Provider.Des:
                    _crypto = new DESCryptoServiceProvider();
                    break;
                case Provider.RC2:
                    _crypto = new RC2CryptoServiceProvider();
                    break;
                case Provider.Rijndael:
                    _crypto = new RijndaelManaged();
                    break;
                case Provider.TripleDes:
                    _crypto = new TripleDESCryptoServiceProvider();
                    break;
            }

            //-- make sure key and IV are always set, no matter what
            this.Key = RandomKey();
            if (useDefaultInitializationVector)
            {
                this.InitializationVector = new QspData(_DefaultIntializationVector);
            }
            else
            {
                this.InitializationVector = RandomInitializationVector();
            }
        }

        /// <summary>
        /// Key size in bytes. We use the default key size for any given provider; if you 
        /// want to force a specific key size, set this property
        /// </summary>
        public int KeySizeBytes
        {
            get { return _crypto.KeySize / 8; }
            set
            {
                if(value > int.MaxValue / 8)
                    throw new ArgumentOutOfRangeException("value", "value must be less than or equal to Int32.MinValue / 8");
                _crypto.KeySize = value * 8;
                _key.MaxBytes = value;
            }
        }

        /// <summary>
        /// Key size in bits. We use the default key size for any given provider; if you 
        /// want to force a specific key size, set this property
        /// </summary>
        public int KeySizeBits
        {
            get { return _crypto.KeySize; }
            set
            {
                _crypto.KeySize = value;
                _key.MaxBits = value;
            }
        }

        /// <summary>
        /// The key used to encrypt/decrypt data
        /// </summary>
        public QspData Key
        {
            get { return _key; }
            set
            {
                _key = value;
                _key.MaxBytes = _crypto.LegalKeySizes[0].MaxSize / 8;
                _key.MinBytes = _crypto.LegalKeySizes[0].MinSize / 8;
                _key.StepBytes = _crypto.LegalKeySizes[0].SkipSize / 8;
            }
        }

        /// <summary>
        /// Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
        /// the value derived from the previous block; the first data block has no previous data block
        /// to use, so it needs an InitializationVector to feed the first block
        /// </summary>
        public QspData InitializationVector
        {
            get { return _iv; }
            set
            {
                _iv = value;
                _iv.MaxBytes = _crypto.BlockSize / 8;
                _iv.MinBytes = _crypto.BlockSize / 8;
            }
        }

        /// <summary>
        /// generates a random Initialization Vector, if one was not provided
        /// </summary>
        public QspData RandomInitializationVector()
        {
            _crypto.GenerateIV();
            QspData d = new QspData(_crypto.IV);
            return d;
        }

        /// <summary>
        /// generates a random Key, if one was not provided
        /// </summary>
        public QspData RandomKey()
        {
            _crypto.GenerateKey();
            QspData d = new QspData(_crypto.Key);
            return d;
        }

        /// <summary>
        /// Ensures that _crypto object has valid Key and IV
        /// prior to any attempt to encrypt/decrypt anything
        /// </summary>
        private void ValidateKeyAndIv(bool isEncrypting)
        {
            if (_key.IsEmpty)
            {
                if (isEncrypting)
                {
                    _key = RandomKey();
                }
                else
                {
                    throw new CryptographicException("No key was provided for the decryption operation!");
                }
            }
            if (_iv.IsEmpty)
            {
                if (isEncrypting)
                {
                    _iv = RandomInitializationVector();
                }
                else
                {
                    throw new CryptographicException("No initialization vector was provided for the decryption operation!");
                }
            }
            _crypto.Key = _key.GetBytes();
            _crypto.IV = _iv.GetBytes();
        }

        /// <summary>
        /// Encrypts the specified Data using provided key
        /// </summary>
        public QspData Encrypt(QspData data, QspData key)
        {
            this.Key = key;
            return Encrypt(data);
        }

        /// <summary>
        /// Encrypts the specified Data using preset key and preset initialization vector
        /// </summary>
        public QspData Encrypt(QspData data)
        {
            var ms = new MemoryStream();

            ValidateKeyAndIv(true);

            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(data.GetBytes(), 0, data.GetBytes().Length);
            cs.Close();
            ms.Close();

            return new QspData(ms.ToArray());
        }

        /// <summary>
        /// Encrypts the stream to memory using provided key and provided initialization vector
        /// </summary>
        public QspData Encrypt(Stream stream, QspData key, QspData iv)
        {
            this.InitializationVector = iv;
            this.Key = key;
            return Encrypt(stream);
        }

        /// <summary>
        /// Encrypts the stream to memory using specified key
        /// </summary>
        public QspData Encrypt(Stream stream, QspData key)
        {
            this.Key = key;
            return Encrypt(stream);
        }

        /// <summary>
        /// Encrypts the specified stream to memory using preset key and preset initialization vector
        /// </summary>
        public QspData Encrypt(Stream stream)
        {
            var ms = new MemoryStream();
            byte[] b = new byte[_BufferSize];
            int i;

            ValidateKeyAndIv(true);

            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            i = stream.Read(b, 0, _BufferSize);
            while (i > 0)
            {
                cs.Write(b, 0, i);
                i = stream.Read(b, 0, _BufferSize);
            }

            cs.Close();
            ms.Close();

            return new QspData(ms.ToArray());
        }

        /// <summary>
        /// Decrypts the specified data using provided key and preset initialization vector
        /// </summary>
        public QspData Decrypt(QspData encryptedData, QspData key)
        {
            this.Key = key;
            return Decrypt(encryptedData);
        }

        /// <summary>
        /// Decrypts the specified stream using provided key and preset initialization vector
        /// </summary>
        public QspData Decrypt(Stream encryptedStream, QspData key)
        {
            this.Key = key;
            return Decrypt(encryptedStream);
        }

        /// <summary>
        /// Decrypts the specified stream using preset key and preset initialization vector
        /// </summary>
        public QspData Decrypt(Stream encryptedStream)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] b = new byte[_BufferSize];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(encryptedStream, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            int i;
            i = cs.Read(b, 0, _BufferSize);

            while (i > 0)
            {
                ms.Write(b, 0, i);
                i = cs.Read(b, 0, _BufferSize);
            }
            cs.Close();
            ms.Close();

            return new QspData(ms.ToArray());
        }

        /// <summary>
        /// Decrypts the specified data using preset key and preset initialization vector
        /// </summary>
        public QspData Decrypt(QspData encryptedData)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(encryptedData.GetBytes(), 0, encryptedData.GetBytes().Length);
            //byte[] b = new byte[encryptedData.GetBytes().Length] { };
            var b = new byte[encryptedData.GetBytes().Length + 1];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(ms, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            try
            {
                cs.Read(b, 0, encryptedData.GetBytes().Length);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException("Unable to decrypt data. The provided key may be invalid.", ex);
            }
            finally
            {
                cs.Close();
            }
            return new QspData(b);
        }

        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this._crypto.Clear();
                }

                // TODO: free your own state (unmanaged objects).
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }
        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}