USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_OrderHeader_Create]    Script Date: 06/07/2017 09:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Kanata_OrderHeader_Create]

	@iOrderID						int,
	@zTeacherFirstName				varchar(50) = 'ZZ',
	@zTeacherLastName				varchar(50) = 'ZZ',
	@zStudentFirstName				varchar(50) = 'ZZ',
	@zStudentLastName				varchar(50) = 'ZZ',
	@iBillToInstance				int,
	@iUserProfileID					int,
	@iCustomerOrderHeaderInstance	int OUT

AS

	SET NOCOUNT ON

	DECLARE @iTeacherInstance		int
	DECLARE @iStudentInstance		int
	DECLARE @iBatchID				int
	DECLARE @dBatchDate				datetime
	DECLARE @iAccountID				int
	DECLARE @iCampaignID			int
	DECLARE @iCount					int
	DECLARE @zFMID 					varchar(4)
	DECLARE @AddressTypeId			int 

	SELECT		@iBatchID = B.ID,
				@dBatchDate = Date,
				@iAccountID = AccountID,
				@iCampaignID = CampaignID,
				@zFMID=c.FMID
	FROM		Batch b , qspcanadacommon..campaign c
	WHERE		OrderID = @iOrderID
	AND			b.Campaignid = c.id

	SELECT		@iCount = COUNT(*)
	FROM		Teacher
	WHERE		AccountID = @iAccountID
	AND			FirstName = @zTeacherFirstName
	AND			LastName = @zTeacherLastName
	AND			ClassRoom = 'ZZ'

	-- Get or create Teacher
	IF(@iCount <> 0)
	BEGIN
		SELECT		@iTeacherInstance = Instance
		FROM			Teacher
		WHERE		AccountID = @iAccountID
		AND			FirstName = @zTeacherFirstName
		AND			LastName = @zTeacherLastName
		AND			ClassRoom = 'ZZ'
	END
	ELSE
	BEGIN		
		EXEC	CreateTeacher
			@iAccountID,
			'ZZ',
			'ZZ',
			@zTeacherFirstName,
			'',
			@zTeacherLastName,
			@iTeacherInstance  OUTPUT
	END

	EXEC	CreateStudent
		@iTeacherInstance,
		@zStudentFirstName,
		@zStudentLastName,
		@iStudentInstance  OUTPUT

	EXEC	CreateOrderHeader
		@dBatchDate,
		@iBatchID,
		@iAccountID,
		@iCampaignID,
		@iBillToInstance,
		@iCustomerOrderHeaderInstance OUTPUT,
		@iUserProfileID

	PRINT	@iCustomerOrderHeaderInstance

/*	IF upper(@zBillTo) = 'SCHOOL'
	BEGIN
			SELECT		@zBillToFirstName = acc.Name,
						@zBillToLastName = '',
						@zBillToAddress1 = a.Street1,
						@zBillToAddress2 = a.Street2,
						@zBillToCity = a.City,
						@zBillToProvince = a.StateProvince,
						@zBillToPostal = a.Postal_Code,
						@zBillToEmail = acc.Email
			FROM		CustomerOrderHeader coh
			LEFT JOIN	QSPCanadaCommon..CAccount acc
							ON	acc.ID = coh.AccountID
			JOIN		QSPCanadaCommon..Address a
							ON	a.AddressListID = acc.AddressListID
			WHERE		a.address_type = 54001 --shipping address
			AND			coh.Instance = @iCustomerOrderHeaderInstance
	END
	
	IF upper(@zBillTo) = 'FM'
	BEGIN
			SELECT		@zBillToFirstName = fm.FirstName,
						@zBillToLastName = fm.LastName,
						@zBillToAddress1 = a.Street1,
						@zBillToAddress2 = a.Street2,
						@zBillToCity = a.City,
						@zBillToProvince = a.StateProvince,
						@zBillToPostal = a.Postal_Code,
						@zBillToEmail = fm.Email
			FROM		CustomerOrderHeader coh
						LEFT JOIN	QSPCanadaCommon..Campaign c
										ON	c.ID = coh.CampaignID
						LEFT JOIN	QSPCanadaCommon..FieldManager fm
										ON	fm.FMID = c.FMID
						JOIN		QSPCanadaCommon..Address a
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
				Email = @zBillToEmail
		WHERE	instance = (SELECT	CustomerBilltoInstance
							FROM	CustomerOrderHeader
							WHERE	Instance = @iCustomerOrderHeaderInstance)
*/
		UPDATE		CustomerOrderHeader 
		SET			StudentInstance = @iStudentInstance,
					PaymentMethodInstance = 50002
		WHERE		Instance = @iCustomerOrderHeaderInstance

		UPDATE		Batch
		SET			TeacherCount = TeacherCount + 1,
					StudentCount = StudentCount + 1,
					CustomerCount = CustomerCount + 1,
					OrderCount = OrderCount + 1,
					OrderCountAccept = OrderCountAccept + 1
		WHERE		OrderID = @iOrderID

	SET NOCOUNT OFF
GO
