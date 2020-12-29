USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_newsletter_subscription]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Newsletter_subscription
CREATE PROCEDURE [dbo].[efrstore_insert_newsletter_subscription] @Subscription_id int OUTPUT, @Partner_id int, @Culture_code nvarchar(10), @Referrer varchar(120), @Email varchar(100), @Fullname varchar(100), @Unsubscribed bit, @Subscribe_date datetime, @Unsubscribe_date datetime AS
begin

insert into Newsletter_subscription(Partner_id, Culture_code, Referrer, Email, Fullname, Unsubscribed, Subscribe_date, Unsubscribe_date) values(@Partner_id, @Culture_code, @Referrer, @Email, @Fullname, @Unsubscribed, @Subscribe_date, @Unsubscribe_date)

select @Subscription_id = SCOPE_IDENTITY()

end
GO
