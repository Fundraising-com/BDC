using System;
using System.Data;

namespace QSPForm.Common
{
	/// <summary>
	/// Summary description for QSPFormMessage.
	/// This class i used to Manage all Message for exception, including
	/// the validation message that we need to provide to the presentation Layer
	/// </summary>
	public class QSPFormCommonType
	{
		
		public QSPFormCommonType()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
	}

	public class EntityType
	{
		public const int TYPE_ORGANIZATION = 1;
		public const int TYPE_ACCOUNT = 2;
		public const int TYPE_CAMPAIGN = 3;
		public const int TYPE_ORDER_BILLING = 4;
		public const int TYPE_ORDER_SHIPPING = 5;		
		public const int TYPE_CREDIT_APPLICATION = 6;		
		public const int TYPE_CREDIT_CARD = 7;
		public const int TYPE_WAREHOUSE = 8;
		public const int TYPE_VENDOR = 9;
        public const int TYPE_PROGRAM_AGREEMENT = 12;
		public const int TYPE_PROMO_COUPON = 64;
	}

	public class PostalAddressType
	{
		public const int TYPE_BILLING = 1;
		public const int TYPE_SHIPPING = 2;
	}

    public class PaymentAssignmentType
    {
        public const int NONE = 0;
        public const int PAY_BY_FSM = 1;
        public const int PAY_BY_QSP = 2;
    }

	public class PhoneNumberType
	{
		/*
		 * 	1	Corporative
			2	Personal or Home Phone Number
			3	Cell Phone
			4	General Fax
			5	Billing Phone Number
			6	Billing Fax
			7	Shipping Phone Numer
			8	Shipping Fax
			9   Fax Number
			10  Receiving Phone Number
		 * */
		public const int TYPE_PHONE_NUMBER = 1;
		public const int TYPE_HOME_PHONE_NUMBER = 2;
		public const int TYPE_CELL_PHONE = 3;
		public const int TYPE_PAGE = 4;
		public const int TYPE_BILLING_PHONE = 5;
		public const int TYPE_BILLING_FAX = 6;
		public const int TYPE_SHIPPING_PHONE = 7;
		public const int TYPE_SHIPPING_FAX = 8;
		public const int TYPE_FAX_NUMBER = 9;
		public const int TYPE_RECEIVING_PHONE_NUMBER = 10;
	}

	public class EmailType
	{
		/* 
			1	Corporative
			2	Corporative 1
			3	Personal
			4	Billing Corporative
			5	Shipping Corporative
		* */
		public const int TYPE_CORPORATIVE = 1;
		public const int TYPE_CORPORATIVE_1 = 2;
		public const int TYPE_PERSONAL = 3;
		public const int TYPE_BILLING = 4;
		public const int TYPE_SHIPPING = 5;
	}

	public class AccountStatus
	{		
		public const int INCOMPLETE = 0;
        public const int CLOSE_NOT_SUBMITTED = 9;
		public const int IN_PROCESS = 101;
        public const int CLOSE_IN_PROCESS = 109;
		public const int EXPORTED = 201;
		public const int PROCESSED = 301;
		public const int PROCESSED_NEW = 302;
        public const int CLOSE_PROCESSED = 309;
        public const int IN_COLLECTION_PROCESSED = 390;
        public const int CLOSE = 709;
        public const int IN_COLLECTION = 790;
		public const int ERROR_GENERAL = 901;	
		public const int ERROR_UNSPECIFIED = 9001;	
		public const int ERROR_SYNCHRONIZATION = 9901;
	}

    public enum ProgramAgreementStatus
    {
        Incomplete                      = 0,
        SavedForLater                   = 1,
        WaitForApproval                 = 5,
        Cancelled                       = 9,
        Test                            = 70,
        InProcess                       = 101,
        InProcessToBeCancelled          = 109,
        Exported                        = 201,
        ExportedForCancelled            = 209,
        Processed                       = 301,
        ProcessedNew                    = 302,
        CancelProcessed                 = 309,
        InvalidFSMNumber                = 9006,
        InvalidAccountNumber            = 9013,
        InvalidTaxExemptCode            = 9119,
        InvalidPA                       = 9214,
        InvalidStartDates               = 9215,
        InvalidEnrollment               = 9216,
        InvalidEstimatedNetSales        = 9217,
        InvalidBrochure                 = 9218,
        InvalidAccountProfit            = 9219,
        InvalidRenewalCode              = 9220,
        InvalidHolidayDates             = 9223,
        InvalidOLRCCDCode               = 9229,
        RecordNotFoundForUpdate         = 9302,
        RecordNumberNotFoundForUpdate   = 9304,
        InvalidEmailAddress             = 9306,
        InvalidAddress                  = 9400,
        AddressIsPOBoxNumber            = 9408,
        InvalidPhoneNumber              = 9409,
        InvalidMagnetBookletCode        = 9449
    }

	public class WarehouseStatus
	{		
		public const int INCOMPLETE = 0;
		public const int CLOSED = 9;
		public const int IN_PROCESS = 101;
		public const int IN_PROCESS_TO_BE_CLOSED = 109;
		public const int EXPORTED = 201;
		public const int EXPORTED_CLOSED = 209;
		public const int PROCESSED = 301;
		public const int PROCESSED_NEW = 302;
		public const int PROCESSED_CLOSED = 309;
	}

	public class OrderStatus
	{		
		//--STATUS RANGE 0 to 99
		//When status is below than 100 
		//the record has never been processed to the AS400
		public const int INCOMPLETE = 0;
		public const int SAVED_FOR_LATER = 1;		
		public const int WAIT_FOR_APPROVAL = 5;
		public const int CANCELLED = 9;
		public const int WAIT_FOR_PERSONALIZATION = 30;
		public const int VOID = 70;
		//--STATUS RANGE 100 to 199
		//When status is between 100 and 199
		//the record are in prosition to be synch with the AS400
		public const int IN_PROCESS = 101;
		public const int IN_PROCESS_CANCELLED = 109;
		public const int IN_PROCESS_VOID = 170;
		//--STATUS RANGE 200 to 299
		//When status is between 200 and 299
		//the record has been exported to the AS400 but not treated yet
		public const int EXPORTED = 201;
		//--STATUS RANGE 300 to 399
		//When status is between 300 and 399
		//the record has been processed to the AS400 
		public const int PROCESSED = 301;
		public const int PROCESSED_NEW = 302;
		public const int PROCESSED_CANCELLED = 309;
		public const int PROCESSED_VOID = 370;
		//--STATUS RANGE is between 400 and 899
		//the record is no longer updatable 
		public const int RELEASED = 401;
		public const int SHIPPED = 501;
		public const int INVOICED = 601;
		public const int COMPLETED = 701;
		public const int RETURNED = 801;
		//--STATUS RANGE is over or equal to 9000
		//the record is in Error state and need to be fix
		//public const int ERROR_GENERAL = 9001;	
		public const int ERROR_UNSPECIFIED = 9001;	
		public const int ERROR_CONCURENT_MODIFICATION = 9204;
		//public const int ERROR_CONCURENT_MODIFICATION = 9801;
		public const int ERROR_ALREADY_RELEASED = 9802;
		public const int ERROR_WAITING_ROLLBACK = 9811;
		public const int ERROR_SYNCHRONIZATION = 9901;
		public const int ERROR_RANGE_END = 9999;	
	}

    public class OrderSource
    {
        public const int ORDER_EXPRESS = 1;
        public const int EFR = 20;
    }

	public class DeliveryMethod
	{
		public const int COMMON_CARRIER = 1;		
		public const int PICK_UP_AT_WAREHOUSE = 2;
		
	}

    public class WarehouseType
    {
        public const int CHRobinson = 1;
    }

	public class TaskType
	{
		public const int SEND_NOTIFICATION = 1;		
		public const int MANAGE_TODO = 2;
		public const int EXECUTE_SQL = 3;		
	}

    public class OrderType
    {
        public const int STANDARD = 1;
        public const int PER_SALE_ESTIMATE = 2;
        public const int SUPPLY = 3;
    }

	public class DocumentType
	{
		public const int TAX_EXEMPTION = 1;
		public const int CREDIT_APPLICATION = 2;
		public const int COUPON_AGREEMENT = 3;				
	}

	public class DataOperation
	{
		public const int INSERT = 1;		
		public const int UPDATE = 2;
		public const int DELETE = 3;
	}

	public class BusinessForm
	{
		/*
		id	form_code	form_name
		------------------------------------------------------
		1	WF1017	WFC Warehouse Stock Order Form
		2	WF1008	WFC Deluxe Asssortment Order Form
		3	FOOD-1	Cookie Dough
		4	GE1010	Credit Application Form
		5	ACC001	Account Form
				 
		  
		  */
		//		public const int WFC_WAREHOUSE_STOCK_ORDER = 1;		
		//		public const int WFC_DELUXE_ASSORTMENT = 2;
		//		public const int COOKIE_DOUGH = 3;
		//		public const int CREDIT_APPLICATION = 4;
		//		public const int ACCOUNT = 5;
		public const int BASE_ACCOUNT_FORM = 6;
		public const int BASE_CREDIT_APP_FORM = 7;
		public const int BASE_ORDER_FORM = 8;
	}

	public class BizTask_AssignmentType
	{
		public const int SPECIFIC_USER = 1;
		public const int CURRENT_USER = 2;
		public const int SPECIFIC_ROLE = 3;
		public const int CURRENT_FSM = 4;
	}

	public class BizNotificationType
	{
		public const int GENERAL_NOTIFICATION = 1;
		public const int TODO = 2;
		public const int SYNCH_SYSTEM_ERROR = 3;
        public const int SUPPLY_IMPORT_ERROR = 4;
        public const int SYNCH_VALIDATION_ERROR = 5;
	}

	public enum BusinessExceptionType: int
	{
		Note = 100, //This is for Warning
		TaxExemptionForm = 101, //This is for Warning
        TaxExemption = 104,  //This is to inform that the account is considered tax exempted
		Freight_Charges = 102, //This is for Warning - the freight charges is added but doesn't stop the process
		Expedited_Freight_Charges = 103, //The Expedited freight charges  is added but doesn't stop the process
		Approved_Exception = 200, //And exception that can be by pass by approval		
		Standard_Exception = 300, //And exception that cannot be by pass by approval
		CreditApplication = 301,
		Mandatory = 900 //Stop the save of the form, there is no exception
	};

	public class InfoStatus
	{
		public const int ERROR = -1;
		public const int NONE = 0;
		public const int SUCCESSFUL = 1;
	}

    public class FormPropertyName
	{
		public const string ACCOUNT_HISTORY_INTERVAL_NB_DAY = "account_history_interval_nb_day";
        public const string ACCOUNT_HISTORY_MIN_TOTAL_AMOUNT = "account_history_min_total_amount";
        public const string COMMON_CARRIER_NAME = "common_carrier_name";

        public const String MIN_LINE_ITEM_QUANTITY = "min_line_item_quantity";
        public const String MIN_TOTAL_QUANTITY = "min_total_quantity";
        public const String MAX_TOTAL_QUANTITY = "max_total_quantity";
        public const String MIN_TOTAL_AMOUNT = "min_total_amount";
        public const String MAX_TOTAL_AMOUNT = "max_total_amount";
        public const String MIN_NB_DAY_LEAD_TIME = "min_nb_day_lead_time";
        
	}

    public class FormProperty
    {
        public const int ACCOUNT_HISTORY_INTERVAL_NB_DAY = 29;
        public const int ACCOUNT_HISTORY_INTERVAL_MIN_TOTAL_AMOUNT = 105;
        
        public const int COMMON_CARRIER_NAME = 38;


        
        public const int MIN_LINE_ITEM_QUANTITY = 106;
        public const int MIN_TOTAL_QUANTITY = 49;
        public const int MAX_TOTAL_QUANTITY = 107;
        public const int MIN_TOTAL_AMOUNT = 45;
        public const int MAX_TOTAL_AMOUNT = 108;
        public const int MIN_NB_DAY_LEAD_TIME = 35;

    }

    public class FormSectionType
    {
        public const int STANDARD_PRODUCT = 1;
        public const int SUPPLY_PRODUCT = 2;
        public const int OTHER_PRODUCT = 3;
    }

    public class FormSectionNumber
    {
        public const int SECTION_1 = 1;
        public const int SECTION_2 = 2;
        public const int SECTION_3 = 3;
    }
	
	
}
