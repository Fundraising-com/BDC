USE [eFundstore]
GO
/****** Object:  View [dbo].[promotion_type]    Script Date: 02/14/2014 13:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[promotion_type]
AS 
	select promotion_type_code, promotion_type_name as name, create_date
	from efrcommon..promotion_type
GO
