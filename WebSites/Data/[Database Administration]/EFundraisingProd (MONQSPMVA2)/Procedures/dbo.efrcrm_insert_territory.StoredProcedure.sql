USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_territory]    Script Date: 02/14/2014 13:07:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Territory
CREATE PROCEDURE [dbo].[efrcrm_insert_territory] @Territory_id int OUTPUT, @Territory_name varchar(25) AS
begin

insert into Territory(Territory_name) values(@Territory_name)

select @Territory_id = SCOPE_IDENTITY()

end
GO
