USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_campaign_reason_descs]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Campaign_reason_desc
CREATE PROCEDURE [dbo].[efrstore_get_campaign_reason_descs] AS
begin

select Campaign_reason_id, Culture_code, Description from Campaign_reason_desc

end
GO
