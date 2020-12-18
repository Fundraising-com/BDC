///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'user'
// Generated by Jas on: Monday, November 03, 2003, 4:18:12 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QSPForm.Common.DataDef;

namespace QSPForm.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'CM_AppItems'.
	/// </summary>
	
	public class ContentManager : DBInteractionBase
	{

		private const String PARAM_PKID     = "@iAppItem_ID";
		private const String PARAM_NO_APP_ITEM  = "@iNoAppItem";
		private const String PARAM_IS_MENU  = "@bIsMenu";
		private const String PARAM_DETAIL_TYPE  = "@iDetailType";
		
		//
		// Stored procedure names for each operation
		private const String SQL_PROC_SELECT_ONE   = "pr_AppItems_SelectOne";
		private const String SQL_PROC_SELECT_ALL   = "pr_AppItems_SelectAll";
		private const String SQL_PROC_SELECT_ALL_WITH_NO_APP_ITEM   = "pr_AppItems_SelectAllWNoAppItemLogic";
		private const String SQL_PROC_SELECT_ALL_DETAIL   = "pr_AppItemDetails_SelectAllWNoAppItemLogic";
		private const String SQL_PROC_SELECT_ALL_FAQ   = "pr_AppItemFAQ_SelectAllWNoAppItemLogic";
		private const String SQL_PROC_SELECT_ONE_FAQ   = "pr_AppItemFAQ_SelectOne";
		//
		// DataSetCommand object
		//
//		private SqlDataAdapter adapter;
//		private SqlCommand insertCommand;
//		private SqlCommand updateCommand;
//		private SqlCommand deleteCommand;


		#region Class Member Declarations
			
		#endregion


		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public ContentManager()
		{
			// Nothing for now.
		}


		


		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>User_id</LI>
		/// </UL>
		/// 
		/// </remarks>
		public new AppItemData SelectOne(int AppMenuItemID)
		{
			
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ONE;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, AppMenuItemID));
			

			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;
			
		}


		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties set after a succesful call of this method: 
		/// <UL>
		///		 <LI>ErrorCode</LI>
		/// </UL>
		/// </remarks>
		public new AppItemData SelectAll()
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			
			
			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}
		

		public AppItemData SelectAllWNoAppItemLogic(int NoAppItem)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL_WITH_NO_APP_ITEM;
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_NO_APP_ITEM, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoAppItem));
			
			
			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectOneFAQ(int FAQ_ID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ONE_FAQ;
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iFAQ_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FAQ_ID));
			
			
			AppItemData toReturn = new AppItemData();
			//toReturn.Tables.Add(AppItemData.TBL2_FAQ_ITEMS);
			DataTable appItemTable = toReturn.Tables[AppItemFAQTable.TBL_FAQ_ITEMS];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}
		
		public AppItemData SelectAllFAQWNoAppItemLogic(int NoAppItem)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL_FAQ;
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_NO_APP_ITEM, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoAppItem));
			
			
			AppItemData toReturn = new AppItemData();
			//toReturn.Tables.Add(AppItemData.TBL2_FAQ_ITEMS);
			DataTable appItemTable = toReturn.Tables[AppItemFAQTable.TBL_FAQ_ITEMS];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectAllDetailWNoAppItemLogic(int NoAppItem)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL_DETAIL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_NO_APP_ITEM, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoAppItem));
			
			
			AppItemData toReturn = new AppItemData();
			//toReturn.Tables.Add(AppItemData.TBL1_DETAIL_ITEMS);
			DataTable appItemTable = toReturn.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectAllDetailWNoAppItemLogic(int NoAppItem, int DetailType)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL_DETAIL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_NO_APP_ITEM, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoAppItem));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DETAIL_TYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DetailType));			
			
			
			AppItemData toReturn = new AppItemData();
			//toReturn.Tables.Add(AppItemData.TBL1_DETAIL_ITEMS);
			DataTable appItemTable = toReturn.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectAllMenuItem()
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_AppItems_SelectAllWIsMenuLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			
			
			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectAllMenuItemWRole_IDLogic(int Role)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_AppItems_SelectAllMenuItemWRole_IDLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iRole_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Role));
			
			
			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectOneWNoStepLogic(int NoStep)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_AppItems_SelectOneWNoStepLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iNoStep", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoStep));
			
			
			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}

		public AppItemData SelectAllWEntityTypeIDLogic(int EntityTypeID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_AppItems_SelectAllWEntityTypeIDLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iEntityTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, EntityTypeID));
			
			
			AppItemData toReturn = new AppItemData();
			DataTable appItemTable = toReturn.Tables[AppItemTable.TBL_APP_ITEM];

			Select(cmdToExecute,appItemTable);
			return toReturn;

		}
		

		public AppItemData SelectAllPermissionsWNoAppItemLogic(int NoAppItem, int RoleID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_Roles_Permissions_SelectAllWNoAppItemLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iNoAppItem", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoAppItem));
			if (RoleID > 0)
				cmdToExecute.Parameters.Add(new SqlParameter("@iRole_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleID));
			
			AppItemData toReturn = new AppItemData();
			DataTable permissionTable = toReturn.Tables[RolePermissionTable.TBL_ROLE_PERMISSION];

			Select(cmdToExecute,permissionTable);
			return toReturn;

		}

		public AppItemData SelectAllPermissionsWNoAppItemLogic(int NoAppItem)
		{
			return SelectAllPermissionsWNoAppItemLogic(NoAppItem,0);

		}

		public AppItemData SelectAllPermissionsWRole_IDLogic(int RoleID, int EntityTypeID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_Roles_Permissions_SelectAllWNoAppItemLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iRole_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleID));
			if (RoleID > 0)
				cmdToExecute.Parameters.Add(new SqlParameter("@iEntityTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, EntityTypeID));
			
			AppItemData toReturn = new AppItemData();
			DataTable permissionTable = toReturn.Tables[RolePermissionTable.TBL_ROLE_PERMISSION];

			Select(cmdToExecute,permissionTable);
			return toReturn;

		}

		public AppItemData SelectAllPermissionsWRole_IDLogic(int RoleID)
		{
			return SelectAllPermissionsWRole_IDLogic(RoleID, 0);

		}

		public AppItemData SelectAllPermissionsWEntityTypeIDLogic(int EntityTypeID, int RoleID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_Roles_Permissions_SelectAllWEntityTypeIDLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;			
			cmdToExecute.Parameters.Add(new SqlParameter("@iEntityTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, EntityTypeID));
			if (RoleID > 0)
				cmdToExecute.Parameters.Add(new SqlParameter("@iRole_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleID));
			
			
			AppItemData toReturn = new AppItemData();
			DataTable permissionTable = toReturn.Tables[RolePermissionTable.TBL_ROLE_PERMISSION];

			Select(cmdToExecute,permissionTable);
			return toReturn;

		}

		public AppItemData SelectAllPermissionsWEntityTypeIDLogic(int EntityTypeID)
		{
			return SelectAllPermissionsWEntityTypeIDLogic(EntityTypeID, 0);

		}
	}
}

