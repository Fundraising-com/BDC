USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_promotion_types]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_type
CREATE PROCEDURE [dbo].[efrstore_get_promotion_types] AS
begin

select Promotion_type_code, Name, Create_date from Promotion_type

end
GO
