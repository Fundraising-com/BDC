USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequestBatch_Select]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequestBatch_Select]

	@StatementPrintRequestBatchID	INT

AS

DECLARE @BusinessDivisionID				INT,
		@StatementTitleLabel			VARCHAR(100),
		@StatementTitleLabelFrench		VARCHAR(100),
		@StatementTitle2Label			VARCHAR(100),
		@StatementTitle2LabelFrench		VARCHAR(100),
		@StatementDateLabel				VARCHAR(100),
		@StatementDateLabelFrench		VARCHAR(100),
		@AccountIDLabel					VARCHAR(100),
		@AccountIDLabelFrench			VARCHAR(100),
		@CampaignIDLabel				VARCHAR(100),
		@CampaignIDLabelFrench			VARCHAR(100),
		@AmountDueDebitLabel			VARCHAR(100),
		@AmountDueDebitLabelFrench		VARCHAR(100),
		@AmountDueCreditLabel			VARCHAR(100),
		@AmountDueCreditLabelFrench		VARCHAR(100),
		@AmountEnclosedLabel			VARCHAR(100),
		@AmountEnclosedLabelFrench		VARCHAR(100),
		@CampaignProgramsLabel			VARCHAR(100),
		@CampaignProgramsLabelFrench	VARCHAR(100),
		@FMIDLabel						VARCHAR(100),
		@FMIDLabelFrench				VARCHAR(100),
		@FMNameLabel					VARCHAR(100),
		@FMNameLabelFrench				VARCHAR(100),
		@PaymentTermsLabel				VARCHAR(100),
		@PaymentTermsLabelFrench		VARCHAR(100),
		@CorpGSTNumberLabel				VARCHAR(100),
		@CorpGSTNumberLabelFrench		VARCHAR(100),
		@CorpQSTNumberLabel				VARCHAR(100),
		@CorpQSTNumberLabelFrench		VARCHAR(100),
		@AccountContactLabel			VARCHAR(100),
		@AccountContactLabelFrench		VARCHAR(100),
		@AccountPhoneNumberLabel		VARCHAR(100),
		@AccountPhoneNumberLabelFrench	VARCHAR(100),
		@Label1							VARCHAR(100),
		@Label1French					VARCHAR(100),
		@Label2							VARCHAR(100),
		@Label2French					VARCHAR(100),
		@Label3							VARCHAR(100),
		@Label3French					VARCHAR(100),
		@Label4							VARCHAR(100),
		@Label4French					VARCHAR(100),
		@Label5							VARCHAR(100),
		@Label5French					VARCHAR(100),
		@TotalLabel						VARCHAR(100),
		@TotalLabelFrench				VARCHAR(100),
		@GridHeader1Name				VARCHAR(100),
		@GridHeader1NameFrench			VARCHAR(100),
		@GridHeader2Name				VARCHAR(100),
		@GridHeader2NameFrench			VARCHAR(100),
		@GridHeader3Name				VARCHAR(100),
		@GridHeader3NameFrench			VARCHAR(100),
		@GridColumn1Name				VARCHAR(100),
		@GridColumn1NameFrench			VARCHAR(100),
		@GridColumn2Name				VARCHAR(100),
		@GridColumn2NameFrench			VARCHAR(100),
		@GridColumn3Name				VARCHAR(100),
		@GridColumn3NameFrench			VARCHAR(100),
		@GridColumn4Name				VARCHAR(100),
		@GridColumn4NameFrench			VARCHAR(100),
		@GridColumn5Name				VARCHAR(100),
		@GridColumn5NameFrench			VARCHAR(100),
		@GridColumn6Name				VARCHAR(100),
		@GridColumn6NameFrench			VARCHAR(100),
		@GridColumn7Name				VARCHAR(100),
		@GridColumn7NameFrench			VARCHAR(100),
		@GridColumn8Name				VARCHAR(100),
		@GridColumn8NameFrench			VARCHAR(100),
		@GridFooter1Name				VARCHAR(100),
		@GridFooter1NameFrench			VARCHAR(100),
		@GridFooter2Name				VARCHAR(100),
		@GridFooter2NameFrench			VARCHAR(100),
		@GridFooter3Name				VARCHAR(100),
		@GridFooter3NameFrench			VARCHAR(100),
		@DateTo							DATETIME

SET @BusinessDivisionID = 1 --1: QSP
SET	@StatementTitleLabel = 'STATEMENT OF ACCOUNT BY CAMPAIGN'
SET	@StatementTitleLabelFrench = 'RELEVE DE COMPTE PAR CAMPAGNE'
SET	@StatementTitle2Label = 'STAFF'
SET	@StatementTitle2LabelFrench = 'De Personnel'
SET	@StatementDateLabel = 'Date:'
SET	@StatementDateLabelFrench = 'Date:'
SET	@AccountIDLabel = 'Group ID:'
SET	@AccountIDLabelFrench = 'N° du groupe:'
SET	@CampaignIDLabel = 'Campaign ID:'
SET	@CampaignIDLabelFrench = 'N° de campagne:'
SET	@AmountDueDebitLabel = 'Amount Due:'
SET	@AmountDueDebitLabelFrench = 'Montant dû:'
SET	@AmountDueCreditLabel = 'Credit Amount:'
SET	@AmountDueCreditLabelFrench = 'Montant crédité:'
SET	@AmountEnclosedLabel = 'Amount Enclosed:'
SET	@AmountEnclosedLabelFrench = 'Montant joint:'
SET	@CampaignProgramsLabel = 'QSP PROGRAMS:'
SET	@CampaignProgramsLabelFrench = 'Programmes QSP:'
SET	@FMIDLabel = 'FM ID:'
SET	@FMIDLabelFrench = 'N° du gérant de territoire:'
SET	@FMNameLabel = 'FM Name:'
SET	@FMNameLabelFrench = 'Nom du gérant de territoire:'
SET	@PaymentTermsLabel = 'Terms:'
SET	@PaymentTermsLabelFrench = 'Termes de paiement:'
SET	@CorpGSTNumberLabel = 'GST:'
SET	@CorpGSTNumberLabelFrench = 'N° de TPS:'
SET	@CorpQSTNumberLabel = 'QST:'
SET	@CorpQSTNumberLabelFrench = 'N° de QST:'
SET	@AccountContactLabel = 'ATTN:'
SET	@AccountContactLabelFrench = 'A L''ATTENTION DE:'
SET	@AccountPhoneNumberLabel = 'Group Phone No.'
SET	@AccountPhoneNumberLabelFrench = 'N° de Téléphone du groupe.'
SET	@Label1 = 'THANK YOU FOR YOUR ORDER!'
SET	@Label1French = 'MERCI DE VOTRE COMMANDE!'
SET	@Label2 = 'Please make your cheque payable to QSP Inc. and indicate your Group ID on it.'
SET	@Label2French = 'Veuillez  libeller le chèque à l’ordre de QSP Inc. et inscrire votre N° de compte.'
SET	@Label3	= 'Please RETURN this statement with CHEQUE'
SET	@Label3French = 'Veuillez retourner ce relevé avec votre paiment si un montant est dû.'
SET	@Label4 = ''
SET	@Label4French = ''
SET	@Label5 = ''
SET	@Label5French = ''
SET	@TotalLabel = 'STATEMENT TOTALS'
SET	@TotalLabelFrench = 'TOTAUX DE RAPPORT'
SET	@GridHeader1Name = 'CATALOGUE ACTIVITY'
SET	@GridHeader1NameFrench = 'ACTIVITÉ DE CATALOGUE'
SET	@GridHeader2Name = 'INTERNET ACTIVITY'
SET	@GridHeader2NameFrench = 'ACTIVITÉ D''INTERNET'
SET	@GridHeader3Name = 'MISCELLANEOUS ADJUSTMENTS'
SET	@GridHeader3NameFrench = 'AJUSTEMENTS'
SET	@GridColumn1Name = 'Transaction Date'
SET	@GridColumn1NameFrench = 'Date de la transaction'
SET	@GridColumn2Name = 'Transaction Number'
SET	@GridColumn2NameFrench = 'N° de la transaction'
SET	@GridColumn3Name = 'Invoice # OrderID'
SET	@GridColumn3NameFrench = 'N° de la Commande'
SET	@GridColumn4Name = 'Transaction Type'
SET	@GridColumn4NameFrench = 'Type de transaction'
SET	@GridColumn5Name = 'Transaction Reference'
SET	@GridColumn5NameFrench = 'Référence de la transaction'
SET	@GridColumn6Name = 'Debit'
SET	@GridColumn6NameFrench = 'Débit'
SET	@GridColumn7Name = 'Credit'
SET	@GridColumn7NameFrench = 'Crédit'
SET	@GridColumn8Name = 'Balance'
SET	@GridColumn8NameFrench = 'Solde'
SET	@GridFooter1Name = 'CATALOGUE TOTALS'
SET	@GridFooter1NameFrench = 'TOTAUX DE CATALOGUE'
SET	@GridFooter2Name = 'INTERNET TOTALS'
SET	@GridFooter2NameFrench = 'TOTAUX D''INTERNET'
SET	@GridFooter3Name = 'MISCELLANEOUS ADJUSTMENT TOTALS'
SET	@GridFooter3NameFrench = 'TOTAUX D''AJUSTEMENT'

SELECT	@DateTo = CONVERT(VARCHAR(10), DATEADD(DAY, 1, MIN(stat.StatementDate)), 120)
FROM	StatementPrintRequest spr
JOIN	[Statement] stat
			ON	stat.StatementID = spr.StatementID
WHERE	spr.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

SELECT		DISTINCT
			1 AS Tag,
			NULL AS Parent,
			sprb.StatementPrintRequestBatchID AS [Batch!1!BatchID!Element],
			@BusinessDivisionID AS [Batch!1!BusinessDivisionID!Element],
			NULL AS [Statement!2!StatementID!Element],
			NULL AS [Statement!2!ChequeID!Element],
			NULL AS [Statement!2!StatementTitleLabel!Element],
			NULL AS [Statement!2!StatementTitle2Label!Element],
			NULL AS [Statement!2!StatementDateLabel!Element],
			NULL AS [Statement!2!StatementDate!Element],
			NULL AS [Statement!2!AccountIDLabel!Element],
			NULL AS [Statement!2!AccountID!Element],
			NULL AS [Statement!2!CampaignIDLabel!Element],
			NULL AS [Statement!2!CampaignID!Element],
			NULL AS [Statement!2!Lang!Element],
			NULL AS [Statement!2!AmountDueLabel!Element],
			NULL AS [Statement!2!AmountDue!Element],
			NULL AS [Statement!2!AmountEnclosedLabel!Element],
			NULL AS [Statement!2!CampaignProgramsLabel!Element],
			NULL AS [Statement!2!CampaignPrograms!Element],
			NULL AS [Statement!2!FMIDLabel!Element],
			NULL AS [Statement!2!FMID!Element],
			NULL AS [Statement!2!FMNameLabel!Element],
			NULL AS [Statement!2!FMFirstName!Element],
			NULL AS [Statement!2!FMLastName!Element],
			NULL AS [Statement!2!PaymentTermsLabel!Element],
			NULL AS [Statement!2!PaymentTerms!Element],
			NULL AS [Statement!2!CorpAttn!Element],
			NULL AS [Statement!2!CorpAddress1!Element],
			NULL AS [Statement!2!CorpAddress2!Element],
			NULL AS [Statement!2!CorpCity!Element],
			NULL AS [Statement!2!CorpProvince!Element],
			NULL AS [Statement!2!CorpPostalCode!Element],
			NULL AS [Statement!2!CorpPhoneNumber!Element],
			NULL AS [Statement!2!CorpGSTNumberLabel!Element],
			NULL AS [Statement!2!CorpGSTNumber!Element],
			NULL AS [Statement!2!CorpQSTNumberLabel!Element],
			NULL AS [Statement!2!CorpQSTNumber!Element],
			NULL AS [Statement!2!AccountName!Element],
			NULL AS [Statement!2!AccountContactLabel!Element],
			NULL AS [Statement!2!AccountContactFirstName!Element],
			NULL AS [Statement!2!AccountContactLastName!Element],
			NULL AS [Statement!2!AccountAddress1!Element],
			NULL AS [Statement!2!AccountAddress2!Element],
			NULL AS [Statement!2!AccountCity!Element],
			NULL AS [Statement!2!AccountProvince!Element],
			NULL AS [Statement!2!AccountPostalCode!Element],
			NULL AS [Statement!2!AccountPhoneNumberLabel!Element],
			NULL AS [Statement!2!AccountPhoneNumber!Element],
			NULL AS [Statement!2!CourierAddress1!Element],
			NULL AS [Statement!2!CourierAddress2!Element],
			NULL AS [Statement!2!CourierCity!Element],
			NULL AS [Statement!2!CourierProvince!Element],
			NULL AS [Statement!2!CourierPostalCode!Element],
			NULL AS [Statement!2!CourierCountry!Element],
			NULL AS [Statement!2!CourierFirstName!Element],
			NULL AS [Statement!2!CourierLastName!Element],
			NULL AS [Statement!2!Label1!Element],
			NULL AS [Statement!2!Label2!Element],
			NULL AS [Statement!2!Label3!Element],
			NULL AS [Statement!2!Label4!Element],
			NULL AS [Statement!2!Label5!Element],
			NULL AS [Statement!2!TotalLabel!Element],
			NULL AS [StatementGrid!3!StatementDetailTypeID!Element],
			NULL AS [StatementGrid!3!Heading!Element],
			NULL AS [StatementGrid!3!Column1Name!Element],
			NULL AS [StatementGrid!3!Column2Name!Element],
			NULL AS [StatementGrid!3!Column3Name!Element],
			NULL AS [StatementGrid!3!Column4Name!Element],
			NULL AS [StatementGrid!3!Column5Name!Element],
			NULL AS [StatementGrid!3!Column6Name!Element],
			NULL AS [StatementGrid!3!Column7Name!Element],
			NULL AS [StatementGrid!3!Column8Name!Element],
			NULL AS [StatementGrid!3!FooterName!Element],
			NULL AS [StatementGridItem!4!Column1Data!Element],
			NULL AS [StatementGridItem!4!Column2Data!Element],
			NULL AS [StatementGridItem!4!Column3Data!Element],
			NULL AS [StatementGridItem!4!Column4Data!Element],
			NULL AS [StatementGridItem!4!Column5Data!Element],
			NULL AS [StatementGridItem!4!Amount!Element],
			NULL AS [Cheque!5!ChequeID!Element],
			NULL AS [Cheque!5!StatementID!Element],
			NULL AS [Cheque!5!ChequeNumber!Element],
			NULL AS [Cheque!5!Bank_Account_ID!Element],
			NULL AS [Cheque!5!Amount!Element],
			NULL AS [Cheque!5!ChequePayableDate!Element],
			NULL AS [Cheque!5!ChequeVoid!Element],
			NULL AS [Cheque!5!AccountAddress1!Element],
			NULL AS [Cheque!5!AccountAddress2!Element],
			NULL AS [Cheque!5!AccountCity!Element],
			NULL AS [Cheque!5!AccountProvince!Element],
			NULL AS [Cheque!5!AccountPostalCode!Element],
			NULL AS [Cheque!5!AccountCountry!Element],
			NULL AS [Cheque!5!AccountFirstName!Element],
			NULL AS [Cheque!5!AccountLastName!Element],
			NULL AS [Cheque!5!CourierAddress1!Element],
			NULL AS [Cheque!5!CourierAddress2!Element],
			NULL AS [Cheque!5!CourierCity!Element],
			NULL AS [Cheque!5!CourierProvince!Element],
			NULL AS [Cheque!5!CourierPostalCode!Element],
			NULL AS [Cheque!5!CourierCountry!Element],
			NULL AS [Cheque!5!CourierFirstName!Element],
			NULL AS [Cheque!5!CourierLastName!Element],
			NULL AS [Cheque!5!ReturnAddress1!Element],
			NULL AS [Cheque!5!ReturnAddress2!Element],
			NULL AS [Cheque!5!ReturnCity!Element],
			NULL AS [Cheque!5!ReturnProvince!Element],
			NULL AS [Cheque!5!ReturnPostalCode!Element],
			NULL AS [Cheque!5!ReturnCountry!Element],
			NULL AS [Cheque!5!ReturnFirstName!Element],
			NULL AS [Cheque!5!ReturnLastName!Element],
			NULL AS [Cheque!5!Description1!Element]
FROM		StatementPrintRequestBatch sprb
JOIN		StatementPrintRequest spr
				ON	spr.StatementPrintRequestBatchID = sprb.StatementPrintRequestBatchID
JOIN		[Statement] stat
				ON	stat.StatementID = spr.StatementID
WHERE		sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

UNION ALL

SELECT		DISTINCT
			2 AS Tag,
			1 AS Parent,
			sprb.StatementPrintRequestBatchID,
			NULL,
			stat.StatementID,
			ISNULL(apc.AP_Cheque_ID, ''),
			CASE stat.Lang WHEN 'FR' THEN @StatementTitleLabelFrench ELSE @StatementTitleLabel END,
			CASE stat.Lang
				WHEN 'FR' THEN	CASE stat.IsStaffCampaign
									WHEN 0 THEN	''
									ELSE		@StatementTitle2LabelFrench
								END
				ELSE			CASE stat.IsStaffCampaign
									WHEN 0 THEN ''
									ELSE		@StatementTitle2Label
								END
			END,
			CASE stat.Lang WHEN 'FR' THEN @StatementDateLabelFrench ELSE @StatementDateLabel END,
			ISNULL(CONVERT(VARCHAR(10), stat.StatementDate, 120), ''),
			CASE stat.Lang WHEN 'FR' THEN @AccountIDLabelFrench ELSE @AccountIDLabel END,
			stat.AccountID,
			CASE stat.Lang WHEN 'FR' THEN @CampaignIDLabelFrench ELSE @CampaignIDLabel END,
			stat.CampaignID,
			stat.Lang,
			CASE stat.Lang
				WHEN 'FR' THEN	CASE	WHEN stat.Balance >= 0 THEN	@AmountDueCreditLabelFrench
										ELSE						@AmountDueDebitLabelFrench
								END
				ELSE			CASE	WHEN stat.Balance < 0 THEN	@AmountDueCreditLabel
										ELSE						@AmountDueDebitLabel
								END
			END,
			ISNULL(stat.Balance, 0),
			CASE stat.Lang WHEN 'FR' THEN @AmountEnclosedLabelFrench ELSE @AmountEnclosedLabel END,
			CASE stat.Lang WHEN 'FR' THEN @CampaignProgramsLabelFrench ELSE @CampaignProgramsLabel END,
			ISNULL(stat.CampaignPrograms, ''),
			CASE stat.Lang WHEN 'FR' THEN @FMIDLabelFrench ELSE @FMIDLabel END,
			stat.FMID,
			CASE stat.Lang WHEN 'FR' THEN @FMNameLabelFrench ELSE @FMNameLabel END,
			stat.FMFirstName,
			stat.FMLastName,
			CASE stat.Lang WHEN 'FR' THEN @PaymentTermsLabelFrench ELSE @PaymentTermsLabel END,
			stat.PaymentTerms,
			ISNULL(stat.CorpAttn, ''),
			stat.CorpAddress1,
			ISNULL(stat.CorpAddress2, ''),
			stat.CorpCity,
			stat.CorpProvince,
			stat.CorpPostalCode,
			stat.CorpPhoneNumber,
			CASE stat.Lang WHEN 'FR' THEN @CorpGSTNumberLabelFrench ELSE @CorpGSTNumberLabel END,
			stat.CorpGSTNumber,
			CASE ISNULL(stat.CorpQSTNumber, '')
				WHEN '' THEN	''
				ELSE			CASE stat.Lang WHEN 'FR' THEN @CorpQSTNumberLabelFrench ELSE @CorpQSTNumberLabel END
			END,
			ISNULL(stat.CorpQSTNumber, ''),
			stat.AccountName,
			CASE stat.Lang WHEN 'FR' THEN @AccountContactLabelFrench ELSE @AccountContactLabel END,
			stat.AccountContactFirstName,
			stat.AccountContactLastName,
			stat.AccountAddress1,
			ISNULL(stat.AccountAddress2, ''),
			stat.AccountCity,
			stat.AccountProvince,
			stat.AccountPostalCode,
			CASE stat.Lang WHEN 'FR' THEN @AccountPhoneNumberLabelFrench ELSE @AccountPhoneNumberLabel END,
			ISNULL(stat.AccountPhoneNumber, ''),
			stat.AccountAddress1,
			ISNULL(stat.AccountAddress2, ''),
			stat.AccountCity,
			stat.AccountProvince,
			stat.AccountPostalCode,
			'CA',
			stat.AccountContactFirstName,
			stat.AccountContactLastName,
			CASE stat.Lang WHEN 'FR' THEN @Label1French ELSE @Label1 END,
			CASE stat.Lang WHEN 'FR' THEN @Label2French ELSE @Label2 END,
			CASE stat.Lang WHEN 'FR' THEN @Label3French ELSE @Label3 END,
			CASE stat.Lang WHEN 'FR' THEN @Label4French ELSE @Label4 END,
			CASE stat.Lang WHEN 'FR' THEN @Label5French ELSE @Label5 END,
			CASE stat.Lang WHEN 'FR' THEN @TotalLabelFrench ELSE @TotalLabel END,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		StatementPrintRequestBatch sprb
JOIN		StatementPrintRequest spr
				ON	spr.StatementPrintRequestBatchID = sprb.StatementPrintRequestBatchID
JOIN		[Statement] stat
				ON	stat.StatementID = spr.StatementID
LEFT JOIN	Refund ref
				ON	ref.Refund_ID = ISNULL(stat.Refund_ID, -1)
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ISNULL(ref.AP_Cheque_ID, -1)
WHERE		sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

UNION ALL

SELECT		DISTINCT
			3 AS Tag,
			2 AS Parent,
			sprb.StatementPrintRequestBatchID,
			NULL,
			stat.StatementID,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			sd.StatementDetailTypeID,
			CASE sd.StatementDetailTypeID
				WHEN 1 THEN	CASE stat.Lang WHEN 'FR' THEN @GridHeader1NameFrench ELSE @GridHeader1Name END
				WHEN 2 THEN CASE stat.Lang WHEN 'FR' THEN @GridHeader2NameFrench ELSE @GridHeader2Name END
				WHEN 3 THEN CASE stat.Lang WHEN 'FR' THEN @GridHeader3NameFrench ELSE @GridHeader3Name END			
			END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn1NameFrench ELSE @GridColumn1Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn2NameFrench ELSE @GridColumn2Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn3NameFrench ELSE @GridColumn3Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn4NameFrench ELSE @GridColumn4Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn5NameFrench ELSE @GridColumn5Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn6NameFrench ELSE @GridColumn6Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn7NameFrench ELSE @GridColumn7Name END,
			CASE stat.Lang WHEN 'FR' THEN @GridColumn8NameFrench ELSE @GridColumn8Name END,
			CASE sd.StatementDetailTypeID
					WHEN 1 THEN	CASE stat.Lang WHEN 'FR' THEN @GridFooter1NameFrench ELSE @GridFooter1Name END
					WHEN 2 THEN CASE stat.Lang WHEN 'FR' THEN @GridFooter2NameFrench ELSE @GridFooter2Name END
					WHEN 3 THEN CASE stat.Lang WHEN 'FR' THEN @GridFooter3NameFrench ELSE @GridFooter3Name END
			END,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		StatementPrintRequestBatch sprb
JOIN		StatementPrintRequest spr
				ON	spr.StatementPrintRequestBatchID = sprb.StatementPrintRequestBatchID
JOIN		[Statement] stat
				ON	stat.StatementID = spr.StatementID
JOIN		UDF_Statement_GetDetails_Aggregated(@DateTo) sd
				ON	sd.StatementID = stat.StatementID
WHERE		sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

UNION ALL

SELECT		DISTINCT
			4 AS Tag,
			3 AS Parent,
			sprb.StatementPrintRequestBatchID,
			NULL,
			stat.StatementID,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			sd.StatementDetailTypeID,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			ISNULL(CONVERT(VARCHAR(10), sd.TransactionDate, 120), ''),
			ISNULL(sd.GroupingTransactionID, ''),
			ISNULL(sd.OrderID, ''),
			sd.TransactionTypeName,
			ISNULL(sd.Reference, ''),
			sd.TransactionAmount,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		StatementPrintRequestBatch sprb
JOIN		StatementPrintRequest spr
				ON	spr.StatementPrintRequestBatchID = sprb.StatementPrintRequestBatchID
JOIN		[Statement] stat
				ON	stat.StatementID = spr.StatementID
JOIN		UDF_Statement_GetDetails_Aggregated(@DateTo) sd
				ON	sd.StatementID = stat.StatementID
WHERE		sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

UNION ALL

SELECT		DISTINCT
			5 AS Tag,
			1 AS Parent,
			sprb.StatementPrintRequestBatchID,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			apc.AP_Cheque_ID,
			stat.StatementID,
			apc.ChequeNumber,
			apc.Bank_Account_ID,
			ref.Amount,
			ISNULL(CONVERT(VARCHAR(10), apc.ChequePayableDate, 120), ''),
			'',
			ref.Address1,
			ISNULL(ref.Address2, ''),
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Country,
			ISNULL(ref.FirstName, ''),
			ISNULL(ref.LastName, ''),
			ref.Address1,
			ISNULL(ref.Address2, ''),
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Country,
			ISNULL(ref.FirstName, ''),
			ISNULL(ref.LastName, ''),
			stat.CorpAddress1,
			ISNULL(stat.CorpAddress2, ''),
			stat.CorpCity,
			stat.CorpProvince,
			stat.CorpPostalCode,
			'CA',
			ISNULL(stat.CorpAttn, ''),
			'',
			'Group Refund for Statement ID ' + CONVERT(VARCHAR(MAX), stat.StatementID)
FROM		StatementPrintRequestBatch sprb
JOIN		StatementPrintRequest spr
				ON	spr.StatementPrintRequestBatchID = sprb.StatementPrintRequestBatchID
JOIN		[Statement] stat
				ON	stat.StatementID = spr.StatementID
JOIN		Refund ref
				ON	ref.Refund_ID = stat.Refund_ID
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

ORDER BY	[Batch!1!BatchID!Element],
			[Statement!2!StatementID!Element],
			[StatementGrid!3!StatementDetailTypeID!Element],
			[StatementGridItem!4!Column1Data!Element],
			[Cheque!5!ChequeID!Element]

FOR XML EXPLICIT
GO
