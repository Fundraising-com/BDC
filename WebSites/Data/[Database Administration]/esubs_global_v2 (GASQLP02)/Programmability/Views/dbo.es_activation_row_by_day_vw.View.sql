USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[es_activation_row_by_day_vw]    Script Date: 02/14/2014 13:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[es_activation_row_by_day_vw] AS
SELECT 	p.partner_id
	  , p.partner_name
      , DATEPART(yy,es.CreateDate) AS ActivationYear
      , DATEPART(mm,es.CreateDate) AS ActivationMonth
      , DATEPART(dd,es.CreateDate) AS ActivationDay
      , COUNT(activation_trx_id) AS Activations
  FROM es_activation_row_vw es 
 INNER JOIN event ev ON ev.event_id = es.event_id
 INNER JOIN dbo.event_group eg ON eg.event_id = ev.event_id
 INNER JOIN dbo.[group] g ON g.group_id = eg.group_id
 INNER JOIN dbo.partner p ON p.partner_id = g.partner_id
WHERE es.CreateDate > '01-01-2010'
GROUP BY p.partner_id,p.partner_name, DATEPART(yy,es.CreateDate), DATEPART(mm,es.CreateDate), DATEPART(dd,es.CreateDate)
GO
