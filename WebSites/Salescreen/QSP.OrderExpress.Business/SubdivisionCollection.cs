using System.Collections;
using System;

namespace QSPForm.Business
{
	[Serializable]
	public class SubdivisionCollection : CollectionBase 
	{
		public void Add(Subdivision subdivision) 
		{
			this.List.Add(subdivision);
		}

		public void Remove(Subdivision subdivision) 
		{
			this.List.Remove(subdivision);
		}

		public Subdivision this[int index] 
		{
			get 
			{
				return (Subdivision) this.List[index];
			}
		}
		/// <summary>
		/// Return the first Subdivision with that key as SubdivisionCode
		/// </summary>
		public Subdivision this[string key]
		{
			get
			{
				Subdivision n = null;
				for(int i=0; i < this.Count;i++)
				{
					if( ((Subdivision)this[i]).SubdivisionCode.ToString() == key)
					{
						n = this[i];
						break;
					}
				}
				return n;
			}
		}
	}
}
