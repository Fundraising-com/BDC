USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[Pr_RemitBatchReport]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[Pr_RemitBatchReport]
@Publisher_ID		int,
@FH_ID 		int,
@From_Batch_ID 	int,
@To_Batch_ID 		int,
@Currency_ID 		int
AS
--written by saqib shah,  August 2004
-- this proc is used in .net remit processing report
-- Added EffeortKey Column MS Aug 16, 2006 For New Remit Processing report With Effort Key Issue #657

Declare	@RepBasePriceTotal_CAD  Numeric(14,2), @RepBasePriceTotal_USD  Numeric(14,2),  
	@ReportTotal_CAD  Numeric(14,2), @ReportTotal_USD  Numeric(14,2) 

SELECT	product.Fulfill_House_Nbr,
		fh.Ful_Name, 
		product.Pub_Nbr,pub.Pub_Name,
		codrh.RemitCode AS TitleCode,
		CASE LEFT(codrh.TitleCode, 1) WHEN 'S' THEN 1 ELSE 0 END AS StaffTitle,
		codrh.MagazineTitle,
		rb.RunID,
		CONVERT(varchar(10), rb.Date, 101) AS RemitBatchDate,
		crh.State,
		codrh.NumberOfIssues,
		codrh.BasePrice,
		codrh.RemitRate,
		CASE codrh.CurrencyID WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' END AS CurrencyID,  
		COUNT(codrh.TitleCode) AS Total_Subs, 
		SUM(codrh.BasePrice) AS TotalBasePrice, 
		ROUND((COUNT(codrh.TitleCode) * codrh.BasePrice), 2) AS TotalAtSubRate,
		ROUND((ISNULL(pd.BasePriceSansPostage, 0) * ISNULL(pd.BaseRemitRate, 0)) + (ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0)), 2) AS RemitPerSub,
		ROUND(SUM((ISNULL(pd.BasePriceSansPostage, 0) * ISNULL(pd.BaseRemitRate, 0)) + (ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0))), 6) AS TotalRemit,
		ROUND(SUM(ISNULL(codrh.Tax,0)), 6) Tax1,
		ROUND(SUM(ISNULL(codrh.Tax2,0)), 6) Tax2,
		pd.Effort_Key PDEKey,
		codrh.EffortKey RhEKey
INTO	#InitialTotals
FROM	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh,
		QSPCanadaOrderManagement..RemitBatch rb,
		QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..CustomerOrderDetail cod,
		QSPCanadaOrderManagement..CustomerRemitHistory crh,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..Product Product,
		QSPCanadaProduct..Fulfillment_House fh,
		QSPCanadaProduct..Publishers pub
WHERE	codrh.RemitBatchID = rb.ID
AND		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		cod.TransID = codrh.TransID
AND		coh.Instance = cod.CustomerOrderHeaderInstance
AND		codrh.CustomerRemitHistoryInstance = crh.Instance
AND		cod.PricingDetailsID = pd.MagPrice_Instance
AND		pd.Product_Instance = Product.Product_Instance 
AND		Product.Fulfill_House_Nbr = fh.Ful_Nbr   
AND		Product.Pub_Nbr = pub.Pub_Nbr
AND		Product.Pub_Nbr = ISNULL(@Publisher_ID,Product.Pub_Nbr)
AND		Product.Fulfill_House_Nbr = ISNULL(@FH_ID,Product.Fulfill_House_Nbr)
AND		rb.RunID = ISNULL(@From_Batch_ID, rb.RunID)
--AND		rb.RunID BETWEEN ISNULL(@From_Batch_ID, rb.RunID) AND ISNULL(@To_Batch_ID, rb.RunID) 
AND		codrh.CurrencyID = ISNULL(@Currency_ID,codrh.CurrencyID)
AND		codrh.STATUS IN (42000, 42001) --needs to be sent, sent
GROUP BY Product.Fulfill_House_Nbr,
		Product.Pub_Nbr,
		codrh.RemitCode,
		CASE LEFT(codrh.TitleCode, 1) WHEN 'S' THEN 1 ELSE 0 END,
		rb.RunID,
		crh.State,
		codrh.MagazineTitle,
		pd.Effort_Key,
		codrh.EffortKey,
		pub.Pub_Name,
		fh.Ful_Name,
		rb.[Date], 
		codrh.NumberOfIssues,
		codrh.BasePrice,
		codrh.RemitRate,
		codrh.CurrencyID,
		pd.BasePriceSansPostage,
		pd.BaseRemitRate,
		pd.PostageAmount,
		pd.PostageRemitRate


SELECT	Fulfill_House_Nbr,
		Pub_Nbr,
		TitleCode,
		PDEKey,
		RHEKey,
		CurrencyID,
		RunID, 
		ROUND(SUM(TotalBasePrice),2) AS TotalBasePrice, 
		ROUND(SUM(TotalRemit),2) AS TotalRemit, 
		isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and RunID = inl.RunID and state  not in ( 'NB','NS','NL','ON','PE')  ),0) +
		isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and RunID = inl.RunID and state  in ( 'NB','NS','NL','ON','PE') ) ,0)	+
		isnull(round(sum(TAX2),2),0) as Tax,
		isnull(round(sum(TotalRemit),2),0) +
		isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and RunID = inl.RunID and state  not in ( 'NB','NS','NL','ON','PE') ),0) +
		isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and RunID = inl.RunID and state  in ( 'NB','NS','NL','ON','PE') ) ,0)	+
		isnull(round(sum(TAX2),2),0) as TotalPayment
INTO	#TitleBatchTotals  
FROM	#InitialTotals AS inl
GROUP BY Fulfill_House_Nbr,
		Pub_Nbr,
		TitleCode,
		PDEKey,
		RHEKey,
		RunID,
		CurrencyID   

SELECT  Fulfill_House_Nbr,
	 Pub_Nbr,
	 TitleCode,
	 PDEKey,RHEKey,
	 CurrencyID,
   	 round(sum(TotalBasePrice),2) 	as TotalBasePrice,
	 round(sum(TotalRemit),2) 	as TotalRemit,
	 isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and state  not in ( 'NB','NS','NL','ON','PE')  ),0) 	as GST,
	 isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and state  in ( 'NB','NS','NL','ON','PE')  ),0) 		as HST,
	 isnull(Round(sum(TAX2),2),0) 																              as PST, 

	 isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and state  not in ( 'NB','NS','NL','ON','PE')  ),0) +
	 isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and state  in ( 'NB','NS','NL','ON','PE')  ),0) 	+
	 isnull(round(sum(TAX2),2),0) as Tax,

              isnull( round(sum(TotalRemit),2),0) +
 	 isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and state  not in ( 'NB','NS','NL','ON','PE')  ),0) +
	 isnull((select Round(Sum(Isnull(TAX1,0)),2) from #InitialTotals where coalesce(PDEKey,'') = coalesce(inl.PDEKEY,'') and coalesce(RHEKey,'') = coalesce(inl.RHEKey,'') and TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID and state  in ( 'NB','NS','NL','ON','PE')  ) ,0)	+
	 isnull(round(sum(TAX2),2),0) as TotalPayment

 Into #TitleTotals 
 FROM     #InitialTotals inl
 Group by Fulfill_House_Nbr, Pub_Nbr,TitleCode,PDEKey,RHEKey,CurrencyID 


 SELECT  Fulfill_House_Nbr,
	 Pub_Nbr,
	 CurrencyID,
   	 sum(TotalBasePrice) as TotalBasePrice,
	 sum(TotalRemit) as TotalRemit,
	 sum(TAX) as TAX,
	 sum(TotalPayment) as TotalPayment
into #PubTotals
FROM     #TitleTotals
Group by Fulfill_House_Nbr, Pub_Nbr,CurrencyID

 SELECT  Fulfill_House_Nbr,
	 CurrencyID,
   	 sum(TotalBasePrice) as TotalBasePrice,
	 sum(TotalRemit) as TotalRemit,
	 sum(TAX) as TAX,
	 sum(TotalPayment) as TotalPayment
into #fulTotals
FROM     #PubTotals
Group by Fulfill_House_Nbr, CurrencyID

 SELECT @RepBasePriceTotal_CAD=sum(TotalBasePrice) from #fulTotals  where  CurrencyID  = 'CAD'  
 SELECT @RepBasePriceTotal_USD=sum(TotalBasePrice) from #fulTotals  where  CurrencyID  = 'USD'  

 SELECT  @ReportTotal_CAD=sum(TotalPayment) FROM     #fulTotals where CurrencyID = 'CAD'  
 SELECT  @ReportTotal_USD=sum(TotalPayment) FROM     #fulTotals where CurrencyID = 'USD'  

SELECT	Fulfill_House_Nbr,
		Ful_Name, 
		Pub_Nbr,
		Pub_Name,
		TitleCode,
		StaffTitle,
		PDEKey,
		RHEKey,
		MagazineTitle,
		RunID,
		RemitBatchDate,
		State,
		NumberOfIssues,
		BasePrice,
		RemitRate,
		CurrencyID,  
		Total_Subs,  
		TotalAtSubRate, 
		RemitPerSub,
		ROUND(TotalRemit, 2) AS TotalRemit,
		(select sum(TotalBasePrice) from #TitleBatchTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 	as BatchTotalBasePrice,
		(select sum(TotalRemit) from #TitleBatchTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 	as BatchTotalRemit,
		(select sum(TAX) from #TitleBatchTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 		as BatchTotalTAX,
		(select sum(TotalPayment) from #TitleBatchTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 	as BatchTotalPayment,
		(select sum(TotalBasePrice) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 	as MagTotalBasePrice,
		(select sum(TotalRemit) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 	 	as MagTotalRemit,
		(select sum(GST) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 		as MagTotalGST,
		(select sum(HST) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 		as MagTotalHST, 
		(select sum(PST) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 		              as MagTotalPST, 
		(select sum(TAX) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 			as MagTotalTAX,
		(select sum(TotalPayment) from #TitleTotals where TitleCode = inl.TitleCode and CurrencyID = inl.CurrencyID ) 	as MagTotalPayment,
		(select sum(TotalBasePrice) from #PubTotals where Pub_Nbr = inl.Pub_Nbr and CurrencyID = inl.CurrencyID ) 	as PubTotalBasePrice,
		(select sum(TotalRemit) from #PubTotals where Pub_Nbr = inl.Pub_Nbr and CurrencyID = inl.CurrencyID ) 		as PubTotalRemit,
		(select sum(TAX) from #PubTotals where Pub_Nbr = inl.Pub_Nbr and CurrencyID = inl.CurrencyID ) 			as PubTotalTAX,
		(select sum(TotalPayment) from #PubTotals where Pub_Nbr = inl.Pub_Nbr and CurrencyID = inl.CurrencyID ) 		as PubTotalPayment,
		(select sum(TotalBasePrice) from #fulTotals where Fulfill_House_Nbr = inl.Fulfill_House_Nbr and CurrencyID = inl.CurrencyID ) as FulTotalBasePrice,
		(select sum(TotalRemit) from #fulTotals where Fulfill_House_Nbr = inl.Fulfill_House_Nbr and CurrencyID = inl.CurrencyID ) 	as FulTotalRemit,
		(select sum(TAX) from #fulTotals where Fulfill_House_Nbr = inl.Fulfill_House_Nbr and CurrencyID = inl.CurrencyID ) 		as FulTotalTAX,
		(select sum(TotalPayment) from #fulTotals where Fulfill_House_Nbr = inl.Fulfill_House_Nbr and CurrencyID = inl.CurrencyID ) 	as FulTotalPayment,
		@RepBasePriceTotal_CAD	as RepBasePriceTotal_CAD,
		@RepBasePriceTotal_USD 	as RepBasePriceTotal_USD,  
		@ReportTotal_CAD 		as ReportTotal_CAD,
		@ReportTotal_USD 		as ReportTotal_USD
FROM    #InitialTotals AS inl
ORDER BY CurrencyID,
		Ful_Name,
		Pub_Name,
		TitleCode,
		RunID,
		State 

drop table #InitialTotals
drop table #TitleBatchTotals 
drop table #TitleTotals
drop table #PubTotals
drop table #fulTotals
GO
