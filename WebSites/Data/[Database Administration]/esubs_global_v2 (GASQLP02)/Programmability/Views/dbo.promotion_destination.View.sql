USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[promotion_destination]    Script Date: 02/14/2014 13:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[promotion_destination]
AS 
	select promotion_destination_id, promotion_destination_url, create_date
	from efrcommon..promotion_destination
GO
