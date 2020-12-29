USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_program_partner]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Program_partner
CREATE PROCEDURE [dbo].[efrstore_update_program_partner] @Program_id int, @Partner_id int, @Program_url varchar(255), @Create_date datetime AS
begin

update Program_partner set Partner_id=@Partner_id, Program_url=@Program_url, Create_date=@Create_date where Program_id=@Program_id

end
GO
