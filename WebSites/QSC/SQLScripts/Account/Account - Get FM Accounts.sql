USE QSPCanadaCommon

SELECT		fm.FMID,
			fm.Country,
			fm.FirstName,
			fm.LastName,
			fm.DMID,
			fm.Lang AS FMLang,
			acc.ID AS AccountID,
			acc.Name AS AccountName,
			acc.Lang AS AccountLang,
			camp.ID AS CampaignID,
			camp.StartDate AS CampaignStartDate
FROM		FieldManager fm
LEFT JOIN	CAccount acc
				ON	acc.Name LIKE '%' + fm.FirstName + '%' + fm.LastName
LEFT JOIN	Campaign camp
				ON	camp.billtoaccountid = acc.id
WHERE		fm.DeletedTF = 0
ORDER BY	fm.Country,
			fm.FMID,
			acc.ID,
			camp.ID