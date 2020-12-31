USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetIsStaffOrder]    Script Date: 06/07/2017 09:17:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetIsStaffOrder]
(
	@OrderID 	int,
	@CampaignID 	int
)
RETURNS TABLE
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/23/2004 
--   Get Is Staff Order For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
RETURN
(
	SELECT C.IsStaffOrder, C.StaffOrderDiscount
	FROM QSPCanadaCommon..Campaign C
		INNER JOIN QSPCanadaOrderManagement..Batch B on C.ID = B.CampaignID
	WHERE B.OrderID = @OrderID
		AND B.CampaignID = @CampaignID
		AND C.IsStaffOrder = 1
	
)
GO
