USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_zip_codes]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sale_Zip_Code
CREATE PROCEDURE [dbo].[efrcrm_get_sale_zip_codes] AS
begin

select Zip_Code, Sales_ID from Sale_Zip_Code

end
GO
