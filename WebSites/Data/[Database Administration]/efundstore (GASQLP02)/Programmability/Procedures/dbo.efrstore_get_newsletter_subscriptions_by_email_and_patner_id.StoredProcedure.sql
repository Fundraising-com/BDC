USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_newsletter_subscriptions_by_email_and_patner_id]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Newsletter_subscription
CREATE PROCEDURE [dbo].[efrstore_get_newsletter_subscriptions_by_email_and_patner_id]
     @partner_id int
    , @email varchar(100) AS
begin

select subscription_id
, partner_id
, culture_code
, Referrer
, Email
, Fullname
, Unsubscribed
, Subscribe_date
, Unsubscribe_date from Newsletter_subscription 
where partner_id=@partner_id
and email=@email 

end
GO
