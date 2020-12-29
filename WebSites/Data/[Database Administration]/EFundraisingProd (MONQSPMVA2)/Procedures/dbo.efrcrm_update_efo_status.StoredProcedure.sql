USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_status]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Status
CREATE PROCEDURE [dbo].[efrcrm_update_efo_status] @Status_ID int, @Status varchar(50) AS
begin

update EFO_Status set Status=@Status where Status_ID=@Status_ID

end
GO
