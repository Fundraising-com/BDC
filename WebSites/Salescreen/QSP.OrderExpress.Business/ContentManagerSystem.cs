using System;
using System.Collections.Generic;
using QSPForm.Common.DataDef;
using QSPForm.Data;
using System.Data;
using QSPForm.Common;
using QSP.Business.Fulfillment;

namespace QSPForm.Business
{
	
	public enum SectionItem: int
	{
		System = 1,
		Setup = 2,
		DataEntry = 3,
		Report = 4,
		Admin = 5,
	};
	
	public enum AppItem: int
	{
		Welcome = 0,
		Login = 1,
		Default = 2,

		OrganizationList = 3,
		OrganizationDetailInfo = 7,
		OrganizationDetail = 37,
		OrganizationSelector = 13,

		OrgForm_Step1 = 30,
		OrgForm_Step2 = 31,
		OrgForm_Step3 = 32,
		OrgForm_Step4 = 33,
		OrgForm_Step5 = 34,
		OrgForm_Step6 = 35,
		OrgForm_Step7 = 36,

		
		AccountSelector = 14,
		AccountList = 4,
		AccountDetailInfo = 8,	
		AccountDetail = 47,
        AccountStatusChangeList = 48,

		AccountForm_Step1 = 40,
		AccountForm_Step2 = 41,
		AccountForm_Step3 = 42,
		AccountForm_Step4 = 43,
		AccountForm_Step5 = 44,
		AccountForm_Step6 = 45,
		AccountForm_Step7 = 46,

        ProgramAgreementSelector = 29,
        ProgramAgreementList = 170,
        ProgramAgreementDetailInfo = 171,
        ProgramAgreementDetail = 172,
        ProgramAgreementStatusChangeList = 173,

        ProgramAgreementForm_Step1 = 174,
        ProgramAgreementForm_Step2 = 175,
        ProgramAgreementForm_Step3 = 176,
        ProgramAgreementForm_Step4 = 177,
        ProgramAgreementForm_Step5 = 178,
        ProgramAgreementForm_Step6 = 179,
        //ProgramForm_Step7 = 46,

		CampaignSelector = 12,
		CampaignList = 5,		
		CampaignDetail = 9,

		CampaignForm_Step1 = 60,
		CampaignForm_Step2 = 61,
		CampaignForm_Step3 = 62,
		CampaignForm_Step4 = 63,
		CampaignForm_Step5 = 64,
		CampaignForm_Step6 = 65,
		CampaignForm_Step7 = 66,
				
		MDRSchoolSelector = 11,
		MDR_Detail = 53,

		FMSelector = 10,
		FM_Detail = 54,
		
		OrderList = 15,
		OrderDetail = 23,
		OrderDetailInfo = 6,
		OrderAwardVisionDetail = 191,
        OrderStatusChangeList = 192,
		
		OrderForm_Step1 = 16,	//Select Account
		OrderForm_Step1_1 = 24,	//Renew Account
		OrderForm_Step2 = 17,	//Select biz Form
		OrderForm_Step3 = 18,	//Update Address Information
		OrderForm_Step4 = 19,	//Enter Product
		OrderForm_Step5 = 25,   //Enter Order Information
		OrderForm_Step6 = 20,	//Enter Supply
		OrderForm_Step7 = 21,	//Validate
		OrderForm_Step8 = 22,	//Confirmation
        OrderForm_Step7_1 = 26,	//Personalization

		Form_List = 50,
		Form_Detail = 51,	
		BusinessFieldList = 52,		
		BusinessForm_Step1 = 110,	//Select EntityType
		BusinessForm_Step2 = 111,	//General Information
		BusinessForm_Step3 = 112,	//Business Rule
		BusinessForm_Step4 = 113,	//Business Exception
		BusinessForm_Step5 = 114,	//Business Task
		BusinessForm_Step6 = 115,	//Confirmation
		
		ProductList = 70,
		ProductDetailInfo = 71,
		ProductDetail = 72,

		TemplateEmailDetail = 75,
		TemplateEmailDetailInfo = 76,
		TemplateEmailList = 77,
		Page_Unknown20 = 78,

		UserSelector = 79,
		UserList = 80,
		UserDetailInfo = 81,
		UserDetail = 82,
		
		CreditApplicationDetailInfo = 83,
		CreditApplicationDetail = 84,		

		NoteList = 85,
		NoteDetailInfo = 86,
		MyNoteList = 87,
		NoteDetail = 88,

		ReportPage = 55,
		RegistryList = 92,
		ExpressionBuilder = 93,
		Calendar = 94,
		BusinessCalendar = 95,
		SessionOff = 96,
		ErrorPage = 97,
		ComingSoon = 98,
		SignOut = 99,

		DocumentList =100,
		DocumentDetailInfo =101,
		DocumentDetail=102,

		WarehouseList =105,
		WarehouseDetailInfo =106,
		WarehouseDetail=107,
		WarehouseSelector=108,
		WarehouseMapInfo=109,

		CatalogList =120,
		CatalogDetailInfo =121,
		CatalogDetail=122,
		CatalogSelector=123,

		TaskList =130,
		TaskDetailInfo =131,
		TaskDetail=132,
		TaskSelector=133,

        ProgramList = 181,
        ProgramDetailInfo = 182,
        ProgramDetail = 183,
        //ProgramSelector = 184,

		PromoList = 134,
		PromoDetailInfo = 135,
		PromoDetail = 136,
		
		LogoList = 137,
		LogoDetailInfo = 138,
		LogoDetail = 139,
        LogoListForFM = 1024, //for fm

		Promo_LogoList = 140,
		Promo_LogoDetailInfo = 141,
		Promo_LogoDetail = 142,
		Promo_LogoSelector = 155,
		Promo_TextList = 143,
		Promo_TextDetailInfo = 144,
		Promo_TextDetail = 145,
		Promo_TextSelector = 156,
		Promo_CouponList = 146,
		Promo_CouponDetailInfo = 147,
		Promo_CouponDetail = 148,

        FavoriteList = 149,
        

		VendorList=150,
		VendorSelector=151,

        CouponStep_1 = 152,
        //CouponStep_2 = 148, //=promo coupon detail
        CouponStep_2 = 161,
        CouponStep_3 = 153,
        CouponStep_4 = 160, 

		//Reports
		Report_WarehouseStockOrderForm = 500,
		Report_OrderList = 501,

		SynchAccountList = 900,
		SynchOrderList =901,
		SynchWarehouseList=902,
		
	};
	
	
	/// <summary>
	/// Summary description for ContentManager.
	/// </summary>
	public class ContentManagerSystem : BusinessSystem
	{
		public ContentManagerSystem()
		{
			
		}
		
		public AppItemData SelectOne(int AppItem_ID)
		{
			AppItemData dataSet = new AppItemData();
			//
			// Get the user DataSet from the DataLayer
			//
			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();			
			dataSet = CM_DataAccess.SelectOne(AppItem_ID);						
		
			return dataSet;
			
		}

		public AppItemData SelectAllByNoAppItem(int NoAppItem)
		{
			AppItemData dataSet = new AppItemData();
			//
			// Get the user DataSet from the DataLayer
			//
			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllWNoAppItemLogic(NoAppItem);				
		
			return dataSet;
			
		}

		public AppItemData SelectAllMenuItem()
		{
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//				
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllMenuItem();				
			
			return dataSet;
			
		}

		public AppItemData SelectAllMenuItemByRole(int Role)
		{
			AppItemData dataSet = new AppItemData();
			//
			// Get the user DataSet from the DataLayer
			//			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllMenuItemWRole_IDLogic(Role);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllMenuItemByEntityTypeID(int EntityTypeID)
		{
			AppItemData dataSet = new AppItemData();
			//
			// Get the user DataSet from the DataLayer
			//			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllWEntityTypeIDLogic(EntityTypeID);				
			
			return dataSet;
			
		}

		public AppItemData SelectOneNoStep(int NoStep)
		{
			AppItemData dataSet = new AppItemData();
			//
			// Get the user DataSet from the DataLayer
			//
			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectOneWNoStepLogic(NoStep);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllContentByNoAppItem(int NoAppItem)
		{
			AppItemData dataSet = new AppItemData();
			
			//
			// Get the user DataSet from the DataLayer
			//
			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllWNoAppItemLogic(NoAppItem);
			AppItemData dts = SelectAllDetailItemsByNoAppItem(NoAppItem);				
			dataSet.Merge(dts);
			
			return dataSet;
			
		}

		

		public DataView SelectAllByDetailType(AppItemData dataSet, int DetailType)
		{
			DataView DV = new DataView();			
			//
			// Filter the DataView for the DetailType
			//			
			DV.Table = dataSet.AppItemDetail;
			DV.RowFilter = AppItemDetailTable.FLD_DETAIL_TYPE_ID + " = " + DetailType.ToString();
			
			return DV;
			
		}

		public AppItemData SelectOneFAQ(int FAQ_ID)
		{
			
			AppItemData dataSet = new AppItemData();		
			//
			// Get the user DataSet from the DataLayer
			//			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectOneFAQ(FAQ_ID);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllFAQItemByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//			
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllFAQWNoAppItemLogic(NoAppItem);				
			
			return dataSet;
			
		}

		
		public AppItemData SelectAllDetailItemsByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllDetailWNoAppItemLogic(NoAppItem);				
			
			return dataSet;
			
		}
		
		public AppItemData SelectAllSortItemsByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllDetailWNoAppItemLogic(NoAppItem, AppItemData.DETAIL_TYPE_REPORT_SORT);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllSearchItemsByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllDetailWNoAppItemLogic(NoAppItem, AppItemData.DETAIL_TYPE_WEBFORM_SEARCH);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllParametersByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllDetailWNoAppItemLogic(NoAppItem, AppItemData.DETAIL_TYPE_REPORT_PARAMETER);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllReportTypeByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllDetailWNoAppItemLogic(NoAppItem, AppItemData.DETAIL_TYPE_REPORT_TYPE);				
			
			return dataSet;
			
		}

		public string GetAppItemToGo(QSPForm.Business.AppItem NoMenu)
		{
			CMAppItems appItems = CMAppItems.GetCMAppItemsByAppItemNumber((int)NoMenu);
			if (appItems != null)
				return appItems.PageUrl;
			
			return "";
		}

		public AppItemData SelectAllPermissionsByNoAppItem(int NoAppItem)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllPermissionsWNoAppItemLogic(NoAppItem);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllPermissionsByNoAppItem(int NoAppItem, int RoleID)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllPermissionsWNoAppItemLogic(NoAppItem, RoleID);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllPermissionsByEntityTypeID(int EntityTypeID, int RoleID)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllPermissionsWEntityTypeIDLogic(EntityTypeID, RoleID);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllPermissionsByEntityTypeID(int EntityTypeID)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllPermissionsWEntityTypeIDLogic(EntityTypeID);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllPermissionsByRoleID(int RoleID)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllPermissionsWRole_IDLogic(RoleID);				
			
			return dataSet;
			
		}

		public AppItemData SelectAllPermissionsByRoleID(int RoleID, int EntityTypeID)
		{
			
			AppItemData dataSet = new AppItemData();			
			//
			// Get the user DataSet from the DataLayer
			//		
			QSPForm.Data.ContentManager CM_DataAccess = new QSPForm.Data.ContentManager();
			dataSet = CM_DataAccess.SelectAllPermissionsWRole_IDLogic(RoleID, EntityTypeID);				
			
			return dataSet;
			
		}

	}
}
