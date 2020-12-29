USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_xavier]    Script Date: 02/14/2014 13:07:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Xavier
CREATE PROCEDURE [dbo].[efrcrm_insert_xavier] @Lead_id int OUTPUT, @Type nvarchar(100) AS
begin

insert into Xavier(Type) values(@Type)

select @Lead_id = SCOPE_IDENTITY()

end
GO
