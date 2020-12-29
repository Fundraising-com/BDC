USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_scratch_book_price_infos]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Scratch_book_price_info
CREATE PROCEDURE [dbo].[efrcrm_get_scratch_book_price_infos] AS
begin

select Country_code, Scratch_book_id, Product_class_id, Effective_date, Unit_price from Scratch_book_price_info

end
GO
