USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_OrderHeader_Update]    Script Date: 06/07/2017 09:20:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Kanata_OrderHeader_Update]

	@iCustomerOrderHeaderInstance		int,
	@zBillTo							varchar(10),
	@zBillToFirstName					varchar(50),
	@zBillToLastName					varchar(50),
	@zBillToEmail						varchar(100),
	@zBillToAddress1					varchar(50),
	@zBillToAddress2					varchar(50),
	@zBillToCity						varchar(50),
	@zBillToProvince					varchar(50),
	@zBillToPostal						varchar(20),
	@zBillToPostal2						varchar(4)

AS

	SET NOCOUNT ON

	IF upper(@zBillTo) = 'SCHOOL'
	BEGIN
			SELECT		@zBillToFirstName = acc.Name,
						@zBillToLastName = '',
						@zBillToAddress1 = a.Street1,
						@zBillToAddress2 = a.Street2,
						@zBillToCity = a.City,
						@zBillToProvince = a.StateProvince,
						@zBillToPostal = a.Postal_Code,
						@zBillToPostal2 = a.Zip4,
						@zBillToEmail = acc.Email
			FROM		CustomerOrderHeader coh
			LEFT JOIN	QSPCanadaCommon..CAccount acc
						ON	acc.ID = coh.AccountID
			LEFT JOIN	QSPCanadaCommon..Address a
						ON	a.AddressListID = acc.AddressListID
			WHERE		a.address_type = 54001 --shipping address
			AND			coh.Instance = @iCustomerOrderHeaderInstance
	END
	ELSE IF upper(@zBillTo) = 'FM'
	BEGIN
			SELECT		@zBillToFirstName = fm.FirstName,
						@zBillToLastName = fm.LastName,
						@zBillToAddress1 = a.Street1,
						@zBillToAddress2 = a.Street2,
						@zBillToCity = a.City,
						@zBillToProvince = a.StateProvince,
						@zBillToPostal = a.Postal_Code,
						@zBillToPostal2 = a.Zip4,
						@zBillToEmail = fm.Email
			FROM		CustomerOrderHeader coh
						LEFT JOIN	QSPCanadaCommon..Campaign c
									ON	c.ID = coh.CampaignID
						LEFT JOIN	QSPCanadaCommon..FieldManager fm
									ON	fm.FMID = c.FMID
						LEFT JOIN	QSPCanadaCommon..Address a
									ON	a.AddressListID = fm.AddressListID
			WHERE		a.address_type = 54004 --home address
			AND			coh.Instance = @iCustomerOrderHeaderInstance
	END

	UPDATE	QSPCanadaordermanagement..customer 
	SET 	FirstName = @zBillToFirstName,
			LastName = @zBillToLastName,
			Address1 = @zBillToAddress1,
			Address2 = @zBillToAddress2,
			City = @zBillToCity,
			State = @zBillToProvince,
			Zip = @zBillToPostal,
			ZipPlusFour = @zBillToPostal2,
			Email = @zBillToEmail
	WHERE	instance = (SELECT	CustomerBilltoInstance
						FROM	CustomerOrderHeader
						WHERE	Instance = @iCustomerOrderHeaderInstance)

	SET NOCOUNT OFF
GO
