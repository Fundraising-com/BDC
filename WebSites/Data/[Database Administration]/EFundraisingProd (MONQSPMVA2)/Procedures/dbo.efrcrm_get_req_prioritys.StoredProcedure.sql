USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_prioritys]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Req_Priority
CREATE PROCEDURE [dbo].[efrcrm_get_req_prioritys] AS
begin

select Priority_Id, Language_Id, Description, Is_Default from Req_Priority

end
GO
