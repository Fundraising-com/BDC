using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GA.BDC.Core.Xml.Serialization
{
	/// <summary>
	/// This component provides fast access to class serialization.
	/// </summary>
	/// <example>
	///		 <code>
	///			// to create file
	///			MyClass myClass = new MyClass();
	///			/* fill my class */
	///			Serializer.SaveObjectToXmlFile("c:\\test.dat", myClass);
	///			
	///			// to retreve from file
	///			myClass = Serializer.GetObjectFromXmlFile("c:\\test.dat", typeof(MyClass));
	///			
	///			/* binary files are also supported, see SaveObjectToBinaryFile and
	///			GetObjectFromBinaryFile */
	///		 </code>
	///	</example>
	///	<remarks>Your class must be serializable.</remarks>
	public class Serializer
	{
		public Serializer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void SaveObjectToBinaryFile(string filename, object obj) {
			Stream stream = null;
			try {
				stream = new FileStream(filename, System.IO.FileMode.Create );
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize( stream, obj);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				if(stream != null) {
					stream.Close();
				}
			}			
		}

		public static object GetObjectFromBinaryFile(string filename) {
			Stream stream = null;
			object obj = null;
			try {
				stream = new FileStream( filename, System.IO.FileMode.Open, FileAccess.Read);

				IFormatter formatter = new BinaryFormatter();
				obj = (object)formatter.Deserialize( stream );
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				if(stream != null) {
					stream.Close();
				}
			}
			return obj;
		}

		public static void SaveObjectToXmlFile(string filename, object obj) {
			TextWriter writer = null; 
			try {
				// MessageBox.Show(typeof(obj).FullName);
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( obj.GetType());
				writer = new StreamWriter( filename );
				serializer.Serialize( writer, obj);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				writer.Close();
			}
		}

		public static object GetObjectFromXmlFile(string filename, Type type) {
			FileStream stream = null;
			object obj = null;
			try {
				System.Xml.Serialization.XmlSerializer serializer = new 
					System.Xml.Serialization.XmlSerializer(type);

				// A FileStream is needed to read the XML document.
				stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
				XmlReader reader = new XmlTextReader(stream);
          
				// Use the Deserialize method to restore the object's state.
				obj = (object) serializer.Deserialize(reader);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				stream.Close();
			}
			return obj;
		}
	}
}
