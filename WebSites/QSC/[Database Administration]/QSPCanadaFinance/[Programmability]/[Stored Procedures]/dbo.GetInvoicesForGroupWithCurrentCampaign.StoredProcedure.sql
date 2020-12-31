USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoicesForGroupWithCurrentCampaign]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInvoicesForGroupWithCurrentCampaign]
	@Group_ID int 
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004  
--   Get All Invoices for a group which has a campaign in the current FY 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

DECLARE @StartDate datetime
DECLARE @EndDate datetime
EXEC QSPCanadaCommon..GetCurrentFiscalStartAndEnd @StartDate OUTPUT, @EndDate OUTPUT

--Get the Invoices for this group in the current FY
SELECT Invoice_ID,
	CASE AT.NAME_RESOURCE_ID 
			WHEN 117 THEN 'Group'
			WHEN 118 THEN 'FM'
			WHEN 119 THEN 'Employee'
			WHEN 120 THEN 'POS Client'
			ELSE ''
		END AS AccountType,	--R.Description as AccountType, 
	CASE IST.NAME_RESOURCE_ID
			WHEN 160 THEN 'Generated'
			WHEN 161 THEN 'Approved'
			WHEN 162 THEN 'Printed'
			ELSE ''
		END AS InvoiceStatus,	--R2.Description as InvoiceStatus, 
	ID as Account_ID, Order_ID, Invoice_Date, Invoice_Due_Date, Invoice_Amount, Is_Printed, DateTime_Approved, Group_Name
FROM INVOICE I
	LEFT JOIN QSPCanadaCommon..CAccount on ID = Group_ID
	LEFT JOIN ACCOUNT_TYPE AT on AT.ACCOUNT_TYPE_ID = I.ACCOUNT_TYPE_ID
	LEFT JOIN INVOICE_STATUS IST on IST.INVOICE_STATUS_ID = I.INVOICE_STATUS_ID
	LEFT JOIN QSPCanadaCommon..OM_TBL_RESOURCE R on R.RESOURCE_ID = AT.NAME_RESOURCE_ID 
	LEFT JOIN QSPCanadaCommon..OM_TBL_RESOURCE R2 on R2.RESOURCE_ID = IST.NAME_RESOURCE_ID
WHERE Order_ID in (
			SELECT Order_ID FROM QSPCanadaOriginalData..OM_TBL_Order 
			WHERE Bill_To_Group_ID = @Group_ID 
 				AND Campaign_ID IN 
				( 	
					SELECT Campaign_ID
					FROM QSPCanadaCommon..OM_TBL_GROUP G
					INNER JOIN QSPCanadaCommon..OM_TBL_Campaign C on Bill_To_Group_ID = Group_ID
						AND From_date >= @StartDate and To_date <= @EndDate
					WHERE Group_ID = @Group_ID
				)
			)
ORDER BY Group_Name


SET NOCOUNT OFF
GO
