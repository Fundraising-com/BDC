USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_salutation_desc]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Salutation_desc
CREATE PROCEDURE [dbo].[efrstore_insert_salutation_desc] @Salutation_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(15) AS
begin

insert into Salutation_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Salutation_id = SCOPE_IDENTITY()

end
GO
