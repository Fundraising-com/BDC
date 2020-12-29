USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_unsubscribe]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Unsubscribe
CREATE PROCEDURE [dbo].[efrstore_insert_unsubscribe] @Unsubscribe_id int OUTPUT, @Email varchar(100), @Unsubscribed bit, @Unsubscribed_date datetime AS
begin

insert into Unsubscribe(Email, Unsubscribed, Unsubscribed_date) values(@Email, @Unsubscribed, @Unsubscribed_date)

select @Unsubscribe_id = SCOPE_IDENTITY()

end
GO
