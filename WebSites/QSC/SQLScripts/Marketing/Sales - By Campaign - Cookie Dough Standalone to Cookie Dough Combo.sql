USE QSPCanadaFinance

SELECT		fm.Firstname + ' ' + fm.Lastname FM,
			acc.Id AccountID,
			acc.Name AccountName,
			(SELECT QSPCanadaCommon.dbo.UDF_GetCampaignPrograms(c.ID) FROM QSPCanadaCommon..Campaign c JOIN QSPCanadaCommon..CampaignProgram cp ON cp.CampaignID = c.ID AND cp.DeletedTF = 0 AND cp.ProgramID = 44 AND cp.OnlineOnly = 0 WHERE c.BillToAccountID = acc.ID AND c.StartDate BETWEEN '2015-07-01' AND '2015-12-31' AND c.Status IN (37002)) Fall2015Programs,
			(SELECT QSPCanadaCommon.dbo.UDF_GetCampaignPrograms(c.ID) FROM QSPCanadaCommon..Campaign c JOIN QSPCanadaCommon..CampaignProgram cp ON cp.CampaignID = c.ID AND cp.DeletedTF = 0 AND cp.ProgramID = 44 AND cp.OnlineOnly = 0 WHERE c.BillToAccountID = acc.ID AND c.StartDate BETWEEN '2016-07-01' AND '2016-12-31' AND c.Status IN (37002)) Fall2016Programs,
			--CASE WHEN camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN QSPCanadaCommon.dbo.UDF_GetCampaignPrograms(camp.ID) ELSE NULL END Fall2016Programs, 
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015CDNetAmount,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2016CDNetAmount,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015CDAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2016CDAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2015CDNumUnits,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2016CDNumUnits,
			SUM(CASE WHEN invSec.section_type_id IN (1, 2, 11, 14, 15) AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015NonCDNetAmount,
			SUM(CASE WHEN invSec.section_type_id IN (1, 2, 11, 14, 15) AND camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2016NonCDNetAmount,
			SUM(CASE WHEN invSec.section_type_id IN (1, 2, 11, 14, 15) AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015NonCDAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id IN (1, 2, 11, 14, 15) AND camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2016NonCDAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id IN (1, 2, 11, 14, 15) AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2015NonCDNumUnits,
			SUM(CASE WHEN invSec.section_type_id IN (1, 2, 11, 14, 15) AND camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2016NonCDNumUnits
FROM		Invoice inv
JOIN		INVOICE_SECTION invSec
				ON	invSec.INVOICE_ID = inv.INVOICE_ID
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = CASE camp.FMID WHEN '0035' THEN '1559' WHEN '0075' THEN '1562' WHEN '1545' THEN '1564' ELSE camp.FMID END
WHERE		(camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' OR camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31')
--AND			(camp.ID IN (SELECT CampaignID FROM QSPCanadaCommon..CampaignProgram cp WHERE cp.ProgramID IN (44) AND cp.DeletedTF = 0 AND cp.OnlineOnly = 0))
--AND			(camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31' OR camp.ID NOT IN (SELECT CampaignID FROM QSPCanadaCommon..CampaignProgram cp WHERE cp.ProgramID IN (1, 2, 4, 45, 47, 49, 50, 52, 53, 54, 55, 56, 58, 59, 60, 61, 62) AND cp.DeletedTF = 0 AND cp.OnlineOnly = 0))
--AND			(camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' OR camp.ID IN (SELECT CampaignID FROM QSPCanadaCommon..CampaignProgram cp WHERE cp.ProgramID IN (1, 2, 4, 45, 47, 49, 50, 52, 53, 54, 55, 56, 58, 59, 60, 61, 62) AND cp.DeletedTF = 0 AND cp.OnlineOnly = 0))
AND			invSec.section_type_id in (1, 2, 9, 11, 14, 15)
AND			acc.ID IN (
				SELECT	DISTINCT camp2.BillToAccountID
				FROM	QSPCanadaCommon..Campaign camp2
				WHERE	camp2.StartDate BETWEEN '2015-07-01' AND '2015-12-31' AND camp2.Status = 37002 AND camp2.OnlineOnlyPrograms = 0 AND camp2.ID IN (SELECT cp1.CampaignID FROM QSPCanadaCommon..CampaignProgram cp1 WHERE cp1.ProgramID = 44 AND cp1.DeletedTF = 0 AND cp1.OnlineOnly = 0)
				AND		camp2.StartDate BETWEEN '2015-07-01' AND '2015-12-31' AND camp2.Status = 37002 AND camp2.OnlineOnlyPrograms = 0 AND camp2.ID NOT IN (SELECT cp2.CampaignID FROM QSPCanadaCommon..CampaignProgram cp2 WHERE cp2.ProgramID IN (1, 2, 4, 45, 47, 49, 50, 52, 53, 54, 55, 56, 58, 59, 60, 61, 62) AND cp2.DeletedTF = 0 AND cp2.OnlineOnly = 0)
			)
AND			acc.ID IN (
				SELECT	DISTINCT camp3.BillToAccountID
				FROM	QSPCanadaCommon..Campaign camp3
				WHERE	camp3.StartDate BETWEEN '2016-07-01' AND '2016-12-31' AND camp3.Status = 37002 AND camp3.OnlineOnlyPrograms = 0 AND camp3.ID IN (SELECT cp3.CampaignID FROM QSPCanadaCommon..CampaignProgram cp3 WHERE cp3.ProgramID = 44 AND cp3.DeletedTF = 0 AND cp3.OnlineOnly = 0)
				AND		camp3.StartDate BETWEEN '2016-07-01' AND '2016-12-31' AND camp3.Status = 37002 AND camp3.OnlineOnlyPrograms = 0 AND camp3.ID IN (SELECT cp4.CampaignID FROM QSPCanadaCommon..CampaignProgram cp4 WHERE cp4.ProgramID IN (1, 2, 4, 45, 47, 49, 50, 52, 53, 54, 55, 56, 58, 59, 60, 61, 62) AND cp4.DeletedTF = 0 AND cp4.OnlineOnly = 0)
			)
AND			acc.ID NOT IN (33745)
GROUP BY	fm.Firstname + ' ' + fm.Lastname,
			acc.ID,
			acc.Name
ORDER BY	fm.Firstname + ' ' + fm.Lastname,
			acc.ID