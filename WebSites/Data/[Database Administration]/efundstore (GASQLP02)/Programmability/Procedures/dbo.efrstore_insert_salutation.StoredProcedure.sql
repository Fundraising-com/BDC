USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_salutation]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Salutation
CREATE PROCEDURE [dbo].[efrstore_insert_salutation] @Salutation_id int OUTPUT, @Description varchar(10) AS
begin

insert into Salutation(Description) values(@Description)

select @Salutation_id = SCOPE_IDENTITY()

end
GO
