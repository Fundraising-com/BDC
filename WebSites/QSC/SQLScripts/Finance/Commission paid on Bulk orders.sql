select fm.FirstName + ' ' + fm.LastName FM, a.ID AccountID, a.name AccountName, c.ID CampaignID, b.OrderID, b.Date OrderDate, (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) NetSale
, CASE isec.section_type_id WHEN 1 THEN 10 WHEN 9 THEN 12 WHEN 11 THEN 12 WHEN 10 THEN 10 ELSE 0 END CommissionRate,*
FROM    
	QSPcanadaOrdermanagement.dbo.Batch B	  	WITH (NOLOCK)
	INNER JOIN QSPCanadaCommon.dbo.CAccount A 	WITH (NOLOCK) ON B.AccountID =A.Id 
    INNER JOIN QSPCanadaCommon.dbo.Campaign C	 	  	WITH (NOLOCK) ON b.CampaignId=c.Id
 	INNER JOIN QSPCanadaFinance.dbo.Invoice I	      		WITH (NOLOCK) ON b.OrderId=i.Order_Id
   
	LEFT JOIN	(QSPCanadaCommon..CampaignCommissionSplit ccs
	JOIN		QSPCanadaCommon..FieldManager fmSplit
					ON	fmSplit.FMID = ccs.FMID)

					ON	ccs.CampaignID = C.ID
					AND	i.Invoice_Date <= ccs.EffectiveToDate

	LEFT  JOIN QSPCanadaCommon.dbo.Contact Cont 	WITH (NOLOCK) ON Cont.ID = C.ShipToCampaignContactID
	INNER JOIN QSPCanadaCommon.dbo.FieldManager FM   		WITH (NOLOCK) ON fm.FMID = ISNULL(fmSplit.FMID, c.FMID)
	INNER JOIN QSPCanadaCommon.dbo.FieldManager DM   		WITH (NOLOCK) ON dm.FMID=fm.DMID
	--QSPCanadaCommon..Tax Tax			WITH (NOLOCK) 
    INNER JOIN QSPCanadaFinance.dbo.Invoice_Section ISec 	WITH (NOLOCK) ON i.Invoice_Id = ISec.Invoice_Id  
    INNER JOIN QSPCanadaProduct..ProgramSectionType PS		WITH (NOLOCK) ON ps.ID = ISec.Section_Type_ID 
    INNER JOIN QSPCanadaFinance.dbo.GL_Entry gle 		WITH (NOLOCK) ON gle.Invoice_ID = i.Invoice_ID
    INNER JOIN QSPCanadaCommon.dbo.FieldManager FMRun WITH (NOLOCK) ON FMRun.FMID = c.FMID
WHERE b.OrderQualifierID = 39022 --The change to Commission report and Sales by FM report would be to exclude this OrderQualifier, perhaps others?
--and  a.CAccountCodeGroup = 'Comm'
and b.OrderQualifierID <> 39001
and dm.DMIndicator='Y'
and		CONVERT(DateTime,CONVERT(Varchar(10), i.Invoice_Date,101)) BETWEEN '2017-07-01' AND '2018-06-30'
AND 	b.OrderTypeCode NOT IN(41006,41007,41011, 41012)	--FM FMBULK
AND     b.OrderQualifierId <> 39006 			--Kanata
AND 	ISec.Section_Type_ID  in (1,2,9,10,11,13,14) -- 1. Gift, 2. Magazine, 9. Cookie Dough, 10. Chocolate, 11. Jewelry, 13. Candles, 14. TRT
AND		gle.BusinessUnitID IN (3,4)
AND		fm.FMID NOT IN ('0508')
order by b.Date, b.OrderID