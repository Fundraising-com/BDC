USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[promotion]    Script Date: 02/14/2014 13:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[promotion]
AS
	select promotion_id, promotion_type_code, promotion_destination_id, promotion_name, script_name, active, create_date
	from efrcommon..promotion
GO
