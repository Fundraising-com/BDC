USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_local_sponsor_steps_by_id]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Local_Sponsor_Steps
CREATE PROCEDURE [dbo].[efrcrm_get_local_sponsor_steps_by_id] @Step_Id int AS
begin

select Step_Id, Description from Local_Sponsor_Steps where Step_Id=@Step_Id

end
GO
