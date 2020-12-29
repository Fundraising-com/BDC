USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_collection_status]    Script Date: 02/14/2014 13:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Collection_Status
CREATE PROCEDURE [dbo].[efrcrm_update_collection_status] @Collection_Status_ID int, @Description varchar(50) AS
begin

update Collection_Status set Description=@Description where Collection_Status_ID=@Collection_Status_ID

end
GO
