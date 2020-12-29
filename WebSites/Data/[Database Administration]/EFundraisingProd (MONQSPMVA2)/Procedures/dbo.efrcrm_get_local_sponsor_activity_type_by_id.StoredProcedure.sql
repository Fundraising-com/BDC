USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_local_sponsor_activity_type_by_id]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Local_Sponsor_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_get_local_sponsor_activity_type_by_id] @Local_Sponsor_Activity_Type_Id int AS
begin

select Local_Sponsor_Activity_Type_Id, Description from Local_Sponsor_Activity_Type where Local_Sponsor_Activity_Type_Id=@Local_Sponsor_Activity_Type_Id

end
GO
