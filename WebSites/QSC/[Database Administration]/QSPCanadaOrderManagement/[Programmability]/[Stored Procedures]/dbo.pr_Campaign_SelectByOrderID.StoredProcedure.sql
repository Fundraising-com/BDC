USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectByOrderID]    Script Date: 06/07/2017 09:19:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Campaign'
-- based on an OrderID from the Batch Table.
-- Jeff Miles 12/20/2006
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Campaign_SelectByOrderID]
	
	@iOrderID int
	
AS
SET NOCOUNT ON

SELECT		ca.FMID
FROM		QspCanadaCommon..Campaign ca
LEFT JOIN	Batch	b
				ON	b.CampaignID = ca.ID
WHERE		b.OrderID = @iOrderID
GO
