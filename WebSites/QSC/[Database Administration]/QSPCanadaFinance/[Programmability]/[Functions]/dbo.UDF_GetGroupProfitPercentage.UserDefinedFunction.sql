USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetGroupProfitPercentage]    Script Date: 06/07/2017 09:17:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetGroupProfitPercentage]
(
	@OrderID 	int,
	@CampaignID 	int,
	@BatchDate	datetime
)
RETURNS bit
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/23/2004 
--   Get Group Profit Percentage For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
BEGIN
	DECLARE @GroupProfit bit

	SELECT @GroupProfit = CP.GroupProfit
	FROM QSPCanadaOrderManagement..Batch B
		INNER JOIN QSPCanadaCommon..CampaignProgram CP on CP.CampaignID = B.CampaignID
		INNER JOIN QSPCanadaCommon..Program Prog on CP.ProgramID = Prog.ID
	WHERE B.OrderID = @OrderID
	AND B.CampaignID = @CampaignID
	AND CP.DeletedTF = 0
	
   	RETURN(@GroupProfit)
END
GO
