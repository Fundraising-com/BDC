USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_FulfillmentHouseContactProduct_Delete]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_FulfillmentHouseContactProduct_Delete]

	@iID		int

AS

	DELETE FROM	FulfillmentHouseContact_Product
	WHERE	ID = @iID
GO
