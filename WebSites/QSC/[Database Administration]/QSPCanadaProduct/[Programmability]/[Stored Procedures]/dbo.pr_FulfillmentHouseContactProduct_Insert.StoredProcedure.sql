USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_FulfillmentHouseContactProduct_Insert]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_FulfillmentHouseContactProduct_Insert]

	@iFulfillmentHouseContactID	int,
	@zProductCode			varchar(20),
	@iUserID			int

AS

	INSERT INTO	FulfillmentHouseContact_Product
			(FulfillmentHouseContactID,
			Product_Code,
			DateCreated,
			UserIDCreated,
			DateChanged,
			UserIDChanged)
	VALUES	(@iFulfillmentHouseContactID,
			@zProductCode,
			GetDate(),
			@iUserID,
			GetDate(),
			@iUserID)

	SELECT SCOPE_IDENTITY()
GO
