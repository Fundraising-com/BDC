using System;
using System.IO;

namespace FileStore
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Serializable]
	public class Store : MarshalByRefObject
	{
		private StoreConfiguration oStoreConfiguration;
		private DirectoryInfo oStoreDirectory;

		public Store() : base()
		{
			InitializeStore();
		}

		protected virtual StoreConfiguration Configuration 
		{
			get 
			{
				return oStoreConfiguration;
			}
		}

		public virtual DirectoryInfo StoreDirectory 
		{
			get 
			{
				return oStoreDirectory;
			}
		}

		protected virtual void InitializeStore() 
		{
			oStoreConfiguration = new StoreConfiguration();

			try 
			{
				oStoreDirectory = new DirectoryInfo(oStoreConfiguration.Directory);

				if(!oStoreDirectory.Exists) 
				{
					oStoreDirectory.Create();
				}

				oStoreDirectory = oStoreDirectory.CreateSubdirectory(FormatDirectoryName());
			} 
			catch
			{
				throw new StoreAccessFailedException();
			}
		}

		public virtual void Close() 
		{
            try
            {
                oStoreDirectory.Delete(true);
            }
            catch
            {
            }
		}

		public virtual void Add(string fileName, byte[] file) 
		{
			FileStream oFileStream = new FileStream(StoreDirectory.FullName + "\\" + fileName, FileMode.Create, FileAccess.Write);

			oFileStream.Write(file, 0, file.Length);
			oFileStream.Flush();
			oFileStream.Close();
		}

		public virtual byte[] Get(string fileName) 
		{
			byte[] file = null;
			FileStream oFileStream = null;
			FileInfo oFileInfo;

			oFileInfo = new FileInfo(StoreDirectory.FullName + "\\" + fileName);

			if(oFileInfo.Exists) 
			{
				file = new byte[oFileInfo.Length];

				try 
				{
					oFileStream = new FileStream(StoreDirectory.FullName + "\\" + fileName, FileMode.Open, FileAccess.Read);

					oFileStream.Read(file, 0, Convert.ToInt32(oFileInfo.Length));
					oFileStream.Close();
				} 
				catch(Exception ex) 
				{
					if(oFileStream != null) 
					{
						oFileStream.Close();
					}

					throw ex;
				}
			} 
			else 
			{
				throw new FileNotFoundException();
			}

			return file;
		}

		private string FormatDirectoryName() 
		{
			return DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
		}
	}
}
