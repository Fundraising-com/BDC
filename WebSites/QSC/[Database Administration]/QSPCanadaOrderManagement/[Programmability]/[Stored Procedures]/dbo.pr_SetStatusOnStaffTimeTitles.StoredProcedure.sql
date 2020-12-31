USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SetStatusOnStaffTimeTitles]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_SetStatusOnStaffTimeTitles]
	@orderid int
as
	update CustomerOrderDetail set StatusInstance=515  
--select orderid,* 
	FROM			Batch,
				CustomerOrderHeader coh,
				CustomerOrderDetail cod,
				QSPCanadaCommon..Campaign c,
				QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p
	WHERE		
	Date = orderbatchdate and batch.id = orderbatchid
	and			cod.CustomerOrderHeaderInstance = coh.Instance	
	AND			c.ID = coh.CampaignID
	AND			pd.MagPrice_Instance = cod.PricingDetailsID
	AND			p.Product_Instance = pd.Product_Instance
	AND			c.IsStaffOrder = 1
	AND			p.Pub_Nbr in ( 57, 58)
--and date >='7/1/06'
	and	 orderid=@orderid
GO
