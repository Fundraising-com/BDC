USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_priority]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Priority
CREATE PROCEDURE [dbo].[efrcrm_update_lead_priority] @Lead_Priority_Id int, @Description varchar(50) AS
begin

update Lead_Priority set Description=@Description where Lead_Priority_Id=@Lead_Priority_Id

end
GO
