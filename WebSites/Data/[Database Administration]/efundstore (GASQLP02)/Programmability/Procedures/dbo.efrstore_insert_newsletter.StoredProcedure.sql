USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_newsletter]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrstore_insert_newsletter] @Newsletter_id int OUTPUT, @Culture_code nvarchar(10), @Partner_id int, @News_month varchar(50), @Url varchar(200), @Display_order tinyint, @Enabled bit AS
begin

insert into Newsletter(Culture_code, Partner_id, News_month, Url, Display_order, Enabled) values(@Culture_code, @Partner_id, @News_month, @Url, @Display_order, @Enabled)

select @Newsletter_id = SCOPE_IDENTITY()

end
GO
