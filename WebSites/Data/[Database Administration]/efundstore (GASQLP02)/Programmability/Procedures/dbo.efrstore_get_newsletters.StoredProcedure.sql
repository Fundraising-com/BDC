USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_newsletters]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrstore_get_newsletters] AS
begin

select Newsletter_id, Culture_code, Partner_id, News_month, Url, Display_order, Enabled from Newsletter

end
GO
