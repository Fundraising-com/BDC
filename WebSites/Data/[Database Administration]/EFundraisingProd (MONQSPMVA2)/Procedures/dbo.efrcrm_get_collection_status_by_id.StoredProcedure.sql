USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_collection_status_by_id]    Script Date: 02/14/2014 13:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Collection_Status
CREATE PROCEDURE [dbo].[efrcrm_get_collection_status_by_id] @Collection_Status_ID int AS
begin

select Collection_Status_ID, Description from Collection_Status where Collection_Status_ID=@Collection_Status_ID

end
GO
