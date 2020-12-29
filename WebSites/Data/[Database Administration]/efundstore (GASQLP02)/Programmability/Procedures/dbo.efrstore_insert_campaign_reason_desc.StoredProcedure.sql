USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_campaign_reason_desc]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Campaign_reason_desc
CREATE PROCEDURE [dbo].[efrstore_insert_campaign_reason_desc] @Campaign_reason_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

insert into Campaign_reason_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Campaign_reason_id = SCOPE_IDENTITY()

end
GO
