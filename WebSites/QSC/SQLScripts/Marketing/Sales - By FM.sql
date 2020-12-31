USE [QSPCanadaFinance]
GO

SELECT  
	FM.FMID, 
	FM.LastName + ' ' + FM.FirstName  FMName, 
	SUM((iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1)) AS Net_Before_Tax_SansPostage	-- 21	NetBeforeTaxSansPostage
	
FROM    
	QSPcanadaOrdermanagement.dbo.Batch B	  	WITH (NOLOCK)
	INNER JOIN QSPCanadaCommon.dbo.CAccount A 	WITH (NOLOCK) ON B.AccountID =A.Id 
    INNER JOIN QSPCanadaCommon.dbo.Campaign C	 	  	WITH (NOLOCK) ON b.CampaignId=c.Id
    
	LEFT JOIN	(QSPCanadaCommon..CampaignCommissionSplit ccs
	JOIN		QSPCanadaCommon..FieldManager fmSplit
					ON	fmSplit.FMID = ccs.FMID)

					ON	ccs.CampaignID = C.ID

	LEFT  JOIN QSPCanadaCommon.dbo.Contact Cont 	WITH (NOLOCK) ON Cont.ID = C.ShipToCampaignContactID
	INNER JOIN QSPCanadaCommon.dbo.FieldManager FM   		WITH (NOLOCK) ON fm.FMID = ISNULL(fmSplit.FMID, c.FMID)
	INNER JOIN QSPCanadaCommon.dbo.FieldManager DM   		WITH (NOLOCK) ON dm.FMID=fm.DMID
	INNER JOIN QSPCanadaFinance.dbo.Invoice I	      		WITH (NOLOCK) ON b.OrderId=i.Order_Id
	--QSPCanadaCommon..Tax Tax			WITH (NOLOCK) 
    INNER JOIN QSPCanadaFinance.dbo.Invoice_Section ISec 	WITH (NOLOCK) ON i.Invoice_Id = ISec.Invoice_Id  
    INNER JOIN QSPCanadaProduct..ProgramSectionType PS		WITH (NOLOCK) ON ps.ID = ISec.Section_Type_ID 
    INNER JOIN QSPCanadaFinance.dbo.GL_Entry gle 		WITH (NOLOCK) ON gle.Invoice_ID = i.Invoice_ID
WHERE   dm.DMIndicator='Y'
AND     CONVERT(DateTime,CONVERT(Varchar(10), i.Invoice_Date,101)) BETWEEN '2013-07-01'  AND '2014-06-30'
AND 	b.OrderTypeCode NOT IN(41006,41007,41011, 41012)	--FM FMBULK
AND     b.OrderQualifierId <> 39006 			--Kanata
AND 	ISec.Section_Type_ID  in (1,2,9,10,11) -- 1. Gift, 2. Magazine, 9. Cookie Dough, 10. Chocolate, 11. Jewelry
AND	gle.BusinessUnitID IN (3,4)
GROUP BY 
	FM.FMID, 
	FM.FirstName ,
	FM.LastName
