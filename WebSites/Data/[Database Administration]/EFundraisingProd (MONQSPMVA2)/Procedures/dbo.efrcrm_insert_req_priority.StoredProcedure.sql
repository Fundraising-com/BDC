USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_priority]    Script Date: 02/14/2014 13:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Priority
CREATE PROCEDURE [dbo].[efrcrm_insert_req_priority] @Priority_Id int OUTPUT, @Language_Id int, @Description varchar(50), @Is_Default bit AS
begin

insert into Req_Priority(Language_Id, Description, Is_Default) values(@Language_Id, @Description, @Is_Default)

select @Priority_Id = SCOPE_IDENTITY()

end
GO
