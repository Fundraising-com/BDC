USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_efundscard_get_new_redemption_orders]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_efundscard_get_new_redemption_orders]

AS

SELECT	o.Order_ID,
		o.SupporterID,
		g.Group_ID
FROM	dbo.es_efundscard_get_redemption_orders() o
JOIN	[Group] g
			ON g.Lead_ID = o.Lead_ID 
WHERE	o.Group_ID <> g.Group_ID
  --AND   g.partner_id = 816
GO
