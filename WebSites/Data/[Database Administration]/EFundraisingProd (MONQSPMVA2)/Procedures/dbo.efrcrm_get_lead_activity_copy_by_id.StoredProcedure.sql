USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_activity_copy_by_id]    Script Date: 02/14/2014 13:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Activity_copy
CREATE PROCEDURE [dbo].[efrcrm_get_lead_activity_copy_by_id] @Lead_Activity_Id int AS
begin

select Lead_Activity_Id, Lead_Id, Lead_Activity_Type_Id, Lead_Activity_Date, Completed_Date, Comments from Lead_Activity_copy where Lead_Activity_Id=@Lead_Activity_Id

end
GO
