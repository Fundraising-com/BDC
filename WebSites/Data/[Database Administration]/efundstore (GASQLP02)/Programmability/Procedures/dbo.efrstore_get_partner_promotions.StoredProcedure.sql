USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_promotions]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_promotion
CREATE PROCEDURE [dbo].[efrstore_get_partner_promotions] AS
begin

select Partner_promotion_id, Partner_id, Promotion_id, Create_date from Partner_promotion

end
GO
