USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_request_types]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Req_Request_Type
CREATE PROCEDURE [dbo].[efrcrm_get_req_request_types] AS
begin

select Request_Type_ID, Language_Id, Description from Req_Request_Type

end
GO
