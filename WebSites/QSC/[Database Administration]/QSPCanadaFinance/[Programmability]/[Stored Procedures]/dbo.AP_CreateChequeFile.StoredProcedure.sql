USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_CreateChequeFile]    Script Date: 06/07/2017 09:17:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_CreateChequeFile]

	@AP_Cheque_Batch_ID	INT

AS

DECLARE @RunDate	DATETIME
SET @RunDate = GETDATE()

IF EXISTS (	SELECT 1
			FROM tempdb..sysobjects
			WHERE type = 'U' and NAME = '##AP_Cheque_Prepped')
BEGIN
	DROP TABLE [dbo].[##AP_Cheque_Prepped]
END

CREATE TABLE ##AP_Cheque_Prepped(ChequeRecord VARCHAR(1000))

DECLARE
	@ChequeRecord	VARCHAR(8000),
	@cnt			INT,
	@StartLoc		INT,
  	@DataSegment	VARCHAR(10),
	--TDF
	@TDF01			VARCHAR(15),
	@TDF02			VARCHAR(15),
	@TDF03			VARCHAR(22),
	@TDF04			VARCHAR(1),
	@TDF05			VARCHAR(6),
	@TDF06			VARCHAR(1),
	@TDF07			VARCHAR(8),
	@TDF10			VARCHAR(6),
	@TDFFiller		VARCHAR(434),
	--BPR
	@BPRSegment		VARCHAR(10),
	@BPR01			VARCHAR(1),
	@BPR02			VARCHAR(17),
	@BPR03			VARCHAR(1),
	@BPR04			VARCHAR(3),
	@BPR05			VARCHAR(10),
	@BPR06			VARCHAR(2),
	@BPR07			VARCHAR(9),
	@BPR08			VARCHAR(2),
	@BPR10			VARCHAR(10),
	@BPRFiller1		VARCHAR(60),
	@BPR016			VARCHAR(6),
	@BPRFiller2		VARCHAR(379),
	--TRN
	@TRNSegment		VARCHAR(10),
	@TRN01			VARCHAR(2),
	@TRN02			VARCHAR(30),
	@TRN03			VARCHAR(10),
	--CUR
	@CURSegment		VARCHAR(10),
	@CUR01			VARCHAR(2),
	@CUR02			VARCHAR(3),
	--REF
	@REFSegment		VARCHAR(10),
	@REF01			VARCHAR(2),
	@REF02			VARCHAR(17),
	@REF03			VARCHAR(80),
	--N1 (Payor)
	@N1Segment		VARCHAR(10),
	@N101R			VARCHAR(2),
	@N102R			VARCHAR(35),
	--N1 (Payee)
	@N1E01		VARCHAR(2),
	@N1E02		VARCHAR(35),
	@N1E03		VARCHAR(2),
	@N1E04		VARCHAR(17),
	@N1E06		VARCHAR(2),
	--N3 (Payee)
	@N3Segment	VARCHAR(10),
	@N301		VARCHAR(35),
	@N302		VARCHAR(35),
	--N4 (Payee)
	@N4Segment	VARCHAR(10),
	@N401		VARCHAR(30),
	@N402		VARCHAR(2),
	@N403		VARCHAR(9),
	@N404		VARCHAR(3),
	--ENT
	@ENTSegment VARCHAR(10),
	@ENT01		VARCHAR(6),
	--PMR
	@RMRSegment VARCHAR(10),
	@RMR01		VARCHAR(2),
	@RMR02		VARCHAR(30),
	@RMR03		VARCHAR(2),
	@RMR04		VARCHAR(17),
	@RMR05		VARCHAR(17),
	@RMR06		VARCHAR(17),
	--REF
	@REFSegment2 VARCHAR(10),
	@REF101	VARCHAR(2),
	@REF102	VARCHAR(30),
	@REF103	VARCHAR(65),
	--DTM
	@DTMSegment VARCHAR(10),
	@DTM01		VARCHAR(3),
	@DTM02		VARCHAR(6),
	@DTMFiller	VARCHAR(503)

SET  @DataSegment	= '820000N'
SET	 @TDF01			= 'EPS'
SET	 @TDF02			= 'EDIBA26B'
SET	 @TDF04			= ''
SET	 @TDF05			= 'TDF34C'
SET	 @TDF06			= 'P'
SET	 @TDF10			= '003040'
SET	 @TDFFiller		= ''
--BPR
SET  @BPRSegment='820BPR1020'
SET	 @BPR01		='C'
SET  @BPR03		='C'
SET	 @BPR04		='CHK'
SET	 @BPR05		=''
SET  @BPR06		='04'
SET  @BPR07		='000417902'
SET	 @BPR08		=''
SET	 @BPR10		='9416299445'
SET	 @BPRFiller1=''
SET	 @BPRFiller2=''
--TRN
SET @TRNSegment	='820TRN1035'
SET @TRN01		='1'
SET @TRN03		=''
--CUR
SET @CURSegment	='820CUR1040'
SET @CUR01		='PR'
--REF
SET	@REFSegment	='820REF1050'
SET @REF01		='TN'
--N1 (Payor)
SET @N1Segment	='820N1 1070'
SET @N101R		='PR'
SET @N102R		='QSP Inc.' 
--N1 (Payee)
SET @N1E01		='PE'
SET @N1E03		='92'
SET	@N1E06		='EN' 
--N3 (Payee)
SET	@N3Segment	='820N3 1090'
--N4 (Payee)
SET	@N4Segment	='820N4 1100'
--ENT
SET	@ENTSegment	='820ENT2010'
SET @ENT01		='000001' 
--RMR
SET	@RMRSegment	='820RMR2150'
SET @RMR01		='IV' --2
SET @RMR03		=''
SET @RMR06		='00000000000000000' --17
--REF
SET	@REFSegment2='820REF2170'
SET @REF101		= 'IV' 
--DTM
SET	@DTMSegment	='820DTM2180'
SET @DTM01		='003' 
SET @DTMFiller	=''

DECLARE ChequeRec CURSOR FOR
SELECT 		-----------------TDF --------------------------------------------------
			QSPcanadaOrderManagement.dbo.udf_padstring(@DataSegment,'',10,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(@TDF01,'',15,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(@TDF02,'',15,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(ChequeNumber,'',22,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(@TDF04,'',1,'R') +
			@TDF05 +
			@TDF06 +
			CONVERT(VARCHAR(4),DATEPART(YEAR,@RunDate)) +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(MONTH, @RunDate)),'0',2,'L') +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(DAY, @RunDate)),'0',2,'L') +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(HOUR, @RunDate)),'0',2,'L') +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(MINUTE, @RunDate)),'0',2,'L') +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(SECOND, @RunDate)),'0',2,'L') +
			QSPCanadaOrderManagement.dbo.udf_padstring(@TDFFiller,'',74,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(@TDF10,'',12,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(@TDFFiller,'',342,'R') +
			-----------------BPR --------------------------------------------------
			@BPRSegment +
			@BPR01 +
			QSPCanadaOrderManagement.dbo.udf_padstring(CONVERT(INT, ISNULL(AmountWithTax, 0) * 100),'0',17,'L') +
			@BPR03 +
			@BPR04 +
			QSPCanadaOrderManagement.dbo.udf_padstring(@BPR05,'',10,'R') +
			@BPR06 +
			QSPCanadaOrderManagement.dbo.udf_padstring(@BPR07,'',12,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(@BPR08,'',2,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(BankAccount, '', 35, 'R') +
			@BPR10 +
			QSPCanadaOrderManagement.dbo.udf_padstring(@TDFFiller,'',60,'R') +
			SUBSTRING(CONVERT(VARCHAR(4),DATEPART(Year, @RunDate)),3,2) +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(MONTH, @RunDate)),'0',2,'L') +
			QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(DAY, @RunDate)),'0',2,'L') +
			QSPcanadaOrderManagement.dbo.udf_padstring(@TDFFiller,'',343,'R') +
			-----------------TRN --------------------------------------------------
			@TRNSegment +
			QSPcanadaOrderManagement.dbo.udf_padstring(@TRN01,'',2,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(ChequeNumber,'',30,'R') + 
			QSPcanadaOrderManagement.dbo.udf_padstring(@TRN03,'',10,'R') +
			QSPcanadaOrderManagement.dbo.udf_padstring(SendTo,'',460,'R') +
			-----------------CUR --------------------------------------------------
			@CURSegment +
			@CUR01 +
			QSPcanadaOrderManagement.dbo.udf_padstring(Currency,'',500,'R') + 
			-----------------REF --------------------------------------------------
			@REFSegment +
			@REF01 +
			QSPCanadaOrderManagement.dbo.udf_padstring(ChequeNumber,'',30,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(ISNULL(QSPCanadaFinance.dbo.UDF_RemoveAccent(Description1, 2), ''),'',470,'R') +
			--------------------------------------------N1 (Payor)------------------
			@N1Segment +
			@N101R +
			QSPCanadaOrderManagement.dbo.udf_padstring(@N102R,'',500,'R')+
			--------------------------------------N1 (Payee)------------------------
			@N1Segment +
			QSPCanadaOrderManagement.dbo.udf_padstring(@N1E01,'',2,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(ISNULL(QSPCanadaFinance.dbo.UDF_RemoveAccent(RecipientName, 2), ''),'',35,'R') +
			@N1E03 +
			QSPCanadaOrderManagement.dbo.udf_padstring(ISNULL(QSPCanadaFinance.dbo.UDF_RemoveAccent(Description2, 2), ''),'',19,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(@N1E06,'',444,'R') +
			---------------------------N3 (Payee)----------------------------------
			@N3Segment +
			QSPCanadaOrderManagement.dbo.udf_padstring(QSPCanadaFinance.dbo.UDF_RemoveAccent(Address1, 2),'',35,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(QSPCanadaFinance.dbo.UDF_RemoveAccent(Address2, 2),'',467,'R') +
			-----------------------------N4 (Payee)--------------------------------
			@N4Segment +
			QSPCanadaOrderManagement.dbo.udf_padstring(QSPCanadaFinance.dbo.UDF_RemoveAccent(City, 2),'',30,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(Province,'',2,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(PostalCode,'',9,'R') +
			QSPCanadaOrderManagement.dbo.udf_padstring(Country,'',461,'R') +
			-----------------------------ENT---------------------------------------
			@ENTSegment +
			QSPCanadaOrderManagement.dbo.udf_padstring(@ENT01,'',502,'R') +
			-----------------------------RMR-----------------------------------------
			@RMRSegment +
			@RMR01 +
			QSPCanadaOrderManagement.dbo.udf_padstring(ISNULL(BatchID, 0),'',30,'R')+
			QSPCanadaOrderManagement.dbo.udf_padstring(@RMR03,'',2,'R')+
			QSPCanadaOrderManagement.dbo.udf_padstring((CONVERT(INT, AmountWithoutTax * 100)),'0',17,'L') + --Monetary Amount
			QSPCanadaOrderManagement.dbo.udf_padstring((CONVERT(INT, AmountWithTax * 100)),'0',17,'L')+--Gross
			QSPCanadaOrderManagement.dbo.udf_padstring(@RMR06,'',434,'R')+
			----------------------------REF-------------------------------------------
			@REFSegment2+@REF101+
			QSPCanadaOrderManagement.dbo.udf_padstring(ISNULL(BatchID, 0),'',30,'R')+
			QSPCanadaOrderManagement.dbo.udf_padstring(QSPCanadaFinance.dbo.UDF_RemoveAccent(Description1, 2), '', 470, 'R') +
			----------------------------DTM-------------------------------------------
			@DTMSegment+@DTM01+
			SUBSTRING(CONVERT(VARCHAR(4),DATEPART(YEAR, @RunDate)),3,2)+
			QSPCanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(MONTH, @RunDate)),'0',2,'L')+
			QSPCanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2),DATEPART(DAY, @RunDate)),'0',2,'L')+ 
			QSPCanadaOrderManagement.dbo.udf_padstring(@DTMFiller,'',493,'R')
FROM		##AP_Cheque
ORDER BY	ChequeNumber

OPEN ChequeRec
FETCH NEXT FROM ChequeRec INTO @ChequeRecord
		
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @cnt = 0
	SET @StartLoc = 1
	WHILE @cnt < 13
	BEGIN	
		INSERT INTO ##AP_Cheque_Prepped
		SELECT SUBSTRING(@ChequeRecord, @StartLoc, 512)
		SET @StartLoc = @StartLoc + 512
		SET @cnt = @Cnt + 1
	END
FETCH NEXT FROM ChequeRec INTO @ChequeRecord
END
CLOSE ChequeRec
DEALLOCATE ChequeRec

IF @@ERROR = 0 
BEGIN
	DECLARE @SQLcommand 	VARCHAR(1000)
	DECLARE @Filename		VARCHAR(100)
	DECLARE @Body 			VARCHAR(500)
	DECLARE @path 			VARCHAR(200)
	DECLARE @SendEmailTo	VARCHAR(1000)
	DECLARE @FileAttachment	VARCHAR(200)

	SET @SendEmailTo = 'qsp-IT-canada@qsp.com'
	SET @Path = 'E:\Projects\Paylater\QSPCAFinance\APCheques\' 

	SET @Filename =  'APCheques'+
		CONVERT(VARCHAR(4),DATEPART(YEAR, @RunDate)) +
		QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2), DATEPART(MONTH, @RunDate)), '0', 2, 'L') +
		QSPcanadaOrderManagement.dbo.udf_padstring(CONVERT(VARCHAR(2), DATEPART(DAY, @RunDate)), '0', 2, 'L') +
		CAST(DATEPART(HOUR,	@RunDate) AS VARCHAR) + 
		CAST(DATEPART(MINUTE, @RunDate) AS VARCHAR) + 
		CAST(DATEPART(SECOND, @RunDate) AS VARCHAR) + '.DAT'

	SELECT	@Cnt = COUNT(*)
	FROM	##AP_Cheque_Prepped

	IF @Cnt > 0 
	BEGIN
		SET @SQLcommand = 'bcp "tempdb.##AP_Cheque_Prepped" out "Q:\Projects\Paylater\QSPCAFinance\APCheques\' + @Filename + '" -c -q -T '
		EXEC master..xp_cmdshell @SQLcommand

		UPDATE	AP_Cheque_Batch
		SET		[FileName] = @FileName
		WHERE	AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID

		SET	@Body = 'AP Cheque data file ' + @Filename + ' has been submitted to MoveIT for onward submission to TD on ' +
					CONVERT(VARCHAR(30), @RunDate, 113)

		EXEC  QSPCanadaCommon.dbo.Send_EMAIL 'APCheques@QSP.com', @SendEmailTo, 'AP Cheques Submitted', @Body
	END
END
GO
