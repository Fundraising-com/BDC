USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_newsletter]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrstore_update_newsletter] @Newsletter_id int, @Culture_code nvarchar(10), @Partner_id int, @News_month varchar(50), @Url varchar(200), @Display_order tinyint, @Enabled bit AS
begin

update Newsletter set Culture_code=@Culture_code, Partner_id=@Partner_id, News_month=@News_month, Url=@Url, Display_order=@Display_order, Enabled=@Enabled where Newsletter_id=@Newsletter_id

end
GO
