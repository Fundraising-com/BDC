USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_campaign_reason_descs]    Script Date: 02/14/2014 13:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Campaign_reason_desc
CREATE PROCEDURE [dbo].[efrcrm_get_campaign_reason_descs] AS
begin

select Campaign_reason_id, Language_id, Description from Campaign_reason_desc

end
GO
