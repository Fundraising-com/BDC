USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_status_by_id]    Script Date: 02/14/2014 13:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Status
CREATE PROCEDURE [dbo].[efrcrm_get_efo_status_by_id] @Status_ID int AS
begin

select Status_ID, Status from EFO_Status where Status_ID=@Status_ID

end
GO
