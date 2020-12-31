USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetStaffOrderDiscount]    Script Date: 06/07/2017 09:17:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetStaffOrderDiscount]
	@OrderID 	int,
	@CampaignID 	int,
	@StaffOrderDiscount numeric(10,2) output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/3/2004 
--   Find out Staff Order Discount For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT @StaffOrderDiscount = convert(numeric(10,2), (convert(numeric(10,2), C.StaffOrderDiscount ) / convert(numeric(10,2), 100 ))  ) 
FROM QSPCanadaCommon..Campaign C
	INNER JOIN QSPCanadaOrderManagement..Batch B on C.ID = B.CampaignID AND B.OrderID = @OrderID
WHERE B.CampaignID = @CampaignID
AND C.IsStaffOrder = 1

SET NOCOUNT OFF
GO
