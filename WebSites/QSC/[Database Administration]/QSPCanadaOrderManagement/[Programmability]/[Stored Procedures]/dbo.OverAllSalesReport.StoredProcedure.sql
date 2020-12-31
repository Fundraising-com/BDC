USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OverAllSalesReport]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE   [dbo].[OverAllSalesReport]	  ( @FmId	 	Int,
							@StartDate                 DateTime,
							@EndDate                  DateTime,
							@DmId	 	Int
						 )
/*******************************************************************************************************
Rewritten May 30, 2007 MS
Added Coulumn s for Online Sales for and Currrent and previous periods
**********************************************************************************************************/
AS
CREATE TABLE #ReportItems (
	OrderQualifierID 	Int, 
	OrderTypeCode			Int, 
	InvoiceID			Int, 
	AccountID			Int, 
	CampaignID			Int, 
	OrderID				Int, 
	FMID				Varchar(8), 
	FMName			Varchar(110), 
	DMID				Varchar(8),
	DMName			Varchar(115), 
	DMNameAndID			Varchar(125), 
	InvoiceDate			DateTime, 
	DiffFromEndDate		Numeric(6,2),
	InvoiceDueDate			DateTime, 
	AccountName			Varchar(110),
	ContactId			Int,
	MagSectionTypeID		Int, 
	MagNetSalesStaff		Numeric(10,2),
	MagItemCountStaff		Int,
	LastYearMagNetSalesStaff 	Numeric(10,2),
	LastYearMagItemCountStaff 	Int,
	MagNetSalesOnline		Numeric(10,2),
	MagItemCountOnline		Int,
	MagNetSalesRegular		Numeric(10,2),
	MagItemCountRegular		Int,
	GiftSectionTypeID		Int, 
	GiftNetSales			Numeric(10,2),
	GiftItemCount			Int,
	TrtSectionTypeID		Int, 
	TrtNetSales			Numeric(10,2),
	TrtItemCount			Int,
   EntertainmentSectionTypeID	Int, 
   EntertainmentNetSales		Numeric(10,2),
   EntertainmentItemCount		Int,
	CandleSectionTypeID		Int, 
	CandleNetSales			Numeric(10,2),
	CandleItemCount			Int,
	CookieSectionTypeID		Int, 
	CookieNetSales			Numeric(10,2),
	CookieItemCount		Int,
	LastYearNonStaffSaleOnline 	Numeric(10,2),
	LastYearNonStaffCountOnline 	Int,
	LastYearNonStaffSaleRegular 	Numeric(10,2),
	LastYearNonStaffCountRegular 	Int,
	LastYearGiftNetSale		Numeric(10,2),
	LastYearGiftItemCount 		Int,
	LastYearTrtNetSale		Numeric(10,2),
	LastYearTrtItemCount 		Int,
   LastYearEntertainmentNetSale		Numeric(10,2),
   LastYearEntertainmentItemCount 		Int,
	LastYearCandleNetSale		Numeric(10,2),
	LastYearCandleItemCount 		Int,
	LastYearCookieNetSale		Numeric(10,2),
	LastYearCookieItemCount 	Int
	)

DECLARE @YearPriorToStartDate  DateTime
DECLARE @YearPriorToEndDate  DateTime
DECLARE @DaysApart Int

SELECT @YearPriorToStartDate = DATEADD(MONTH,-12,@StartDate)

Select @DaysApart= DATEDIFF(DAY,@StartDate,@EndDate)

Select @YearPriorToEndDate = DATEADD(DAY,@DaysApart,@YearPriorToStartDate)

	
INSERT #ReportItems
SELECT  OrderQualifierID, 
	OrderTypeCode, 
	i.Invoice_ID, 
	A.ID  AcctID, 
	CampaignID, 
	i.Order_Id, 
	C.FMID, 
	FM.LastName + ' ' + FM.FirstName  FMName, 
	DM.DMID,
	DM.LastName + ' ' + DM.FirstName  DMName,
	DM.DMID+ ' - '+DM.LastName + ' ' + DM.FirstName  DMNameAndID,
	CONVERT(Varchar(10), Invoice_Date,101)  Invoice_Date, 
	DATEDIFF(DAY, @StartDate,i.INVOICE_DATE)HowRecent, 
	CONVERT(varchar(10), Invoice_Due_Date,101)  Invoice_Due_Date, 
	A.Name  AcctName,
	MAX(Cont.Id) ContactId,
	---------------------------------------Magazine---------------
	CASE ISecMag.Section_Type_Id
		WHEN 2 THEN 2
		ELSE 0 
	END MagSectionTypeId
	-------------  Staff (Current) --------------------
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN  1 THEN	CASE ISecMag.Section_Type_Id
			WHEN 2 THEN  CASE ISNULL(C.IsStaffOrder,0)
				     WHEN 1 THEN ISNULL(SUM(ISecMag.net_before_Tax),0)
				     ELSE 0
				     END
			ELSE 0 
			END
	WHEN  0 THEN	CASE ISecMag.Section_Type_Id
			WHEN 2 THEN  CASE ISNULL(C.IsStaffOrder,0)
				     WHEN 1 THEN ISNULL(SUM(ISecMag.net_before_Tax),0)
				     ELSE 0
				     END
			ELSE 0 
			END
	ELSE 0 
	End MagNetSaleStaff
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN  1 THEN	CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL(SUM(ISecMag.Item_Count),0)
				    ELSE 0
				    END
			ELSE 0 
			END
	WHEN  0 THEN	CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL(SUM(ISecMag.Item_Count),0)
				    ELSE 0
				    END
			ELSE 0 
			END
	ELSE 0
	END MagCountStaff
	-------------------Staff LastYear -----------------
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN  -1 THEN  CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
			    WHEN -1 THEN CASE ISecMag.Section_Type_Id
				                  WHEN 2 THEN  CASE ISNULL(C.IsStaffOrder,0)
				     	                                WHEN 1 THEN ISNULL(SUM(ISecMag.net_before_Tax),0)
				     	            		      ELSE 0
				     	          		      END
				     	     ELSE 0 
				     	     END
			   ELSE 0 
			   END
	ELSE 0 
	End LastYearMagNetSaleStaff
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
			   WHEN -1 THEN CASE ISecMag.Section_Type_Id
				    	     WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
				    		 	     WHEN 1 THEN ISNULL(SUM(ISecMag.Item_Count),0)
				    		 	      ELSE 0
				    		 	      END
				     	     ELSE 0 
				     	     END
			  ELSE 0 
			  END
	ELSE 0
	END LastYearMagCountStaff
	/*,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN  -1 THEN	CASE ISecMag.Section_Type_Id
			WHEN 2 THEN  CASE ISNULL(C.IsStaffOrder,0)
				     WHEN 1 THEN ISNULL(SUM(ISecMag.net_before_Tax),0)
				     ELSE 0
				     END
			ELSE 0 
			END
	ELSE 0 
	End LastYearMagNetSaleStaff
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN  -1 THEN	CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
				    WHEN 1 THEN ISNULL(SUM(ISecMag.Item_Count),0)
				    ELSE 0
				    END
			ELSE 0 
			END
	ELSE 0
	END LastYearMagCountStaff*/
	------------------------ Non Staff with and without online -------------------
	---------Current Non Staff Online --------------
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN 1 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
				    WHEN 1 THEN 0
				    ELSE CASE OrderQualifierID
			     		 WHEN 39009  THEN ISNULL(SUM(ISecMag.net_before_Tax),0)
			     		 ELSE 0
			     		 END
				    END
			ELSE 0 
			END
	 WHEN 0 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
			    		WHEN 1 THEN 0
			    		ELSE CASE OrderQualifierID
			        		WHEN 39009  THEN ISNULL(SUM(ISecMag.net_before_Tax),0)
			         		ELSE 0
			         		END
			    		END
			ELSE 0 
			END
	ELSE 0
	End MagNetSaleOnline

	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN 1 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE OrderQualifierID
				     		WHEN 39009 THEN ISNULL(SUM(ISecMag.Item_Count),0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	 WHEN 0 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE OrderQualifierID
				     		WHEN 39009 THEN ISNULL(SUM(ISecMag.Item_Count),0)
				     		ELSE 0
				     		END
				        END
		        ELSE 0 
			END
	ELSE 0
	END MagCountOnline
	------------ Current Non Staff Regular ----------
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN 1 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL(SUM(ISecMag.net_before_Tax),0)
				     		END
					END
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL(SUM(ISecMag.net_before_Tax),0)
				     		END
					END
			ELSE 0 
			END
	ELSE 0
	End MagNetSaleRegular
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	 WHEN 1 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL(SUM(ISecMag.Item_Count),0)
				     		END
					END
			ELSE 0 
			END
	 WHEN 0 THEN CASE ISecMag.Section_Type_Id
			WHEN 2 THEN CASE ISNULL(C.IsStaffOrder,0)
					WHEN 1 THEN 0
					ELSE CASE OrderQualifierID
				     		WHEN 39009	THEN 0
				     		ELSE ISNULL(SUM(ISecMag.Item_Count),0)
				     		END
					END
			ELSE 0 
			END
	ELSE 0
	END MagCountRegular
	-----------------------GIFT Sales---------------
	,CASE ISecGift.SECTION_TYPE_ID
		WHEN 1 THEN 1
		ELSE 0 
	END GiftSecTypeId
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecGift.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecGift.net_before_Tax),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecGift.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecGift.net_before_Tax),0)
			ELSE 0 
			END
	ELSE 0
	END GiftNetSales
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecGift.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecGift.Item_Count),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecGift.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecGift.Item_Count),0)
			ELSE 0 
			END
	ELSE 0
	END GiftCount
	-----------------------TRT---------------
	,CASE ISecTrt.SECTION_TYPE_ID
		WHEN 14 THEN 14
		ELSE 0 
	END TrtSecTypeId
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecTrt.Section_Type_Id
			WHEN 14 THEN ISNULL(SUM(ISecTrt.net_before_Tax),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecTrt.Section_Type_Id
			WHEN 14 THEN ISNULL(SUM(ISecTrt.net_before_Tax),0)
			ELSE 0 
			END
	ELSE 0
	END TrtNetSales
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecTrt.Section_Type_Id
			WHEN 14 THEN ISNULL(SUM(ISecTrt.Item_Count),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecTrt.Section_Type_Id
			WHEN 14 THEN ISNULL(SUM(ISecTrt.Item_Count),0)
			ELSE 0 
			END
	ELSE 0
	END TrtCount
	
	-----------------------Entertainment---------------
	,CASE ISecEntertainment.SECTION_TYPE_ID
		WHEN 1 THEN 1
		ELSE 0 
	END EntertainmentSecTypeId
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecEntertainment.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecEntertainment.net_before_Tax),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecEntertainment.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecEntertainment.net_before_Tax),0)
			ELSE 0 
			END
	ELSE 0
	END EntertainmentNetSales
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecEntertainment.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecEntertainment.Item_Count),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecEntertainment.Section_Type_Id
			WHEN 1 THEN ISNULL(SUM(ISecEntertainment.Item_Count),0)
			ELSE 0 
			END
	ELSE 0
	END EntertainmentCount	
	
	-----------------------Candle---------------
	,CASE ISecCandle.SECTION_TYPE_ID
		WHEN 13 THEN 13
		ELSE 0 
	END CandleSecTypeId
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecCandle.Section_Type_Id
			WHEN 13 THEN ISNULL(SUM(ISecCandle.net_before_Tax),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecCandle.Section_Type_Id
			WHEN 13 THEN ISNULL(SUM(ISecCandle.net_before_Tax),0)
			ELSE 0 
			END
	ELSE 0
	END CandleNetSales
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecCandle.Section_Type_Id
			WHEN 13 THEN ISNULL(SUM(ISecCandle.Item_Count),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecCandle.Section_Type_Id
			WHEN 13 THEN ISNULL(SUM(ISecCandle.Item_Count),0)
			ELSE 0 
			END
	ELSE 0
	END CandleCount
	
	-----------------------Cookie Dough Sales---------------
	,CASE ISecCookie.SECTION_TYPE_ID
		WHEN 6 THEN 6
		ELSE 0 
	END CookieSecTypeId
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecCookie.Section_Type_Id
			WHEN 6 THEN ISNULL(SUM(ISecCookie.net_before_Tax),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecCookie.Section_Type_Id
			WHEN 6 THEN ISNULL(SUM(ISecCookie.net_before_Tax),0)
			ELSE 0 
			END
	ELSE 0
	END CookieNetSales
	,CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN 1 THEN CASE ISecCookie.Section_Type_Id
			WHEN 6 THEN ISNULL(SUM(ISecCookie.Item_Count),0)
			ELSE 0 
			END
	WHEN 0 THEN CASE ISecCookie.Section_Type_Id
			WHEN 6 THEN ISNULL(SUM(ISecCookie.Item_Count),0)
			ELSE 0 
			END
	ELSE 0
	END CookieCount,
	-------------------------------LAST YEAR SALES NON STAFF ONLY-----------
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	 WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	 WHEN 1 THEN 0
				   	 ELSE  CASE b.OrderQualifierId 
		      			 	WHEN 39009 THEN ISNULL(SUM(ISecMag.Net_Before_Tax),0)
		      			 	ELSE 0
		      			 	END
				   	 END
		      	ELSE 0
		      	END
	ELSE  0    
	END LastYear_NetBeforeTax_Online,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	  WHEN 1 THEN 0
				   	  ELSE  CASE b.OrderQualifierId 
		      			 	WHEN 39009 THEN ISNULL(SUM(ISecMag.Item_Count),0)
		      			 	ELSE 0
		      			 	END
				   	END
		      	ELSE 0
		     	END
	ELSE  0    
	END LastYear_MagOnlineCount,
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	 WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	 WHEN 1 THEN 0
				   	 ELSE CASE b.OrderQualifierId 
		      				WHEN 39009 THEN 0
		      				ELSE ISNULL(SUM(ISecMag.Net_Before_Tax),0)
		      				END
				   	END
		      	 ELSE 0
		      	 END
	ELSE  0    
	END LastYear_NetBeforeTax_Regular,
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	 WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	  WHEN 1 THEN 0
				   	  ELSE CASE b.OrderQualifierId 
		      				WHEN 39009 THEN 0
		      				ELSE ISNULL(SUM(ISecMag.Item_Count),0)
		      				END
				   	END
		      	ELSE 0
		      	END
	ELSE  0    
	END LastYear_MagRegularCount,
	/*CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				WHEN 1 THEN 0
				ELSE  CASE b.OrderQualifierId 
		      			WHEN 39009 THEN ISNULL(SUM(ISecMag.Net_Before_Tax),0)
		      			ELSE 0
		      			END
				END
	ELSE  0    
	END LastYear_NetBeforeTax_Online,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				WHEN 1 THEN 0
				ELSE  CASE b.OrderQualifierId 
		      			WHEN 39009 THEN ISNULL(SUM(ISecMag.Item_Count),0)
		      			ELSE 0
		      			END
				END
	ELSE  0    
	END LastYear_MagOnlineCount,
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
			WHEN 1 THEN 0
			ELSE CASE b.OrderQualifierId 
		      		WHEN 39009 THEN 0
		      		ELSE ISNULL(SUM(ISecMag.Net_Before_Tax),0)
		      		END
			END
	ELSE  0    
	END LastYear_NetBeforeTax_Regular,
	CASE SIGN(DATEDIFF(day, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
			WHEN 1 THEN 0
			ELSE CASE b.OrderQualifierId 
		      		WHEN 39009 THEN 0
		      		ELSE ISNULL(SUM(ISecMag.Item_Count),0)
		      		END
			END
	ELSE  0    
	END LastYear_MagRegularCount,*/
	-----------------------------------LAST YEAR GIFT, TRT and COOKIE --------------
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				   	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecGift.Net_Before_Tax),0)
				   	  END
		      	  ELSE 0
		      	  END
	ELSE  0    
	END LastYear_NetBeforeTax_Gift,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecGift.Item_Count),0)
				   	  END
		      	 ELSE 0
		      	 END
	ELSE  0    
	END LastYear_GiftCount,
		
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				   	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecTrt.Net_Before_Tax),0)
				   	  END
		      	  ELSE 0
		      	  END
	ELSE  0    
	END LastYear_NetBeforeTax_Trt,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecTrt.Item_Count),0)
				   	  END
		      	 ELSE 0
		      	 END
	ELSE  0    
	END LastYear_TrtCount,	
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				   	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecEntertainment.Net_Before_Tax),0)
				   	  END
		      	  ELSE 0
		      	  END
	ELSE  0    
	END LastYear_NetBeforeTax_Entertainment,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecEntertainment.Item_Count),0)
				   	  END
		      	 ELSE 0
		      	 END
	ELSE  0    
	END LastYear_EntertainmentCount,	
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				   	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecCandle.Net_Before_Tax),0)
				   	  END
		      	  ELSE 0
		      	  END
	ELSE  0    
	END LastYear_NetBeforeTax_Candle,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	  WHEN 1 THEN 0
				   	  ELSE  ISNULL(SUM(ISecCandle.Item_Count),0)
				   	  END
		      	 ELSE 0
		      	 END
	ELSE  0    
	END LastYear_CandleCount,
	
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				  	   WHEN 1 THEN 0
				   	   ELSE  ISNULL(SUM(ISecCookie.Net_Before_Tax),0)
		     		   	    END
		      	  ELSE 0
		      	  END 
	ELSE  0    
	END LastYear_NetBeforeTax_CookieDough,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE SIGN(DATEDIFF(day, @YearPriorToEndDate,i.INVOICE_DATE))
		      	  WHEN -1 THEN CASE ISNULL(C.IsStaffOrder,0)
				   	   WHEN 1 THEN 0
				   	   ELSE  ISNULL(SUM(ISecCookie.Item_Count),0)
		     		   	   END
		      	  ELSE 0
		      	  END
	ELSE  0    
	END LastYear_CookieCount
	/*CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
			WHEN 1 THEN 0
			ELSE  ISNULL(SUM(ISecGift.Net_Before_Tax),0)
			END
	ELSE  0    
	END LastYear_NetBeforeTax_Gift,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
			WHEN 1 THEN 0
			ELSE  ISNULL(SUM(ISecGift.Item_Count),0)
			END
	ELSE  0    
	END LastYear_GiftCount,

	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
			WHEN 1 THEN 0
			ELSE  ISNULL(SUM(ISecCookie.Net_Before_Tax),0)
		     	END
	ELSE  0    
	END LastYear_NetBeforeTax_CookieDough,
	CASE SIGN(DATEDIFF(DAY, @StartDate,i.INVOICE_DATE))
	WHEN  -1 THEN CASE ISNULL(C.IsStaffOrder,0)
			WHEN 1 THEN 0
			ELSE  ISNULL(SUM(ISecCookie.Item_Count),0)
		     	END
	ELSE  0    
	END LastYear_CookieCount*/
FROM    QSPCanadaFinance.dbo.Invoice I	      			(NOLOCK)
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section ISecMag    	(NOLOCK) ON i.Invoice_Id = ISecMag.Invoice_Id and ISecMag.Section_type_id=2
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section ISecGift   	(NOLOCK) ON i.Invoice_Id = ISecGift.Invoice_Id and ISecGift.Section_type_id=1
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section ISecCookie 	(NOLOCK) ON i.Invoice_Id = ISecCookie.Invoice_Id and ISecCookie.Section_type_id=6
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section ISecCandle 	(NOLOCK) ON i.Invoice_Id = ISecCandle.Invoice_Id and ISecCandle.Section_type_id=13
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section ISecTrt 	(NOLOCK) ON i.Invoice_Id = ISecTrt.Invoice_Id and ISecTrt.Section_type_id=14
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section ISecEntertainment 	(NOLOCK) ON i.Invoice_Id = ISecEntertainment.Invoice_Id and ISecEntertainment.Section_type_id=15
	INNER JOIN QSPcanadaOrdermanagement.dbo.Batch B	(NOLOCK) ON B.OrderId=I.Order_Id
	INNER JOIN QSPCanadaCommon.dbo.CAccount A 	(NOLOCK) ON B.AccountID =A.Id 
     	INNER JOIN QSPCanadaCommon.dbo.Campaign C	(NOLOCK) ON B.CampaignId=C.Id
	INNER JOIN QSPCanadaCommon.dbo.FieldManager FM  	(NOLOCK) ON fm.FMID = C.FMID
	INNER JOIN QSPCanadaCommon.dbo.FieldManager DM  	(NOLOCK) ON fm.DMID = dm.FMID
	LEFT  JOIN QSPCanadaCommon.dbo.Contact Cont 	(NOLOCK) ON Cont.ID = C.ShipToCampaignContactID
WHERE CONVERT(DateTime,CONVERT(Varchar(10),i.Invoice_Date,101) )BETWEEN @YearPriorToStartDate  AND @Enddate
AND 	b.OrderTypeCode NOT IN(41006,41007,41011)	--FM FMBULK,CLOSEOUT
AND     b.OrderQualifierId <> 39006 			--Kanata
AND     c.FMID=CASE ISNULL(@FMID,'') WHEN '' THEN c.FMID ELSE @FMID END
AND     dm.DMID=CASE ISNULL(@DMID,'') WHEN '' THEN dm.DMID ELSE @DMID END
AND   (ISecMag.SECTION_TYPE_ID =2 OR ISecGift.SECTION_TYPE_ID=1 or ISecCookie.SECTION_TYPE_ID=6 OR ISecTrt.SECTION_TYPE_ID=13 OR ISecTrt.SECTION_TYPE_ID=14 OR ISecEntertainment.SECTION_TYPE_ID = 15)
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
	C.IsStaffOrder,
	i.Order_Id, 
	DM.DMID,
	C.FMID, 
	DM.FirstName ,
	DM.LastName , 
	FM.FirstName ,
	FM.LastName , 
	Invoice_Date,
	Invoice_Due_Date,
	A.Name ,
	ISecMag.Section_Type_Id, 
	ISecMag.Item_Count,
	ISecMag.Net_Before_Tax ,
	ISecGift.Section_Type_Id, 
	ISecGift.Item_Count,
	ISecGift.Net_Before_Tax ,
	ISecTrt.Section_Type_Id, 
	ISecTrt.Item_Count,
	ISecTrt.Net_Before_Tax,  
	ISecEntertainment.Section_Type_Id, 
	ISecEntertainment.Item_Count,
	ISecEntertainment.Net_Before_Tax, 
	ISecCandle.Section_Type_Id, 
	ISecCandle.Item_Count,
	ISecCandle.Net_Before_Tax,
	ISecCookie.Section_Type_Id, 
	ISecCookie.Item_Count,
	ISecCookie.Net_Before_Tax
	
	
--Select * from #ReportItems

SELECT   DMID,DMName,DMNameAndID, 
	 FMID, FMName,
	 --MagSectionTypeID, 
	 --Current Staff
	 SUM(MagNetSalesStaff) MagNetSalesStaff ,
	 SUM(MagItemCountStaff)MagItemCountStaff ,
	 --LastYear Staff
	 Sum(LastYearMagNetSalesStaff) LastYearMagNetSalesStaff,
	 Sum(LastYearMagItemCountStaff) LastYearMagItemCountStaff,
	 --Current Online (Non Staff)
	 SUM(MagNetSalesOnline)MagNetSalesOnline,
	 SUM(MagItemCountOnline) MagItemCountOnline,
	 SUM(LastYearNonStaffSaleOnline) LastYearNonStaffSaleOnline,
	 SUM(LastYearNonStaffCountOnline) LastYearNonStaffCountOnline,
	 --Current Regular (Non Staff)
 	 SUM(MagNetSalesRegular) MagNetSalesRegular,
	 SUM(MagItemCountRegular) MagItemCountRegular,
	 SUM(LastYearNonStaffSaleRegular) LastYearNonStaffSaleRegular , 
	 SUM(LastYearNonStaffCountRegular) LastYearNonStaffCountRegular,
	 --GiftSectionTypeID,
	 --Current Gift
	 SUM(GiftNetSales)GiftNetSales ,
	 SUM(GiftItemCount)GiftItemCount,
	 SUM(LastYearGiftNetSale) LastYearGiftNetSale,
	 SUM(LastYearGiftItemCount) LastYearGiftItemCount,
	 --TrtSectionTypeID,
	 --Current Trt
	 SUM(TRTNetSales)TrtNetSales ,
	 SUM(TRTItemCount)TrtItemCount,
	 SUM(LastYearTrtNetSale) LastYearTrtNetSale,
	 SUM(LastYearTrtItemCount) LastYearTrtItemCount,
	  --TrtSectionTypeID,
	 --Current Trt
	 SUM(EntertainmentNetSales)EntertainmentNetSales ,
	 SUM(EntertainmentItemCount)EntertainmentItemCount,
	 SUM(LastYearEntertainmentNetSale) LastYearEntertainmentNetSale,
	 SUM(LastYearEntertainmentItemCount) LastYearEntertainmentItemCount,
	 --CandleSectionTypeID,
	 --Current Candle
	 SUM(CandleNetSales)CandleNetSales ,
	 SUM(CandleItemCount)CandleItemCount,
	 SUM(LastYearCandleNetSale) LastYearCandleNetSale,
	 SUM(LastYearCandleItemCount) LastYearCandleItemCount,
	 --CookieSectionTypeID,
	 --Current Cookie Dough
	 SUM(CookieNetSales)CookieNetSales,
	 SUM(CookieItemCount)CookieItemCount ,
	 SUM(LastYearCookieNetSale) LastYearCookieNetSale,
	 SUM(LastYearCookieItemCount) LastYearCookieItemCount
	 --AccountID,AccountName
FROM #ReportItems 
GROUP BY DMID,DMName,DMNameAndID,
	 FMID, FMName
	--MagSectionTypeID,GiftSectionTypeID,CookieSectionTypeID,
	 --AccountID,AccountName
ORDER BY DMId, FMName--FMId
 

DROP TABLE #ReportItems
GO
