USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_local_sponsor_steps]    Script Date: 02/14/2014 13:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor_Steps
CREATE PROCEDURE [dbo].[efrcrm_update_local_sponsor_steps] @Step_Id int, @Description varchar(50) AS
begin

update Local_Sponsor_Steps set Description=@Description where Step_Id=@Step_Id

end
GO
