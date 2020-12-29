USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_activity_type_by_id]    Script Date: 02/14/2014 13:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_get_lead_activity_type_by_id] @Lead_Activity_Type_Id int AS
begin

select Lead_Activity_Type_Id, Description, Priority from Lead_Activity_Type where Lead_Activity_Type_Id=@Lead_Activity_Type_Id

end
GO
