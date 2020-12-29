USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_priority]    Script Date: 02/14/2014 13:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Priority
CREATE PROCEDURE [dbo].[efrcrm_insert_priority] @Priority_ID int OUTPUT, @Description varchar(50), @Color_Code int AS
begin

insert into Priority(Description, Color_Code) values(@Description, @Color_Code)

select @Priority_ID = SCOPE_IDENTITY()

end
GO
