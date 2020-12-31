USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_Batch_Select]    Script Date: 06/07/2017 09:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_Batch_Select]

	@AP_Cheque_Batch_ID	INT

AS

DECLARE @BusinessDivisionID	INT

SET @BusinessDivisionID = 1 --1: QSP

SELECT		DISTINCT
			1 AS Tag,
			NULL AS Parent,
			apcb.AP_Cheque_Batch_ID AS [Batch!1!BatchID!Element],
			@BusinessDivisionID AS [Batch!1!BusinessDivisionID!Element],
			NULL AS [Cheque!2!ChequeID!Element],
			NULL AS [Cheque!2!StatementID!Element],
			NULL AS [Cheque!2!ChequeNumber!Element],
			NULL AS [Cheque!2!Bank_Account_ID!Element],
			NULL AS [Cheque!2!Amount!Element],
			NULL AS [Cheque!2!ChequePayableDate!Element],
			NULL AS [Cheque!2!AccountAddress1!Element],
			NULL AS [Cheque!2!AccountAddress2!Element],
			NULL AS [Cheque!2!AccountCity!Element],
			NULL AS [Cheque!2!AccountProvince!Element],
			NULL AS [Cheque!2!AccountPostalCode!Element],
			NULL AS [Cheque!2!AccountCountry!Element],
			NULL AS [Cheque!2!AccountFirstName!Element],
			NULL AS [Cheque!2!AccountLastName!Element],
			NULL AS [Cheque!2!CourierAddress1!Element],
			NULL AS [Cheque!2!CourierAddress2!Element],
			NULL AS [Cheque!2!CourierCity!Element],
			NULL AS [Cheque!2!CourierProvince!Element],
			NULL AS [Cheque!2!CourierPostalCode!Element],
			NULL AS [Cheque!2!CourierCountry!Element],
			NULL AS [Cheque!2!CourierFirstName!Element],
			NULL AS [Cheque!2!CourierLastName!Element],
			NULL AS [Cheque!2!ReturnAddress1!Element],
			NULL AS [Cheque!2!ReturnAddress2!Element],
			NULL AS [Cheque!2!ReturnCity!Element],
			NULL AS [Cheque!2!ReturnProvince!Element],
			NULL AS [Cheque!2!ReturnPostalCode!Element],
			NULL AS [Cheque!2!ReturnCountry!Element],
			NULL AS [Cheque!2!ReturnFirstName!Element],
			NULL AS [Cheque!2!ReturnLastName!Element],
			NULL AS [Cheque!2!Description1!Element]
FROM		AP_Cheque_Batch apcb
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_Batch_ID = apcb.AP_Cheque_Batch_ID
WHERE		apcb.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID

UNION ALL

SELECT		DISTINCT
			2 AS Tag,
			1 AS Parent,
			apcb.AP_Cheque_Batch_ID,
			NULL,
			apc.AP_Cheque_ID,
			'',
			apc.ChequeNumber,
			apc.Bank_Account_ID,
			ref.Amount,
			ISNULL(CONVERT(VARCHAR(10), apc.ChequePayableDate, 120), ''),
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
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'6600, route Transcanadienne - bureau 750' 
				ELSE			'695 Riddell Road' 	
			END,
			'',
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'Pointe-Claire' 
				ELSE			'Orangeville' 
			END,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'QC'
				ELSE			'ON'
			END,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN	'H9R 4S2' 
				ELSE			'L9W 4Z5' 
			END,
			'CA',
			'QSP Inc. c/o Resolve',
			'',
			CASE ref.Refund_Type_ID
				WHEN 2 THEN	refType.Description + ' - ' + SUBSTRING(cod.ProductCode, 1, 6) + ' - ' + SUBSTRING(cod.ProductName, 1, 35) + ' - Sub ID ' + CONVERT(VARCHAR(13), ref.CustomerOrderHeaderInstance)
				ELSE		refType.Description + ' - Group ID ' + CONVERT(VARCHAR(8), camp.BillToAccountID) + ' - Campaign ID ' + CONVERT(VARCHAR(8), camp.ID)
			END
FROM		AP_Cheque_Batch apcb
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_Batch_ID = apcb.AP_Cheque_Batch_ID
JOIN		Refund ref
				ON	ref.AP_Cheque_ID = apc.AP_Cheque_ID
JOIN		Refund_Type refType
				ON	refType.Refund_Type_ID = ref.Refund_Type_ID
LEFT JOIN	QSPCanadaCommon..Campaign camp
				ON	camp.ID = ref.Campaign_ID
LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
				AND	cod.TransID = ref.TransID
WHERE		apcb.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID

ORDER BY	[Batch!1!BatchID!Element],
			[Cheque!2!ChequeID!Element]

FOR XML EXPLICIT
GO
