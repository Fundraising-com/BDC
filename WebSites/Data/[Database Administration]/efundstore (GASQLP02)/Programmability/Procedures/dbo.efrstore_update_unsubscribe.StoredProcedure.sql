USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_unsubscribe]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Unsubscribe
CREATE PROCEDURE [dbo].[efrstore_update_unsubscribe] @Unsubscribe_id int, @Email varchar(100), @Unsubscribed bit, @Unsubscribed_date datetime AS
begin

update Unsubscribe set Email=@Email, Unsubscribed=@Unsubscribed, Unsubscribed_date=@Unsubscribed_date where Unsubscribe_id=@Unsubscribe_id

end
GO
