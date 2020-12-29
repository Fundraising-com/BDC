USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_request_type]    Script Date: 02/14/2014 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Request_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_req_request_type] @Request_Type_ID int OUTPUT, @Language_Id int, @Description varchar(50) AS
begin

insert into Req_Request_Type(Language_Id, Description) values(@Language_Id, @Description)

select @Request_Type_ID = SCOPE_IDENTITY()

end
GO
