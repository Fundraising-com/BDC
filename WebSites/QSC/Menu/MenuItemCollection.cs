using System;
using System.Web;
using System.ComponentModel;
using System.Collections;
using System.Web.UI;

namespace skmMenu
{
	/// <summary>
	/// MenuItemCollection represents a collection of <see cref="MenuItem"/> instances.
	/// </summary>
	/// <remarks>Each item in a menu is represented by an instance of the <see cref="MenuItem"/> class.
	/// The MenuItem class has a <see cref="SubItems"/> property, which is of type MenuItemCollection.  This
	/// MenuItemCollection, then, allows for each MenuItem to have a submenu of MenuItems.<p />This flexible
	/// object model allows for an unlimited number of submenu depths.</remarks>
	public class MenuItemCollection : ICollection, IStateManager
	{
		#region Private Member Variables
		// private member variables
		private ArrayList menuItems = new ArrayList();
		private bool isTrackingViewState = false;
		#endregion

		#region ICollection Implementation
		/// <summary>
		/// Adds a MenuItem to the collection.  If the ViewState is being tracked, the
		/// MenuItem's TrackViewState() method is called and the item is set to dirty, so
		/// that we don't lose any settings made prior to the Add() call.
		/// </summary>
		/// <param name="item">The MenuItem to add to the collection</param>
		/// <returns>The ordinal position of the added item.</returns>
		public virtual int Add(MenuItem item)
		{
			int result = menuItems.Add(item);

			return result;
		}


		/// <summary>
		/// Adds a spacer to the collection.  
		/// </summary>
		/// <param name="itemHeight">The height of the spacer to add to the collection</param>
		/// <returns>The ordinal position of the added item.</returns>
		public virtual int AddSpacer(int itemHeight)
		{
			// A spacer is really just a menuitem which has blank text and image properties.
			int result = menuItems.Add(new MenuItem(" "));

			MenuItem added = (MenuItem) menuItems[result];

			added.Height = itemHeight;
			added.Text = "";
			added.MenuType = MenuItemType.MenuSeparator;

			return result;
		}


		/// <summary>
		/// Adds a spacer to the collection.  
		/// </summary>
		/// <param name="itemHeight">The height of the spacer to add to the collection</param>
		/// <param name="itemCssClass">The CssClass for the spacer to add to the collection</param>
		/// <returns>The ordinal position of the added item.</returns>
		public virtual int AddSpacer(int itemHeight, string itemCssClass)
		{
			// A spacer is really just a menuitem which has (usually) blank text and image properties.
			int result = menuItems.Add(new MenuItem(" "));

			MenuItem added = (MenuItem) menuItems[result];

			added.Height = itemHeight;
			added.CssClass = itemCssClass;
			added.Text = "";
			added.MenuType = MenuItemType.MenuSeparator;

			return result;
		}


		/// <summary>
		/// Adds a spacer to the collection.
		/// </summary>
		/// <param name="itemHeight">The height of the spacer to add to the collection</param>
		/// <param name="itemCssClass">The CssClass for the spacer to add to the collection</param>
		/// /// <param name="itemText">The Text for the spacer to add to the collection</param>
		/// <returns>The ordinal position of the added item.</returns>
		public virtual int AddSpacer(int itemHeight, string itemCssClass, string itemText)
		{
			// A spacer is really just a menuitem which has blank text and image properties.
			int result = menuItems.Add(new MenuItem(itemText));

			MenuItem added = (MenuItem) menuItems[result];

			added.Height = itemHeight;
			added.CssClass = itemCssClass;
			added.MenuType = MenuItemType.MenuSeparator;

			return result;
		}
		

		/// <summary>
		/// Adds a header to the collection.  
		/// </summary>
		/// <param name="itemText">The text for the header to add to the collection</param>
		/// <returns>The ordinal position of the added item.</returns>
		public virtual int AddHeader(string itemText)
		{
			// A header is really just a menuitem which has only a text property.
			int result = menuItems.Add(new MenuItem(itemText));

			MenuItem added = (MenuItem) menuItems[result];

			added.MenuType = MenuItemType.MenuHeader;

			return result;
		}


		/// <summary>
		/// Adds the MenuItems in a MenuItemCollection.
		/// </summary>
		/// <param name="items">The MenuItemCollection instance whose MenuItems to add.</param>
		public virtual void AddRange(MenuItemCollection items)
		{
			menuItems.AddRange(items);
		}

		/// <summary>
		/// Clears out the entire MenuItemCollection.
		/// </summary>
		public virtual void Clear()
		{
			menuItems.Clear();
		}

		/// <summary>
		/// Determines if a particular MenuItem exists within the collection.
		/// </summary>
		/// <param name="item">The MenuItem instance to check for.</param>
		/// <returns>A Boolean - true if the MenuItem is in the collection, false otherwise.</returns>
		public virtual bool Contains(MenuItem item)
		{
			return menuItems.Contains(item);
		}

		/// <summary>
		/// Returns the ordinal index of a MenuItem, if it exists; if the item does not exist,
		/// -1 is returned.
		/// </summary>
		/// <param name="item">The MenuItem to search for.</param>
		/// <returns>The ordinal position of the item in the collection.</returns>
		public virtual int IndexOf(MenuItem item)
		{
			return menuItems.IndexOf(item);
		}

		/// <summary>
		/// Inserts a MenuItem instance at a particular location in the collection.
		/// </summary>
		/// <param name="index">The ordinal location to insert the item.</param>
		/// <param name="item">The MenuItem to insert.</param>
		public virtual void Insert(int index, MenuItem item)
		{
			menuItems.Insert(index, item);
		}

		/// <summary>
		/// Removes a specified MenuItem from the collection.
		/// </summary>
		/// <param name="item">The MenuItem instance to remove.</param>
		public void Remove(MenuItem item)
		{
			menuItems.Remove(item);
		}

		/// <summary>
		/// Removes a MenuItem from a particular ordinal position in the collection.
		/// </summary>
		/// <param name="index">The ordinal position of the MenuItem to remove.</param>
		public void RemoveAt(int index)
		{
			menuItems.RemoveAt(index);
		}

		/// <summary>
		/// Copies the contents of the MenuItem to an array.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(Array array, int index)
		{
			menuItems.CopyTo(array, index);
		}

		/// <summary>
		/// Gets an Enumerator for enumerating through the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return menuItems.GetEnumerator();
		}
		#endregion
        
		#region IStateManager Interface
		/// <summary>
		/// Indicates that changes to the view state should be tracked.  Calls TrackViewState()
		/// for each MenuItem in the collection.
		/// </summary>
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewState = true;
			foreach(MenuItem item in this.menuItems)
				((IStateManager) item).TrackViewState();
		}

		/// <summary>
		/// Saves the view state in an object array.  Each item in the collection has its
		/// SaveViewState() method called.  This array is then returned, representing the
		/// state of the MenuItemCollection.
		/// </summary>
		/// <returns>An object array.</returns>
		object IStateManager.SaveViewState()
		{
			bool isAllNulls = true;
			object [] state = new object[this.menuItems.Count];
			for (int i = 0; i < this.menuItems.Count; i++)
			{
				// Save each item's viewstate...
				state[i] = ((IStateManager) this.menuItems[i]).SaveViewState();
				if (state[i] != null)
					isAllNulls = false;
			}

			// If all items returned null, simply return a null rather than the object array
			if (isAllNulls)
				return null;
			else
				return state;
		}

		/// <summary>
		/// Iterate through the object array passed in.  For each element in the object array
		/// passed-in, a new MenuItem instance is created, added to the collection, and populated
		/// by calling LoadViewState().
		/// </summary>
		/// <param name="savedState">The object array returned by the SaveViewState() method in
		/// the previous page visit.</param>
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object [] state = (object[]) savedState;

				// Create an ArrayList of the precise size
				menuItems = new ArrayList(state.Length);

				for (int i = 0; i < state.Length; i++)
				{
					MenuItem mi = new MenuItem();		// create MenuItem
					((IStateManager) mi).TrackViewState();	// Indicate that it needs to track its view state

					// Add the MenuItem to the collection
					menuItems.Add(mi);

					if (state[i] != null)
					{
						// Load its state via LoadViewState()
						((IStateManager) menuItems[i]).LoadViewState(state[i]);
					}
				}
			}
		}
		#endregion

		#region MenuItemCollection Properties
		/// <summary>
		/// Returns the number of elements in the MenuItemCollection.
		/// </summary>
		/// <value>The actual number of elements contained in the <see cref="MenuItemCollection"/>.</value>
		[ Browsable(false) ]
		public virtual int Count
		{
			get
			{
				return menuItems.Count;
			}
		}


		/// <summary>
		/// Gets a value indicating whether access to the <see cref="MenuItemCollection"/> is synchronized (thread-safe).
		/// </summary>
		[ Browsable(false) ]
		public virtual bool IsSynchronized
		{
			get
			{
				return menuItems.IsSynchronized;
			}
		}


		/// <summary>
		/// Gets an object that can be used to synchrnoize access to the <see cref="MenuItemCollection"/>.
		/// </summary>
		[ Browsable(false) ]
		public virtual object SyncRoot
		{
			get
			{
				return menuItems.SyncRoot;
			}
		}


		/// <summary>
		/// Gets the <see cref="MenuItem"/> at a specified ordinal index.
		/// </summary>
		/// <remarks>Allows read-only access to the <see cref="MenuItemCollection"/>'s elements by index.
		/// For example, myMenuCollection[4] would return the fifth <see cref="MenuItem"/> instance.</remarks>
		public virtual MenuItem this[int index]
		{
			get
			{
				return (MenuItem) menuItems[index];
			}
		}


		/// <summary>
		/// Gets the <see cref="MenuItem"/> with a specified <see cref="Name"/>.
		/// </summary>
		/// <remarks>The <see cref="MenuItem"/> class has a <see cref="Name"/> property that allows for items
		/// to be indexed by name.<p />For example, myMenuCollection["Contact"] would return the 
		/// <see cref="MenuItem"/> instance with the <see cref="Name"/> "Contact", or <b>null</b> if no such
		/// MenuItem existed in the MenuItemCollection.</remarks>
		public virtual MenuItem this[string name]
		{
			get
			{
				foreach(MenuItem item in menuItems)
				{
					if (item.Name == name)
						return item;
				}
				return null;
			}
		}


		/// <summary>
		/// A required property since MenuItemCollection implements IStateManager.  This
		/// property simply indicates if the MenuItemCollection is tracking its view state or not.
		/// </summary>
		bool IStateManager.IsTrackingViewState
		{
			get 
			{ 
				return this.isTrackingViewState; 
			}
		}
		#endregion
	}
}
