USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SetStatusOnLoonieTimeTitles]    Script Date: 06/07/2017 09:20:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SetStatusOnLoonieTimeTitles]
	@orderid int
AS
update CustomerOrderDetail set StatusInstance=516
--select orderid,* 
	FROM			Batch,
				CustomerOrderHeader coh,
				CustomerOrderDetail cod,
				QSPCanadaCommon..Campaign c,
				QSPCanadaCommon..CampaignProgram cp,
				QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p
	WHERE		
	Date = orderbatchdate and batch.id = orderbatchid
	and			cod.CustomerOrderHeaderInstance = coh.Instance	
	AND			c.ID = coh.CampaignID
	AND			cp.CampaignID = c.ID
	AND			pd.MagPrice_Instance = cod.PricingDetailsID
	AND			p.Product_Instance = pd.Product_Instance
	AND			c.IsStaffOrder = 0 -- Staff CAs are already 515
	AND			cp.ProgramID = 24 -- Loonie
	AND			cp.DeletedTF = 0
	AND			p.Pub_Nbr in ( 57, 58)
--and date >='7/1/06'
	and	 orderid=@orderid
GO
