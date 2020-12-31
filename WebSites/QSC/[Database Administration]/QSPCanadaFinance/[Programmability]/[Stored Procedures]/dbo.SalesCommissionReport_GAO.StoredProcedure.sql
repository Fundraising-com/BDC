USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[SalesCommissionReport_GAO]    Script Date: 06/07/2017 09:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE   [dbo].[SalesCommissionReport_GAO]	
(
	@FmId	 		Varchar(4) ='' ,
	@SectionType	Int = 0,		--2  for Magazine 1 for Gift
	@StartDate      DateTime,
	@EndDate        DateTime,
	@DmId	 		Varchar(4) = ''
	--@ShowTimeSales	Bit = 0
)

AS

DECLARE @ProcessingFees int
SET @ProcessingFees = 8

CREATE TABLE #ReportItems (
	OrderQualifierID 	Int, 
	OrderTypeCode		Int, 
	InvoiceID		Int, 
	AccountID		Int, 
	CampaignID		Int, 
	OrderID			Int, 
	FMID			Varchar(8), 
	FMName		Varchar(110), 
	DMID			Varchar(8),
	DMName		Varchar(115), 
	DMNameAndID		Varchar(125), 
	InvoiceDate		DateTime, 
	DiffFromEndDate	Numeric(6,2),
	InvoiceDueDate		DateTime, 
	AccountName		Varchar(110),
	ContactId		Int,
	SectionTypeID		Int, 
	CommTitle		Varchar(110),
	GPRate			Numeric(10,2), 
	NetBeforeTax		Numeric(10,2), 
	NetBeforeTaxSansPostage		Numeric(10,2), 
	LastYearNetOnline	Numeric(10,2), 
	LastYearNetOnlineSansPostage	Numeric(10,2), 
	LastYearNetRegular	Numeric(10,2), 
	LastYearNetRegularSansPostage	Numeric(10,2), 
	CurrentReturn		Numeric(10,2), 
	CurrentReturnSansPostage		Numeric(10,2), 
	CurrentNetOnline 	Numeric(10,2), 
	CurrentNetOnlineSansPostage 	Numeric(10,2), 
	CurrentNetRegular	Numeric(10,2), 
	CurrentNetRegularSansPostage	Numeric(10,2), 
	CurrentMagItemCount 	Int, 
	LastYearMagItemCount		Int, 
	PercentComm					Numeric(10,2),
	Current_ProcessingFeeCount		int,				-- LB
	LastYear_ProcessingFeeCount		int,				-- LB
	LastYear_ProcessingFees			Numeric(10,2),		-- LB
	Current_ProcessingFees			Numeric(10,2)		-- LB
	)

CREATE TABLE #FMWithoutCommRate ( FMID Varchar(8),
				  	SectionType Int,
				  	PrevMagSold Int)

CREATE TABLE #GiftCommRate ( CommRate Numeric(6,2),
				   EffectiveDate DateTime)

DECLARE @YearPriorToStartDate  DateTime
DECLARE @YearPriorToEndDate  DateTime
DECLARE @DaysApart Int

SELECT @YearPriorToStartDate = DateAdd(month,-12,@StartDate)
Select @DaysApart= DATEDIFF(DAY,@StartDate,@EndDate)
Select @YearPriorToEndDate = DATEADD(DAY,@DaysApart,@YearPriorToStartDate)

	
INSERT #ReportItems
SELECT  
	OrderQualifierID, 
	OrderTypeCode, 
	i.Invoice_ID, 
	A.ID  AcctID, 
	CampaignID, 
	i.Order_Id, 
	C.FMID, 
	FM.LastName + ' ' + FM.FirstName  FMName, 
	DM.FMID DMID,
	DM.LastName + ' ' + DM.FirstName  DMName,
	DM.FMID+ ' - '+DM.LastName + ' ' + DM.FirstName  DMNameAndID,
	CONVERT(Varchar(10), Invoice_Date,101)  Invoice_Date, 
	DATEDIFF(DAY, @StartDate,i.INVOICE_DATE), 
	CONVERT(varchar(10), Invoice_Due_Date,101)  Invoice_Due_Date, 
	A.Name  AcctName,
	MAX(Cont.Id) ContactId,
	Isec.Section_Type_Id, 
	
	CASE ISec.Section_Type_Id
		WHEN 1 THEN 'Gift Program'
		WHEN 2 THEN 'Regular Magazine Program (including Magnet,Magazine Express,Magazines in Combo Programs)'
		WHEN 6 THEN 'Cookie Dough'
		WHEN 8 THEN 'Processing Fees'		--LB
		ELSE ''
	END AS CommTitle,
	
	Group_Profit_Rate, 
	
	iSec.Net_Before_Tax AS Net_Before_Tax,	-- 20	  NetBeforeTax
	
	iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00) AS Net_Before_Tax_SansPostage,	-- 21	NetBeforeTaxSansPostage
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN  
			CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
				WHEN -1 THEN CASE b.OrderQualifierId 
				WHEN 39009 THEN iSec.Net_Before_Tax
				ELSE 0
			END
			ELSE 0
		END
		ELSE  0    
	END AS LastYear_NetBeforeTax_Online,			-- 22	LastYearNetOnline
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN  
			CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
				WHEN -1 THEN CASE b.OrderQualifierId 
				WHEN 39009 THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
		      	ELSE 0
		    END
			ELSE 0
		END
		ELSE  0    
	END AS LastYear_NetBeforeTax_Online_SansPostage,	-- 23	LastYearNetOnlineSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN  
			CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
				WHEN -1  THEN CASE b.OrderQualifierId 
		      	WHEN 39009 THEN 0
		     	ELSE iSec.Net_Before_Tax
		    END
			ELSE 0
		END
		ELSE  0    
	END AS LastYear_NetBeforeTax_Regular,				-- 24	LastYearNetRegular
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN  
		CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
			WHEN -1  THEN CASE b.OrderQualifierId 
		    WHEN 39009 THEN 0
		    ELSE iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
		END
			ELSE 0
		END
		ELSE  0    
	END AS LastYear_NetBeforeTax_Regular_SansPostage,	-- 25	LastYearNetRegularSansPostage
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN 1 THEN 
			Case b.OrderTypeCode
				WHEN '41004' THEN iSec.Net_Before_Tax
				WHEN '41003' THEN iSec.Net_Before_Tax
				ELSE 0
  			END
		WHEN 0 THEN 
			Case b.OrderTypeCode
				WHEN '41004'  THEN iSec.Net_Before_Tax
				WHEN '41003'  THEN iSec.Net_Before_Tax
				ELSE 0
  			END
		ELSE 0	
	END AS Current_ReturnCredit,						-- 26	CurrentReturn
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN 1 THEN 
			Case b.OrderTypeCode
				WHEN '41004' THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
				WHEN '41003' THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
				ELSE 0
  			END
		WHEN 0 THEN 
			Case b.OrderTypeCode
				WHEN '41004'  THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
				WHEN '41003'  THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
				ELSE 0
  			END
			 ELSE 0	
	END AS Current_ReturnCredit_SansPostage,			-- 27	CurrentReturnSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN iSec.Net_Before_Tax
				  ELSE 0
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN iSec.Net_Before_Tax
				  ELSE 0			END
		ELSE  0    
	END AS Current_NetBeforeTax_Online,					-- 28	CurrentNetOnline
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
				  ELSE 0
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
				  ELSE 0			END
		ELSE  0    
	END AS Current_NetBeforeTax_Online_SansPostage,		-- 29	CurrentNetOnlineSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE iSec.Net_Before_Tax
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE iSec.Net_Before_Tax
			END
		ELSE  0    
	END AS Current_NetBeforeTax_Regular,				-- 30	CurrentNetRegular
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)
			END
		ELSE  0    
	END AS Current_NetBeforeTax_Regular_SansPostage,	-- 31	CurrentNetRegularSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 2 THEN Isec.ITEM_COUNT
				ELSE 0
			END
		WHEN  0 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 2 THEN Isec.ITEM_COUNT
				ELSE 0
			END
		ELSE  0    
	END AS Current_MagItemCount ,						-- 32	CurrentMagItemCount
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 2 THEN Isec.ITEM_COUNT
				ELSE 0
			END
		ELSE  0    
	END AS LastYear_MagItemCount,						-- 33	LastYearMagItemCount
	
	0  as Percent_Comm,

	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 8 THEN Isec.ITEM_COUNT
				ELSE 0
			END
		WHEN  0 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 8 THEN Isec.ITEM_COUNT
				ELSE 0
			END
		ELSE  0    
	END AS Current_ProcessingFeeCount ,					-- 34 Current_ProcessingFeeCount  LB		
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISEC.SECTION_TYPE_ID
			WHEN @ProcessingFees THEN Isec.ITEM_COUNT  
			ELSE 0
			END
	ELSE  0    
	END LastYear_ProcessingFeeCount ,					-- 35 LastYear_ProcessingFeeCount	LB
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN  
			CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
				WHEN -1  THEN 
					CASE ISEC.SECTION_TYPE_ID 
		      			WHEN @ProcessingFees THEN iSec.Total_Tax_Included
		     			ELSE 0
					END
					ELSE 0
				END
		ELSE  0    
	END AS LastYear_ProcessingFees,				-- 36  LastYear_ProcessingFees	LB		
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE ISEC.SECTION_TYPE_ID 
				  WHEN @ProcessingFees THEN iSec.Total_Tax_Included
				  ELSE 0
			END
		WHEN  0 THEN 
			CASE ISEC.SECTION_TYPE_ID 
				  WHEN @ProcessingFees THEN iSec.Total_Tax_Included
				  ELSE 0
			END
		ELSE  0    
	END AS Current_ProcessingFees					-- 37  CurrentProcessingFees  LB	
	
FROM    
	QSPcanadaOrdermanagement.dbo.Batch B	  	(NOLOCK)
	INNER JOIN QSPCanadaCommon.dbo.CAccount A 	(NOLOCK) ON B.AccountID =A.Id ,
     	QSPCanadaCommon.dbo.Campaign C	 	  	(NOLOCK)
	LEFT  JOIN QSPCanadaCommon.dbo.Contact Cont 	(NOLOCK) ON Cont.ID = C.ShipToCampaignContactID,
	QSPCanadaCommon.dbo.FieldManager FM   		(NOLOCK),
	QSPCanadaCommon.dbo.FieldManager DM   		(NOLOCK),
	QSPCanadaFinance.dbo.Invoice I	      		(NOLOCK),
	QSPCanadaCommon..Tax Tax				(NOLOCK),
     	QSPCanadaProduct..ProgramSectionType PS		(NOLOCK),
     	QSPCanadaFinance.dbo.Invoice_Section ISec 		(NOLOCK)
WHERE b.OrderId=i.Order_Id
AND 	b.CampaignId=c.Id
AND 	fm.FMID = c.FMID 
AND     dm.FMID=fm.DMID
AND     dm.DMIndicator='Y'
AND	i.Invoice_Id = ISec.Invoice_Id 
AND 	ps.ID = ISec.Section_Type_ID 
AND    CONVERT(DateTime,CONVERT(Varchar(10), i.Invoice_Date,101)) BETWEEN @YearPriorToStartDate  AND @Enddate
AND 	b.OrderTypeCode NOT IN(41006,41007,41011, 41012)	--FM FMBULK
AND     b.OrderQualifierId <> 39006 			--Kanata
AND     c.FMID=CASE ISNULL(@FMID,'') WHEN '' THEN c.FMID ELSE @FMID END
AND     ISec.Section_Type_ID = CASE ISNULL(@SectionType,0) WHEN 0 THEN ISec.Section_Type_ID ELSE @SectionType END
AND       dm.DMID=CASE ISNULL(@DMID,'') WHEN '' THEN dm.DMID ELSE @DMID END

AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 0

AND NOT EXISTS (SELECT 1  FROM QSPcanadaCommon..Campaignprogram CP
		     WHERE CP.ProgramId=24 
		     AND CP.deletedTF=0
		     AND CP.campaignId=C.id)
GROUP BY 
	OrderQualifierID, 
	OrderTypeCode, 
	i.Invoice_ID, 
	A.ID , 
	CampaignID, 
	i.Order_Id, 
	DM.FMID,
	C.FMID, 
	DM.FirstName ,
	DM.LastName , 
	FM.FirstName ,
	FM.LastName , 
	Invoice_Date,
	Invoice_Due_Date,
	A.Name ,
	Isec.Section_Type_Id, 
	ISec.Item_Count,
	Group_Profit_Rate, 
	iSec.Net_Before_Tax,
	iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00),
	iSec.Total_Tax_Included

--Commissions - Mag - FM
Update #ReportItems Set PercentComm = Percent_Comm
			from QSPCanadaFinance.dbo.Commission COM, #ReportItems 
				where  FMID = COM.FM_ID AND COM.SECTION_TYPE_ID=2 AND COM.COMMISSION_TYPE_CODE='PERCENT' AND com.COMM_EFFECTIVE_DATE =
			( Select max (COMM_EFFECTIVE_DATE) from QSPCanadaFinance.dbo.Commission C where C.FM_ID = COM.FM_ID and  c.COMM_EFFECTIVE_DATE <= @EndDate AND C.COMMISSION_TYPE_CODE='PERCENT')

--Commissions - Mag - General
INSERT #FMWithoutCommRate
Select FMID,SectionTypeID,SUM(LastYearMagItemCount)
FROM #ReportItems
WHERE SectionTypeID =2
AND ISNULL(PERCENTComm,0) = 0
GROUP BY FMID,SectionTypeID

UPDATE #ReportItems
SET PercentComm =  PERCENT_COMM 
FROM   QSPcanadaFinance.dbo.COMMISSION c, #FMWithoutCommRate
WHERE ISNULL(PercentComm,0) = 0
AND c.Section_Type_Id = #FMWithoutCommRate.SectionType
AND c.FM_ID=#FMWithoutCommRate.FMID
AND c.FM_ID=#ReportItems.FMID
AND Commission_Type_Code = 'PERCENT'			
AND Min_Target_Number <= #FMWithoutCommRate.PrevMagSold
AND Max_Target_Number >= #FMWithoutCommRate.PrevMagSold
AND Comm_Effective_Date <= @EndDate


--GIFT commission rates
INSERT #GiftCommRate
SELECT MAX(percent_comm),comm_effective_date
FROM   QSPCanadaFinance.dbo.Commission
WHERE  Section_Type_Id = 1
AND    Commission_Type_Code = 'PERCENT'
GROUP BY comm_effective_date 

--Multiple Commission rates (by Effective Date) for Gift 
DECLARE @Cnt Int
SELECT  @Cnt=Count(*)  FROM #GiftCommRate	
IF @Cnt =1
BEGIN
	UPDATE #ReportItems
	SET PercentComm = CommRate 
	FROM #GiftCommRate
	WHERE #ReportItems.SectionTypeId = 1
END
ELSE
	-- More Than One rates
BEGIN
	DECLARE @Rate Int,
		     @EffectiveFrom DateTime

           	DECLARE  AllCommRate CURSOR FOR
				 SELECT   * FROM   #GiftCommRate ORDER BY 2 ASC

	OPEN AllCommRate
	FETCH NEXT FROM AllCommRate INTO @Rate,@EffectiveFrom
					
	WHILE(@@Fetch_status = 0)
	BEGIN
		UPDATE #ReportItems
		SET PercentComm = @Rate 
		FROM   #GiftCommRate
		WHERE CONVERT(Datetime,Convert(Varchar(10),#ReportItems.InvoiceDate,101)) >= @EffectiveFrom
		AND #ReportItems.SectionTypeId = 1
			
		FETCH NEXT FROM AllCommRate  INTO @Rate,@EffectiveFrom
	END
	CLOSE AllCommRate
	DEALLOCATE AllCommRate
END

--Processing Fee
UPDATE	#ReportItems
SET		PercentComm = 0
FROM	Commission
WHERE	SectionTypeId = @ProcessingFees


SELECT		DMID,
			DMName,
			DMNameAndID,
			FMID,
			FMName,
			SectionTypeID,
			CommTitle,
			AccountID,
			AccountName,
			CampaignID,
			SUM(NetBeforeTax) AS NetSales,
			SUM(NetBeforeTaxSansPostage) AS NetSalesSansPostage,
			SUM(LastYearNetOnline) AS LastYearOnlineSales,
			SUM(LastYearNetOnlineSansPostage) AS LastYearOnlineSalesSansPostage,
			SUM(LastYearNetRegular) AS LastYearRegularSales,
			SUM(LastYearNetRegularSansPostage) AS LastYearRegularSalesSansPostage,
			GPRate,
			PercentComm, 
			SUM(CurrentNetOnline) AS CurrentOnlineSales,
			SUM(CurrentNetOnlineSansPostage) AS CurrentOnlineSalesSansPostage,
			SUM(CurrentNetRegular) AS CurrentRegularSales,
			SUM(CurrentNetRegularSansPostage)AS CurrentRegularSalesSansPostage,
			SUM(Current_ProcessingFeeCount)  AS CurrentProcessingFeeCount,			-- LB
			SUM(LastYear_ProcessingFeeCount) AS LastYearProcessingFeeCount,			-- LB
			SUM(LastYear_ProcessingFees)	 AS LastYearProcessingFees,				-- LB
			SUM(Current_ProcessingFees)		 AS CurrentProcessingFees				-- LB
FROM		#ReportItems 
GROUP BY	DMID,
			DMName,
			DMNameAndID,
			FMID,
			FMName,
			SectionTypeID,
			CommTitle,
			AccountID,
			AccountName,
			CampaignID,
			GPRate,
			PercentComm
ORDER BY	DMID,
			FMID,
			AccountName



DROP TABLE #ReportItems
DROP TABLE #FMWithoutCommRate
DROP TABLE #GiftCommRate
GO
