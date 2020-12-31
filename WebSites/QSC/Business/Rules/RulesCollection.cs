using System;
using System.Collections;
using System.ComponentModel;

namespace Business.Rules
{
	/// <summary>
	/// Summary description for RulesCollection.
	/// </summary>
	public class RulesCollection : ICollection, IEnumerable
	{
		#region Fields

		private ArrayList list = null;
		private bool synchronized = false; 

		#endregion

		public RulesCollection()
		{
			list = new ArrayList();
		}

		/// <summary>
		/// Gets the Rule from the collection at the specified index.
		/// </summary>
		public virtual RulesBase this[int index]
		{
			get
			{
				return (RulesBase) list[index];
			}
		}

		/// <summary>
		/// Gets a value indicating whether the RulseCollection is synchronized.
		/// </summary>
		[Browsable (false)]
		public bool IsSynchronized
		{
			get
			{
				return synchronized;
			}
		}

		/// <summary>
		/// Gets the total number of elements in a collection.
		/// </summary>
		[Browsable(false)]
		public int Count
		{
			get
			{
				return list.Count;
			}
		}

		/// <summary>
		/// Gets the items of the collection as a list.
		/// </summary>
		protected virtual ArrayList List 
		{
			get { return list; }
		}

		/// <summary>
		/// Gets an object that can be used to synchronize the collection.
		/// </summary>
		[Browsable (false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// Creates and adds the specified Rules object to the RulesCollection.
		/// </summary>
		/// <param name="rule">The Rules to add.</param>
		public void Add(RulesBase rule)
		{

			if (rule == null)
				throw new ArgumentNullException ("rule", "'rule' argument cannot be null.");

			/*if (Contains(column.ColumnName))
			{
				if (object.ReferenceEquals(this [column.ColumnName], column))
					throw new ArgumentException ("Column '" + column.ColumnName + "' already belongs to this DataTable.");
				else if (CaseSensitiveContains (column.ColumnName))
					throw new DuplicateNameException("A column named '" + column.ColumnName + "' already belongs to this DataTable.");
			}*/

			CollectionChangeEventArgs e = new CollectionChangeEventArgs(CollectionChangeAction.Add, this);

			list.Add(rule);

			OnCollectionChanged (e);
		}

		/// <summary>
		/// Copies the elements of the specified Rules array to the end of the collection.
		/// </summary>
		/// <param name="rules">The array of Rules objects to add to the collection.</param>
		public void AddRange(RulesBase[] rules)
		{
			foreach (RulesBase rule in rules)
			{
				Add(rule);
			}
			return;
		}

		/// <summary>
		/// Clears the collection of any columns.
		/// </summary>
		public void Clear()
		{
			CollectionChangeEventArgs e = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this);

			list.Clear();
			OnCollectionChanged(e);
			return;
		}

		/// <summary>
		/// Checks whether the collection contains the specified rule.
		/// </summary>
		/// <param name="rule">The Rules to check for.</param>
		/// <returns>true if this rule exists; otherwise, false.</returns>
		public bool Contains(RulesBase rule)
		{
			return list.Contains(rule);
		}

		/// <summary>
		/// Raises the OnCollectionChanged event.
		/// </summary>
		/// <param name="ccevent">A CollectionChangeEventArgs that contains the event data.</param>
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (CollectionChanged != null) 
			{
				CollectionChanged(this, ccevent);
			}
		}

		/// <summary>
		/// Raises the OnCollectionChanging event.
		/// </summary>
		/// <param name="ccevent">A CollectionChangeEventArgs that contains the event data.</param>
		protected internal virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (CollectionChanged != null) 
			{
				CollectionChanged(this, ccevent);
			}
		}

		/// <summary>
		/// Removes the specified Rules object from the collection.
		/// </summary>
		/// <param name="rule">The rule to remove.</param>
		public void Remove(RulesBase rule)
		{
			if (!list.Contains(rule))
				throw new ArgumentException ("Cannot remove a rule that doesn't belong to this collection.");

			CollectionChangeEventArgs e = new CollectionChangeEventArgs(CollectionChangeAction.Remove, this);
			
			list.Remove(rule);
			OnCollectionChanged(e);
		}

		/// <summary>
		/// Copies all the elements in the current InternalDataCollectionBase to a one-
		/// dimensional Array, starting at the specified InternalDataCollectionBase index.
		/// </summary>
		public void CopyTo(Array array, int index)
		{
			list.CopyTo(array, index);
		}

		/// <summary>
		/// Gets an IEnumerator for the collection.
		/// </summary>
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>
		/// Occurs when the columns collection changes, either by adding or removing a column.
		/// </summary>
		public event CollectionChangeEventHandler CollectionChanged;
	}
}
