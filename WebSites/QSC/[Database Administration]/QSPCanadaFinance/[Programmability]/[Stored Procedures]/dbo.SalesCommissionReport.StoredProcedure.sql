USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[SalesCommissionReport]    Script Date: 06/07/2017 09:17:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE   [dbo].[SalesCommissionReport]	
(
	@FmId	 						Varchar(4) = '',
	@SectionType					Int = 0,
	@StartDate						DateTime,
	@EndDate						DateTime,
	@DmId	 						Varchar(4) = '',
	@ShowCCRPOnly					Bit = 0,
	@ShowOnlyBDCReferredAccounts	Bit = 0
	--@ShowTimeSales	Bit = 0
)

AS

DECLARE @ProcessingFees int

/*Now handled in a nightly job due to efficiency issues
--------------------------------------------------------------
-- This Code Updates GL  - needed to handle the GA acquisition
--------------------------------------------------------------
--Must set correct BusinessUnit (Time vs GAO) first
DECLARE     @Accounting_Year  INT,
            @Accounting_Period      INT

SELECT      @Accounting_Year = Accounting_Year,
            @Accounting_Period = Accounting_Period
FROM  Accounting_Period
WHERE [Start_Date] = (SELECT MIN([Start_Date]) FROM Accounting_Period WHERE Is_Closed = 'N')

EXEC  [dbo].[GL_Entry_SwitchToGAO]
            @AccountingYear = @Accounting_Year,
            @AccountingPeriod = @Accounting_Period
--------------------------------------------------------------
-- This Code Updates GL  - needed to handle the GA acquisition
--------------------------------------------------------------
*/

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
	CurrentMagItemCount 	Numeric(10,2), 
	LastYearMagItemCount		Numeric(10,2), 
	PercentComm					Numeric(10,2),
	Current_ProcessingFeeCount		Numeric(10,2),				-- LB
	LastYear_ProcessingFeeCount		Numeric(10,2),				-- LB
	LastYear_ProcessingFees			Numeric(10,2),		-- LB
	Current_ProcessingFees			Numeric(10,2),		-- LB
	FMIDRun		Varchar(4),
	IsFaculty Varchar(10),
	CCRPAccountType VARCHAR(30),
	CCRPPayoutPercentage	Numeric(10,2),
	ProgramType Int
	)

CREATE TABLE #FMWithoutCommRate ( FMID Varchar(8),
				  	SectionType Int,
				  	PrevMagSold Int)

CREATE TABLE #NonMagCommRate ( Section_Type_id int,EffectiveDate DateTime, CommRate Numeric(6,2))

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
	B.CampaignID, 
	i.Order_Id, 
	FM.FMID, 
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
	ps.DESCRIPTION,
	Group_Profit_Rate, 
	iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0) AS Net_Before_Tax,	-- 20	  NetBeforeTax
	(iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1)* ISNULL(gcpmt.GiftCardPercentage, 1.0) AS Net_Before_Tax_SansPostage,	-- 21	NetBeforeTaxSansPostage
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN  
			CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
				WHEN -1 THEN CASE b.OrderQualifierId 
				WHEN 39009 THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
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
				WHEN 39009 THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
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
		     	ELSE iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
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
		    ELSE (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
		END
			ELSE 0
		END
		ELSE  0    
	END AS LastYear_NetBeforeTax_Regular_SansPostage,	-- 25	LastYearNetRegularSansPostage
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN 1 THEN 
			Case b.OrderTypeCode
				WHEN '41004' THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				WHEN '41003' THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
  			END
		WHEN 0 THEN 
			Case b.OrderTypeCode
				WHEN '41004'  THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				WHEN '41003'  THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
  			END
		ELSE 0	
	END AS Current_ReturnCredit,						-- 26	CurrentReturn
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
		WHEN 1 THEN 
			Case b.OrderTypeCode
				WHEN '41004' THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				WHEN '41003' THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
  			END
		WHEN 0 THEN 
			Case b.OrderTypeCode
				WHEN '41004'  THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				WHEN '41003'  THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
  			END
			 ELSE 0	
	END AS Current_ReturnCredit_SansPostage,			-- 27	CurrentReturnSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				  ELSE 0
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				  ELSE 0
			END
		ELSE  0    
	END AS Current_NetBeforeTax_Online,					-- 28	CurrentNetOnline
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				  ELSE 0
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				  ELSE 0
			END
		ELSE  0    
	END AS Current_NetBeforeTax_Online_SansPostage,		-- 29	CurrentNetOnlineSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE iSec.Net_Before_Tax * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
			END
		ELSE  0    
	END AS Current_NetBeforeTax_Regular,				-- 30	CurrentNetRegular
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
			END
		WHEN  0 THEN 
			CASE b.OrderQualifierId 
				  WHEN 39009 THEN 0
				  ELSE (iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
			END
		ELSE  0    
	END AS Current_NetBeforeTax_Regular_SansPostage,	-- 31	CurrentNetRegularSansPostage
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 2 THEN Isec.ITEM_COUNT * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
			END
		WHEN  0 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 2 THEN Isec.ITEM_COUNT * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
			END
		ELSE  0    
	END AS Current_MagItemCount ,						-- 32	CurrentMagItemCount
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 2 THEN Isec.ITEM_COUNT * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
			END
		ELSE  0    
	END AS LastYear_MagItemCount,						-- 33	LastYearMagItemCount
	
	0  as Percent_Comm,

	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 8 THEN Isec.ITEM_COUNT * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
			END
		WHEN  0 THEN 
			CASE ISEC.SECTION_TYPE_ID
				WHEN 8 THEN Isec.ITEM_COUNT * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				ELSE 0
			END
		ELSE  0    
	END AS Current_ProcessingFeeCount ,					-- 34 Current_ProcessingFeeCount  LB		
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISEC.SECTION_TYPE_ID
			WHEN @ProcessingFees THEN Isec.ITEM_COUNT  * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
			ELSE 0
			END
	ELSE  0    
	END LastYear_ProcessingFeeCount ,					-- 35 LastYear_ProcessingFeeCount	LB
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  -1 THEN  
			CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
				WHEN -1  THEN 
					CASE ISEC.SECTION_TYPE_ID 
		      			WHEN @ProcessingFees THEN iSec.Total_Tax_Included * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
		     			ELSE 0
					END
					ELSE 0
				END
		ELSE  0    
	END AS LastYear_ProcessingFees,				-- 36  LastYear_ProcessingFees	LB		
	
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
		WHEN  1 THEN 
			CASE ISEC.SECTION_TYPE_ID 
				  WHEN @ProcessingFees THEN iSec.Total_Tax_Included * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				  ELSE 0
			END
		WHEN  0 THEN 
			CASE ISEC.SECTION_TYPE_ID 
				  WHEN @ProcessingFees THEN iSec.Total_Tax_Included * ISNULL(ccs.CommissionPercentage/100.00, 1) * ISNULL(gcpmt.GiftCardPercentage, 1.0)
				  ELSE 0
			END
		ELSE  0    
	END AS Current_ProcessingFees,					-- 37  CurrentProcessingFees  LB	
	
	FMRun.FMID FMIDRun,
	
	CASE c.IsStaffOrder WHEN 1 Then 'Yes' ELSE 'No' End IsFaculty,
	
	CASE A.ParentID WHEN 33873 THEN 'RegularCCRPAccount'
					WHEN 33874 THEN 'DeclinedRepCCRPAccount'
					ELSE			'QSPAccount'
	END CCRPAccountType,
	
	CASE A.ParentID WHEN 33873 THEN CASE iSec.Section_Type_ID WHEN 1 THEN 1.0 WHEN 2 THEN 8.5 WHEN 9 THEN 2.0 WHEN 11 THEN 1.0 WHEN 13 THEN 1.0 ELSE 0.0 END
					WHEN 33874 THEN CASE iSec.Section_Type_ID WHEN 2 THEN 5.0 ELSE 0.0 END
					ELSE 0
	END CCRPPayoutPercentage,
	
	ISec.ProgramType

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
	LEFT JOIN	(SELECT		pmt.Order_ID, SUM(CASE pmt.Payment_Method_ID WHEN 50007 THEN 0 ELSE pmt.Payment_Amount END) / SUM(pmt.Payment_Amount) GiftCardPercentage --Exclude Gift Card Portion of Gift Card Redemption Orders
				FROM		QSPCanadaFinance..Payment pmt
				WHERE		pmt.PAYMENT_AMOUNT > 0.00
				GROUP BY	pmt.Order_ID) gcpmt
				ON	gcpmt.ORDER_ID = b.OrderID
WHERE   dm.DMIndicator='Y'
AND     (CONVERT(DateTime,CONVERT(Varchar(10), i.Invoice_Date,101)) BETWEEN @YearPriorToStartDate AND @YearPriorToEndDate
OR		CONVERT(DateTime,CONVERT(Varchar(10), i.Invoice_Date,101)) BETWEEN @StartDate AND @Enddate)
AND 	b.OrderTypeCode NOT IN(41006,41007,41011, 41012)	--FM FMBULK
AND     b.OrderQualifierId <> 39006 			--Kanata
AND     fm.FMID = CASE ISNULL(@FMID,'') WHEN '' THEN fm.FMID ELSE @FMID END
AND     ISec.Section_Type_ID = CASE ISNULL(@SectionType,0) WHEN 0 THEN ISec.Section_Type_ID ELSE @SectionType END
AND     dm.DMID=CASE ISNULL(@DMID,'') WHEN '' THEN dm.DMID ELSE @DMID END
AND 	ISec.Section_Type_ID  in (1,2,9,10,11,13,14,15,16,17,18) -- 1. Gift, 2. Magazine, 9. Cookie Dough, 10. Chocolate, 11. Jewelry, 13. Candles, 14. TRT, 15. Entertainment, 16. Gift Cards, 17. Pretzel Rods 40%, 18. Pretzel Rods 30%
AND		gle.BusinessUnitID IN (3,4)
AND		fm.FMID NOT IN ('0508')
AND		(ISNULL(@ShowCCRPOnly, 0) = 0 OR a.ParentID IN (33873, 33874))
AND		(ISNULL(@ShowCCRPOnly, 0) = 0 OR (c.IsStaffOrder = 0 OR fm.FMID IN ('1546', '1547', '1548', '1549', '1550', '1551'))) --Exclude Faculty Sales not made by a CCRP Rep
AND		(ISNULL(@ShowOnlyBDCReferredAccounts, 0) = 0 OR a.ParentID IN (34838))
GROUP BY 
	OrderQualifierID, 
	OrderTypeCode, 
	i.Invoice_ID, 
	A.ID , 
	B.CampaignID, 
	i.Order_Id, 
	DM.FMID,
	FM.FMID, 
	DM.FirstName ,
	DM.LastName , 
	FM.FirstName ,
	FM.LastName , 
	Invoice_Date,
	Invoice_Due_Date,
	A.Name ,
	Isec.Section_Type_Id, 
	ps.DESCRIPTION,
	ISec.Item_Count,
	Group_Profit_Rate, 
	iSec.Net_Before_Tax,
	iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00),
	iSec.Total_Tax_Included,
	ccs.CommissionPercentage,
	FMRun.FMID,
	c.IsStaffOrder,
	A.ParentID,
	ISec.ProgramType,
	gcpmt.GiftCardPercentage

--Commissions - Mag/TRT - Split Commission, use higher FM commission
Update		ri 
Set			PercentComm =  (SELECT	MAX(comm.PERCENT_COMM)
							FROM	#ReportItems ri2
							JOIN	QSPCanadaFinance..Commission comm ON comm.FM_ID = ri2.FMID AND comm.SECTION_TYPE_ID = 2 AND comm.COMMISSION_TYPE_CODE = 'PERCENT' AND comm.COMM_EFFECTIVE_DATE =
									(Select max (COMM_EFFECTIVE_DATE) from QSPCanadaFinance.dbo.Commission c where c.FM_ID = comm.FM_ID and  c.COMM_EFFECTIVE_DATE <= @EndDate AND c.COMMISSION_TYPE_CODE='PERCENT')
							WHERE ri2.OrderID = ri.OrderID)
FROM		#ReportItems ri
WHERE		ri.SectionTypeID IN (2, 14, 15, 16)

--Commissions - TRT - Set to 12% when not comboed with Mag
/*UPDATE		ri
SET			PercentComm =  12
FROM		#ReportItems ri
WHERE		ri.SectionTypeID = 14
AND			ri.CampaignID NOT IN (SELECT	cp.CampaignID
									FROM	QSPCanadaCommon..CampaignProgram cp
									WHERE	cp.ProgramID IN (1, 2, 47)
									AND		cp.DeletedTF = 0)
*/

/*
--Commissions - Mag - FM
Update #ReportItems Set PercentComm = Percent_Comm
			from QSPCanadaFinance.dbo.Commission COM, #ReportItems 
				where  FMIDRun = COM.FM_ID AND COM.SECTION_TYPE_ID=2 AND COM.COMMISSION_TYPE_CODE='PERCENT' AND com.COMM_EFFECTIVE_DATE =
			( Select max (COMM_EFFECTIVE_DATE) from QSPCanadaFinance.dbo.Commission C where C.FM_ID = COM.FM_ID and  c.COMM_EFFECTIVE_DATE <= @EndDate AND C.COMMISSION_TYPE_CODE='PERCENT')

--Commissions - Mag - Split Commission, use higher FM commission
Update #ReportItems Set PercentComm = COM.PERCENT_COMM
			from QSPCanadaFinance.dbo.Commission COM, #ReportItems 
				where  FMID = COM.FM_ID AND COM.SECTION_TYPE_ID=2 AND COM.COMMISSION_TYPE_CODE='PERCENT' AND com.COMM_EFFECTIVE_DATE =
			( Select max (COMM_EFFECTIVE_DATE) from QSPCanadaFinance.dbo.Commission C where C.FM_ID = COM.FM_ID and  c.COMM_EFFECTIVE_DATE <= @EndDate AND C.COMMISSION_TYPE_CODE='PERCENT')
			and FMID <> FMIDRun
			and COM.PERCENT_COMM > #ReportItems.PercentComm
*/

--Commissions - Mag - General
/*INSERT #FMWithoutCommRate
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
AND c.FM_ID=#ReportItems.FMIDRun
AND Commission_Type_Code = 'PERCENT'			
AND Min_Target_Number <= #FMWithoutCommRate.PrevMagSold
AND Max_Target_Number >= #FMWithoutCommRate.PrevMagSold
AND Comm_Effective_Date <= @EndDate*/


--GIFT commission rates
INSERT #NonMagCommRate
SELECT Section_Type_Id, comm_effective_date, MAX(percent_comm)
FROM   QSPCanadaFinance.dbo.Commission
WHERE  Section_Type_Id IN (1,9,10,11,13,17,18) -- Gift, Cookie Dough, Chocolate, Jewelry, Candles, Pretzel Rods 40%, Pretzel Rods 30%
AND    Commission_Type_Code = 'PERCENT'
GROUP BY Section_Type_Id, comm_effective_date 
ORDER BY Section_Type_Id, comm_effective_date 

--Multiple Commission rates (by Effective Date) for Gift 

DECLARE @Section_Type_Id int, 
		@EffectiveFrom DateTime, 
		@Rate Int

DECLARE	AllCommRate CURSOR FOR
SELECT	* 
FROM	#NonMagCommRate

OPEN AllCommRate
FETCH NEXT FROM AllCommRate INTO @Section_Type_Id,@EffectiveFrom, @Rate
				
WHILE(@@Fetch_status = 0)
BEGIN
	UPDATE #ReportItems
	SET PercentComm = CASE #ReportItems.ProgramType
						WHEN 30327 THEN 15 --Exception for Donations
						WHEN 30345 THEN 8 --Exception for Cool Cards
						ELSE @Rate
					  END
	FROM   #NonMagCommRate nm
	WHERE CONVERT(Datetime,Convert(Varchar(10),#ReportItems.InvoiceDate,101)) >= @EffectiveFrom
	AND #ReportItems.SectionTypeId = @Section_Type_Id
		
	FETCH NEXT FROM AllCommRate  INTO @Section_Type_Id,@EffectiveFrom, @Rate
END
CLOSE AllCommRate
DEALLOCATE AllCommRate

--Exception for Sales Associates
UPDATE	#ReportItems
SET		PercentComm = 3
WHERE	FMID IN ('1568')

UPDATE	#ReportItems
SET		PercentComm = 6
WHERE	FMID IN ('1574')

--Additional Commission for Lower Group Profit Rate for Discount Cards
UPDATE		ri
SET			PercentComm =  CASE ri.GPRate WHEN 40.00 THEN PercentComm+5.0 WHEN 45.00 THEN PercentComm+2.5 ELSE PercentComm END
FROM		#ReportItems ri
WHERE		ri.SectionTypeID = 15

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
			SUM(Current_ProcessingFeeCount)  AS CurrentProcessingFeeCount,
			SUM(LastYear_ProcessingFeeCount) AS LastYearProcessingFeeCount,
			SUM(LastYear_ProcessingFees)	 AS LastYearProcessingFees,
			SUM(Current_ProcessingFees)		 AS CurrentProcessingFees,
			IsFaculty,
			CCRPAccountType,
			CCRPPayoutPercentage,
			SUM(CurrentNetOnlineSansPostage + CurrentNetRegularSansPostage) * CCRPPayoutPercentage / 100.0 AS CCRPPayoutAmount
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
			PercentComm,
			IsFaculty,
			CCRPAccountType,
			CCRPPayoutPercentage
HAVING		(@ShowCCRPOnly = 0 OR SUM(CurrentNetOnlineSansPostage + CurrentNetRegularSansPostage) * CCRPPayoutPercentage / 100.0 > 0.00)
ORDER BY	FMName,
			AccountName


DROP TABLE #ReportItems
DROP TABLE #FMWithoutCommRate
DROP TABLE #NonMagCommRate
GO
