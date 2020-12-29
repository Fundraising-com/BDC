USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[es_activation_row_vw]    Script Date: 02/14/2014 13:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[es_activation_row_vw] AS
SELECT event_id, Min(et.CreateDate) as CreateDate, min(et.id) as activation_trx_id
  FROM event_participation ep 
 INNER JOIN qspecommerce.dbo.efundraisingtransaction et on ep.event_participation_id = et.suppid
GROUP BY event_id
GO
