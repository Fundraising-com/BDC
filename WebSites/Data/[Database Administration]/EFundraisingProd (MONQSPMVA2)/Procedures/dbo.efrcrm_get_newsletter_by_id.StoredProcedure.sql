USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_newsletter_by_id]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrcrm_get_newsletter_by_id] @Newsletter_ID int AS
begin

select Newsletter_ID, Referrer, Email, Fullname, Unsubscribed from Newsletter where Newsletter_ID=@Newsletter_ID

end
GO
