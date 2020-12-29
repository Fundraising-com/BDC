USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_production_status]    Script Date: 02/14/2014 13:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Production_Status
CREATE PROCEDURE [dbo].[efrcrm_update_production_status] @Production_Status_ID int, @Description varchar(50) AS
begin

update Production_Status set Description=@Description where Production_Status_ID=@Production_Status_ID

end
GO
