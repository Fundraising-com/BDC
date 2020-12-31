using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for OrderQualifier.
	/// </summary>
	[Serializable]
	public enum OrderQualifier 
	{
		None = 0,
		Main = 39001,
		Supplement = 39002,
		Staff = 39003,
		Test = 39004,
		ProblemSolver = 39005,
		Kanata = 39006,
		FieldSupplies = 39007,
		CustomerService = 39008,
		Internet = 39009,
		GiftFix = 39010,
		InternetFix = 39011,
		OrderCorrection = 39012,
		CreditCardReprocess = 39013,
		CCReprocessCourtesy = 39014,
		CCReprocessedtoinvoice = 39015,
		FMGiftSample = 39016,
		WFCSigningBonus = 39017,
		KanataPSolver = 39018,
		GiftPSolver = 39019,
		FMBulkSupply = 39022,
        BookProblemSolver = 39023

}
}
