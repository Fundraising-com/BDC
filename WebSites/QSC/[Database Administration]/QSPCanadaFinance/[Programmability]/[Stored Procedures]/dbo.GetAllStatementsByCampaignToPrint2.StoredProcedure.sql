USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStatementsByCampaignToPrint2]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[GetAllStatementsByCampaignToPrint2]
	
	@CAStatementsDateFrom			datetime,
	@CAStatementsDateTo				datetime,
	@OnlineDateFrom					datetime,
	@OnlineDateTo					datetime,
	@CustomerServiceDateFrom		datetime,
	@CustomerServiceDateTo			datetime

AS

SET NOCOUNT ON

CREATE TABLE #Statements (AccountID int, CampaignID int, Name varchar(50), FMID varchar(4), LastName varchar(50), FirstName varchar(50), Lang varchar(2), Amount numeric(10,2), InvoiceAmount numeric(10, 2), PaymentAmount numeric(10, 2), AdjustmentAmount numeric(10, 2))

INSERT INTO #Statements EXEC GetAllStatementsByCampaignToPrint @CAStatementsDateFrom, @CAStatementsDateTo

-- Online - Add because we need the CA statement even if the balance is 0
SELECT	c.FMID,
		a.Campaign_ID AS CampaignID,
		a.Account_ID AS AccountID,
		SUM(a.Adjustment_Amount) AS Amount
INTO		#Online
FROM		Adjustment a,
		QSPCanadaCommon..Campaign c
WHERE	c.ID = a.Campaign_ID
AND		a.Adjustment_Type_ID = 49028
AND		a.Adjustment_Effective_Date BETWEEN @OnlineDateFrom AND @OnlineDateTo
GROUP BY	c.FMID,
		a.Campaign_ID,
		a.Account_ID


-- Customer Service - Add because we need the CA statement even if the balance is 0
SELECT	c.FMID,
		a.Campaign_ID AS CampaignID,
		a.Account_ID AS AccountID,
		SUM(a.Adjustment_Amount) AS Amount
INTO		#CustomerService
FROM		Adjustment a,
		QSPCanadaCommon..Campaign c
WHERE	c.ID = a.Campaign_ID
AND		a.Adjustment_Type_ID = 49030
AND		a.Adjustment_Effective_Date BETWEEN @CustomerServiceDateFrom AND @CustomerServiceDateTo
GROUP BY	c.FMID,
		a.Campaign_ID,
		a.Account_ID


--Don't show statement if all campaigns in that particular account are $0 and no Customer Service or Online adjustments exist

DELETE FROM #Statements
WHERE		AccountID not in (SELECT AccountID FROM	#Statements	WHERE Amount <> 0.00)
AND			AccountID not in (SELECT AccountID FROM #Online)
AND			AccountID not in (SELECT AccountID FROM #CustomerService)



SELECT		s.AccountID,
			s.CampaignID,
			s.Name,
			s.FMID,
			s.LastName,
			s.FirstName,
			s.Amount,
			s.InvoiceAmount,
			s.PaymentAmount,
			s.AdjustmentAmount,
			s.Lang
FROM		#Statements s
ORDER BY	s.FMID,
			s.AccountID,
			s.CampaignID


DROP TABLE #Statements
DROP TABLE #Online
DROP TABLE #CustomerService

SET NOCOUNT OFF
GO
