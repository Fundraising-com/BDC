USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_language]    Script Date: 02/14/2014 13:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Language
CREATE PROCEDURE [dbo].[efrcrm_insert_req_language] @Language_Id int OUTPUT, @Language varchar(30) AS
begin

insert into Req_Language(Language) values(@Language)

select @Language_Id = SCOPE_IDENTITY()

end
GO
