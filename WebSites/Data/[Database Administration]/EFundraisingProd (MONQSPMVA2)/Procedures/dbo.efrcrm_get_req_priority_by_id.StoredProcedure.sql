USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_priority_by_id]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Priority
CREATE PROCEDURE [dbo].[efrcrm_get_req_priority_by_id] @Priority_Id int AS
begin

select Priority_Id, Language_Id, Description, Is_Default from Req_Priority where Priority_Id=@Priority_Id

end
GO
