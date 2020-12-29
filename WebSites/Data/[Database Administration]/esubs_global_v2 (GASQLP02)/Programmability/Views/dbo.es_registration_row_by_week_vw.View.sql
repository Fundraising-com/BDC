USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[es_registration_row_by_week_vw]    Script Date: 02/14/2014 13:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[es_registration_row_by_week_vw] AS
SELECT prt.partner_id
	, prt.partner_name
	, DATEPART(yy,ev.create_date) AS RegistrationYear
	, DATEPART(mm,ev.create_date) AS RegistrationMonth
	, DATEPART(ww,ev.create_date) AS RegistrationWeek
	, count(distinct ev.event_id) as Registrations
FROM event ev 
	INNER JOIN event_group eg ON   ev.event_id = eg.event_id
	INNER JOIN [group] g ON g.group_id = eg.group_id
	INNER JOIN partner prt ON prt.partner_id = g.partner_id
WHERE ev.create_date >= '2010-01-01'
GROUP BY  prt.partner_id
	, prt.partner_name
	, DATEPART(yy,ev.create_date)
	, DATEPART(mm,ev.create_date)
	, DATEPART(ww,ev.create_date)
GO
