USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_decision]    Script Date: 02/14/2014 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Decision
CREATE PROCEDURE [dbo].[efrcrm_insert_req_decision] @Decision_Id int OUTPUT, @Language_Id int, @Description varchar(100) AS
begin

insert into Req_Decision(Language_Id, Description) values(@Language_Id, @Description)

select @Decision_Id = SCOPE_IDENTITY()

end
GO
