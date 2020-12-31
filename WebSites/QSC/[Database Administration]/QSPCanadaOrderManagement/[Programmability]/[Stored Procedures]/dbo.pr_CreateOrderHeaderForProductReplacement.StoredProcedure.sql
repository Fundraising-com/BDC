USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateOrderHeaderForProductReplacement]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CreateOrderHeaderForProductReplacement]

	@iOrderID				int,
	@zTeacherFirstName			varchar(50) = 'ZZ',
	@zTeacherLastName			varchar(50) = 'ZZ',
	@zStudentFirstName			varchar(50) = 'ZZ',
	@zStudentLastName			varchar(50) = 'ZZ',
	@iUserProfileID				int,
	@iCustomerOrderHeaderInstance		int OUT

AS
SET NOCOUNT ON

	DECLARE @iTeacherInstance		int
	DECLARE @iStudentInstance		int
	DECLARE @iCustomerInstance		int
	DECLARE @iBatchID			int
	DECLARE @dBatchDate		datetime
	DECLARE @iAccountID			int
	DECLARE @iCampaignID		int
	DECLARE @iCount			int


	SELECT		@iBatchID = ID,
				@dBatchDate = Date,
				@iAccountID = AccountID,
				@iCampaignID = CampaignID
	FROM			Batch
	WHERE		OrderID = @iOrderID


	SELECT		@iCount = COUNT(*)
	FROM			Teacher
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
			@iAccountID ,
			'ZZ',
			'ZZ',
			@zTeacherFirstName,
			'',
			@zTeacherLastName,
			@iTeacherInstance  OUTPUT
	END

	EXEC	CreateStudent
		@iTeacherInstance ,
		@zStudentFirstName,
		@zStudentLastName,
		@iStudentInstance  OUTPUT

	--Create dummy customer

	EXEC	CreateCustomerAccount
		@iAccountID,
		@iUserProfileID,
		@iCustomerInstance OUTPUT
	
	EXEC	CreateOrderHeader
		@dBatchDate,
		@iBatchID,
		@iAccountID,
		@iCampaignID,
		@iCustomerInstance,
		@iCustomerOrderHeaderInstance OUTPUT,
		@iUserProfileID

	PRINT	@iCustomerOrderHeaderInstance
	
	UPDATE		CustomerOrderHeader 
	SET			StudentInstance = @iStudentInstance,
				PaymentMethodInstance=50002
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
