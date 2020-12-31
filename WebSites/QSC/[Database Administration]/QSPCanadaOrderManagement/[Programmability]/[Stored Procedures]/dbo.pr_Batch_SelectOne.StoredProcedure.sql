USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Batch_SelectOne]    Script Date: 06/07/2017 09:19:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Batch'
-- based on the Primary Key.
-- Gets: @daDate datetime
-- Gets: @iID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Batch_SelectOne]
	@daDate datetime,
	@iID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Date],
	[ID],
	[AccountID],
	[EnterredCount],
	[EnterredAmount],
	[CalculatedAmount],
	[StatusInstance],
	[KE3FileName],
	[ChangeUserID],
	[ChangeDate],
	[TeacherCount],
	[StudentCount],
	[CustomerCount],
	[OrderCount],
	[OrderCountAccept],
	[OrderDetailCount],
	[OrderDetailCountError],
	[StartImportTime],
	[EndImportTime],
	[ImportTimeSeconds],
	[Clerk],
	[DateCreated],
	[UserIDCreated],
	[DateKeyed],
	[DateBatchCompleted],
	[OverridePctState],
	[PctState],
	[OriginalStatusInstance],
	[OrderTypeCode],
	[CampaignID],
	[BillToAddressID],
	[ShipToAddressID],
	[ShipToAccountID],
	[BillToFMID],
	[ShipToFMID],
	[ReportedEnvelopes],
	[PaymentSend],
	[SalesBeforeTax],
	[DateSent],
	[DateReceived],
	[ContactFirstName],
	[ContactLastName],
	[ContactEmail],
	[ContactPhone],
	[Comment],
	[IncentiveCalculationStatus],
	[MagnetBookletCount],
	[MagnetCardCount],
	[MagnetGoodCardCount],
	[MagnetCardsMailed],
	[MagnetMailDate],
	[PickDate],
	[IsDMApproved],
	[CountryCode],
	[PickLine],
	[OrderQualifierID],
	[CheckPayableToQSPAmount],
	[IsIncentive],
	[OrderDeliveryDate],
	[RefNumber],
	[PaymentBatchDate],
	[PaymentBatchID],
	[IsStaffOrder],
	[InquireUponComplete],
	[GroupProfit],
	[OrderID],
	[OrderAmntDue],
	[MagnetPostage],
	[OrderIDIncentive],
	[IsInvoiced],
	[CampaignNetTotal]
FROM [dbo].[Batch]
WHERE
	[Date] = @daDate
	AND [ID] = @iID
GO
