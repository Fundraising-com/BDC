USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GiftCardOutput_SelectAll]    Script Date: 06/07/2017 09:20:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GiftCardOutput_SelectAll] AS

--select * from GiftCardOutput gco inner join GiftCardRemitBatch gcb on gco.id = gcb.RemitBatchID

SELECT gco.Date, gco.ID, gco.FileName, count(gcrb.RemitBatchID)AS TotalRemitBatches
   FROM GiftCardOutput gco, GiftCardRemitBatch gcrb
WHERE gco.ID = gcrb.GiftCardOutputID
GROUP BY gco.Date, gco.ID, gco.FileName
GO
