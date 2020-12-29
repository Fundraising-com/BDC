USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_visit_by_id]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Visit
CREATE PROCEDURE [dbo].[efrcrm_get_lead_visit_by_id] @Lead_Visit_ID int AS
begin

select Lead_Visit_ID, Promotion_ID, Lead_ID, Temp_Lead_ID, Visit_Date, Channel_Code from Lead_Visit where Lead_Visit_ID=@Lead_Visit_ID

end
GO
