USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_xjumpstart]    Script Date: 02/14/2014 13:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for XJumpstart
CREATE PROCEDURE [dbo].[efrcrm_insert_xjumpstart] @Lead_id int OUTPUT, @Year int AS
begin

insert into XJumpstart(Year) values(@Year)

select @Lead_id = SCOPE_IDENTITY()

end
GO
