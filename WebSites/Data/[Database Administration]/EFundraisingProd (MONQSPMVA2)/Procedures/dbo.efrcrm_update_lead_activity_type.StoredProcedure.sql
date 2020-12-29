USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_activity_type]    Script Date: 02/14/2014 13:08:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_update_lead_activity_type] @Lead_Activity_Type_Id int, @Description varchar(50), @Priority int AS
begin

update Lead_Activity_Type set Description=@Description, Priority=@Priority where Lead_Activity_Type_Id=@Lead_Activity_Type_Id

end
GO
