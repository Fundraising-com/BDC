USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_promotions]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion
CREATE PROCEDURE [dbo].[efrstore_get_promotions] AS
begin

select Promotion_id, Promotion_type_code, Promotion_destination_id, Name, Script_name, Active, Create_date from Promotion

end
GO
