USE [eFundstore]
GO
/****** Object:  View [dbo].[partner_promotion]    Script Date: 02/14/2014 13:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[partner_promotion]
AS 
	select partner_promotion_id, partner_id, promotion_id, create_date
	from efrcommon..partner_promotion
GO
