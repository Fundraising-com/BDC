USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_efundscard_get_redemption_orders]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_efundscard_get_redemption_orders]()

RETURNS TABLE

AS

RETURN
(
	SELECT		DISTINCT
				o.Order_ID,
				efrTran.SuppID AS SupporterID,
				c.Lead_ID,
				eg.Group_ID,
				o.Order_Date
	FROM        QSPFulfillment.dbo.[Order] o
	JOIN		QSPFulfillment.dbo.Campaign camp
					ON	camp.Campaign_ID = o.Campaign_ID
	JOIN		QSPFulfillment.dbo.Account acc
					ON	acc.Account_ID = camp.Account_ID
	JOIN		QSPFulfillment.dbo.Discount_Order do
					ON	do.Order_ID = o.Order_ID
	JOIN		QSPFulfillment.dbo.Discount d
					ON	d.Discount_ID = do.Discount_ID
	JOIN		QSPFulfillment.dbo.Discount_Program_Discount_Class dpdc
					ON	dpdc.Discount_Program_Discount_Class_ID = d.Discount_Program_Discount_Class_ID
					AND	dpdc.Discount_Class_ID = 24 --24: EFR eFunds Card
	JOIN		EFundraisingProd..Sale s
					ON	s.Ext_Order_ID = d.Order_ID
	JOIN		EFundraisingProd..Client c 
					ON	c.Client_ID = s.Client_ID
					AND	c.Client_Sequence_Code = s.Client_Sequence_Code 
	JOIN		QSPEcommerce.dbo.EFundraisingTransaction efrTran
					ON	efrTran.OrderID = o.Order_ID
	LEFT JOIN	(Event_Participation ep 
	JOIN		Event_Group eg
					ON	eg.Event_ID = ep.Event_ID)
				ON	ep.Event_Participation_ID = efrTran.SuppID
	WHERE       acc.Account_ID in (23138,697202) --Efunds Card Partner
)
GO
