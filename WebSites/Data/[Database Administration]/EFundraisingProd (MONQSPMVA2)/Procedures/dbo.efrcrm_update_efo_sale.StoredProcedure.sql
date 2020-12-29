USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_sale]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Sale
CREATE PROCEDURE [dbo].[efrcrm_update_efo_sale] @Sale_ID int, @Supporter_ID int, @Sale_Date smalldatetime, @Amount_To_Group smallmoney, @Amount_To_Supplier smallmoney, @Amount smallmoney, @Delivery_Address varchar(75), @State_Code varchar(10), @Country_Code varchar(10), @Delivery_City varchar(50), @Delivery_Zip_Code varchar(10), @Card_Name varchar(30), @Card_Address varchar(75), @Transaction_ID varchar(15) AS
begin

update EFO_Sale set Supporter_ID=@Supporter_ID, Sale_Date=@Sale_Date, Amount_To_Group=@Amount_To_Group, Amount_To_Supplier=@Amount_To_Supplier, Amount=@Amount, Delivery_Address=@Delivery_Address, State_Code=@State_Code, Country_Code=@Country_Code, Delivery_City=@Delivery_City, Delivery_Zip_Code=@Delivery_Zip_Code, Card_Name=@Card_Name, Card_Address=@Card_Address, Transaction_ID=@Transaction_ID where Sale_ID=@Sale_ID

end
GO
