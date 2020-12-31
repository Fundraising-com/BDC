USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[SubBonusReport]    Script Date: 06/07/2017 09:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE   [dbo].[SubBonusReport]	(@FmId	 	Int,
						@StartDate        DateTime,
						@EndDate         DateTime,
						 @DmId	 	Int
						 )
AS
BEGIN

SET NOCOUNT ON

CREATE TABLE #ReportSummary (
	DMID			Varchar(8),
	DMName		Varchar(115), 
	DMNameAndID		Varchar(125), 
	FMID			Varchar(8), 
	FMName		Varchar(110), 
	CurrentMagCountStaff 	Int, 
	LastYearMagCountStaff	Int,
	 CurrentTrtCountStaff 	Int, 
	 LastYearTrtCountStaff	Int,  
	CurrentMagCountOnline	Int,
	CurrentMagCountRegular	Int,
     CurrentTrtCountOnline	Int,
	 CurrentTrtCountRegular	Int,
	LastYearMagCountOnline	Int,
	LastYearMagCountRegular	Int,
	 LastYearTrtCountOnline	Int,
	 LastYearTrtCountRegular	Int,
	CurrentMagTotalStaff	Numeric(10,2),
	CurrentMagTotalOnline	Numeric(10,2),
	CurrentMagTotalRegular  Numeric(10,2),
	 CurrentTrtTotalStaff	Numeric(10,2),
	 CurrentTrtTotalOnline	Numeric(10,2),
	 CurrentTrtTotalRegular  Numeric(10,2)		
	)

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
	CurrentMagCountStaff 	Numeric(10,2), 
	LastYearMagCountStaff	Numeric(10,2), 
	 CurrentTrtCountStaff 	Numeric(10,2), 
	 LastYearTrtCountStaff	Numeric(10,2), 
	CurrentMagCountOnline	Numeric(10,2),
	CurrentMagCountRegular	Numeric(10,2),
	 CurrentTrtCountOnline	Numeric(10,2),
	 CurrentTrtCountRegular	Numeric(10,2),
	LastYearMagCountOnline	Numeric(10,2),
	LastYearMagCountRegular	Numeric(10,2),
	 LastYearTrtCountOnline	Numeric(10,2),
	 LastYearTrtCountRegular	Numeric(10,2),
	BonusPercent		Numeric(5,2),
	TargetBase		Numeric(10,2),
	CurrentMagTotalStaff	Numeric(10,2),
	CurrentMagTotalOnline	Numeric(10,2),
	CurrentMagTotalRegular  Numeric(10,2),
	 CurrentTrtTotalStaff	Numeric(10,2),
	 CurrentTrtTotalOnline	Numeric(10,2),
	 CurrentTrtTotalRegular  Numeric(10,2)
	)

DECLARE @YearPriorToStartDate  DateTime
DECLARE @YearPriorToEndDate  DateTime
DECLARE @DaysApart Int
DECLARE @TRTDivider Int

SELECT @YearPriorToStartDate = DateAdd(month,-12,@StartDate)

Select @DaysApart= DATEDIFF(DAY,@StartDate,@EndDate)

Select @YearPriorToEndDate = DATEADD(DAY,@DaysApart,@YearPriorToStartDate)

SET @TRTDivider = 18.00

	
INSERT #ReportItems
SELECT  b.OrderQualifierID, 
	b.OrderTypeCode, 
	inv.Invoice_ID, 
	acc.ID  AcctID, 
	b.CampaignID, 
	inv.Order_Id, 
	fm.FMID,
	fm.LastName + ' ' + fm.FirstName  FMName, 
	dm.FMID DMID,
	dm.LastName + ' ' + dm.FirstName  DMName,
	dm.FMID+ ' - '+ dm.LastName + ' ' + dm.FirstName  DMNameAndID,
	CONVERT(Varchar(10), inv.Invoice_Date,101)  Invoice_Date, 
	DATEDIFF(DAY, @StartDate,inv.INVOICE_DATE), 
	CONVERT(varchar(10), inv.Invoice_Due_Date,101)  Invoice_Due_Date, 
	acc.Name  AcctName,
	MAX(Cont.Id) ContactId,
	iSec.Section_Type_Id, 
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN  1 THEN	CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    ELSE 0
				    END
			ELSE 0 
			END
	WHEN  0 THEN	CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    ELSE 0
				    END
			ELSE 0 
			END
	ELSE 0
	END MagCountStaff,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,inv.INVOICE_DATE))
			   WHEN -1 THEN CASE iSec.Section_Type_Id
				    	     WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    		 	     WHEN 1 THEN ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    		 	      ELSE 0
				    		 	      END
				     	     ELSE 0 
				     	     END
			  ELSE 0 
			  END
	ELSE 0
	END LastYearMagCountStaff,
	--Trt
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN  1 THEN	CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				    ELSE 0
				    END
			ELSE 0 
			END
	WHEN  0 THEN	CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				    ELSE 0
				    END
			ELSE 0 
			END
	ELSE 0
	END TrtCountStaff,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,inv.INVOICE_DATE))
			   WHEN -1 THEN CASE iSec.Section_Type_Id
				    	     WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    		 	     WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				    		 	      ELSE 0
				    		 	      END
				     	     ELSE 0 
				     	     END
			  ELSE 0 
			  END
	ELSE 0
	END LastYearTrtCountStaff,
	--end 
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	ELSE 0
	END MagCountOnline,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		END
					END
			ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		END
					END
			ELSE 0 
			END
	ELSE 0
	END MagCountRegular,
	
	--Trt
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	ELSE 0
	END TrtCountOnline,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				     		END
					END
			ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
				     		END
					END
			ELSE 0 
			END
	ELSE 0
	END TrtCountRegular,
		
	--End
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,inv.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,inv.INVOICE_DATE))
		      	  WHEN -1 THEN CASE iSec.SECTION_TYPE_ID
		      		 WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				  		  WHEN 1 THEN 0
				   		  ELSE  CASE b.OrderQualifierId 
		      			 		WHEN 39009 THEN ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
		      			 		ELSE 0
		      			 		END
				   		END
				   	ELSE 0
				   	END
		      	ELSE 0
		     	END
	ELSE  0    
	END LastYear_MagOnlineCount,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,inv.INVOICE_DATE))
		      	 WHEN -1 THEN CASE iSec.SECTION_TYPE_ID
		      		 WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				  		  WHEN 1 THEN 0
				   		  ELSE CASE b.OrderQualifierId 
		      					WHEN 39009 THEN 0
		      					ELSE ISNULL(iSec.Item_Count /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
		      					END		      		 	
				   		END
				   	ELSE 0
				   	END
		      	ELSE 0
		      	END
	ELSE  0    
	END LastYear_MagRegularCount,
	
	--Trt
	CASE SIGN(DATEDIFF(DAY, @StartDate,inv.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,inv.INVOICE_DATE))
		      	  WHEN -1 THEN CASE iSec.Section_Type_Id
								WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				  					 WHEN 1 THEN 0
				   					 ELSE  CASE b.OrderQualifierId 
		      								WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
		      			 					ELSE 0
		      			 				    END
		      			 			END
		      			 	    ELSE 0
				   				END
		      	ELSE 0
		     	END
	ELSE  0    
	END LastYear_TrtOnlineCount,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,inv.INVOICE_DATE))
		      	 WHEN -1 THEN CASE iSec.Section_Type_Id
								WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				  					 WHEN 1 THEN 0
				   					 ELSE CASE b.OrderQualifierId 
		      								WHEN 39009 THEN 0
		      								ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/ * vw.ProgramMultiplier / @TRTDivider, 0)
		      								END
				   					END
				   				ELSE 0
				   				END
		      	ELSE 0
		      	END
	ELSE  0    
	END LastYear_TrtRegularCount,
	--ENd
	Convert(Numeric(5,2),0),  --BonusPercent
	Convert(Numeric(10,2),0), --FMtargetBase
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN  1 THEN	CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00))/* * ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    ELSE 0
				    END
			ELSE 0 
			END
	WHEN  0 THEN	CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    ELSE 0
				    END
			ELSE 0 
			END
	ELSE 0
	END CurrentMagTotalStaff,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	ELSE 0
	END CurrentMagTotalOnline,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		END
					END
			ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		END
					END
			ELSE 0 
			END
	ELSE 0
	END CurrentMagTotalRegular,
	
	--Trt
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN  1 THEN	CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    ELSE 0
				    END
			ELSE 0 
			END
	WHEN  0 THEN	CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				    ELSE 0
				    END
			ELSE 0 
			END
	ELSE 0
	END CurrentTrtTotalStaff,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009 THEN ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	ELSE 0
	END CurrentTrtTotalOnline,
	CASE SIGN(DATEDIFF(day, @StartDate,inv.INVOICE_DATE))
	 WHEN 1 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		END
					END
			ELSE 0 
			END
	 WHEN 0 THEN CASE iSec.Section_Type_Id
			WHEN 14 THEN CASE ISNULL(camp.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE b.OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL((iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00)) /** ISNULL(ccs.CommissionPercentage/100.00, 1)*/,0)
				     		END
					END
			ELSE 0 
			END
	ELSE 0
	END CurrentTrtTotalRegular
FROM		QSPcanadaOrdermanagement..Batch b (NOLOCK)
JOIN		QSPCanadaCommon..Campaign camp (NOLOCK) ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc (NOLOCK) ON camp.BillToAccountID = acc.ID
JOIN		QSPCanadaFinance..Campaign_Program_Multiplier_vw VW (NOLOCK) ON VW.CampaignId = camp.ID
/*LEFT JOIN	(QSPCanadaCommon..CampaignCommissionSplit ccs (NOLOCK)
				JOIN	QSPCanadaCommon..FieldManager fmSplit ON fmSplit.FMID = ccs.FMID) ON ccs.CampaignID = camp.ID*/
LEFT JOIN	QSPCanadaCommon.dbo.Contact cont (NOLOCK) ON cont.ID = camp.ShipToCampaignContactID
--JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK) ON fm.FMID = ISNULL(fmSplit.FMID, QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, @EndDate))
JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK) ON fm.FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, @EndDate)
JOIN		QSPCanadaCommon..FieldManager DM (NOLOCK) ON dm.FMID = fm.DMID AND dm.DMIndicator='Y'
JOIN		QSPCanadaFinance..Invoice inv (NOLOCK) ON inv.Order_ID = b.OrderID
JOIN     	QSPCanadaFinance..Invoice_Section iSec (NOLOCK) ON iSec.Invoice_ID = inv.Invoice_ID
JOIN     	QSPCanadaProduct..ProgramSectionType ps (NOLOCK) ON ps.ID = ISec.Section_Type_ID
WHERE		CONVERT(DateTime, CONVERT(Varchar(10), inv.Invoice_Date,101)) BETWEEN @YearPriorToStartDate AND @Enddate
AND			iSec.Section_Type_ID in (2, 14)
AND 		b.OrderTypeCode NOT IN (41006,41007,41011, 41012) --FM FMBULK FREE SUBS
AND			b.OrderQualifierId <> 39006
AND			fm.FMID = CASE ISNULL(@FMID,'') WHEN '' THEN fm.FMID ELSE @FMID END
AND			dm.FMID = CASE ISNULL(@DMID,'') WHEN '' THEN dm.FMID ELSE @DMID END
GROUP BY 
	b.OrderQualifierID, 
	b.OrderTypeCode, 
	inv.Invoice_ID, 
	acc.ID , 
	b.CampaignID, 
	inv.Order_Id, 
	dm.FMID,
	fm.FMID, 
	dm.FirstName ,
	dm.LastName , 
	fm.FirstName ,
	fm.LastName , 
	inv.Invoice_Date,
	inv.Invoice_Due_Date,
	acc.Name ,
	iSec.Section_Type_Id, 
	iSec.Item_Count,
	camp.IsStaffOrder,
	iSec.NET_BEFORE_TAX - ISNULL(iSec.US_Postage_Amount, 0.00),
	--ccs.CommissionPercentage,
	vw.ProgramMultiplier

INSERT #ReportSummary
SELECT   DMID,DMName,DMNameAndID,
	 FMID, FMName,
 	 SUM(CurrentMagCountStaff)   CurrentMagCountStaff,
	 SUM(LastYearMagCountStaff)  LastYearMagCountStaff,
	  SUM(CurrentTrtCountStaff)   CurrentTrtCountStaff,
	  SUM(LastYearTrtCountStaff)  LastYearTrtCountStaff,
	 SUM(CurrentMagCountOnline)     CurrentMagCountOnline,
	 SUM(CurrentMagCountRegular)    CurrentMagCountRegular,
	  SUM(CurrentTrtCountOnline)     CurrentTrtCountOnline,
	  SUM(CurrentTrtCountRegular)    CurrentTrtCountRegular,
	 SUM(LastYearMagCountOnline)    LastYearMagCountOnline,
	 SUM(LastYearMagCountRegular)   LastYearMagCountRegular,
	  SUM(LastYearTrtCountOnline)    LastYearTrtCountOnline,
	  SUM(LastYearTrtCountRegular)   LastYearTrtCountRegular,
	 SUM(CurrentMagTotalStaff)   	CurrentMagTotalStaff,
	 SUM(CurrentMagTotalOnline)   	CurrentMagTotalOnline,
	 SUM(CurrentMagTotalRegular)    CurrentMagTotalRegular,
	  SUM(CurrentTrtTotalStaff)   	CurrentTrtTotalStaff,
	  SUM(CurrentTrtTotalOnline)   	CurrentTrtTotalOnline,
	  SUM(CurrentTrtTotalRegular)    CurrentTrtTotalRegular
	 
FROM #ReportItems 
GROUP BY DMID,DMName,DMNameAndID,
	 FMID, FMName
ORDER BY DMId, FMId

UPDATE #ReportItems
SET     TargetBase = rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular,
		BonusPercent = CASE WHEN @StartDate < '2013-07-01' AND (rs.CurrentMagCountStaff + rs.CurrentMagCountOnline + rs.CurrentMagCountRegular + rs.CurrentTrtCountStaff + rs.CurrentTrtCountOnline + rs.CurrentTrtCountRegular) - (rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular) >= 2000 THEN 2
							WHEN @StartDate < '2013-07-01' AND (rs.CurrentMagCountStaff + rs.CurrentMagCountOnline + rs.CurrentMagCountRegular + rs.CurrentTrtCountStaff + rs.CurrentTrtCountOnline + rs.CurrentTrtCountRegular) - (rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular) BETWEEN 1000 AND 1999 THEN 1
							WHEN @StartDate < '2013-07-01' AND (rs.CurrentMagCountStaff + rs.CurrentMagCountOnline + rs.CurrentMagCountRegular + rs.CurrentTrtCountStaff + rs.CurrentTrtCountOnline + rs.CurrentTrtCountRegular) - (rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular) < 1000 THEN 0
							WHEN @StartDate >= '2013-07-01' AND (rs.CurrentMagCountStaff + rs.CurrentMagCountOnline + rs.CurrentMagCountRegular + rs.CurrentTrtCountStaff + rs.CurrentTrtCountOnline + rs.CurrentTrtCountRegular) - (rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular) >= 4000 THEN 2
							WHEN @StartDate >= '2013-07-01' AND (rs.CurrentMagCountStaff + rs.CurrentMagCountOnline + rs.CurrentMagCountRegular + rs.CurrentTrtCountStaff + rs.CurrentTrtCountOnline + rs.CurrentTrtCountRegular) - (rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular) BETWEEN 2000 AND 3999 THEN 1
							WHEN @StartDate >= '2013-07-01' AND (rs.CurrentMagCountStaff + rs.CurrentMagCountOnline + rs.CurrentMagCountRegular + rs.CurrentTrtCountStaff + rs.CurrentTrtCountOnline + rs.CurrentTrtCountRegular) - (rs.LastYearMagCountStaff + rs.LastYearMagCountOnline + rs.LastYearMagCountRegular + rs.LastYearTrtCountStaff + rs.LastYearTrtCountOnline + rs.LastYearTrtCountRegular) < 2000 THEN 0
						END
FROM	#ReportItems ri
JOIN	#ReportSummary rs
			ON	rs.FMID = ri.FMID

/*
UPDATE #ReportItems
SET     TargetBase = CONVERT(NUMERIC(10,2),C.MAX_TARGET_NUMBER),  -- MAX_TARGET_NUMBER is Varchar column
	BonusPercent = CASE SIGN((#ReportSummary.CurrentMagCountStaff+
			 	  #ReportSummary.CurrentMagCountOnline+
				  #ReportSummary.CurrentMagCountRegular) - CONVERT(NUMERIC(10,2),C.MAX_TARGET_NUMBER)-1000)
		   WHEN 1 THEN  CASE SIGN((#ReportSummary.CurrentMagCountStaff+
			 		   	#ReportSummary.CurrentMagCountOnline+
			 		   	#ReportSummary.CurrentMagCountRegular)- CONVERT(NUMERIC(10,2),C.MAX_TARGET_NUMBER) - 2000)
				WHEN -1 THEN 1
				ELSE 2
				END
		    WHEN 0 THEN  CASE SIGN(( #ReportSummary.CurrentMagCountStaff+
			 		  	 #ReportSummary.CurrentMagCountOnline+
			 		   	 #ReportSummary.CurrentMagCountRegular)- CONVERT(NUMERIC(10,2),C.MAX_TARGET_NUMBER) - 2000)
				WHEN -1 THEN 1
				ELSE 2
				END
		   ELSE 0
		   END
FROM QSPCanadaFinance.dbo.Commission C , #ReportSummary
WHERE CONVERT(Int, #ReportSummary.FMID)=C.FM_ID
AND #ReportSummary.FMID=#ReportItems.FMID
AND Commission_Type_Code = 'BONUS_ON_SUBS'
AND Section_type_id =  2
AND C.COMMISSION_ID = (SELECT MAX(c1.COMMISSION_ID) FROM QSPCanadaFinance.dbo.Commission c1 WHERE c1.FM_Id= c.Fm_ID AND Commission_Type_Code = 'BONUS_ON_SUBS' AND Section_type_id =  2)
AND  Is_used_for_exceed_target = 'N'
AND CONVERT(Datetime , CONVERT(Varchar(10),comm_effective_date,101)) <= CAST(@EndDate as Datetime)
AND CONVERT(Datetime , CONVERT(Varchar(10),comm_end_date,101))       >= CAST('5/31/2004' as Datetime)
*/

SELECT DMID,DMName,DMNameAndID,
	 FMID, FMName,
	 AccountID,AccountName,
 	 SUM(CurrentMagCountStaff)     CurrentMagCountStaff,
	 SUM(LastYearMagCountStaff)  LastYearMagCountStaff,
	  SUM(CurrentTrtCountStaff)     CurrentTrtCountStaff,
	  SUM(LastYearTrtCountStaff)  LastYearTrtCountStaff,
	 SUM(CurrentMagCountOnline)      CurrentMagCountOnline,
	 SUM(CurrentMagCountRegular)    CurrentMagCountRegular,
	  SUM(CurrentTrtCountOnline)      CurrentTrtCountOnline,
	  SUM(CurrentTrtCountRegular)    CurrentTrtCountRegular,
	 SUM(LastYearMagCountOnline)    LastYearMagCountOnline,
	 SUM(LastYearMagCountRegular)  LastYearMagCountRegular,
	  SUM(LastYearTrtCountOnline)    LastYearTrtCountOnline,
	  SUM(LastYearTrtCountRegular)  LastYearTrtCountRegular,
	 BonusPercent,
	 TargetBase,
	 SUM(CurrentMagTotalStaff)   	CurrentMagTotalStaff,
	 SUM(CurrentMagTotalOnline)   	CurrentMagTotalOnline,
	 SUM(CurrentMagTotalRegular)    CurrentMagTotalRegular,
	  SUM(CurrentTrtTotalStaff)   	CurrentTrtTotalStaff,
	  SUM(CurrentTrtTotalOnline)   	CurrentTrtTotalOnline,
	  SUM(CurrentTrtTotalRegular)    CurrentTrtTotalRegular
FROM #ReportItems 
GROUP BY DMID,DMName,DMNameAndID,
	 FMID, FMName,
	 AccountID,AccountName,BonusPercent,TargetBase
ORDER BY DMId, FMId, AccountName
	
END
GO
