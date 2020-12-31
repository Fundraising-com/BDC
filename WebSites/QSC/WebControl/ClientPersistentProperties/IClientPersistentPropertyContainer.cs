using System;

namespace QSP.WebControl.ClientPersistentProperties
{
	/// <summary>
	/// Defines the contract that a control will manage client persistent properties.
	/// </summary>
	/// <remarks>
	/// Author(s):		Benoit Nadon, Éric Charest
	/// Last Update:	2005/08/04
	/// 
	/// Implement in a web server control, add the manager to the Controls collection
	/// in the OnInit method and render it in the Render method.
	/// </remarks>
	public interface IClientPersistentPropertyContainer
	{
		ClientPersistentPropertiesManagerControl ClientPersistentProperties 
		{
			get;
		}
	}
}
