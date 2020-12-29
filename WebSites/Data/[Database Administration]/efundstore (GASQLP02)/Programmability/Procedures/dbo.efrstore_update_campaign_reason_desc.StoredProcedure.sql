USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_campaign_reason_desc]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Campaign_reason_desc
CREATE PROCEDURE [dbo].[efrstore_update_campaign_reason_desc] @Campaign_reason_id tinyint, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

update Campaign_reason_desc set Culture_code=@Culture_code, Description=@Description where Campaign_reason_id=@Campaign_reason_id

end
GO
