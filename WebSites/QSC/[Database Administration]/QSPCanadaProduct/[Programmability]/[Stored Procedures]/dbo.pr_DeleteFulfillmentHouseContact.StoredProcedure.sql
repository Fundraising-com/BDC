USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeleteFulfillmentHouseContact]    Script Date: 06/07/2017 09:17:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeleteFulfillmentHouseContact]

	@iFulfillmentHouseContactID	int

AS

DELETE FROM	Fulfillment_House_Contacts
WHERE	Instance = @iFulfillmentHouseContactID
GO
