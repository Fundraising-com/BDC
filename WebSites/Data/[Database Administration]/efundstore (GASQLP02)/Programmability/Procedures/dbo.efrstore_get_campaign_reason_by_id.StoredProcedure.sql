USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_campaign_reason_by_id]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Campaign_reason
CREATE PROCEDURE [dbo].[efrstore_get_campaign_reason_by_id] @Campaign_reason_id int AS
begin

select Campaign_reason_id, Party_type_id, Description from Campaign_reason where Campaign_reason_id=@Campaign_reason_id

end
GO
