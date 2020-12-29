USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efr_lead_activity]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Efr_Lead_Activity
CREATE PROCEDURE [dbo].[efrcrm_update_efr_lead_activity] @Lead_Activity_Id int, @Lead_Id int, @Lead_Activity_Type_Id int, @Lead_Activity_Date smalldatetime, @Completed_Date smalldatetime, @Comments text AS
begin

update Efr_Lead_Activity set Lead_Id=@Lead_Id, Lead_Activity_Type_Id=@Lead_Activity_Type_Id, Lead_Activity_Date=@Lead_Activity_Date, Completed_Date=@Completed_Date, Comments=@Comments where Lead_Activity_Id=@Lead_Activity_Id

end
GO
