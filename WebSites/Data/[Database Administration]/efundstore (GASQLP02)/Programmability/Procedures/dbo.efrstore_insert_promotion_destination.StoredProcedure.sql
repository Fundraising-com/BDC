USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_promotion_destination]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion_destination
CREATE PROCEDURE [dbo].[efrstore_insert_promotion_destination] @Promotion_destination_id int OUTPUT, @Url varchar(255), @Create_date datetime AS
begin

insert into Promotion_destination(Url, Create_date) values(@Url, @Create_date)

select @Promotion_destination_id = SCOPE_IDENTITY()

end
GO
