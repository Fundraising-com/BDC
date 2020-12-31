USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GiftCardDetail_Old]    Script Date: 06/07/2017 09:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GiftCardDetail_Old] AS 
(SELECT codrh.RemitBatchID, 
       codrh.CustomerOrderHeaderInstance,
       codrh.TransID,
       b.CampaignID, 
       b.OrderID, 
       codrh.TitleCode,
       codrh.MagazineTitle, 
       codrh.Lang,
       codrh.NumberOfIssues,
       codrh.SupporterName,
       crh.FirstName,
       crh.LastName,
       crh.Address1,
       crh.Address2,
       crh.City,
       crh.State,
       crh.Zip,
       a.Name as GroupName,
       rb.RunID,
       c.IsStaffOrder    
  FROM CustomerOrderDetailRemitHistory codrh, CustomerRemitHistory crh, CustomerOrderHeader coh, CustomerOrderDetail cod, Batch b, QSPCanadaCommon..Campaign c, QSPCanadaCommon..CAccount a, RemitBatch rb 
 WHERE codrh.CustomerRemitHistoryInstance = crh.Instance AND 
       codrh.CustomerOrderHeaderInstance  = coh.Instance AND 
       codrh.CustomerOrderHeaderInstance  = cod.CustomerOrderHeaderInstance AND
       codrh.TransID  = cod.TransID AND
       coh.OrderBatchDate = b.Date AND 
       coh.OrderBatchID   = b.ID AND 
       b.CampaignID = c.ID AND
       c.BillToAccountID = a.ID AND
       codrh.RemitBatchID = rb.ID AND
       cod.IsGift = 1 AND 
       cod.IsGiftCardSent = 0 AND 
       codrh.GiftOrderType = 'R' AND 
       codrh.Status = '42001'
/*
UNION ALL 
SELECT codrh.RemitBatchID, 
       codrh.CustomerOrderHeaderInstance,
       codrh.TransID,
       b.CampaignID, 
       b.OrderID, 
       codrh.TitleCode,
       codrh.MagazineTitle, 
       codrh.Lang,
       codrh.NumberOfIssues,
       codrh.SupporterName,
       crh.FirstName,
       crh.LastName,
       crh.Address1,
       crh.Address2,
       crh.City,
       crh.State,
       crh.Zip,
       a.Name as GroupName  ,
       rb.RunID,
       c.IsStaffOrder     
  FROM CustomerOrderDetailRemitHistory codrh, CustomerRemitHistory crh, CustomerOrderHeader coh, CustomerOrderDetail cod, Batch b , QSPCanadaCommon..Campaign c, QSPCanadaCommon..CAccount a, RemitBatch rb  
 WHERE codrh.CustomerRemitHistoryInstance = crh.Instance AND 
       codrh.CustomerOrderHeaderInstance  = coh.Instance AND 
       codrh.CustomerOrderHeaderInstance  = cod.CustomerOrderHeaderInstance AND
       codrh.TransID  = cod.TransID AND
       coh.OrderBatchDate = b.Date AND 
       coh.OrderBatchID   = b.ID AND 
       b.CampaignID = c.ID AND
       c.BillToAccountID = a.ID AND
       codrh.RemitBatchID = rb.ID AND
       cod.IsGift = 1 AND 
       cod.IsGiftCardSent = 0 AND 
       codrh.GiftOrderType = 'X' AND 
       codrh.Status = '42001' */
--AND
  --     getDate() >= (SELECT CAST(TextValue AS DATETIME)   
--	    	       FROM QSPCanadaCommon..SystemOptions 
--		      WHERE KeyValue = 'first_mail_date_xmas_gift_card') AND
  --     getDate() < (SELECT CAST(TextValue AS DATETIME)   
	--    	       FROM QSPCanadaCommon..SystemOptions 
		--      WHERE KeyValue = 'last_mail_date_xmas_gift_card')

)
GO
