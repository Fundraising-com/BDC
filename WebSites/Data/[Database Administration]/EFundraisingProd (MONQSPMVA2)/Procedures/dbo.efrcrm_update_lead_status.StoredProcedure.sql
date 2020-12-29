USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_status]    Script Date: 02/14/2014 13:08:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Status
CREATE PROCEDURE [dbo].[efrcrm_update_lead_status] @Lead_Status_ID int, @Description varchar(50) AS
begin

update Lead_Status set Description=@Description where Lead_Status_ID=@Lead_Status_ID

end
GO
