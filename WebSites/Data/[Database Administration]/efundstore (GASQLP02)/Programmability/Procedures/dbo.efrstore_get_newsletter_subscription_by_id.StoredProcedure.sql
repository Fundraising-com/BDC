USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_newsletter_subscription_by_id]    Script Date: 02/14/2014 13:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Newsletter_subscription
CREATE PROCEDURE [dbo].[efrstore_get_newsletter_subscription_by_id] @Subscription_id int AS
begin

select Subscription_id, Partner_id, Culture_code, Referrer, Email, Fullname, Unsubscribed, Subscribe_date, Unsubscribe_date from Newsletter_subscription where Subscription_id=@Subscription_id

end
GO
