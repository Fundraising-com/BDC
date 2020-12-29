USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_status_by_id]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Status
CREATE PROCEDURE [dbo].[efrcrm_get_lead_status_by_id] @Lead_Status_ID int AS
begin

select Lead_Status_ID, Description from Lead_Status where Lead_Status_ID=@Lead_Status_ID

end
GO
