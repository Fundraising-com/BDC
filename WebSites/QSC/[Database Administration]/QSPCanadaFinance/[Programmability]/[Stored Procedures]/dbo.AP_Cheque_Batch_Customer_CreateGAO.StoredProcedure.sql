USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_Batch_Customer_CreateGAO]    Script Date: 06/07/2017 09:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_Batch_Customer_CreateGAO]

	@AP_Cheque_Batch_ID INT = NULL
	
AS

DECLARE @RunDate			DATETIME,
		@ChequeType			VARCHAR(50),
		@Refund_ID			INT,
		@ChequeNumber		BIGINT,
		@AP_Cheque_ID		INT,
		@BankAccountID		INT,
		@RefundsToSend		INT

SET	@RunDate = GETDATE()
SET @ChequeType = 'Customer Refund - GAO'
SET @BankAccountID = 6

IF ISNULL(@AP_Cheque_Batch_ID, 0) = 0
BEGIN

	SELECT		TOP 1 @RefundsToSend = 1
	FROM		Refund ref
	LEFT JOIN	AP_Cheque apc
					ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
	WHERE		apc.AP_Cheque_ID IS NULL
	AND			ref.Refund_Type_ID = 1 --Customer Refund
	AND			ref.Cancelled = 0
	AND			ref.CreateDate > '2009-02-06 11:46' --Started sending Customer Refunds directly at this time

	IF ISNULL(@RefundsToSend, 0) = 0
	BEGIN
		SET	@AP_Cheque_Batch_ID = 0
		RETURN
	END

	BEGIN TRANSACTION

	INSERT INTO AP_Cheque_Batch
	(
		CreationDate,
		[Type]
	)
	SELECT	@RunDate,
			@ChequeType

	SET @AP_Cheque_Batch_ID = SCOPE_IDENTITY()
	DECLARE	Refund CURSOR FOR
	SELECT		ref.Refund_ID
	FROM		Refund ref
	LEFT JOIN	AP_Cheque apc
					ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
	WHERE		apc.AP_Cheque_ID IS NULL
	AND			ref.Refund_Type_ID = 1 --Customer Refund
	AND			ref.Cancelled = 0
	AND			ref.CreateDate > '2009-02-06 11:46' --Started sending Customer Refunds directly at this time
	OPEN Refund
	FETCH NEXT FROM Refund INTO @Refund_ID

	WHILE @@FETCH_STATUS = 0
	BEGIN			EXEC AP_Cheque_CreateFromRefund			@Refund_ID = @Refund_ID,			@AP_Cheque_ID = @AP_Cheque_ID OUTPUT
		UPDATE	AP_Cheque
		SET		AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
		WHERE	AP_Cheque_ID = @AP_Cheque_ID

		FETCH NEXT FROM Refund INTO @Refund_ID

	END
	CLOSE Refund
	DEALLOCATE Refund

	UPDATE	AP_Cheque
	SET		ChequeNumber = 0
	WHERE	AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID

	COMMIT
END

SELECT	Refund_ID,
		apc.AP_Cheque_ID,
		Amount,
		Address1,
		Address2,
		City,
		Province,
		PostalCode,
		Country,
		CustomerOrderHeaderInstance,
		TransID,
		FirstName,
		LastName,
		Campaign_ID,
		CreateDate,
		CreateUserID,
		UpdateDate,
		UpdateUserID,
		AP_Cheque_Batch_ID
FROM	Refund ref
JOIN	AP_Cheque apc
			ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE	apc.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID

/*
--Email report
Declare @FileName 		    Varchar(100)
Declare @Query    		    Varchar(4000)

Declare @Cnt			    Int 
Declare @Cmd  		        Varchar(1000)
Declare @ColList  		    Varchar(400)
Declare @TemplateFileName 	Varchar(100)
Declare @FilePath 		    Varchar(200)  

Declare @SubjectLine		Varchar(100)
Declare @MessageText		Varchar(200) 
Declare @EmailList 		    Varchar(500)

Set @ColList= 'Refund_ID,AP_Cheque_ID,Amount,Address1,Address2,City,Province,PostalCode,Country,CustomerOrderHeaderInstance,TransID,FirstName,LastName,Campaign_ID,CreateDate,CreateUserID,UpdateDate,UpdateUserID,AP_Cheque_Batch_ID'
Set @FilePath= 'Q:\projects\paylater\QSPCAFinance\CustomerRefunds\'
Set @TemplateFileName = @FilePath+'TemplateCustomerRefunds.xls'
Set @fileName= @FilePath+'CustomerRefunds.xls'

Set @EmailList = 'jmiles@gafundraising.com'--'qsp-qspfulfillment-dev@qsp.com;TMaples@southwestern.com;tunderwood@southwestern.com;carmine_moscardini@qsp.ca;gotts@southwestern.com'
Set @SubjectLine = 'Customer Refunds'

Set @query= 	'SELECT	Refund_ID,apc.AP_Cheque_ID,Amount,Address1,Address2,City,Province,PostalCode,Country,CustomerOrderHeaderInstance,TransID,FirstName,LastName,Campaign_ID,CreateDate,CreateUserID,UpdateDate,UpdateUserID,AP_Cheque_Batch_ID
				FROM	Refund ref
				JOIN	AP_Cheque apc
							ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
				WHERE	apc.AP_Cheque_Batch_ID = ' + CONVERT(VARCHAR(10),@AP_Cheque_Batch_ID)

SELECT @Cmd = 'Copy '+@templateFileName+'  '+ @fileName
EXEC MASTER..XP_CMDSHELL @cmd

Set @cmd = 'Insert CA_EXCEL_IMPORT2...[ExcelTable$] ' +  ' ( '+@colList+' ) '+ @query
Exec (@cmd)

--Select @Cnt= Count(*) From CA_EXCEL_IMPORT2...ExcelTable$ 

--If @Cnt > 0
--Begin
	Set @MessageText = 'The attached refunds require manual cheques to be cut.'
			
	Exec  QSPCanadaCommon.dbo.Send_EMAIL_ATTACH 'NoReply@qsp.com', @EmailList,@SubjectLine,@MessageText,@fileName
--End
*/
GO
