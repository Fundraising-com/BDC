USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_local_sponsor_activity_type]    Script Date: 02/14/2014 13:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_update_local_sponsor_activity_type] @Local_Sponsor_Activity_Type_Id int, @Description varchar(50) AS
begin

update Local_Sponsor_Activity_Type set Description=@Description where Local_Sponsor_Activity_Type_Id=@Local_Sponsor_Activity_Type_Id

end
GO
