USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_request_type_by_id]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Request_Type
CREATE PROCEDURE [dbo].[efrcrm_get_req_request_type_by_id] @Request_Type_ID int AS
begin

select Request_Type_ID, Language_Id, Description from Req_Request_Type where Request_Type_ID=@Request_Type_ID

end
GO
