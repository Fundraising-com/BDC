USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitBatchByStatusByDateNestedGridSecondLevel]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_RemitBatchByStatusByDateNestedGridSecondLevel] 

@iStatus int,
@daDateTo datetime = null,
@daDateFrom datetime = null

AS


if(@daDateFrom = null or @daDateTo = null)
Begin
SELECT    LEFT( CONVERT(varchar, rb.[Date], 120), 10) as Date, rb.[ID],rb.[Filename], rb.FulfillmentHouseNbr, rb.TotalBasePrice, rb.TotalUnits, rb.TotalCHADD, rb.TotalCancelled, rb.DateChanged, rb.UserIDChanged, rb.Status, 
                      rb.CountryCode,fh.Ful_Name, gco.[ID] as GiftCardOutputID,gco.[FileName] as FileNameCard, gcb.GiftCardOutputID,totalCatalogprice,totalitemprice
FROM         dbo.RemitBatch rb  inner join QSPCanadaProduct..FULFILLMENT_HOUSE fh  on rb.FulfillmentHouseNbr = fh.Ful_Nbr
		left outer join GiftCardRemitBatch gcb on gcb.RemitBatchId = rb.[ID]
		left outer join GiftCardOutput gco on gcb.GiftCardOutputID = gco.[ID]

where  rb.Status = @iStatus and gcb.GiftCardOutputID is not null

end

else
Begin
SELECT    LEFT( CONVERT(varchar, rb.[Date], 120), 10) as Date, rb.[ID],rb.[Filename], rb.FulfillmentHouseNbr, rb.TotalBasePrice, rb.TotalUnits, rb.TotalCHADD, rb.TotalCancelled, rb.DateChanged, rb.UserIDChanged, rb.Status, 
                      rb.CountryCode,fh.Ful_Name, gco.[ID] as GiftCardOutputID,gco.[FileName] as FileNameCard, gcb.GiftCardOutputID,totalCatalogprice,totalitemprice
FROM         dbo.RemitBatch rb  inner join QSPCanadaProduct..FULFILLMENT_HOUSE fh  on rb.FulfillmentHouseNbr = fh.Ful_Nbr
		left outer join GiftCardRemitBatch gcb on gcb.RemitBatchId = rb.[ID]
		left outer join GiftCardOutput gco on gcb.GiftCardOutputID = gco.[ID]

where  rb.Status = @iStatus and gcb.GiftCardOutputID is not null and  LEFT( CONVERT(varchar, rb.[Date], 120), 10)  between (LEFT( CONVERT(varchar,@daDateFrom, 120), 10) ) and  LEFT( CONVERT(varchar,@daDateTo, 120), 10)


end
GO
