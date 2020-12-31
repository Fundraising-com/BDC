using System;

namespace Business
{
	/// <summary>
	/// Class:  FulfillmentIterator
	/// Description:  Abstract class that specs out order fulfillment processes
	/// </summary>
	public abstract class FulfillmentIterator :  QBusinessObject
	{
		public FulfillmentIterator()
		{

		}
		abstract public bool BeginProcess();
		abstract public bool GetBatchesReadyForFulfillment();
		abstract public bool ProcessBatches();
		abstract public bool EndProcess();

	}
}
