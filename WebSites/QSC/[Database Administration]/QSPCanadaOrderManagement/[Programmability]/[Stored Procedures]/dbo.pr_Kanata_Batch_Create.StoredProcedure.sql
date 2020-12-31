USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_Batch_Create]    Script Date: 06/07/2017 09:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Kanata_Batch_Create]

	@iCampaignID				int,
	@iIsFMAccount				int,
	@iOrderQualifierID			int,
	@zComment					varchar(300) = '',
	@iUserProfileID				int,
	@dOrderDeliveryDate			datetime,				
	@iOrderID					int OUT

AS

SET NOCOUNT ON

DECLARE @dTempDate		datetime
DECLARE @iAccountID			int
DECLARE @dBatchDate		datetime
DECLARE @iOrderType			int

SET @dTempDate = GETDATE()
SELECT @dBatchDate = CAST(CAST(DATEPART(YYYY, @dTempDate) AS varchar) + '-' + RIGHT('0' + CAST(DATEPART(MM, @dTempDate) AS varchar), 2) + '-' + RIGHT('0' + CAST(DATEPART(DD, @dTempDate) AS varchar), 2) AS datetime)

-- first grab the account from the campaign
SELECT		@iAccountID = BillToAccountID
FROM			QspCanadaCommon..Campaign
WHERE		ID = @iCampaignID

SELECT  @iOrderType= CASE @iIsFMAccount 
			WHEN 1 THEN 41006
			ELSE 41008
			END

EXEC	CreateBatch
	@dBatchDate,
	@iAccountID,
	@iAccountID,
	@iCampaignID,
	40002,		  -- in process
	@iOrderType,  	 
	@iOrderQualifierID,
	@iOrderID OUTPUT

PRINT	@iOrderID

UPDATE		Batch 
SET			EnterredCount = 0,
			EnterredAmount = 0,
			CalculatedAmount = 0,
			TeacherCount = 0,
			StudentCount = 0,
			CustomerCount = 1,
			OrderCount = 1,
			OrderCountAccept = 1,
			OrderDetailCount = 0,
			OrderDetailCountError = 0,
			ReportedEnvelopes = 0,
			MagnetBookletCount = 0,
			MagnetCardCount = 0,
			MagnetGoodCardCount = 0,
			MagnetCardsMailed = 0,
			--IsStaffOrder = 0,  MS March 13, 2008
			Comment = @zComment,
			OrderDeliveryDate = @dOrderDeliveryDate
WHERE		OrderID = @iOrderID

SET NOCOUNT OFF
GO
