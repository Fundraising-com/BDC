USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_campaign_reason]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Campaign_reason
CREATE PROCEDURE [dbo].[efrstore_update_campaign_reason] @Campaign_reason_id tinyint, @Party_type_id tinyint, @Description varchar(50) AS
begin

update Campaign_reason set Party_type_id=@Party_type_id, Description=@Description where Campaign_reason_id=@Campaign_reason_id

end
GO
