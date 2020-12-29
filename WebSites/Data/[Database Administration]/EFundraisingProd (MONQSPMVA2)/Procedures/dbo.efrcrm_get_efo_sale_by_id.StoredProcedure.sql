USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_sale_by_id]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Sale
CREATE PROCEDURE [dbo].[efrcrm_get_efo_sale_by_id] @Sale_ID int AS
begin

select Sale_ID, Supporter_ID, Sale_Date, Amount_To_Group, Amount_To_Supplier, Amount, Delivery_Address, State_Code, Country_Code, Delivery_City, Delivery_Zip_Code, Card_Name, Card_Address, Transaction_ID from EFO_Sale where Sale_ID=@Sale_ID

end
GO
