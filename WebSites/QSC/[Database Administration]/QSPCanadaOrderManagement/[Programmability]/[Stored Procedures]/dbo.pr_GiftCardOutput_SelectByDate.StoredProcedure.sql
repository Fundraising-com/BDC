USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GiftCardOutput_SelectByDate]    Script Date: 06/07/2017 09:20:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GiftCardOutput_SelectByDate] 

@daDateFrom datetime,
@daDateTo datetime

AS

if(@daDateFrom = null or @daDateTo = null)
BEGIN

SELECT gco.Date, gco.ID, gco.FileName, count(gcrb.RemitBatchID)AS TotalRemitBatches
   FROM GiftCardOutput gco, GiftCardRemitBatch gcrb
WHERE gco.ID = gcrb.GiftCardOutputID
GROUP BY gco.Date, gco.ID, gco.FileName
END

else
BEGIN

SELECT gco.Date, gco.ID, gco.FileName, count(gcrb.RemitBatchID)AS TotalRemitBatches
   FROM GiftCardOutput gco, GiftCardRemitBatch gcrb
WHERE gco.ID = gcrb.GiftCardOutputID  and  LEFT( CONVERT(varchar, gco.[Date], 120), 10) between LEFT( CONVERT(varchar,@daDateFrom, 120), 10) and LEFT( CONVERT(varchar, @daDateTo, 120), 10)
GROUP BY gco.Date, gco.ID, gco.FileName

END
GO
