USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_req_request_type]    Script Date: 02/14/2014 13:08:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Req_Request_Type
CREATE PROCEDURE [dbo].[efrcrm_update_req_request_type] @Request_Type_ID int, @Language_Id int, @Description varchar(50) AS
begin

update Req_Request_Type set Language_Id=@Language_Id, Description=@Description where Request_Type_ID=@Request_Type_ID

end
GO
