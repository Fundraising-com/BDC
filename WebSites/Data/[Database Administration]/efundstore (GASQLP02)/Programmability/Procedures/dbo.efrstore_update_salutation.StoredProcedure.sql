USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_salutation]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Salutation
CREATE PROCEDURE [dbo].[efrstore_update_salutation] @Salutation_id tinyint, @Description varchar(10) AS
begin

update Salutation set Description=@Description where Salutation_id=@Salutation_id

end
GO
