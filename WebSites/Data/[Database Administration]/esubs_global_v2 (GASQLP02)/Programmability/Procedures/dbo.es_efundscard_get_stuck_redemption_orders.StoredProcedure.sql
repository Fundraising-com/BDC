USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_efundscard_get_stuck_redemption_orders]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_efundscard_get_stuck_redemption_orders]

AS

SELECT		o.Order_ID,
			o.Order_Date,
			o.SupporterID
FROM		dbo.es_efundscard_get_redemption_orders() o
LEFT JOIN	[Group] g
				ON g.Lead_ID = o.Lead_ID 
WHERE		(g.Group_ID IS NULL OR o.Group_ID <> g.Group_ID)
AND			o.Order_Date < DATEADD(dd, -2, GETDATE())
--AND         g.partner_id = 816
GO
