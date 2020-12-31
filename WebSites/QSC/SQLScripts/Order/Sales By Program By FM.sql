USE QSPCanadaOrderManagement
GO

SELECT		fm.Firstname + ' ' + fm.Lastname FM,
			cdProgramType.Description ProgramType,
			COUNT(DISTINCT c.ID) NumberOfFundaisersRunningProgramType,
			SUM(cod.Price) GrossAmountOfSalesFromProgramType
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaProduct..PRICING_DETAILS pd ON pd.Magprice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..programsection ps ON ps.ID = pd.ProgramSectionID
JOIN		QSPCanadaProduct..PROGRAM_MASTER pm ON pm.Program_ID = ps.Program_ID
JOIN		QSPCanadaCommon..CodeDetail cdProgramType ON cdProgramType.Instance = pm.SubType
JOIN		QSPCanadaCommon..Campaign c ON c.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = c.FMID
JOIN		QSPCanadaFinance..Invoice i ON i.INVOICE_ID = cod.InvoiceNumber
WHERE		i.INVOICE_DATE BETWEEN '2015-07-01' AND '2015-12-31'
AND			cdProgramType.Instance IN (30323, 30324)
GROUP BY	fm.Firstname + ' ' + fm.Lastname,
			cdProgramType.Description
ORDER BY	fm.Firstname + ' ' + fm.Lastname,
			cdProgramType.Description

--QA
SELECT		fm.Firstname + ' ' + fm.Lastname, cp.ProgramID, COUNT(*)
FROM		QSPCanadaCommon..Campaign c
JOIN		QSPCanadaCommon..CampaignProgram cp on cp.campaignid = c.id and cp.DeletedTF = 0
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = c.FMID
where		cp.ProgramID in (53,54)
and c.StartDate between '2015-07-01' and '2015-12-31'
group by fm.Firstname + ' ' + fm.Lastname, cp.ProgramID
order by fm.Firstname + ' ' + fm.Lastname, cp.ProgramID