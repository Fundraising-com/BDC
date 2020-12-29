USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_visits]    Script Date: 02/14/2014 13:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Visit
CREATE PROCEDURE [dbo].[efrcrm_get_lead_visits] AS
begin

select Lead_Visit_ID, Promotion_ID, Lead_ID, Temp_Lead_ID, Visit_Date, Channel_Code from Lead_Visit

end
GO
