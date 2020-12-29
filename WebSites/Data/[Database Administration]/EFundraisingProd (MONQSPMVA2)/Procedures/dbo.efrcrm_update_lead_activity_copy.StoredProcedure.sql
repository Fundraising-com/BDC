USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_activity_copy]    Script Date: 02/14/2014 13:08:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Activity_copy
CREATE PROCEDURE [dbo].[efrcrm_update_lead_activity_copy] @Lead_Activity_Id int, @Lead_Id int, @Lead_Activity_Type_Id int, @Lead_Activity_Date datetime, @Completed_Date datetime, @Comments text AS
begin

update Lead_Activity_copy set Lead_Id=@Lead_Id, Lead_Activity_Type_Id=@Lead_Activity_Type_Id, Lead_Activity_Date=@Lead_Activity_Date, Completed_Date=@Completed_Date, Comments=@Comments where Lead_Activity_Id=@Lead_Activity_Id

end
GO
