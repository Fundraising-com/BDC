USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateFulfillmentHouseContact]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateFulfillmentHouseContact]

	@iFulfillmentHouseContactID		int,
	@zContactFirstName			varchar(50),
	@zContactLastName			varchar(50),
	@zPosition				varchar(50),
	@zEmail				varchar(100),
	@zWorkPhone				varchar(20),
	@zFax					varchar(20),
	@zCustomerServiceContactFirstName	varchar(50),
	@zCustomerServiceContactLastName	varchar(50),
	@zCustomerServiceContactEmail		varchar(100),
	@zCustomerServiceContactPhone	varchar(50)

AS

	UPDATE	FULFILLMENT_HOUSE_CONTACTS
	SET		FirstName = @zContactFirstName,
			LastName = @zContactLastName,
			Title = @zPosition,
			Email = @zEmail,
			WorkPhone = @zWorkPhone,
			Fax = @zFax,
			CustSvcContactQSPFirstName = @zCustomerServiceContactFirstName,
			CustSvcContactQSPLastName = @zCustomerServiceContactLastName,
			CustSvcContactQSPEmail = @zCustomerServiceContactEmail,
			CustSvcContactQSPPhone = @zCustomerServiceContactPhone
	WHERE	Instance = @iFulfillmentHouseContactID
GO
