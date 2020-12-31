using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Manages the persisted properties for another control.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// </remarks>
	public class ClientPersistentPropertiesManagerControl : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		/// <summary>
		///	Adds a persistent property to a control.
		/// </summary>
		/// <remarks>
		///	This method should be called during the Init stage.
		/// </remarks>
		/// <exception cref="System.ArgumentException">
		///	Thrown if the property type specified in ClientPersistentPropertyEnum doesn't exist.
		/// </exception>
		/// <param name=propertyType>A persistent property enum type specified in ClientPersistentPropertyEnum.</param>
		public void Add(ClientPersistentPropertyEnum propertyType) 
		{
			ClientPersistentProperty property;

			try 
			{
				property = (ClientPersistentProperty) System.Activator.CreateInstance(null, "QSP.WebControl." + propertyType.ToString(), false, BindingFlags.Default, null, new object[] {this.Parent}, null, null, null).Unwrap();
						
				if(property != null) 
				{
					property.ID = "ClientPersistentProperty" + Controls.Count.ToString();

					Controls.Add(property);
				} 
				else 
				{
					throw new Exception();
				}
			} 
			catch
			{
				throw new ArgumentException("Client persistent property type [ " + propertyType.ToString() + " ] does not exist. Please check ClientPersistentPropertyEnum.cs.");
			}
		}
	}
}