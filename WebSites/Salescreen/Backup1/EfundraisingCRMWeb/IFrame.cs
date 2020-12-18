using System;

namespace EFundraisingCRMWeb
{
	public enum FrameModel {
		Normal
	}

	/// <summary>
	/// Summary description for IFrame.
	/// </summary>
	interface IFrame {
		string Title { get; }
		FrameModel Model { get; }
	}
}
