USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_local_sponsor_activity_by_id]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Local_Sponsor_Activity
CREATE PROCEDURE [dbo].[efrcrm_get_local_sponsor_activity_by_id] @Local_Sponsor_Activity_ID int AS
begin

select Local_Sponsor_Activity_ID, Local_Sponsor_Activity_Type_ID, Sales_ID, Sponsor_Consultant_ID, Local_Sponsor_Activity_Date, Completed_Date, Comments, Brand_ID, Local_Sponsor_ID from Local_Sponsor_Activity where Local_Sponsor_Activity_ID=@Local_Sponsor_Activity_ID

end
GO
