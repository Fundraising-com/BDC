using System;

namespace GA.BDC.Core.Utilities.IO {

	/// <summary>
	/// 
	/// </summary>
	public enum ObjectType : short {
		File = 0,
		Folder = 1
	}

	/// <summary>
	/// 
	/// </summary>
	public enum ActionType : short {
		AddNew = 0,
		Replace = 1,
		Delete = 2,
		MoveTo = 3
	}
}
