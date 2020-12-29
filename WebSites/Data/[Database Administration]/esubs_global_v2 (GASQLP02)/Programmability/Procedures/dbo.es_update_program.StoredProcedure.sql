USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_program]    Script Date: 02/14/2014 13:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Program
CREATE PROCEDURE [dbo].[es_update_program] @Program_id int, @Program_name varchar(50), @Create_date datetime AS
begin

update Program set Program_name=@Program_name, Create_date=@Create_date where Program_id=@Program_id

end
GO
