using System;
using System.Collections;

namespace Business.Objects.RemitTests
{
	/// <summary>
	/// Summary description for RemitTestCollection.
	/// </summary>
	public class RemitTestCollection:CollectionBase
	{
		public RemitTestCollection()
		{
		}

		public virtual void Add(RemitTest remitTest)
		{
			this.List.Add(remitTest);
		}

		public virtual RemitTest this[int Index]
		{
			get
			{
				return (RemitTest) this.List[Index];
			}
		}
	}
}
