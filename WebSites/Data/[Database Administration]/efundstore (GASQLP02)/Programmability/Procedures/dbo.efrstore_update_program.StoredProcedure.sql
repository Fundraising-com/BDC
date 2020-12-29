USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_program]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Program
CREATE PROCEDURE [dbo].[efrstore_update_program] @Program_id int, @Name varchar(50), @Create_date datetime AS
begin

update Program set Name=@Name, Create_date=@Create_date where Program_id=@Program_id

end
GO
