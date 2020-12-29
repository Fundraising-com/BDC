USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_unsubscribes]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Unsubscribe
CREATE PROCEDURE [dbo].[efrstore_get_unsubscribes] AS
begin

select Unsubscribe_id, Email, Unsubscribed, Unsubscribed_date from Unsubscribe

end
GO
