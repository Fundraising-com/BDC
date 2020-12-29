USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efr_lead_activity_by_id]    Script Date: 02/14/2014 13:04:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Efr_Lead_Activity
CREATE PROCEDURE [dbo].[efrcrm_get_efr_lead_activity_by_id] @Lead_Activity_Id int AS
begin

select Lead_Activity_Id, Lead_Id, Lead_Activity_Type_Id, Lead_Activity_Date, Completed_Date, Comments from Efr_Lead_Activity where Lead_Activity_Id=@Lead_Activity_Id

end
GO
