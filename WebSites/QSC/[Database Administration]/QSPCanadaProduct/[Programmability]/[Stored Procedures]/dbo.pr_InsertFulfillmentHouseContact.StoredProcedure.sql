USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertFulfillmentHouseContact]    Script Date: 06/07/2017 09:17:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertFulfillmentHouseContact]

	@iFulfillmentHouseID			int,
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

	INSERT INTO	FULFILLMENT_HOUSE_CONTACTS
			(Ful_Nbr,
			FirstName,
			LastName,
			Title,
			Email,
			WorkPhone,
			Fax,
			CustSvcContactQSPFirstName,
			CustSvcContactQSPLastName,
			CustSvcContactQSPEmail,
			CustSvcContactQSPPhone,
			DateChanged,
			UserIDChanged)
	VALUES
			(@iFulfillmentHouseID,
			@zContactFirstName,
			@zContactLastName,
			@zPosition,
			@zEmail,
			@zWorkPhone,
			@zFax,
			@zCustomerServiceContactFirstName,
			@zCustomerServiceContactLastName,
			@zCustomerServiceContactEmail,
			@zCustomerServiceContactPhone,
			getdate(),
			null)

SELECT	SCOPE_IDENTITY()
GO
