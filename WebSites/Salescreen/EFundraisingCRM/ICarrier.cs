using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ICarrier.
	/// </summary>
	interface ICarrier
	{
		void Send();
		string TrackingNumber { get; }
		int CarrierId { get; }
		string Description { get; }
	}
}
