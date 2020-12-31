USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectRemitBatchByStatusByDate]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectRemitBatchByStatusByDate] 

@iStatus int,
@daDateTo datetime = '',
@daDateFrom datetime = ''

AS

if(@daDateFrom = '' or @daDateTo = '')
Begin



SELECT    LEFT( CONVERT(varchar, rb.[Date], 120), 10) as Date, rb.[ID],'Test', rb.FulfillmentHouseNbr, rb.TotalBasePrice, rb.TotalUnits, rb.TotalCHADD, rb.TotalCancelled, rb.DateChanged, rb.UserIDChanged, rb.Status, 
                      rb.CountryCode,fh.Ful_Name,  totalCatalogprice,totalitemprice
FROM         dbo.RemitBatch rb  inner join QSPCanadaProduct..FULFILLMENT_HOUSE fh  on rb.FulfillmentHouseNbr = fh.Ful_Nbr
		

where  rb.Status = @iStatus


END

else
BEGIN
SELECT    LEFT( CONVERT(varchar, rb.[Date], 120), 10) as Date, rb.[ID],rb.[Filename], rb.FulfillmentHouseNbr, rb.TotalBasePrice, rb.TotalUnits, rb.TotalCHADD, rb.TotalCancelled, rb.DateChanged, rb.UserIDChanged, rb.Status, 
                      rb.CountryCode,fh.Ful_Name,  totalCatalogprice,totalitemprice
FROM         dbo.RemitBatch rb  inner join QSPCanadaProduct..FULFILLMENT_HOUSE fh  on rb.FulfillmentHouseNbr = fh.Ful_Nbr

where  rb.Status = @iStatus  and  LEFT( CONVERT(varchar, rb.[Date], 120), 10)  between (LEFT( CONVERT(varchar,@daDateFrom, 120), 10) ) and  LEFT( CONVERT(varchar,@daDateTo, 120), 10)


END
GO
