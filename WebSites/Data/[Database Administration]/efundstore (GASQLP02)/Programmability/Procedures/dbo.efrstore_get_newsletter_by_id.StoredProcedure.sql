USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_newsletter_by_id]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrstore_get_newsletter_by_id] @Newsletter_id int AS
begin

select Newsletter_id, Culture_code, Partner_id, News_month, Url, Display_order, Enabled from Newsletter where Newsletter_id=@Newsletter_id

end
GO
