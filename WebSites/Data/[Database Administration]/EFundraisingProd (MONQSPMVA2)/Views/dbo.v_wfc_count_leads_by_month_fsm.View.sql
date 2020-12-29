USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_wfc_count_leads_by_month_fsm]    Script Date: 02/14/2014 13:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_wfc_count_leads_by_month_fsm]
AS
SELECT     lead_year, lead_month, COUNT(Lead_ID) AS nb_leads
FROM         dbo.v_wfc_leads
WHERE     (Is_Fm = 1)
GROUP BY lead_month, lead_year
GO
