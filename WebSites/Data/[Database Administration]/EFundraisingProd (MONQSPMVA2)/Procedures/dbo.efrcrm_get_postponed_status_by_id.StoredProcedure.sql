USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_postponed_status_by_id]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Postponed_Status
CREATE PROCEDURE [dbo].[efrcrm_get_postponed_status_by_id] @Postponed_Status_ID int AS
begin

select Postponed_Status_ID, Description from Postponed_Status where Postponed_Status_ID=@Postponed_Status_ID

end
GO
