USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_campaign_reasons]    Script Date: 02/14/2014 13:03:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Campaign_reason
CREATE PROCEDURE [dbo].[efrcrm_get_campaign_reasons] AS
begin

select Campaign_reason_id, Party_type_id, Campaign_reason_desc from Campaign_reason

end
GO
