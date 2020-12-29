USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_salutation_desc]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Salutation_desc
CREATE PROCEDURE [dbo].[efrstore_update_salutation_desc] @Salutation_id tinyint, @Culture_code nvarchar(10), @Description varchar(15) AS
begin

update Salutation_desc set Culture_code=@Culture_code, Description=@Description where Salutation_id=@Salutation_id

end
GO
