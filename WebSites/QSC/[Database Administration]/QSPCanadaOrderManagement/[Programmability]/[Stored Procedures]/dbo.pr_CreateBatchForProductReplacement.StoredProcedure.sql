USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateBatchForProductReplacement]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CreateBatchForProductReplacement]

	@iCampaignID				int,
	@iOrderQualifierID			int,
	@zComment				varchar(300) = '',
	@iUserProfileID				int,
	@iOrderID				int OUT

AS

	SET NOCOUNT ON

	DECLARE @dTempDate		datetime
	DECLARE @iAccountID			int
	DECLARE @dBatchDate		datetime

	
	SET @dTempDate = GETDATE()
	SELECT @dBatchDate = CAST(CAST(DATEPART(YYYY, @dTempDate) AS varchar) + '-' + RIGHT('0' + CAST(DATEPART(MM, @dTempDate) AS varchar), 2) + '-' + RIGHT('0' + CAST(DATEPART(DD, @dTempDate) AS varchar), 2) AS datetime)

	-- first grab the account from the campaign
	SELECT		@iAccountID = BillToAccountID
	FROM			QspCanadaCommon..Campaign
	WHERE		ID = @iCampaignID
	
	EXEC	CreateBatch
		@dBatchDate,
		@iAccountID,
		@iAccountID,
		@iCampaignID,
		40002,  -- in process
		41008,   -- group
		@iOrderQualifierID,
		@iOrderID OUTPUT

	PRINT	@iOrderID

	UPDATE		Batch 
	SET			EnterredCount = 0,
				EnterredAmount = 0,
				CalculatedAmount = 0,
				TeacherCount = 1,
				StudentCount = 1,
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
				--IsStaffOrder = 0,	MS March 13, 2008
				Comment = @zComment
	WHERE		OrderID = @iOrderID

	SET NOCOUNT OFF
GO
