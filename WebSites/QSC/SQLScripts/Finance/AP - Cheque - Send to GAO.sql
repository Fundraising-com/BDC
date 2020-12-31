USE [QSPCanadaFinance]
GO

select apc.AP_Cheque_ID, *
from refund ref
join ap_cheque apc on apc.ap_cheque_id = ref.ap_cheque_id
where ref.campaign_id = 85474
order by apc.ap_cheque_id desc


begin tran

DECLARE @AP_Cheque_ID	INT
SET @AP_Cheque_ID = 107726

update ap_cheque
set chequenumber = 0
where ap_cheque_ID = @AP_Cheque_ID

commit tran

DECLARE @AP_Cheque_ID	INT
SET @AP_Cheque_ID = 107726

SELECT		CONVERT(VARCHAR(35), acc.Name) Name1,
			ref.Address1,
			ref.City,
			ref.Province [State/Province],
			ref.PostalCode,
			ref.Country,
			'CDN' Currency,
			ref.Amount,
			'QSP-CA' CompanyIdentifier,
			apc.AP_Cheque_ID ReferenceID,
			'' Salutation,
			'' Name2,
			ref.Address2,
			acc.ID CustomerNumber,
			'' ItemText,
			ref.Campaign_ID CampaignID
FROM		Refund ref
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = ref.Campaign_ID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
WHERE		apc.AP_Cheque_ID = @AP_Cheque_ID

