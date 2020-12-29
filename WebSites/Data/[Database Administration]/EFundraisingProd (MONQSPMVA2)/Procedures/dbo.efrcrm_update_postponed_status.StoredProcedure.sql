USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_postponed_status]    Script Date: 02/14/2014 13:08:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Postponed_Status
CREATE PROCEDURE [dbo].[efrcrm_update_postponed_status] @Postponed_Status_ID int, @Description varchar(30) AS
begin

update Postponed_Status set Description=@Description where Postponed_Status_ID=@Postponed_Status_ID

end
GO
