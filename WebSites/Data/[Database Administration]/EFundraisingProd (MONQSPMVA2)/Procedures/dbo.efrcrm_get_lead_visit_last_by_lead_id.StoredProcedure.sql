USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_visit_last_by_lead_id]    Script Date: 02/14/2014 13:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Visit
CREATE PROCEDURE [dbo].[efrcrm_get_lead_visit_last_by_lead_id] 
@Lead_ID int AS
begin

select TOP 1 Lead_Visit_ID, Promotion_ID, Lead_ID, Temp_Lead_ID, Visit_Date, Channel_Code
 from Lead_Visit
 where Lead_ID=@Lead_ID order by Visit_Date DESC

end
GO
