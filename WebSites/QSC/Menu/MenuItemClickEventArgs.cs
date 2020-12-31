using System;

namespace skmMenu
{
	/// <summary>
	/// Provides the EventArgs class for the MenuItemClick event.
	/// This EventArgs provides a single string parameter: CommandName
	/// </summary>
	public class MenuItemClickEventArgs : EventArgs
	{
		private string commandName;

		/// <summary>
		/// Describes which menuitem was clicked by returning the command name property.
		/// </summary>
		public MenuItemClickEventArgs(string name)
		{
			commandName = name;
		}

		/// <summary>
		/// Readonly access to commandName parameter of EventArgs class
		/// </summary>
		public string CommandName
		{
			get
			{
				return commandName;
			}
		}
	}
}
