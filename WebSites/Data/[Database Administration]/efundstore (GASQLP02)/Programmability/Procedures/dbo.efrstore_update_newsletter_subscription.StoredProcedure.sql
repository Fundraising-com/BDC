USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_newsletter_subscription]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Newsletter_subscription
CREATE PROCEDURE [dbo].[efrstore_update_newsletter_subscription] @Subscription_id int, @Partner_id int, @Culture_code nvarchar(10), @Referrer varchar(120), @Email varchar(100), @Fullname varchar(100), @Unsubscribed bit, @Subscribe_date datetime, @Unsubscribe_date datetime AS
begin

update Newsletter_subscription set Partner_id=@Partner_id, Culture_code=@Culture_code, Referrer=@Referrer, Email=@Email, Fullname=@Fullname, Unsubscribed=@Unsubscribed, Subscribe_date=@Subscribe_date, Unsubscribe_date=@Unsubscribe_date where Subscription_id=@Subscription_id

end
GO
