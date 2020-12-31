using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace skmMenu
{
	/// <summary>
	/// Provides a collection of roles.
	/// </summary>
	public class RoleCollection : IStateManager
	{
		// private member variables
		StringDictionary roles = new StringDictionary();
		bool isTrackingViewState = false;

		#region IStateManager Interface
		/// <summary>
		/// Indicates that changes to the view state should be tracked.
		/// </summary>
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewState = true;
		}

		/// <summary>
		/// Returns an array of objects, where each object is a string in the collection.
		/// </summary>
		/// <returns>An object array.</returns>
		object IStateManager.SaveViewState()
		{
			if (roles.Count == 0 || this.isTrackingViewState == false)
				return null;

			object [] state = new object[roles.Keys.Count];			
			roles.Keys.CopyTo(state, 0);

			return state;
		}

		/// <summary>
		/// Iterate through the object array passed in.  For each element in the object array
		/// passed-in, a new role is added to the collection.
		/// </summary>
		/// <param name="savedState">The object array returned by the SaveViewState() method in
		/// the previous page visit.</param>
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object [] state = (object[]) savedState;

				roles.Clear();
				
				for (int i = 0; i < state.Length; i++)
					roles.Add((string) state[i], String.Empty);
			}
		}
		#endregion

		#region Collection-Related Methods
		/// <summary>
		/// Adds a new role to the collection.
		/// </summary>
		/// <param name="role">The name of the role to add.</param>
		public virtual void Add(string role)
		{
			roles.Add(role, String.Empty);
		}

		/// <summary>
		/// Adds a range of roles to the collection.
		/// </summary>
		/// <param name="roles">A string array of roles to add.</param>
		public virtual void AddRange(string [] roles)
		{
			for (int i = 0; i < roles.Length; i++)
				this.Add(roles[i]);
		}

		/// <summary>
		/// Removes a role from the collection.
		/// </summary>
		/// <param name="role">The name of the role to remove.</param>
		public virtual void Remove(string role)
		{
			roles.Remove(role);
		}

		/// <summary>
		/// Clears out the roles collection.
		/// </summary>
		public virtual void Clear()
		{
			roles.Clear();
		}

		/// <summary>
		/// Returns a Boolean indicating if the passed-in role name exists in the role collection.
		/// </summary>
		/// <param name="role">A role.</param>
		/// <returns>Returns <b>true</b> if <b>role</b> is contained in the collection, <b>false</b> if it is not.</returns>
		public virtual bool Contains(string role)
		{
			return roles.ContainsKey(role);
		}

		/// <summary>
		/// Returns true if this role collection and the passed in role collection are disjoint (share no elements in
		/// common).
		/// </summary>
		/// <param name="roles">A role collection.</param>
		/// <returns><b>true</b> if the collections are disjoint, <b>false</b> otherwise.</returns>
		public virtual bool Disjoint(RoleCollection roles)
		{
			foreach(string role in this.roles.Keys)
				if (roles.Contains(role))
					return false;

			// no common roles, return true
			return true;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Returns the number of roles in the collection.
		/// </summary>
		/// <value>An integer value greater than or equal to zero indicating the number of roles in the role collection.</value>
		public virtual int Count
		{
			get
			{
				return roles.Count;
			}
		}

		/// <summary>
		/// Returns the list of roles as a string array.
		/// </summary>
		/// <remarks>The RoleCollection class only contains methods to add, remove, and check to see if a role exists in
		/// the collection.  It does not provide a means to enumerate through the roles.  In order to enumerate through
		/// the roles, use this property to retrieve the roles as a string array.</remarks>
		public virtual string [] Roles
		{
			get
			{
				string [] rArray = new string[roles.Keys.Count];
				this.roles.Keys.CopyTo(rArray, 0);
				return rArray;
			}
		}

		/// <summary>
		/// Specifies if the RoleCollection is tracking ViewState.  Required, since RoleCollection
		/// implements the IStateManager interface.
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
