//
// 2005-08-30 - Stephen Lim - New class.
//

using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections;

 
namespace GA.BDC.Core.Xml.Serialization
{
 
	/// <summary>
	/// Serializes and deserializes objects into and from XML documents. The OpenNETCF.Xml.Serialization.XmlSerializer enables you to control how objects are encoded into XML. 
	/// </summary>
	public class XmlSerializer
	{

		#region Constants
		private const int MaxDepth = 8;
		#endregion

		#region Fields
		private Type t;
		#endregion
 
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the System.Xml.Serialization.XmlSerializer class that can serialize objects of the specified type into XML documents, and vice versa. 
		/// </summary>
		/// <param name="type">The type of the object that this OpenNETCF.Xml.Serialization.XmlSerializer can serialize.</param>
		public XmlSerializer(Type type) 
		{
			this.t = type;
		}
		#endregion

		#region Methods
 
		/// <summary>
		/// Serializes the specified System.Object and writes the XML document to a file using the specified System.Xml.XmlWriter.
		/// </summary>
		/// <param name="w">The System.xml.XmlWriter used to write the XML document.</param>
		/// <param name="graph">The System.Object to serialize.</param>
		public void SerializeFields(XmlTextWriter w, object graph) 
		{
			w.WriteStartDocument();
 
			// Check to [XmlRoot] attributes
			object[] clsAttribs = t.GetCustomAttributes(typeof(XmlRootAttribute), false);
 
			if(clsAttribs.Length > 0) 
				w.WriteStartElement((clsAttribs[0] as XmlRootAttribute).ElementName);	
			else 
				w.WriteStartElement(graph.GetType().Name);
 
			SerializeFieldsGraph(w, graph, 0);
 
			w.WriteEndElement();
			w.WriteEndDocument();
			w.Close();
		}
 
		/// <summary>
		/// Serializes the specified System.Object and writes the XML document to a file using the specified System.IO.TextWriter.
		/// </summary>
		/// <param name="textWriter">The System.IO.TextWriter used to write the XML document.</param>
		/// <param name="graph">The System.Object to serialize.</param>
		public void SerializeFields(TextWriter textWriter, object graph)
		{
			XmlTextWriter w = new XmlTextWriter(textWriter);
			w.Formatting = Formatting.Indented;
			this.SerializeFieldsGraph(w, graph, 0);
		}
 
		/// <summary>
		/// Serializes the specified System.Object and writes the XML document to a file using the specified System.IO.Stream.
		/// </summary>
		/// <param name="stream">The System.IO.Stream used to write the XML document.</param>
		/// <param name="graph">The System.Object to serialize.</param>
		public void SerializeFields(Stream stream, object graph)
		{
			XmlTextWriter w = new XmlTextWriter(stream, System.Text.Encoding.ASCII);
			w.Formatting = Formatting.Indented;
			this.SerializeFieldsGraph(w, graph, 0);
		}

		/// <summary>
		/// Recursive internal method that loops through the object's properties and builds an XML tree.
		/// </summary>
		/// <param name="graph">The object graph to serialize.</param>
		/// <param name="w">The XmlTextWriter used to build the XML tree.</param>
		internal void SerializePropertiesGraph(XmlTextWriter w, object graph, int depth) 
		{
			if (++depth > MaxDepth)
				return;

			// Ignore null
			if (graph == null)
				return;

			Type t = graph.GetType();
 
			if (IsCollectionType(t.BaseType))
			{
				#region Process each item in the collection and serialize
 
				System.Collections.CollectionBase coll = (System.Collections.CollectionBase)graph;
				foreach (object obj in coll)
				{
					SerializePropertiesGraph(w,obj, depth);
				}				
 
				#endregion
			}
			else
			{
				#region Loop through each property
 
				// Reflect only on the properties of the type that has been passed to us
				PropertyInfo[] ps = GetAllProperties(t);
 
				// Loop through each property...
				foreach (PropertyInfo p in ps) 
				{
					try 
					{
						// Check the property for [XmlIgnore] and ignore if it's present
						if ( p.IsDefined(typeof(XmlIgnoreAttribute), false) )
							continue;
 
						//Ignore read-only properties. We cannot deserialize them anyway
						//if ( !p.CanWrite || !p.CanRead )
						//	continue;
 
						//Ignore static properties
						if (( p.GetGetMethod() != null && p.GetGetMethod().IsStatic) || 
							(p.GetSetMethod() != null && p.GetSetMethod().IsStatic ))
							continue;
			
						// Check the property for any attributes
						object[] attribs = p.GetCustomAttributes(false);
						object attrib = null;
 
						// Pull out the first attribute and hope it's an XmlAttribute
						if (attribs.Length > 0) 
						{
							int i = 0;
							while(i < attribs.Length) 
							{
								if(attribs[i].ToString().StartsWith("System.Xml.Serialization.Xml")) 
								{
									attrib = attribs[i];
									break;
								}
								i++;
							}
						}
 
						#region Check for Enum
					
						if (p.PropertyType.IsEnum) 
						{
							object o = p.GetValue(graph,null);
							if(attrib == null)
								w.WriteElementString(p.Name, Convert.ToInt32(o).ToString());
							else if (attrib.GetType() == typeof(XmlElementAttribute)) 
								w.WriteElementString((attrib as XmlElementAttribute).ElementName, Convert.ToInt32(o).ToString());
							else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
								w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, Convert.ToInt32(o).ToString());
							continue;
						}
					
						#endregion
 
						#region Check for ValueType or String
					
						if (p.PropertyType.IsValueType || p.PropertyType.FullName == "System.String") 
						{
							object o = p.GetValue(graph,null);
 
							// Is it a structure?
							if ( !p.PropertyType.IsPrimitive && p.PropertyType.FullName != "System.String")
							{
								w.WriteStartElement(p.Name);
								SerializePropertiesGraph(w, o, depth);
								w.WriteEndElement();
								continue;
							}
 
							if (o != null) 
							{
								if (attrib == null)
									w.WriteElementString(p.Name, o.ToString());
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteElementString((attrib as XmlElementAttribute).ElementName,o.ToString());
								else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
									w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, o.ToString());
							}					
							else
							{
								if (attrib == null)
									w.WriteElementString(p.Name, "");
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteElementString((attrib as XmlElementAttribute).ElementName,"");
								else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
									w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, "");
							}
						}
							#endregion
 
							#region Check for Collection
					
						else if (IsCollectionType(p.PropertyType.BaseType)) 
						{
							// Are we at the beginning of a collection?
							bool bCollectionStart = false; 
						
							System.Collections.CollectionBase col = (System.Collections.CollectionBase)p.GetValue(graph,null);
							foreach (object obj in col) 
							{
								if (!bCollectionStart) 
								{
									if (attrib == null)
										w.WriteStartElement(p.Name);
									else if (attrib.GetType() == typeof(XmlRootAttribute)) 
										w.WriteStartElement((attrib as XmlRootAttribute).ElementName);
									else if (attrib.GetType() == typeof(XmlElementAttribute)) 
										w.WriteStartElement((attrib as XmlElementAttribute).ElementName);									
 
									bCollectionStart = true;
								}
 
								// Recurse on the collection we found. 
								SerializePropertiesGraph(w,obj, depth);
							}
							w.WriteEndElement();
						}
							#endregion
 
							#region Check for ReferenceType
					
						else if (t.Assembly.GetType(p.PropertyType.FullName,false) != null) 
						{
							object objInstance = p.GetValue(graph,null);
							if (objInstance != null) 
							{
								if (attrib == null)
									w.WriteStartElement(p.Name);
								else if (attrib.GetType() == typeof(XmlRootAttribute)) 
									w.WriteStartElement((attrib as XmlRootAttribute).ElementName);
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteStartElement((attrib as XmlElementAttribute).ElementName);
							
								// Recurse on the reference type we found. 
								SerializePropertiesGraph(w,objInstance, depth);
					
								w.WriteEndElement();
							}
							else
							{
								if (attrib == null)
									w.WriteElementString(p.Name, "");
								else if (attrib.GetType() == typeof(XmlRootAttribute)) 
									w.WriteElementString((attrib as XmlRootAttribute).ElementName,"");
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteAttributeString((attrib as XmlElementAttribute).ElementName, "");
							}
						}
						#endregion
					}
					catch {}
				}
 
				#endregion
			}
		}

		internal bool IsCollectionType(Type t)
		{
			if (t == null)
				return false;
			else if (t == typeof(System.Collections.CollectionBase))
				return true;
			else
				return IsCollectionType(t.BaseType);
		}


		/// <summary>
		/// Recursively get all properties including from base classes.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		internal PropertyInfo[] GetAllProperties(Type t)
		{
			PropertyInfo[] typeProperties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
			Type baseType = t.BaseType;
			if (baseType != null)
			{
				PropertyInfo[] baseProperties = GetAllProperties(baseType);
				if (baseProperties.Length > 0)
				{
					PropertyInfo[] combinedProperties = new PropertyInfo[typeProperties.Length + baseProperties.Length];
					typeProperties.CopyTo(combinedProperties, 0);
					baseProperties.CopyTo(combinedProperties, typeProperties.Length);
					typeProperties = combinedProperties;
				}
			}
			return typeProperties;
		}
 
		/// <summary>
		/// Recursively get all fields including from base classes.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		internal FieldInfo[] GetAllFields(Type t)
		{
			FieldInfo[] typeFields = t.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
			Type baseType = t.BaseType;
			if (baseType != null)
			{
				FieldInfo[] baseFields = GetAllFields(baseType);
				if (baseFields.Length > 0)
				{
					FieldInfo[] combinedFields = new FieldInfo[typeFields.Length + baseFields.Length];
					typeFields.CopyTo(combinedFields, 0);
					baseFields.CopyTo(combinedFields, typeFields.Length);
					typeFields = combinedFields;
				}
			}
			return typeFields;
		}


		/// <summary>
		/// Recursive internal method that loops through the object's properties and builds an XML tree.
		/// </summary>
		/// <param name="graph">The object graph to serialize.</param>
		/// <param name="w">The XmlTextWriter used to build the XML tree.</param>
		internal void SerializeFieldsGraph(XmlTextWriter w, object graph, int depth) 
		{
			if (++depth > MaxDepth)
				return;

			// Ignore null
			if (graph == null)
				return;

			Type t = graph.GetType();
			if (IsCollectionType(t))
			{
				#region Process each item in the collection and serialize
 
				System.Collections.CollectionBase coll = (System.Collections.CollectionBase)graph;
				foreach (object obj in coll)
				{
					SerializeFieldsGraph(w, obj, depth);
				}				
 
				#endregion
			}
			else
			{
				#region Loop through each field
 
				// Reflect only on the properties of the type that has been passed to us
				//FieldInfo[] fs = t.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
				FieldInfo[] fs = GetAllFields(t);
 
				// Loop through each field...
				foreach (FieldInfo f in fs) 
				{
					try 
					{
						// Check the field for [XmlIgnore] and ignore if it's present
						if ( f.IsDefined(typeof(XmlIgnoreAttribute), false) )
							continue;
 
						//Ignore static fields
						if (f.IsStatic)
							continue;
			
						// Check the field for any attributes
						object[] attribs = f.GetCustomAttributes(false);
						object attrib = null;
 
						// Pull out the first attribute and hope it's an XmlAttribute
						if (attribs.Length > 0) 
						{
							int i = 0;
							while (i < attribs.Length) 
							{
								if (attribs[i].ToString().StartsWith("System.Xml.Serialization.Xml")) 
								{
									attrib = attribs[i];
									break;
								}
								i++;
							}
						}
 
						#region Check for Enum
					
						if (f.FieldType.IsEnum) 
						{
							object o = f.GetValue(graph);
							if (attrib == null)
								w.WriteElementString(f.Name, o.ToString());
							else if (attrib.GetType() == typeof(XmlElementAttribute)) 
								w.WriteElementString((attrib as XmlElementAttribute).ElementName, o.ToString());
							else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
								w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, o.ToString());
							continue;
						}
					
						#endregion
 
						#region Check for ValueType or String or DateTime
					
						if (f.FieldType.IsValueType || 
							f.FieldType.FullName == "System.String" || 
							f.FieldType.FullName == "System.DateTime") 
						{
							object o = f.GetValue(graph);
 
							// Is it a structure?
							if (! f.FieldType.IsPrimitive && 
								f.FieldType.FullName != "System.String" && 
								f.FieldType.FullName != "System.DateTime")
							{
								w.WriteStartElement(f.Name);
								SerializeFieldsGraph(w, o, depth);
								w.WriteEndElement();
								continue;
							}
 
							if (o != null) 
							{
								if (attrib == null)
									w.WriteElementString(f.Name, o.ToString());
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteElementString((attrib as XmlElementAttribute).ElementName,o.ToString());
								else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
									w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, o.ToString());
							}					
							else
							{
								if (attrib == null)
									w.WriteElementString(f.Name, "");
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteElementString((attrib as XmlElementAttribute).ElementName,"");
								else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
									w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, "");
							}
						}
							#endregion

						else if (f.FieldType.FullName == "System.Byte[]")
						{
							byte[] o = (byte[]) f.GetValue(graph);

							if(o != null) 
							{
								if (attrib == null)
									w.WriteElementString(f.Name, Convert.ToBase64String(o));
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteElementString((attrib as XmlElementAttribute).ElementName, Convert.ToBase64String(o));
								else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
									w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, Convert.ToBase64String(o));
							}					
							else
							{
								if (attrib == null)
									w.WriteElementString(f.Name, "");
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteElementString((attrib as XmlElementAttribute).ElementName,"");
								else if (attrib.GetType() == typeof(XmlAttributeAttribute)) 
									w.WriteAttributeString((attrib as XmlAttributeAttribute).AttributeName, "");
							}
						}

						else if (f.FieldType.IsArray)
						{
							// Are we at the beginning of a collection?
							bool bCollectionStart = false; 

							foreach (object obj in (Array) f.GetValue(graph))
							{
								if (! bCollectionStart)
								{
									if (attrib == null)
										w.WriteStartElement(f.Name);
									else if (attrib.GetType() == typeof(XmlRootAttribute)) 
										w.WriteStartElement((attrib as XmlRootAttribute).ElementName);
									else if (attrib.GetType() == typeof(XmlElementAttribute)) 
										w.WriteStartElement((attrib as XmlElementAttribute).ElementName);

									bCollectionStart = true;
								}

								// Recursion rocks! Recurse on the collection we found. 
								SerializeFieldsGraph(w, obj, depth);
							}
							w.WriteEndElement();
						}
 
							#region Check for Collection
					
						else if (IsCollectionType(f.FieldType.BaseType)) 
						{
							// Are we at the beginning of a collection?
							bool bCollectionStart = false; 
						
							System.Collections.CollectionBase col = (System.Collections.CollectionBase) f.GetValue(graph);
							foreach (object obj in col) 
							{
								if (!bCollectionStart) 
								{
									if (attrib == null)
										w.WriteStartElement(f.Name);
									else if (attrib.GetType() == typeof(XmlRootAttribute)) 
										w.WriteStartElement((attrib as XmlRootAttribute).ElementName);
									else if (attrib.GetType() == typeof(XmlElementAttribute)) 
										w.WriteStartElement((attrib as XmlElementAttribute).ElementName);									
 
									bCollectionStart = true;
								}
 
								// Recurse on the collection we found. 
								SerializeFieldsGraph(w, obj, depth);
							}
							w.WriteEndElement();
						}
							#endregion
 
							#region Check for ReferenceType
					
						else if (t.Assembly.GetType(f.FieldType.FullName,false) != null) 
						{
							object objInstance = f.GetValue(graph);
							if (objInstance != null) 
							{
								if (attrib == null)
									w.WriteStartElement(f.Name);
								else if (attrib.GetType() == typeof(XmlRootAttribute)) 
									w.WriteStartElement((attrib as XmlRootAttribute).ElementName);
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteStartElement((attrib as XmlElementAttribute).ElementName);
							
								// Recurse on the reference type we found. 
								SerializeFieldsGraph(w, objInstance, depth);
					
								w.WriteEndElement();
							}
							else
							{
								if (attrib == null)
									w.WriteElementString(f.Name, "");
								else if (attrib.GetType() == typeof(XmlRootAttribute)) 
									w.WriteElementString((attrib as XmlRootAttribute).ElementName,"");
								else if (attrib.GetType() == typeof(XmlElementAttribute)) 
									w.WriteAttributeString((attrib as XmlElementAttribute).ElementName, "");
							}
						}
						#endregion
					}
					catch {}
				}
 
				#endregion
			}
		}

		#endregion
	}
}