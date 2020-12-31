/**
**   Enumerations
*/


namespace Business
{
	public enum CodeHeader
	{
		ProgramMasterSubType=30300,
		ProgramMasterStatus=30400,
		PhoneType=30500,
		MagazineStatus=30600,
		MagazineCategory=30700,
		GiftCategory=30800,
		FulfillmentType=30900,
		SectionType=31000,          //ProgramSection
		InterfaceMedia=32000,
		InterfaceLayout=33000,
		FulfillmentHouseStatus=34000,
		AccountStatus=35000,
		ProgramType = 36000,
		CampaignStatus=37000,
		SalesRegion=38000,
		OrderQualifier=39000,
		RegularBatchStatus=40000,
		BatchType=41000,
		Remit=42000,                 //Suite of ids for remitting
		FSApplicability=43000,
		FSDistributionLevel=44000,    // Extra data is in gross level field.  Column "ADPCode" is an alias to "Extra_Limit_Rate"
		OverrideID = 45000,
		CODProductType = 46000,
		BankDepositStatus = 55000,
		AccountType = 48000,
		AdjustmentType = 49000,
		PaymentMethod = 50000,
		MisCharges= 50500,
		CustomerType=50600
	};

	public enum ProgramMasterSubType
	{
		Fundraising=30301,
		Incentive,
		Rewards,
		Promotional
	};

	public enum ProgramMasterStatus
	{
		Inactive=30401	,
		Pending,
		Approved,
		InUse
	};

	public enum PhoneType
	{
		Undefined = 30500,
		Work = 30501,
		Home = 30502,
		Fax = 30503,
		Other = 30504,
		Main = 30505,
		Pager = 30506,
		Mobile = 30507,
		OfficeFax = 30508,
		Mailbox = 30509,
		TollFreeLine = 30510,
		SpeedDial = 30511,
		CustomerService = 30512,
		GiftDept = 30513,
		MagazineDept = 30514,
		PrizeDivision = 30515
	};

	public enum CampaignStatus
	{
		PendingIncomplete = 37001,
		Approved,
		PendingFS,
		OnHold,
		Cancel,
		Inactive,
		OrderLogged
	};

	public enum MagazineTitleStatus
	{
		Active=30600,
		Inactive,
		Pending
	};

	public enum BatchType
	{
		CA=		41001,
		CAFS,
		CREDITCM,
		DEBITCM,
		EMP,
		FM,
		FMBULK,
		GROUP,
		MAGNET,
		POS
	};
	
	public enum BatchStatus
	{
		New = 40001,
		InProcess,
		UnderReview,
		Approved,
		Cancelled,
		CreditCardInProcess,
		Housekeeping1,
		Housekeeping2,
		Pickable,
		AtWarehouse,
		Shipped,
		SentToTPL,
		Fulfilled
	};

	/*
	public enum CustomerOrderStatus
	{
		New = 31000,
		UnderReview,
		InProcess,
		CreditCardInProcess,
		Approved,
		SentToRemit,
		Shipped,
		Cancelled
	};
	*/

	public enum BillIncentivesTo
	{
		Split	= 51001,
		FM		= 51002,
		Account = 51003,
		QSP		= 51004
	};

	public enum CArenewalStatus
	{
		undefined = 0,
		New = 1,
		Renewal = 2
	};

	public enum AccountClass
	{
		School = 1,
		Sports_Clubs_Affinities = 2,
		Non_School = 3
	};

	public enum AccountCode
	{
		Sc_Elementary = 1,
		Sc_High_School = 2,
		Sc_Junior_High_School = 3,
		Sc_Middle_School = 4,
		Sc_Cegep = 5,
		Sc_College = 6,
		Sc_University = 7,
		Sc_School_Board = 8,
		Sc_Adult = 9,
		Sc_Vocational = 10,
		Sc_Other = 11,
		Sc_Combined = 12,
		Sc_Pre_School = 13,
		Sp_Ice_Skating = 14,
		Sp_Hockey = 15,
		Sp_Bowling = 16,
		Sp_Soccer = 17,
		Sp_Baseball = 18,
		Sp_Volleyball = 19,
		Sp_Gymnastics = 20,
		Sp_Basketball = 21,
		Sp_Travel = 22,
		Sp_Music_Band = 23,
		Sp_Theater = 24,
		Sp_Athletics = 25,
		Sp_Dance = 26,
		Sp_Karaté = 27,
		Sp_Curling = 28,
		Sp_Equestrian = 29,
		Sp_Aqua_Swim = 30,
		NSc_Daycare = 31,
		NSc_Gym = 32,
		NSc_Scouts_Guides = 33,
		NSc_Company = 34,
		NSc_Church = 35,
		NSc_Lodge = 36,
		NSc_Other = 37
	};

	public enum ShipSuppliesTo
	{
		Undefined = 0,
		Account = 1,
		FM = 2,
		SupplyAddress = 3,
	};

	public enum BankDepositStatus
	{
		New = 55001,
		Approved
	};

	public enum AccountStatus
	{
		Active = 1,
		Pending = 2,
		Inactive = 3
	};

	public enum IncentivesDistribution
	{
		Undefined		= 0,
		Account			= 1,
		ClassRoomBoxes	= 2,
		ParticipantBag	= 3,
	};

	public enum AddressType
	{
		Undefined = 54000,
		ShipTo = 54001,
		BillTo = 54002,
		Secondary = 54003,
		Home = 54004,
		SupplyAddr = 54005
	};

	public enum CustomerType
	{
		Regular = 50601,
		FM,
		Account,
		Employee
	};

	public enum ProgramIDType
	{
		Magazine = 1,
		MagazineExpress = 2,
		Magnet = 3,
		Gift = 4,
		EasyAsPie = 5,
		PrizeZone = 6,
		ReachForTheStars = 7,
		HersheyChocolate = 8,
		DrawPrize = 9,
		ChocolateSymphony = 10,
		PlanetaryRewards = 11,
		KanataExtremeRewards = 12,
		MagazineCombo = 13,
		MagazineStaff = 14,
		CumulativeRewards = 15,
		ChartRewards = 16,
        LargeChartWithNumSubs=17
	};

	public enum enum_Report
	{
		Order_Control_Sheet = 1,
		Pick_List = 2,
		Confirm_Shipment_Report = 3,
		Book_CD_Video_Labels = 4,
		Participant_Listing = 5,
		Homeroom_Summary_Report_ = 6,
		Group_Summary_Report = 7,
		Magazine_Item_Summary_Report = 8,
		Problem_Solver_Report = 9,
		Classroom_Box_Labels = 10,
		Order_Entry_Followup_Report = 11,
		Price_Discrepancy_Report = 12,
		Packing_Slip = 13,
		Over_All_Sales_Report = 14,
		Sales_Commission_Report = 15,
		Sub_Bonus_Report = 16
	};

	public enum enum_ReportType
	{
		PickList = 1,
		Shipping = 2,
		PickList_Report_BHE_Labels = 3,
		PickList_Report_Classroom_Box_Labels = 4,
		Participant_Listing = 5,
		Homeroom_Summary_Report = 6,
		Group_Summary_Report = 7,
		Magazine_Item_Summary_Report = 8,
		Problem_Solver_Report = 9,
		Classroom_Box_Labels = 10,
		Order_Entry_Followup_Report = 11,
		Price_Discrepancy_Report = 12,
		Combined_Picklist_Report = 13,
		Packing_Slip = 14,
		Over_All_Sales_Report = 15,
		Sales_Commission_Report = 16,
		Sub_Bonus_Report = 17
	};
}