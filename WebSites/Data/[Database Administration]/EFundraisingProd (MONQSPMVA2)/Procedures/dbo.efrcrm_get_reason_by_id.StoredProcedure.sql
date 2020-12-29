USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_reason_by_id]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Reason
CREATE PROCEDURE [dbo].[efrcrm_get_reason_by_id] @Reason_ID int AS
begin

select Reason_ID, Description, Is_Active from Reason where Reason_ID=@Reason_ID

end
GO
