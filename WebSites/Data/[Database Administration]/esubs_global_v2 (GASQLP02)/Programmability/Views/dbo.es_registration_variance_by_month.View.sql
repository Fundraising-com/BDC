USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[es_registration_variance_by_month]    Script Date: 02/14/2014 13:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[es_registration_variance_by_month] AS 
SELECT er.partner_id, er.partner_name, er.registrationyear AS PY_registrationyear, er.registrationmonth AS PY_registrationmonth
, er.Registrations AS PY_Registrations, er2.registrationyear, er2.registrationmonth, er2.Registrations
, (er2.Registrations - er.Registrations) AS Variance
FROM [es_registration_row_by_month_vw] er
JOIN [es_registration_row_by_month_vw] er2 ON er.partner_id = er2.partner_id AND er.RegistrationMonth = er2.RegistrationMonth
WHERE er.RegistrationYear = 2011
AND er2.RegistrationYear = 2012
GO
