USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_production_status_by_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Production_Status
CREATE PROCEDURE [dbo].[efrcrm_get_production_status_by_id] @Production_Status_ID int AS
begin

select Production_Status_ID, Description from Production_Status where Production_Status_ID=@Production_Status_ID

end
GO
