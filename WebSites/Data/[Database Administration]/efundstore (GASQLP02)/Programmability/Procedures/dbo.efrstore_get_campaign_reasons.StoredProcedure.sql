USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_campaign_reasons]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Campaign_reason
CREATE PROCEDURE [dbo].[efrstore_get_campaign_reasons] AS
begin

select Campaign_reason_id, Party_type_id, Description from Campaign_reason

end
GO
