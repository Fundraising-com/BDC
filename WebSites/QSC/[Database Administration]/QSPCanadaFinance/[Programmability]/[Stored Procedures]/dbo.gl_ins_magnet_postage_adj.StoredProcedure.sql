USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[gl_ins_magnet_postage_adj]    Script Date: 06/07/2017 09:17:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[gl_ins_magnet_postage_adj]
  @pBillToAccountID	int,
  @pOrderID		int,
  @pCampaignID	int,
  @pEffectiveDate	datetime,
  @pAdjustmentAmount	decimal(10,2), --ora:numeric(18,4),
  @pCountryCode	varchar(10),
  @pUserID		varchar(30)  
AS

-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
-- Description:
-- This procedure inserts an adjustment record for MAGnet postage fees 
-- into the QSPCanadaFinance..Adjustment 
-- It also calls the necessary proc to insert the adjustment into the G/L tables.
-- 
-- Revision History:
-- June 2004 - Joshua Caesar
-- Inital Revision based upon om_fct_ins_magnet_postage_adj previous Oracle system.
-- =~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~

-- insert the QSPCanadaFinance..ACCOUNT record if necessary
-- (GL account, not the CAccount !)
-----------------------------------------------------------------------------------------------------
DECLARE @iAccountRecords int
SELECT @iAccountRecords = count(ACCOUNT_ID) 
  FROM QSPCanadaFinance..ACCOUNT
 WHERE QSPCanadaFinance..ACCOUNT.[ACCOUNT_ID] = @pBillToAccountID;
 
IF @iAccountRecords = 0
BEGIN
	
	INSERT INTO QSPCanadaFinance..ACCOUNT(
		ACCOUNT_ID,
		ACCOUNT_TYPE_ID,
		COUNTRY_CODE,
		DATE_CREATED,
		DATE_MODIFIED,	
		LAST_UPDATED_BY
	) VALUES ( 
		@pBillToAccountID,
		1, --OM_PACK_CONSTANT.C_GROUP_ACCOUNT, 
		   --c_group_account CONSTANT  OM_TBL_ACCOUNT_TYPE.account_type_id%TYPE :=1;
		@pCountryCode,
		getdate(),
		getdate(),
		@pUserID
	);
END 

-- insert the Adjustment record
-------------------------------------------
DECLARE @iAdjustmentID int

INSERT INTO QSPCanadaFinance..ADJUSTMENT(
	ACCOUNT_ID,
	ACCOUNT_TYPE_ID,
	ADJUSTMENT_TYPE_ID,
	ADJUSTMENT_EFFECTIVE_DATE,
	ADJUSTMENT_AMOUNT,
	DATE_CREATED,
	DATETIME_MODIFIED,
	LAST_UPDATED_BY,
	COUNTRY_CODE,
	ORDER_ID,
	CAMPAIGN_ID
)VALUES(
	@pBillToAccountID, --ACCOUNT_ID,int
	50601, --ACCOUNT_TYPE_ID,int
	   --OM_PACK_CONSTANT.C_GROUP_ACCOUNT,
	49016,--ADJUSTMENT_TYPE_ID,int
	   --c_adjtype_magnet_postage_debit 
	   --CONSTANT OM_TBL_ADJUSTMENT_TYPE.ADJUSTMENT_TYPE_ID%TYPE := 16;
	@pEffectiveDate, --ADJUSTMENT_EFFECTIVE_DATE,datetime
	@pAdjustmentAmount, --ADJUSTMENT_AMOUNT,decimal(10,2)
	getdate(), --DATE_CREATED,datetime
	getdate(), --DATETIME_MODIFIED,datetime
	@pUserID, --LAST_UPDATED_BY,varchar(30)
	@pCountryCode, --COUNTRY_CODE,varchar(10)
	@pOrderID, --ORDER_ID,int
	@pCampaignID --CAMPAIGN_ID,int	
);
--Null fields : NOTE_TO_PRINT,INTERNAL_COMMENT 
--ORA fields missing in SQL: cheque_date, adjustment_status_id

-- get the id of the record just inserted
------------------------------------------
SELECT @iAdjustmentID = @@Identity

-- insert the necessary info into the General Ledger tables.
-------------------------------------------------------------
DECLARE @iInsertedAdjustmentRetval int
SELECT @iInsertedAdjustmentRetval = 0
--call the procedure to insert the adjustment into G/L tables.
exec QSPCanadaFinance..gl_ins_magnet_adj
  @pAdjustmentID	= @iAdjustmentID,
  @pAdjustmentAmount	= @pAdjustmentAmount,
  @pTransactionTypeID	= 6,--om_pack_constant.c_trans_type_magnet_postage,
  @pEntityID		= 62,--om_pack_constant.c_qsp_entity,
  @Retval		= @iInsertedAdjustmentRetval output
  
return @iAdjustmentID;
GO
