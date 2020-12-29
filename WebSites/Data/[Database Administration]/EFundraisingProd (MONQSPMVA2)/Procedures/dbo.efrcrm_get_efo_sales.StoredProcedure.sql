USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_sales]    Script Date: 02/14/2014 13:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Sale
CREATE PROCEDURE [dbo].[efrcrm_get_efo_sales] AS
begin

select Sale_ID, Supporter_ID, Sale_Date, Amount_To_Group, Amount_To_Supplier, Amount, Delivery_Address, State_Code, Country_Code, Delivery_City, Delivery_Zip_Code, Card_Name, Card_Address, Transaction_ID from EFO_Sale

end
GO
